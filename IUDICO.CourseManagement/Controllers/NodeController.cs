using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Xml.Serialization;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Shared;
using IUDICO.CourseManagement.Models;
using IUDICO.CourseManagement.Models.ManifestModels;
using IUDICO.CourseManagement.Models.ManifestModels.SequencingModels;
using IUDICO.CourseManagement.Models.ManifestModels.SequencingModels.RollupModels;
using IUDICO.CourseManagement.Models.Storage;

namespace IUDICO.CourseManagement.Controllers
{
    public class NodeController : CourseBaseController
    {
        protected Course _CurrentCourse;
        private readonly ICourseStorage _Storage;

        public NodeController(ICourseStorage courseStorage)
        {
            _Storage = courseStorage;
        }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            //StorageFactory factory = new StorageFactory();
            //Storage = factory.CreateStorage(StorageType.Mixed);

            var courseId = Convert.ToInt32(requestContext.RouteData.Values["CourseID"]);
            _CurrentCourse = _Storage.GetCourse(courseId);

            if (_CurrentCourse == null)
            {
                requestContext.HttpContext.Response.Redirect("/Course/Index");
            }

            //LmsService.Inform("yo/andrij", "test1", "test2");

            base.Initialize(requestContext);
        }

        public ActionResult Index()
        {
            ViewData["SequencingPatternsList"] = new List<SelectListItem>
                                                     {
                                                         new SelectListItem
                                                             {
                                                                 Text = Localization.getMessage("Default"),
                                                                 Value = SequencingPattern.OrganizationDefaultSequencingPattern.ToString()
                                                             },
                                                         new SelectListItem
                                                             {
                                                                 Text = Localization.getMessage("ControlChapter"),
                                                                 Value = SequencingPattern.ControlChapterSequencingPattern.ToString()
                                                             },
                                                         new SelectListItem
                                                             {
                                                                 Text = Localization.getMessage("RandomSet"),
                                                                 Value = SequencingPattern.RandomSetSequencingPattern.ToString()
                                                             }
                                                     };
            return View(_CurrentCourse);
        }

        [HttpPost]
        public JsonResult List(int? id)
        {
            try
            {
                var nodes = _Storage.GetNodes(_CurrentCourse.Id, id).ToJsTrees();

                return Json(nodes);
            }
            catch (Exception)
            {
                return Json(new { });
            }
        }

        [HttpPost]
        public JsonResult Create(Node node)
        {
            try
            {
                node.CourseId = _CurrentCourse.Id;

                var id = _Storage.AddNode(node);

                if (id != null)
                {
                    return Json(new { status = true, id = node.Id });
                }
                else
                {
                    return Json(new { status = false });
                }
            }
            catch
            {
                return Json(new { status = false });
            }
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                _Storage.DeleteNode(id);

                return Json(new { status = true });
            }
            catch (Exception)
            {
                return Json(new { status = false });
            }
        }

        [HttpPost]
        public JsonResult Rename(int id, string name)
        {
            try
            {
                var node = _Storage.GetNode(id);
                node.Name = name;

                _Storage.UpdateNode(id, node);

                return Json(new { status = true });
            }
            catch (Exception)
            {
                return Json(new { status = false });
            }
        }

        [HttpPost]
        public JsonResult Move(int id, int? parentId, int position, bool copy)
        {
            try
            {
                var node = _Storage.GetNode(id);

                if (copy)
                {
                    var newId = _Storage.CreateCopy(node, parentId, position);

                    if (newId != null)
                    {
                        return Json(new { status = true, id = newId });
                    }
                    else
                    {
                        return Json(new { status = false });
                    }
                }
                else
                {
                    node.ParentId = parentId;
                    node.Position = position;

                    _Storage.UpdateNode(id, node);

                    return Json(new { status = true, id = id });
                }
            }
            catch
            {
                return Json(new { status = false });
            }
        }

        [HttpPost]
        public JsonResult Data(int id)
        {
            try
            {
                return Json(new { data = _Storage.GetNodeContents(id), status = true });
            }
            catch (Exception)
            {
                return Json(new { status = true, data = "" });
            }
        }

        [HttpPost]
        public JsonResult Preview(int id)
        {
            try
            {
                var path = Path.Combine(HttpContext.Request.ApplicationPath, _Storage.GetPreviewNodePath(id)).Replace('\\', '/');

                return Json(new { status = true, path = path });
            }
            catch (Exception)
            {
                return Json(new { status = true, path = "" });
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public void Edit(int id, string data)
        {
            _Storage.UpdateNodeContents(id, data);

            //return Json(new { result = true });
        }

        [HttpPost]
        public JsonResult ApplyPattern(int id, SequencingPattern pattern, int data)
        {
            var node = _Storage.GetNode(id);
            var xml = new XmlSerializer(typeof(Sequencing));

            var xelement = id == 0 ? _CurrentCourse.Sequencing : node.Sequencing;

            var sequencing = xelement == null ? new Sequencing() : (Sequencing)xml.DeserializeXElement(xelement);

            switch (pattern)
            {
                case SequencingPattern.ControlChapterSequencingPattern:
                    sequencing = SequencingPatternManager.ApplyControlChapterSequencing(sequencing);
                    break;
                case SequencingPattern.RandomSetSequencingPattern:
                    sequencing = SequencingPatternManager.ApplyRandomSetSequencingPattern(sequencing, data);
                    break;
                case SequencingPattern.OrganizationDefaultSequencingPattern:
                    sequencing = SequencingPatternManager.ApplyDefaultChapterSequencing(sequencing);
                    break;
            }

            if (id == 0)
            {
                _CurrentCourse.Sequencing = xml.SerializeToXElemet(sequencing);
                _Storage.UpdateCourse(_CurrentCourse.Id, _CurrentCourse);
            }
            else
            {
                node.Sequencing = xml.SerializeToXElemet(sequencing);
                _Storage.UpdateNode(id, node);
            }

            return Json(new { status = true });
        }

        [HttpPost]
        public JsonResult Properties(int id, string type)
        {
            var xml = new XmlSerializer(typeof(Sequencing));

            var xelement = id == 0 ? _CurrentCourse.Sequencing : _Storage.GetNode(id).Sequencing;

            var sequencing = xelement == null ? new Sequencing() : (Sequencing) xml.DeserializeXElement(xelement);
            
            NodeProperty model;

            var partialView = "Properties";

            if (type == "ControlMode")
            {
                model = sequencing.ControlMode ?? new ControlMode();
            }
            else if (type == "LimitConditions")
            {
                model = sequencing.LimitConditions ?? new LimitConditions();
            }
            else if (type == "ConstrainedChoiceConsiderations")
            {
                model = sequencing.ConstrainedChoiceConsiderations ?? new ConstrainedChoiceConsiderations();
            }
            else if (type == "RandomizationControls")
            {
                model = sequencing.RandomizationControls ?? new RandomizationControls();
            }
            else if (type == "DeliveryControls")
            {
                model = sequencing.DeliveryControls ?? new DeliveryControls();
            }
            else if (type == "RollupRules")
            {
                model = sequencing.RollupRules ?? new RollupRules();
            }
            else if (type == "RollupConsiderations")
            {
                model = sequencing.RollupConsiderations ?? new RollupConsiderations();
            }
            else
            {
                throw new NotImplementedException();
            }

            model.CourseId = _CurrentCourse.Id;
            model.NodeId = id;
            model.Type = type;

            return Json(new { status = true, type = type, data = PartialViewHtml(partialView, model, ViewData) });
        }

        [HttpPost]
        public JsonResult SaveProperties(int nodeId, string type)
        {
            var node = _Storage.GetNode(nodeId);
            var xml = new XmlSerializer(typeof(Sequencing));

            var xelement = nodeId == 0 ? _CurrentCourse.Sequencing : node.Sequencing;

            var sequencing = xelement == null ? new Sequencing() : (Sequencing)xml.DeserializeXElement(xelement);
            
            object model;

            if (type == "ControlMode")
            {
                model = new ControlMode();
                TryUpdateModel(model as ControlMode);
                sequencing.ControlMode = model as ControlMode;
            }
            else if (type == "LimitConditions")
            {
                model = new LimitConditions();
                TryUpdateModel(model as LimitConditions);
                sequencing.LimitConditions = model as LimitConditions;
            }
            else if (type == "ConstrainedChoiceConsiderations")
            {
                model = new ConstrainedChoiceConsiderations();
                TryUpdateModel(model as ConstrainedChoiceConsiderations);
                sequencing.ConstrainedChoiceConsiderations = model as ConstrainedChoiceConsiderations;
            }
            else if (type == "RandomizationControls")
            {
                model = new RandomizationControls();
                TryUpdateModel(model as RandomizationControls);
                sequencing.RandomizationControls = model as RandomizationControls;
            }
            else if (type == "DeliveryControls")
            {
                model = new DeliveryControls();
                TryUpdateModel(model as DeliveryControls);
                sequencing.DeliveryControls = model as DeliveryControls;
            }
            else if (type == "RollupRules")
            {
                model = new RollupRules();
                TryUpdateModel(model as RollupRules);
                sequencing.RollupRules = model as RollupRules;
            }
            else if (type == "RollupConsiderations")
            {
                model = new RollupConsiderations();
                TryUpdateModel(model as RollupConsiderations);
                sequencing.RollupConsiderations = model as RollupConsiderations;
            }
            else
            {
                throw new NotImplementedException();
            }

            if(nodeId == 0)
            {
                _CurrentCourse.Sequencing = xml.SerializeToXElemet(sequencing);
                _Storage.UpdateCourse(_CurrentCourse.Id, _CurrentCourse);
            }
            else
            {
                node.Sequencing = xml.SerializeToXElemet(sequencing);
                _Storage.UpdateNode(nodeId, node);
            }
            

            return Json(new { status = true });
        }
        
        [HttpGet]
        public ActionResult Images(int nodeId, string FileName)
        {
            var path = Path.Combine(@"\Data\Courses\", _CurrentCourse.Id.ToString());
            path = Path.Combine(path, "Node");
            path = Path.Combine(path, nodeId.ToString());
            path = Path.Combine(path, "Images");
            path = Path.Combine(path, FileName);
            return File(path, "image/png");
        }
    }
}    

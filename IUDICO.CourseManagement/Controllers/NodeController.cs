using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using System.Xml.Serialization;
using IUDICO.Common.Models;
using IUDICO.CourseManagement.Models;
using IUDICO.CourseManagement.Models.ManifestModels.SequencingModels;
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
        public JsonResult Delete(int[] ids)
        {
            try
            {
                _Storage.DeleteNodes(new List<int>(ids));

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
        [ValidateInput(false)]
        public void Edit(int id, string data)
        {
            _Storage.UpdateNodeContents(id, data);

            //return Json(new { result = true });
        }

        [HttpPost]
        public JsonResult ApplyPattern(int id, int pattern)
        {
            var node = _Storage.GetNode(id);
            node.SequencingPattern = pattern;
            _Storage.UpdateNode(id, node);

            return Json(new { status = true });
        }

        [HttpPost]
        public JsonResult Properties(int id, string type)
        {
            var node = _Storage.GetNode(id);

            var xml = new XmlSerializer(typeof(Sequencing));
            var sequencing = (Sequencing)xml.DeserializeXElement(node.Sequencing);
            
            NodeProperty model;
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
            else
            {
                throw new NotImplementedException();
            }

            model.CourseId = node.CourseId;
            model.NodeId = node.Id;
            model.Type = type;

            return Json(new { status = true, data = PartialViewHtml("Properties", model, ViewData) });
        }

        [HttpPost]
        public JsonResult SaveProperties(int nodeId, string type)
        {
            var node = _Storage.GetNode(nodeId);
            var xml = new XmlSerializer(typeof(Sequencing));
            var sequencing = (Sequencing)xml.DeserializeXElement(node.Sequencing);
            
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
            else
            {
                throw new NotImplementedException();
            }
            node.Sequencing = xml.SerializeToXElemet(sequencing);

            _Storage.UpdateNode(nodeId, node);

            return Json(new { status = true });
        }
    }
}    

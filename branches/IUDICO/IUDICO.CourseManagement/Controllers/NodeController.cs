using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using System.Web.Routing;
using System.Xml.Serialization;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Shared;
using IUDICO.CourseManagement.Models;
using IUDICO.CourseManagement.Models.ManifestModels;
using IUDICO.CourseManagement.Models.ManifestModels.SequencingModels;
using IUDICO.CourseManagement.Models.ManifestModels.SequencingModels.RollupModels;
using IUDICO.CourseManagement.Models.Storage;
using IUDICO.Common.Models.Attributes;

namespace IUDICO.CourseManagement.Controllers
{
    using IUDICO.Common;

    public class NodeController : CourseBaseController
    {
        protected Course currentCourse;
        private readonly ICourseStorage storage;

        public NodeController(ICourseStorage courseStorage)
        {
            this.storage = courseStorage;
        }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            // StorageFactory factory = new StorageFactory();
            // Storage = factory.CreateStorage(StorageType.Mixed);

            var courseId = Convert.ToInt32(requestContext.RouteData.Values["CourseID"]);
            this.currentCourse = this.storage.GetCourse(courseId);

            if (this.currentCourse == null)
            {
                requestContext.HttpContext.Response.Redirect("/Course/Index");
            }

            // LmsService.Inform("yo/andrij", "test1", "test2");

            base.Initialize(requestContext);
        }

        [Allow(Role = Role.CourseCreator)]
        public ActionResult Index()
        {
            ViewData["SequencingPatternsList"] = new List<SelectListItem>
                                                     {
                                                         new SelectListItem
                                                             {
                                                                 Text = Localization.GetMessage("Default"),
                                                                 Value = SequencingPattern.OrganizationDefaultSequencingPattern.ToString()
                                                             },
                                                         new SelectListItem
                                                             {
                                                                 Text = Localization.GetMessage("ControlChapter"),
                                                                 Value = SequencingPattern.ControlChapterSequencingPattern.ToString()
                                                             },
                                                         new SelectListItem
                                                             {
                                                                 Text = Localization.GetMessage("RandomSet"),
                                                                 Value = SequencingPattern.RandomSetSequencingPattern.ToString()
                                                             }
                                                     };
            return View(this.currentCourse);
        }

        [HttpPost]
        public JsonResult List(int? id)
        {
            try
            {
                var nodes = this.storage.GetNodes(this.currentCourse.Id, id).ToJsTrees();

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
                node.CourseId = this.currentCourse.Id;

                var id = this.storage.AddNode(node);

                if (id != null)
                {
                    ApplyPattern(id ?? 0, SequencingPattern.OrganizationDefaultSequencingPattern, 0);
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
                this.storage.DeleteNode(id);

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
                var node = this.storage.GetNode(id);
                node.Name = name;

                this.storage.UpdateNode(id, node);

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
                var node = this.storage.GetNode(id);

                if (copy)
                {
                    var newId = this.storage.CreateCopy(node, parentId, position);

                    return Json(new { status = true, id = newId });
                }
                else
                {
                    node.ParentId = parentId;
                    node.Position = position;

                    this.storage.UpdateNode(id, node);

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
                return Json(new { data = this.storage.GetNodeContents(id), status = true });
            }
            catch (Exception)
            {
                return Json(new { status = true, data = string.Empty });
            }
        }

        [HttpPost]
        public JsonResult Preview(int id)
        {
            var path = Path.Combine(HttpContext.Request.ApplicationPath, this.storage.GetPreviewNodePath(id)).Replace('\\', '/');

            if (this.storage.GetNode(id).IsFolder)
            {
                throw new Exception();
            }

            return Json(new { status = true, path = path });
        }

        [HttpPost]
        [ValidateInput(false)]
        public void Edit(int id, string data)
        {
            this.storage.UpdateNodeContents(id, data);
        }

        [HttpPost]
        public JsonResult ApplyPattern(int id, SequencingPattern pattern, int data)
        {
            var node = this.storage.GetNode(id);
            var xml = new XmlSerializer(typeof(Sequencing));

            var xelement = id == 0 ? this.currentCourse.Sequencing : node.Sequencing;

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
                this.currentCourse.Sequencing = xml.SerializeToXElemet(sequencing);
                this.storage.UpdateCourse(this.currentCourse.Id, this.currentCourse);
            }
            else
            {
                node.Sequencing = xml.SerializeToXElemet(sequencing);
                this.storage.UpdateNode(id, node);
            }

            return Json(new { status = true });
        }

        [HttpPost]
        public JsonResult Properties(int id, string type)
        {
            var xml = new XmlSerializer(typeof(Sequencing));

            var xelement = id == 0 ? this.currentCourse.Sequencing : this.storage.GetNode(id).Sequencing;

            var sequencing = xelement == null ? new Sequencing() : (Sequencing)xml.DeserializeXElement(xelement);
            
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

            model.CourseId = this.currentCourse.Id;
            model.NodeId = id;
            model.Type = type;

            return Json(new { status = true, type = type, data = PartialViewAsString("Properties", model) });
        }

        [HttpPost]
        public JsonResult SaveProperties(int nodeId, string type)
        {
            var node = this.storage.GetNode(nodeId);
            var xml = new XmlSerializer(typeof(Sequencing));

            var xelement = nodeId == 0 ? this.currentCourse.Sequencing : node.Sequencing;

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

            if (nodeId == 0)
            {
                this.currentCourse.Sequencing = xml.SerializeToXElemet(sequencing);
                this.storage.UpdateCourse(this.currentCourse.Id, this.currentCourse);
            }
            else
            {
                node.Sequencing = xml.SerializeToXElemet(sequencing);
                this.storage.UpdateNode(nodeId, node);
            }
            

            return Json(new { status = true });
        }
        
        [HttpGet]
        public ActionResult Images(int nodeId, string fileName)
        {
            var path = Path.Combine(@"\Data\Courses\", this.currentCourse.Id.ToString());
            path = Path.Combine(path, "Node");
            path = Path.Combine(path, nodeId.ToString());
            path = Path.Combine(path, "Images");
            path = Path.Combine(path, fileName);
            return File(path, "image/png");
        }
    }
}    

using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using IUDICO.Common.Models;
using IUDICO.CourseManagement.Models;
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
                return Json(new {});
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

                return Json(new {status = true});
            }
            catch (Exception)
            {
                return Json(new {status = false});
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
                        return Json(new {status = true, id = newId});
                    }
                    else
                    {
                        return Json(new {status = false});
                    }
                }
                else
                {
                    node.ParentId = parentId;
                    node.Position = position;

                    _Storage.UpdateNode(id, node);

                    return Json(new {status = true, id = id});
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
                return Json(new {status = true, data = "test"});
            }
        }
    }
}

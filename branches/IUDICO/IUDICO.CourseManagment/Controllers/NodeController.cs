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
        protected Course CurrentCourse;
        private readonly ICourseStorage _storage;

        public NodeController(ICourseStorage courseStorage)
        {
            _storage = courseStorage;
        }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            //StorageFactory factory = new StorageFactory();
            //Storage = factory.CreateStorage(StorageType.Mixed);

            int courseId = Convert.ToInt32(requestContext.RouteData.Values["CourseID"]);
            CurrentCourse = _storage.GetCourse(courseId);

            if (CurrentCourse == null)
            {
                requestContext.HttpContext.Response.Redirect("/Course/Index");
            }

            base.Initialize(requestContext);
        }

        public ActionResult Index()
        {
            return View(CurrentCourse);
        }

        [HttpPost]
        public JsonResult List(int? id)
        {
            try
            {
                var nodes = _storage.GetNodes(CurrentCourse.Id, id).ToJsTrees();

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
                node.CourseId = CurrentCourse.Id;

                int? id = _storage.AddNode(node);

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
                _storage.DeleteNodes(new List<int>(ids));

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
                Node node = _storage.GetNode(id);
                node.Name = name;
                _storage.UpdateNode(id, node);

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
                Node node = _storage.GetNode(id);

                if (copy)
                {
                    int? newId = _storage.CreateCopy(node, parentId, position);

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

                    _storage.UpdateNode(id, node);

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
                return Json(new { data = _storage.GetNodeContents(id), status = true });
            }
            catch (Exception)
            {
                return Json(new {status = true, data = "test"});
            }
        }
    }
}

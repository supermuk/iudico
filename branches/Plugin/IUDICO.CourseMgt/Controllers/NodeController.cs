using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using IUDICO.Common.Models;
using IUDICO.CourseMgt.Models;
using IUDICO.CourseMgt.Models.Storage;

namespace IUDICO.CourseMgt.Controllers
{
    public class NodeController : CourseBaseController
    {
        protected Course CurrentCourse;

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            //StorageFactory factory = new StorageFactory();
            //Storage = factory.CreateStorage(StorageType.Mixed);

            int courseId = Convert.ToInt32(requestContext.RouteData.Values["CourseID"]);
            CurrentCourse = Storage.GetCourse(courseId);

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
                var nodes = Storage.GetNodes(CurrentCourse.Id, id).ToJsTreeList();

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

                int? id = Storage.AddNode(node);

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
                Storage.DeleteNodes(new List<int>(ids));

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
                Node node = Storage.GetNode(id);
                node.Name = name;
                Storage.UpdateNode(id, node);

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
                Node node = Storage.GetNode(id);

                if (copy)
                {
                    int? newId = Storage.CreateCopy(node, parentId, position);

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

                    Storage.UpdateNode(id, node);

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
                return Json(new { data = Storage.GetNodeContents(id), status = true });
            }
            catch (Exception)
            {
                return Json(new {status = true, data = "test"});
            }
        }
    }
}

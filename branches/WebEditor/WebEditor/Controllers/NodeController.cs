using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebEditor.Models;
using WebEditor.Models.Storage;

namespace WebEditor.Controllers
{
    public class NodeController : BaseController
    {
        protected IStorageInterface Storage;
        protected Course CurrentCourse;

        protected override void Initialize(RequestContext requestContext)
        {
            StorageFactory factory = new StorageFactory();
            Storage = factory.CreateStorage(StorageType.Mixed);

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
            var nodes = Storage.GetNodes(CurrentCourse.Id, id).ToJsTreeList();

            return Json(nodes);
        }

        [HttpPost]
        public JsonResult Create(Node node)
        {
            node.CourseId = CurrentCourse.Id;

            int id = Storage.AddNode(node);

            return Json(new {status = true, id = node.Id});
        }

        [HttpPost]
        public JsonResult Remove(List<int> ids)
        {
            try
            {
                Storage.DeleteNodes(ids);

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
            Node node = Storage.GetNode(id);
            node.Name = name;
            Storage.UpdateNode(id, node);

            return Json(new { status = true });
        }

        [HttpPost]
        public JsonResult Move(int id, int? parentId, int position, bool copy)
        {
            Node node = Storage.GetNode(id);

            if (copy)
            {
                int newId = Storage.CreateCopy(node, parentId, position);

                return Json(new { status = true, id = newId });
            }
            else
            {
                node.ParentId = parentId;
                node.Position = position;

                Storage.UpdateNode(id, node);

                return Json(new {status = true, id = id});
            }
        }

        [HttpPost]
        public JsonResult Data(int id)
        {
            return Json(new {data = Storage.GetNodeContents(id)});
        }
    }
}

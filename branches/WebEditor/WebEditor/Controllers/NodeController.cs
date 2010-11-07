using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebEditor.Models;

namespace WebEditor.Controllers
{
    public class NodeController : BaseController
    {
        protected Course CurrentCourse;
        protected override void Initialize(RequestContext requestContext)
        {
            int courseId = Convert.ToInt32(requestContext.RouteData.Values["CourseID"]);
            CurrentCourse = db.GetCourse(courseId);

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
        public JsonResult List(int id)
        {
            db.ClearCache();

            var nodes = CurrentCourse.Nodes.OrderBy(n => n.Position).ToList();

            if (id == 0)
            {
                nodes = nodes.Where(n => n.ParentId == null).ToList();
            }
            else
            {
                nodes = nodes.Where(n => n.ParentId == id).ToList();
            }

            var result = (from n in nodes select new JsTreeNode(n.Id, n.Name, n.IsFolder)).ToArray();

            return Json(result);
        }

        [HttpPost]
        public JsonResult Create(string data, int parentId, int position, string type)
        {
            Node node = new Node
                {
                    CourseId = CurrentCourse.Id,
                    Name = data,
                    ParentId = (parentId == 0 ? null : (int?) parentId),
                    IsFolder = (type != "default"),
                    Position = position
                };

            db.Nodes.InsertOnSubmit(node);
            db.SubmitChanges();

            return Json(new {status = true, id = node.Id});
        }

        [HttpPost]
        public JsonResult Remove(List<int> ids)
        {
            try
            {
                var nodes = (from n in db.Nodes where ids.Contains(n.Id) select n);
                db.Nodes.DeleteAllOnSubmit(nodes);
                db.SubmitChanges();

                return Json(new {status = true});
            }
            catch (Exception e)
            {
                return Json(new {status = false});
            }
        }

        [HttpPost]
        public JsonResult Rename(int id, string data)
        {
            var node = db.Nodes.SingleOrDefault(n => n.Id == id);
            node.Name = data;
            
            db.SubmitChanges();

            return Json(new { status = true });
        }

        [HttpPost]
        public JsonResult Move(int id, int? parentId, int position, bool copy)
        {
            Node node = db.Nodes.SingleOrDefault(n => n.Id == id);

            if (parentId == 0)
            {
                parentId = null;
            }

            if (copy)
            {
                Node newnode = new Node
                {
                    CourseId = node.CourseId,
                    Name = node.Name,
                    ParentId = parentId,
                    IsFolder = node.IsFolder,
                    Position = position
                };

                db.CopyNodes(node, newnode);
                db.Nodes.InsertOnSubmit(newnode);
                db.SubmitChanges();

                return Json(new {status = true, id = newnode.Id});
            }
            else
            {
                node.ParentId = parentId;
                node.Position = position;

                db.SubmitChanges();

                return Json(new {status = true, id = node.Id});
            }
        }
    }
}

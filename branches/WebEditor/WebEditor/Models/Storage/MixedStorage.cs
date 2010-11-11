using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebEditor.Models.Storage
{
    public class MixedStorage: IStorageInterface
    {
        protected ButterflyDB db = ButterflyDB.Instance;

        public List<Course> GetCourses()
        {
            try
            {
                return db.Courses.ToList();
            }
            catch
            {
                return null;
            }
        }

        public Course GetCourse(int id)
        {
            try
            {
                return db.Courses.Single(c => c.Id == id);
            }
            catch
            {
                return null;
            }
        }

        public int? AddCourse(Course course)
        {
            try
            {
                course.Created = DateTime.Now;
                course.Updated = DateTime.Now;

                db.Courses.InsertOnSubmit(course);
                db.SubmitChanges();

                return course.Id;
            }
            catch
            {
                return null;
            }
        }

        public bool UpdateCourse(int id, Course course)
        {
            try
            {
                Course oldCourse = db.Courses.Single(c => c.Id == id);

                oldCourse.Name = course.Name;
                oldCourse.Owner = course.Owner;
                oldCourse.Updated = DateTime.Now;

                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteCourse(int id)
        {
            try
            {
                Course course = db.Courses.Single(c => c.Id == id);

                db.Courses.DeleteOnSubmit(course);
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteCourses(List<int> ids)
        {
            try
            {
                var courses = (from n in db.Courses where ids.Contains(n.Id) select n);

                db.Courses.DeleteAllOnSubmit(courses);
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;   
            }
        }

        public List<Node> GetNodes(int courseId)
        {
            return GetNodes(courseId, null);
        }

        public List<Node> GetNodes(int courseId, int? parentId)
        {
            try
            {
                db.ClearCache();

                Course course = db.Courses.SingleOrDefault(c => c.Id == courseId);
                List<Node> nodes = course.Nodes.OrderBy(n => n.Position).ToList();

                if (parentId == null)
                {
                    nodes = nodes.Where(n => n.ParentId == null).ToList();
                }
                else
                {
                    nodes = nodes.Where(n => n.ParentId == parentId).ToList();
                }

                return nodes;
            }
            catch (Exception ex)
            {
                return new List<Node>();
            }
        }

        public Node GetNode(int id)
        {
            return db.Nodes.SingleOrDefault(n => n.Id == id);
        }

        public int AddNode(Node node)
        {
            db.Nodes.InsertOnSubmit(node);
            db.SubmitChanges();

            return node.Id;
        }

        public void UpdateNode(int id, Node node)
        {
            try
            {
                Node oldNode = db.Nodes.SingleOrDefault(n => n.Id == id);

                oldNode.Name = node.Name;
                oldNode.ParentId = node.ParentId;
                oldNode.Position = node.Position;

                db.SubmitChanges();
            }
            catch (Exception ex)
            {

            }
        }

        public void DeleteNode(int id)
        {
            try
            {
                Node node = db.Nodes.SingleOrDefault(n => n.Id == id);

                db.Nodes.DeleteOnSubmit(node);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                
            }
        }

        public void DeleteNodes(List<int> ids)
        {
            try
            {
                var nodes = (from n in db.Nodes where ids.Contains(n.Id) select n);

                db.Nodes.DeleteAllOnSubmit(nodes);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                
            }
        }

        public int CreateCopy(Node node, int? parentId, int position)
        {
            Node newnode = new Node
            {
                CourseId = node.CourseId,
                Name = node.Name,
                ParentId = parentId,
                IsFolder = node.IsFolder,
                Position = position
            };

            CopyNodes(node, newnode);

            db.Nodes.InsertOnSubmit(newnode);
            db.SubmitChanges();

            return newnode.Id;
        }

        public void CopyNodes(Node node, Node newnode)
        {
            foreach (Node child in node.Nodes)
            {
                Node newchild = new Node
                {
                    CourseId = child.CourseId,
                    Name = child.Name,
                    IsFolder = child.IsFolder,
                };

                newnode.Nodes.Add(newchild);

                if (child.Nodes.Count > 0)
                {
                    CopyNodes(child, newchild);
                }
            }
        }
    }
}
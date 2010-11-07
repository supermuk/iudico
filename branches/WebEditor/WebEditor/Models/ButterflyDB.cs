using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Linq.Expressions;
using System.ComponentModel;
using System;

namespace WebEditor.Models
{
    public class ButterflyDB: ButterflyDataContext
    {
        protected static readonly ButterflyDB instance = new ButterflyDB();

        protected ButterflyDB() :
            base(global::System.Configuration.ConfigurationManager.ConnectionStrings["ButterflyConnectionString"].ConnectionString)
        {
        }

        public static ButterflyDB Instance
        {
            get
            {
                return instance;
            }
        }

        public int AddCourse(Course course)
        {
            course.Created = DateTime.Now;
            course.Updated = DateTime.Now;
            base.Courses.InsertOnSubmit(course);
            base.SubmitChanges();
            return course.Id;
        }

        public Course GetCourse(int CourseID)
        {
            return Courses.SingleOrDefault(c => c.Id == CourseID);
        }

        public int AddNode(Node node)
        {
            base.Nodes.InsertOnSubmit(node);
            base.SubmitChanges();
            return node.Id;
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

    public static class ContextExtension
    {
        public static void ClearCache(this ButterflyDataContext context)
        {
            const BindingFlags FLAGS = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

            var method = context.GetType().GetMethod("ClearCache", FLAGS);

            method.Invoke(context, null);
        }
    }
}
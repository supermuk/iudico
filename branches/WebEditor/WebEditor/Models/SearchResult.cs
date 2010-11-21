using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebEditor.Models
{
    public interface ISearchResult
    {
        int GetID();
        String GetName();
        String GetText();
        String GetUrl();
    }

    public class CourseResult : ISearchResult
    {
        protected Course course;

        public CourseResult(Course course)
        {
            this.course = course;
        }

        public int GetID()
        {
            return course.Id;
        }

        public String GetName()
        {
            return course.Name;
        }

        public String GetText()
        {
            return "";
        }

        public String GetUrl()
        {
            return "/Course/" + course.Id.ToString() + "/Node/Index";
        }
    }

    public class NodeResult : ISearchResult
    {
        protected Node node;
        protected String text;

        public NodeResult(Node node, String text)
        {
            this.node = node;
            this.text = text;
        }

        public int GetID()
        {
            return node.Id;
        }

        public String GetName()
        {
            return node.Name;
        }

        public String GetText()
        {
            if (!node.IsFolder)
                return text;
            else
                return "";
        }

        public String GetUrl()
        {
            return "/Course/" + node.CourseId.ToString() + "/Node/Index#" + node.Id;
        }
    }

    
}
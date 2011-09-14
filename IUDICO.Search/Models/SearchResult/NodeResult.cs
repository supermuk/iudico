using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Common.Models;

namespace IUDICO.Search.Models.SearchResult
{
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
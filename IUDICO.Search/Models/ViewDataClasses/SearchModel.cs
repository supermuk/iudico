using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using IUDICO.Common.Models.Shared;
using IUDICO.Search.Models.SearchResult;

namespace IUDICO.Search.Models.ViewDataClasses
{
    public class SearchModel
    {
        public string SearchText { get; set; }
        public int Score { get; set; }
        public int Total { get; set; }
        // public List<CheckBoxModel> CheckBoxes { get; set; }
        // public List<ISearchResult> SearchResult { get; set; }

        public IEnumerable<User> Users;

        public IEnumerable<Discipline> Disciplines;

        public IEnumerable<Course> Courses;

        public IEnumerable<Topic> Topics;

        public IEnumerable<Node> Nodes;
    }
}
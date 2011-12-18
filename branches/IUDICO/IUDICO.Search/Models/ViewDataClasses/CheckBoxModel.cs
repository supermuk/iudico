using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IUDICO.Search.Models.ViewDataClasses
{
    public class CheckBoxModel
    {
        public bool IsChecked { get; set; }
        public string Text { get; set; }
        public SearchType SearchType { get; set; }

        public CheckBoxModel()
        {

        }

        public CheckBoxModel(SearchType searchType)
        {
            SearchType = searchType;
            IsChecked = true;
            switch (searchType)
            {
                case SearchType.Courses:
                    Text="Courses";
                    break;
                case SearchType.Curriculums:
                    Text = "Curriculums";
                    break;
                case SearchType.Themes:
                    Text = "Themes";
                    break;
                case SearchType.Users:
                    Text = "Users";
                    break;
            }
        }
    }
}
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
            this.IsChecked = true;
            switch (searchType)
            {
                case SearchType.Courses:
                    this.Text = "Courses";
                    break;
                case SearchType.Disciplines:
                    this.Text = "Disciplines";
                    break;
                case SearchType.Topics:
                    this.Text = "Topics";
                    break;
                case SearchType.Users:
                    this.Text = "Users";
                    break;
                case SearchType.Groups:
                    this.Text = "Groups";
                    break;
            }
        }
    }
}
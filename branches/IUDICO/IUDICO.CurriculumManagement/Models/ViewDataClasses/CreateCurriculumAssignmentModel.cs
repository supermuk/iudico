using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Shared;

namespace IUDICO.CurriculumManagement.Models.ViewDataClasses
{
    public class CreateCurriculumModel
    {
        public IEnumerable<SelectListItem> Groups { get; set; }
        public int GroupId { get; set; }

        public CreateCurriculumModel()
        {
        }

        public CreateCurriculumModel(IEnumerable<Group> groups, int groupId)
        {
            Groups = groups
                     .Select(item => new SelectListItem
                     {
                         Text = item.Name,
                         Value = item.Id.ToString(),
                         Selected = false
                     });
            GroupId = groupId;
        }
    }
}
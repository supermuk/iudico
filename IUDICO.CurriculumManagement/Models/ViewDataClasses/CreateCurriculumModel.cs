﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Shared;
using System.ComponentModel.DataAnnotations;
using IUDICO.Common.Models.Attributes;

namespace IUDICO.CurriculumManagement.Models.ViewDataClasses
{
    public class CreateCurriculumModel
    {
        public IEnumerable<SelectListItem> Groups { get; set; }
        public IEnumerable<SelectListItem> Disciplines { get; set; }

        [DropDownList(SourceProperty = "Groups")]
        [LocalizedDisplayName("ChooseGroupForCurriculum")]
        public int GroupId { get; set; }

        [DropDownList(SourceProperty = "Disciplines")]
        [LocalizedDisplayName("ChooseDisciplineForCurriculum")]
        public int DisciplineId { get; set; }

        [LocalizedDisplayName("SetDate")]
        public bool SetDate { get; set; }

        [LocalizedDisplayName("StartDate")]
        [LocalizedRequired(ErrorMessage = "StartDateRequired")]
        [UIHint("DateTimeWithPicker")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [LocalizedDisplayName("EndDate")]
        [LocalizedRequired(ErrorMessage = "StartDateRequired")]
        [UIHint("DateTimeWithPicker")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        public CreateCurriculumModel()
        {
        }

        public CreateCurriculumModel(IEnumerable<Group> groups, int groupId,
            IEnumerable<Discipline> disciplines, int disciplineId,
            DateTime? startDate, DateTime? endDate)
        {
            Groups = groups
                     .Select(item => new SelectListItem
                     {
                         Text = item.Name,
                         Value = item.Id.ToString(),
                         Selected = false
                     });
            GroupId = groupId;
            Disciplines = disciplines
                     .Select(item => new SelectListItem
                     {
                         Text = item.Name,
                         Value = item.Id.ToString(),
                         Selected = false
                     });
            DisciplineId = disciplineId;
            SetDate = startDate.HasValue && endDate.HasValue;
            StartDate = startDate ?? DateTime.Now;
            EndDate = endDate ?? DateTime.Now;
        }
    }
}
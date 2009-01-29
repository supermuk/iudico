using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IUDICO.DataModel;
using IUDICO.DataModel.Controllers;

public partial class CurriculumTimeline : ControlledPage<CurriculumTimelineController>
{
    protected override void BindController(CurriculumTimelineController c)
    {
        base.BindController(c);

        c.CurriculumTree = TreeView_Curriculum;

        c.DateSinceTextBox = TextBox_DateSince;
        c.DateTillTextBox = TextBox_DateTill;
        c.TimeSinceTextBox = TextBox_TimeSince;
        c.TimeTillTextBox = TextBox_TimeTill;

        c.GrantButton = Button_Grant;
        c.CurriculumForGroupLabel = Label_CurriculumForGroup;
        c.OperationList = DropDownList_Operation;

        CurriculumAssigment p = PreviousPage as CurriculumAssigment;
        if (p != null)
        {
            c.curriculumID = p.CurriculumID;
            c.groupID = p.GroupID;
        }

        Load += c.PageLoad;
    }
}

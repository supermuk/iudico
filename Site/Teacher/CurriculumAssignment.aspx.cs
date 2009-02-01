using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IUDICO.DataModel;
using IUDICO.DataModel.Controllers;

public partial class CurriculumAssignment : ControlledPage<CurriculumAssignmentController>
{
    protected override void BindController(CurriculumAssignmentController c)
    {
        base.BindController(c);

        c.AssigmentsTree = TreeView_Assigments;

        c.AssignButton = Button_Assign;
        c.SwitchViewButton = Button_SwitchView;
        c.UnsignButton = Button_Unsign;

        c.GroupsListBox = ListBox_Groups;
        c.CurriculumsListBox = ListBox_Curriculums;
        
        Load += c.PageLoad;
    }
}

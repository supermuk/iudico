using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IUDICO.DataModel;
using IUDICO.DataModel.Controllers;

public partial class CurriculumAssigment : ControlledPage<CurriculumAssignController>
{
    protected override void BindController(CurriculumAssignController c)
    {
        base.BindController(c);

        c.AssigmentsTree = TreeView_Assigments;

        c.AssignButton = Button_Assign;
        c.SwitchViewButton = Button_SwitchView;
        c.TimelineButton = Button_Timeline;

        c.GroupsListBox = ListBox_Groups;
        c.CurriculumsListBox = ListBox_Curriculums;
        
        Load += c.PageLoad;
    }

    public int GroupID
    {
        get
        {
            if (ListBox_Groups.SelectedValue != null)
            {
                return int.Parse(ListBox_Groups.SelectedValue);
            }
            else
            {
                return -1;
            }
        }
    }

    public int CurriculumID
    {
        get
        {
            if (ListBox_Curriculums.SelectedValue != null)
            {
                return int.Parse(ListBox_Curriculums.SelectedValue);
            }
            else
            {
                return -1;
            }
        }
    }
}

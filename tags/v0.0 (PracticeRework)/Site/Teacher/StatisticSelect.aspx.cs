﻿using IUDICO.DataModel;
using IUDICO.DataModel.Controllers;

public partial class StatisticSelect : ControlledPage<StatisticSelectController>
{
    protected override void BindController(StatisticSelectController c)
    {
        base.BindController(c);

        Bind(Button_Show, c.ShowButton_Click);
        BindEnabled(Button_Show, c.ShowButtonEnabled);

        Bind(Label_PageCaption, c.Caption);
        Bind(Label_PageDescription, c.Description);
        Bind(Label_PageMessage, c.Message);
        BindTitle(c.Title, gn => gn);

        c.CurriculumsDropDownList = DropDownList_Curriculums;
        c.GroupsDropDownList = DropDownList_Groups;
        c.RawUrl = Request.RawUrl;
        c.IsPostBack = IsPostBack;
    }
}
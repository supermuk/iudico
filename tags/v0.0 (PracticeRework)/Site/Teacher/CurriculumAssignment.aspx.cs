﻿using IUDICO.DataModel;
using IUDICO.DataModel.Controllers;

public partial class CurriculumAssignment : ControlledPage<CurriculumAssignmentController>
{
    protected override void BindController(CurriculumAssignmentController c)
    {
        base.BindController(c);

        Bind(Label_PageCaption, c.Caption);
        Bind(Label_PageDescription, c.Description);
        Bind(Label_PageMessage, c.Message);
        BindTitle(c.Title, gn => gn);
        BindVisible(Table_Main, c.MainTableVisible);
    }
}
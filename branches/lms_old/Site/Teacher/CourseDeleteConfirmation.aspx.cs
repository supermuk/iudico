using IUDICO.DataModel;
using IUDICO.DataModel.Controllers;

public partial class CourseDeleteConfirmation : ControlledPage<CourseDeleteConfirmationController>
{
    protected override void BindController(CourseDeleteConfirmationController c)
    {
        base.BindController(c);

        Bind(Label_PageCaption, c.Caption);
        Bind(Label_PageDescription, c.Description);
        Bind(Label_PageMessage, c.Message);
        BindTitle(c.Title, gn => gn);
        Bind(Button_Delete, c.DeleteButton_Click);

        Button_Back.PostBackUrl = c.BackUrl;

        
    }

    public override void DataBind()
    {
        base.DataBind();

        GridView_Dependencies.DataSource = Controller.GetDependencies();
        GridView_Dependencies.DataBind();
    }

}

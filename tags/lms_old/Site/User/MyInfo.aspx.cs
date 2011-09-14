using IUDICO.DataModel;
using IUDICO.DataModel.Controllers;

public partial class User_MyInfo : ControlledPage<UserInfoController>
{

    protected override void BindController(UserInfoController c)
    {
        base.BindController(c);

        Bind2Ways(TextBox_FirstName, c.FirstName);
        Bind2Ways(TextBox_SecondName, c.SecondName);
        Bind(TextBox_Login, c.Login);
        Bind2Ways(TextBox_Email, c.Email);
        Bind(Label_Roles, c.Roles);
        Bind(Button_Update, c.UpdateButton_Click);

        BindTitle(c.Title, gn => gn);
        Bind(Label_PageCaption, c.Caption);
        Bind(Label_PageDescription, c.Description);
        Bind(Label_PageMessage, c.Message);
    }

    protected override void OnLoad(System.EventArgs e)
    {
        base.OnLoad(e);

        if (IsFirstTimeRequest)
        {
            ChangePassword.ContinueDestinationPageUrl = Request.RawUrl;
        }
    }

    public override void DataBind()
    {
        base.DataBind();

        BulletedList_Groups.DataSource = Controller.GetGroups();
        BulletedList_Groups.DataBind();
    }
}

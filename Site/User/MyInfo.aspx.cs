using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IUDICO.DataModel;
using IUDICO.DataModel.Controllers;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.Security;

public partial class User_MyInfo : ControlledPage<UserInfoController>
{

    protected override void BindController(UserInfoController c)
    {
        base.BindController(c);

        c.FirstNameTextBox = TextBox_FirstName;
        c.SecondNameTextBox = TextBox_SecondName;
        c.LoginTextBox = TextBox_Login;
        c.EmailTextBox = TextBox_Email;
        c.RolesTextBox = TextBox_Roles;
        c.GroupsTextBox = TextBox_Groups;
        c.ChangePassword = ChangePassword;
        c.UpdateButton = Button_Update;

        Load += c.PageLoad;
    }
}

using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.DB.Base;

namespace IUDICO.DataModel.Controllers
{
    public class UserInfoController : ControllerBase
    {
        public TextBox FirstNameTextBox { get; set; }
        public TextBox SecondNameTextBox { get; set; }

        public TextBox EmailTextBox { get; set; }
        public TextBox LoginTextBox { get; set; }

        public TextBox RolesTextBox { get; set; }
        public TextBox GroupsTextBox { get; set; }

        public ChangePassword ChangePassword { get; set; }

        public Button UpdateButton { get; set; }

        public void PageLoad(object sender, EventArgs e)
        {
            //registering for events            
            UpdateButton.Click += new EventHandler(UpdateButton_Click);
            if (!(sender as Page).IsPostBack)
            {
                fillFields();
            }
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            TblUsers currentUser = ServerModel.DB.Load<TblUsers>(ServerModel.User.Current.ID);

            currentUser.FirstName = FirstNameTextBox.Text;
            currentUser.LastName = SecondNameTextBox.Text;
            if (IsValidEmailAddress(EmailTextBox.Text))
            {
                currentUser.Email = EmailTextBox.Text;
                EmailTextBox.Style["color"] = "black";
            }
            else
            {
                EmailTextBox.Style["color"] = "red";
                return;
            }
            currentUser.Login = LoginTextBox.Text;
            ServerModel.DB.Update<TblUsers>(currentUser);
        }

        private void fillFields()
        {
            TblUsers currentUser = ServerModel.DB.Load<TblUsers>(ServerModel.User.Current.ID);

            FirstNameTextBox.Text = currentUser.FirstName;
            SecondNameTextBox.Text = currentUser.LastName;
            EmailTextBox.Text = currentUser.Email;
            LoginTextBox.Text = currentUser.Login;

            string roles = "";
            foreach (string role in ServerModel.User.Current.Roles)
            {
                roles += role + ", ";
            }
            roles.TrimEnd(' ', ',');

            string groups = "";
            foreach (TblGroups group in getUserGroups(currentUser))
            {
                groups += group.Name + ", ";
            }
            groups.TrimEnd(' ', ',');

            RolesTextBox.Text = roles;
            GroupsTextBox.Text = groups;


        }

        private IList<TblGroups> getUserGroups(TblUsers user)
        {
            return ServerModel.DB.Query<TblGroups>(
                new InCondition<int>(
                   DataObject.Schema.ID,
                   new SubSelectCondition<RelUserGroups>("GroupRef",
                      new CompareCondition<int>(
                         DataObject.Schema.UserRef,
                         new ValueCondition<int>(user.ID),
                         COMPARE_KIND.EQUAL
                     )
                 ),
                 IN_CONDITION_KIND.NOT_IN
             ));
        }

        public static bool IsValidEmailAddress(string sEmail)
        {
            return true;
        }


    }
}

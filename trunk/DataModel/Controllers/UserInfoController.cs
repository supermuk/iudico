using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.DB.Base;
using LEX.CONTROLS;
using IUDICO.DataModel.Common;

namespace IUDICO.DataModel.Controllers
{
    public class UserInfoController : ControllerBase
    {
        [PersistantField]
        public IVariable<string> FirstName = string.Empty.AsVariable();
        [PersistantField]
        public IVariable<string> SecondName = string.Empty.AsVariable();
        [PersistantField]
        public IVariable<string> Email = string.Empty.AsVariable();
        
        public IVariable<string> Login = string.Empty.AsVariable();
        public IVariable<string> Roles = string.Empty.AsVariable();
        public IVariable<string> Caption = string.Empty.AsVariable();
        public IVariable<string> Description = string.Empty.AsVariable();
        public IVariable<string> Message = string.Empty.AsVariable();
        public IVariable<string> Title = string.Empty.AsVariable();

        //"magic words"
        private const string pageCaption = "User personal info.";
        private const string pageDescription = "This is your info page. Here you can change your name, email and password. Look up for your roles and groups.";

        public override void Loaded()
        {
            base.Loaded();

            Caption.Value = pageCaption;
            Description.Value = pageDescription;
            Title.Value = Caption.Value;

            fillInfo();
        }


        public void UpdateButton_Click()
        {
            TblUsers currentUser = ServerModel.DB.Load<TblUsers>(ServerModel.User.Current.ID);

            currentUser.FirstName = FirstName.Value;
            currentUser.LastName = SecondName.Value;
            currentUser.Email = Email.Value;

            ServerModel.DB.Update<TblUsers>(currentUser);
        }

        private void fillInfo()
        {
            TblUsers currentUser = ServerModel.DB.Load<TblUsers>(ServerModel.User.Current.ID);

            FirstName.Value = currentUser.FirstName == null ? string.Empty : currentUser.FirstName;
            SecondName.Value = currentUser.LastName;
            Email.Value = currentUser.Email;
            Login.Value = currentUser.Login;

            string roles = "";
            foreach (string role in ServerModel.User.Current.Roles)
            {
                roles += role + ", ";
            }
            roles = roles.TrimEnd(' ', ',');
            Roles.Value = roles;
        }

        public IList<string> GetGroups()
        {
            TblUsers currentUser = ServerModel.DB.Load<TblUsers>(ServerModel.User.Current.ID);
            List<string> result = new List<string>();
            foreach (TblGroups group in TeacherHelper.GetUserGroups(currentUser))
            {
                result.Add(group.Name);
            }

            return result;
        }

    }
}

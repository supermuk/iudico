using System.Collections.Generic;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.DB;
using LEX.CONTROLS;
using LEX.CONTROLS.Expressions;

namespace IUDICO.DataModel.Controllers
{
    /// <summary>
    /// Controller for UserInfo.aspx page
    /// </summary>
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
        private readonly string pageCaption = Translations.UserInfoController_pageCaption;
        private readonly string pageDescription = Translations.UserInfoController_pageDescription_This_is_your_info_page__Here_you_can_change_your_name__email_and_password__Look_up_for_your_roles_and_groups_;

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

            ServerModel.DB.Update(currentUser);
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

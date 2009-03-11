using System;
using System.Collections.Generic;
using System.Web;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.DB;
using LEX.CONTROLS;

namespace IUDICO.DataModel.Controllers
{
    public class Admin_CreateBulkUserController : ControllerBase
    {
        [PersistantField]
        public readonly IVariable<string> Prefix = string.Empty.AsVariable();
        [PersistantField]
        public readonly IVariable<string> Count = string.Empty.AsVariable();
        [PersistantField]
        public readonly IVariable<string> ErrorText = string.Empty.AsVariable();
        [PersistantField]
        public readonly IVariable<string> Password = string.Empty.AsVariable();

        public void DoCreate()
        {
            int count;
            if (int.TryParse(Count.Value, out count))
            {
                if (Password.Value.Trim().IsNotNull())
                {


                    var users = new List<TblUsers>(count);
                    var pswHash = ServerModel.User.GetPasswordHash(Password.Value);
                    for (int i = 0; i < count; i++)
                    {
                        var login = Prefix.Value + i;
                        users.Add(new TblUsers
                                      {
                                          Email = login,
                                          Login = login,
                                          LastName = login,
                                          FirstName = login,
                                          PasswordHash = pswHash
                                      });
                    }
                    try
                    {
                        ServerModel.DB.Insert<TblUsers>(users);
                        ErrorText.Value = null;
                        RedirectToController(new Admin_UsersController {BackUrl = HttpContext.Current.Request.RawUrl});
                    }
                    catch
                    {
                        ErrorText.Value = "Some of users like these already exist";
                    }
                }
                else
                {
                    ErrorText.Value = "Password is not specified";
                }
            }
            else
            {
                ErrorText.Value = string.Format("{0} is not a number", Count.Value);
            }
        }
    }
}

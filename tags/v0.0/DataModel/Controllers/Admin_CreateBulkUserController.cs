using System.Collections.Generic;
using System.Diagnostics;
using System.Web;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.DB;
using LEX.CONTROLS;
using LEX.CONTROLS.Expressions;

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

        [PersistantField]
        public readonly IVariable<int> SelectedGroupID = (-2).AsVariable();

        [PersistantField]
        public readonly IVariable<string> NewGroupName = string.Empty.AsVariable();

        [PersistantField] 
        public readonly IVariable<bool> MakeStudent = false.AsVariable();

        [PersistantField]
        public readonly IVariable<bool> AddToGroup = false.AsVariable();

        public readonly IVariable<ComparableDictionary<int, string>> Groups = new ComparableDictionary<int, string>().AsVariable();
        

        public override void Initialize()
        {
            base.Initialize();

            var groups = ServerModel.DB.Query<TblGroups>(null);
            var list = new ComparableDictionary<int, string> { { -1, "<new>" } };
            foreach (var g in groups)
            {
                list.Add(g.ID, g.Name);
            }

            Groups.Value = list;
        }

        public void DoCreate()
        {
            int count;
            if (int.TryParse(Count.Value, out count))
            {
                if (AddToGroup.Value && SelectedGroupID.Value == -1 && string.IsNullOrEmpty(NewGroupName.Value))
                {
                    ErrorText.Value = "Group name is empty";
                }

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

                        if (AddToGroup.Value)
                        {
                            Debug.Assert(SelectedGroupID.Value >= -1);

                            TblGroups group;
                            if (SelectedGroupID.Value == -1)
                            {
                                ServerModel.DB.Insert(group = new TblGroups{Name = NewGroupName.Value});
                            }
                            else
                            {
                                group = ServerModel.DB.Load<TblGroups>(SelectedGroupID.Value);
                            }

                            foreach (var u in users)
                            {
                                ServerModel.DB.Link(u, group);
                                if (MakeStudent.Value)
                                {
                                    ServerModel.DB.Link(u, FxRoles.STUDENT);
                                }
                            }
                        }

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

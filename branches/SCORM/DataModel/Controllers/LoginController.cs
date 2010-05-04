﻿using System.Web.Security;
using System.Web.UI.WebControls;
using System.Web;
using System.Linq;

namespace IUDICO.DataModel.Controllers
{
    /// <summary>
    /// Controller for Login.aspx page
    /// </summary>
    public class LoginController : ControllerBase
    {
        public void Authenticate(object sender, AuthenticateEventArgs e)
        {
            var l = (Login)sender;
            bool isAuthenticated = Membership.ValidateUser(l.UserName, l.Password);

            if (isAuthenticated)
            {
                var user = ServerModel.DB.TblUsers.Where(u => u.Login == l.UserName).SingleOrDefault();
                string IP = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (IP == null)
                {
                    IP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
                var computerId = this.LookupOrAddComputer(IP);
                var signInInfo = ServerModel.DB.TblUsersSignIn.Where(u => u.TblUsers == user).SingleOrDefault();
                if (signInInfo != null)
                {
                    signInInfo.LastLogin = System.DateTime.Now;
                    signInInfo.ComputerId = computerId;
                }
                else
                {
                    signInInfo = new IUDICO.DataModel.DB.TblUsersSignIn();
                    signInInfo.UserId = user.ID;
                    signInInfo.ComputerId = computerId;
                    signInInfo.LastLogin = System.DateTime.Now;
                    ServerModel.DB.TblUsersSignIn.InsertOnSubmit(signInInfo);
                }

                ServerModel.DB.SubmitChanges();
            }

            e.Authenticated = isAuthenticated; 
        }

        private int LookupOrAddComputer(string ip)
        {
            var computer = ServerModel.DB.TblComputers.Where(c => c.IP == ip).FirstOrDefault();
            if (computer != null)
            {
                return computer.ID;
            }
            else
            {
                //creating
                computer = new IUDICO.DataModel.DB.TblComputers();
                computer.IP = ip;
                computer.LectureRoom = "";
                computer.ComputerName = "";
                ServerModel.DB.TblComputers.InsertOnSubmit(computer);
                ServerModel.DB.SubmitChanges();
                return computer.ID;
            }
        }
    }
}

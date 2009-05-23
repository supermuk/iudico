<%@ WebHandler Language="C#" Class="Logout" %>

using System.Web;
using System.Web.Security;
using IUDICO.DataModel;
using IUDICO.DataModel.Controllers;

public class Logout : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        FormsAuthentication.SignOut();
        context.Response.Redirect(ServerModel.Forms.BuildRedirectUrl(new LoginController { BackUrl = string.Empty }));
    }

    public bool IsReusable
    {
        get
        {
            return true;
        }
    }

}
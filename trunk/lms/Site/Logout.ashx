<%@ WebHandler Language="C#" Class="Logout" %>

using System.Web;
using System.Web.Security;
using IUDICO.DataModel;
using IUDICO.DataModel.Controllers;
using System.Collections.Generic;
using System.Linq;
using System;

public class Logout : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        Dictionary<string, DateTime> activity = (Dictionary<string, DateTime>)HttpContext.Current.Application["activityList"];
        bool isOnline = false;
        if (activity.Keys.Contains(context.User.Identity.Name))
        {
            activity.Remove(context.User.Identity.Name);
        }
        
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
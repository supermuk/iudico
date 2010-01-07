using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using IUDICO.DataModel;
using IUDICO.DataModel.Common;

/// <summary>
/// Summary description for API
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class API : System.Web.Services.WebService
{

    public API()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public int Initialize(int themeId)
    {
        int attemptId = Cmi.Initialize(themeId);

        return attemptId;
    }

    [WebMethod]
    public void SetValue(string name, string value, int attemptId)
    {
        Cmi.SetVariable(name, value, attemptId);
    }

    [WebMethod]
    public string GetValue(string name, int attemptId)
    {
        switch (name)
        {
            case "cmi._version":
                return "1.0";
            case "cmi.learner_id":
                return ServerModel.User.Current.ID.ToString();
            default:
                string[] parts = name.Split('.');

                switch (parts[parts.Length - 1])
                {
                    case "_children":
                        return Cmi.GetChildren(string.Join(".", parts, 0, parts.Length - 1), attemptId);
                    case "_count":
                        return Cmi.GetCount(string.Join(".", parts, 0, parts.Length - 1), attemptId);
                    default:
                        return Cmi.GetVariable(name, attemptId);
                }
        }
    }
}


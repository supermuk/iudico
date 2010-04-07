using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.SessionState;
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
    CmiDataModel CmiDM;
    LnuDataModel LnuDM;

    public API()
    {
        int LearnerSessionId = Convert.ToInt32(HttpContext.Current.Session["CurrentLearnerSessionId"].ToString());
        int UserId = ServerModel.User.Current.ID;

        CmiDM = new CmiDataModel(LearnerSessionId, UserId, false);
        LnuDM = new LnuDataModel();
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod(EnableSession = true)]
    public int Initialize()
    {
        //CmiDM = new CmiDataModel(themeId, ServerModel.User.Current.ID);
        //return CmiDM.Attempt.ID;
        return 1;
    }

    [WebMethod(EnableSession = true)]
    public void SetValue(string name, string value)
    {
        string[] parts = name.Split('.');

        if (parts[0] != "cmi")
        {
            throw new NotImplementedException();
        }
        else
        {
            CmiDM.SetValue(string.Join(".", parts, 1, parts.Length - 1), value);
        }
    }

    [WebMethod(EnableSession = true)]
    public string GetValue(string name)
    {
        string[] parts = name.Split('.');

        if (parts[0] == "lnu")
        {
            return LnuDM.GetValue(string.Join(".", parts, 1, parts.Length - 1));
        }
        else if (parts[0] != "cmi")
        {
            throw new NotImplementedException();
        }
        else
        {
            return CmiDM.GetValue(string.Join(".", parts, 1, parts.Length - 1));
        }
    }
}


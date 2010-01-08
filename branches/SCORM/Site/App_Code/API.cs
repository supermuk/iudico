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
    CmiDataModel CmiDM;

    public API()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public int Initialize(int themeId)
    {
        CmiDM = new CmiDataModel(themeId, ServerModel.User.Current.ID);

        return CmiDM.Attempt.ID;
    }

    [WebMethod]
    public void SetValue(string name, string value, int attemptId)
    {
        string[] parts = name.Split('.');

        if (parts[0] != "cmi")
        {
            throw new NotImplementedException();
        }
        else
        {
            CmiDM = new CmiDataModel(attemptId);
            CmiDM.SetValue(string.Join(".", parts, 1, parts.Length - 1), value);
        }
    }

    [WebMethod]
    public string GetValue(string name, int attemptId)
    {
        string[] parts = name.Split('.');

        if (parts[0] != "cmi")
        {
            throw new NotImplementedException();
        }
        else
        {
            CmiDM = new CmiDataModel(attemptId);
            return CmiDM.GetValue(string.Join(".", parts, 1, parts.Length - 1));
        }
    }
}


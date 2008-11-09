<%@ Import Namespace="IUDICO.DataModel"%>
<%@ Import Namespace="IUDICO.DataModel.Common"%>
<%@ Import Namespace="IUDICO.DataModel.Security"%>
<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        using (var c = ServerModel.AcruireOpenedConnection())
        {
            PermissionsManager.Current.Initialize(c);
        }
    } 
        
    void Application_End(object sender, EventArgs e) 
    {

    }
        
    void Application_Error(object sender, EventArgs ea)
    {
        Exception e = Server.GetLastError();
        var es = new StringBuilder("EXCEPTION: ");
        while (e != null)
        {
            es.AppendLine(e.Message);
            es.AppendLine("STACK: " + e.StackTrace);
            e = e.InnerException;
            if (e != null)
            {
                es.Append("INNER EXCEPTION: ");
            }
        }
        Logger.WriteLine(es.ToString());
    }

    void Session_Start(object sender, EventArgs e) 
    {
        
    }

    void Session_End(object sender, EventArgs e) 
    {
    } 
</script>

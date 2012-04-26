<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<System.Web.Mvc.HandleErrorInfo>" %>
<%@ Import Namespace="IUDICO.Common" %>

<asp:Content ID="errorTitle" ContentPlaceHolderID="TitleContent" runat="server">
    <%=Localization.GetMessage("Error")%>
</asp:Content>
<asp:Content ID="errorContent" ContentPlaceHolderID="MainContent" runat="server">
    <% if (Model != null)
       { %>
            <b><%=Localization.GetMessage("ExceptionOfType")%>: </b><%: Model.ActionName %> </b><%: Model.Exception.GetType().Name %><b> <%=Localization.GetMessage("occurred")%>.</b><br />
            <b><%=Localization.GetMessage("Message")%>: </b><%: Model.ActionName %>: </b>"<%: Model.Exception.Message %>"<br />
            <b><%=Localization.GetMessage("Controller")%>: </b><%: Model.ActionName %>: </b><%: Model.ControllerName %><br />
            <b><%=Localization.GetMessage("Action")%>: </b><%: Model.ActionName %><br />
    <% } %>
    <% else
       { %>
            <%=Localization.GetMessage("SorryErrorOccurredWhileProcessingYourRequest") %>
    <% } %>
</asp:Content>

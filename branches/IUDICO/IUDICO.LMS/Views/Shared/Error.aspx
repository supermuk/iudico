<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<System.Web.Mvc.HandleErrorInfo>" %>

<asp:Content ID="errorTitle" ContentPlaceHolderID="TitleContent" runat="server">
    <%=IUDICO.LMS.Localization.GetMessage("Error")%>
</asp:Content>
<asp:Content ID="errorContent" ContentPlaceHolderID="MainContent" runat="server">
    <% if (Model != null)
       { %>
            <b><%=IUDICO.LMS.Localization.GetMessage("ExceptionOfType")%>: </b><%: Model.ActionName %> </b><%: Model.Exception.GetType().Name %><b> <%=IUDICO.LMS.Localization.GetMessage("occurred")%>.</b><br />
            <b><%=IUDICO.LMS.Localization.GetMessage("Message")%>: </b><%: Model.ActionName %>: </b>"<%: Model.Exception.Message %>"<br />
            <b><%=IUDICO.LMS.Localization.GetMessage("Controller")%>: </b><%: Model.ActionName %>: </b><%: Model.ControllerName %><br />
            <b><%=IUDICO.LMS.Localization.GetMessage("Action")%>: </b><%: Model.ActionName %><br />
    <% } %>
    <% else
       { %>
            <%=IUDICO.LMS.Localization.GetMessage("SorryErrorOccurredWhileProcessingYourRequest") %>
    <% } %>
</asp:Content>

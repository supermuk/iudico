<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<System.Web.Mvc.HandleErrorInfo>" %>

<asp:Content ID="errorTitle" ContentPlaceHolderID="TitleContent" runat="server">
    <%=IUDICO.LMS.Localization.getMessage("Error")%>
</asp:Content>
<asp:Content ID="errorContent" ContentPlaceHolderID="MainContent" runat="server">
    <% if (Model != null)
       { %>
            <b><%=IUDICO.LMS.Localization.getMessage("ExceptionOfType")%>: </b><%: Model.ActionName %> </b><%: Model.Exception.GetType().Name %><b> <%=IUDICO.LMS.Localization.getMessage("occurred")%>.</b><br />
            <b><%=IUDICO.LMS.Localization.getMessage("Message")%>: </b><%: Model.ActionName %>: </b>"<%: Model.Exception.Message %>"<br />
            <b><%=IUDICO.LMS.Localization.getMessage("Controller")%>: </b><%: Model.ActionName %>: </b><%: Model.ControllerName %><br />
            <b><%=IUDICO.LMS.Localization.getMessage("Action")%>: </b><%: Model.ActionName %><br />
    <% } %>
    <% else
       { %>
            <%=IUDICO.LMS.Localization.getMessage("SorryErrorOccurredWhileProcessingYourRequest") %>
    <% } %>
</asp:Content>

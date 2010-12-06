<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<System.Web.Mvc.HandleErrorInfo>" %>

<asp:Content ID="errorTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Error
</asp:Content>
<asp:Content ID="errorContent" ContentPlaceHolderID="MainContent" runat="server">
    <% if (Model != null)
       { %>
            <b>An exception of type </b><%: Model.Exception.GetType().Name %><b> occured.</b><br />
            <b>Message: </b>"<%: Model.Exception.Message %>"<br />
            <b>Controler: </b><%: Model.ControllerName %><br />
            <b>Action: </b><%: Model.ActionName %><br />
    <% } %>
    <% else
       { %>
            Sorry, an error occurred while processing your request.
    <% } %>
</asp:Content>

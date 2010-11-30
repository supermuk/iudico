<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<System.Web.Mvc.HandleErrorInfo>" %>

<asp:Content ID="errorTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Error
</asp:Content>
<asp:Content ID="errorContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <% if (Model != null)
           { %>
                An exception of type
                <%: Html.Label(Model.Exception.GetType().Name)%>
                occured.<br />
                Message: "<%: Html.Label(Model.Exception.Message)%>"<br />
                Controler:
                <%: Html.Label(Model.ControllerName)%><br />
                Action:
                <%: Html.Label(Model.ActionName)%><br />
         <%} %>
        <% else
           { %>
            Sorry, an error occurred while processing your request.
        <% } %>
    </h2>
</asp:Content>

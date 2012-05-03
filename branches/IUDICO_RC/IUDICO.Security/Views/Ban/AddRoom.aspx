<%@ Assembly Name="IUDICO.Security" %>

<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Security.ViewModels.Ban.AddRoomViewModel>" %>
<%@ Import Namespace="IUDICO.Common" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Add Room
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <fieldset>
    <legend> <%=Localization.GetMessage("Add room") %></legend>
    <% if (string.IsNullOrEmpty(Model.Name))
       { %>
    <% using (Html.BeginForm())
       { %>
            <%: Html.ValidationSummary(true) %>
            <%= Html.EditorForModel() %>
            <p>
               <input type="submit" value="<%=Localization.GetMessage("Save") %>" />
           </p>
    <% }
       }
       else
       {
           Writer.Write(Html.DisplayForModel().ToHtmlString());
           Writer.Write(Html.ActionLink(Localization.GetMessage("BackToList"), "BanRoom", "Ban"));
       } %>
       
    </fieldset>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

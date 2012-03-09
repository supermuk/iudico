<%@ Assembly Name="IUDICO.Security" %>

<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Security.ViewModels.Ban.AddRoomViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Add Room
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <fieldset>
    <legend> Add Room</legend>
    <% if (string.IsNullOrEmpty(Model.Name))
       { %>
    <% using (Html.BeginForm())
       { %>
            <%: Html.ValidationSummary(true) %>
            <%= Html.EditorForModel() %>
            <p>
               <input type="submit" value="Save" />
           </p>
    <% }
       }
       else
       {
           Writer.Write(Html.DisplayForModel().ToHtmlString());
       } %>
       
    </fieldset>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

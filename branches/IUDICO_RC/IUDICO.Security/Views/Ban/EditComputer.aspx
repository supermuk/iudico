<%@ Assembly Name="IUDICO.Security" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Security.ViewModels.Ban.EditComputersViewModel>" %>
<%@ Import Namespace="IUDICO.Common" %>

<asp:Content ID="Content0" ContentPlaceHolderID="TitleContent" runat="server">
	EditComputer
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <fieldset>

    <h2>Edit Computer</h2>

    <% using (Html.BeginForm())
           { %>
            
            <%= Html.EditorForModel() %>
        <p>
           <input type="submit" value="<%=Localization.GetMessage("Save")%>" name="saveButton" />
       </p>
        <% } %>
        
        <%Writer.Write(Html.ActionLink(Localization.GetMessage("BackToList"), "BanComputer", "Ban")); %>     

</fieldset>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

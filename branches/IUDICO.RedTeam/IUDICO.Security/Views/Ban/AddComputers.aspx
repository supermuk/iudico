<%@ Assembly Name="IUDICO.Security" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Security.ViewModels.BanAddComputerViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Add Computer
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Add Computer</h2>
    
    <% if (string.IsNullOrEmpty(Model.ComputerIP)) 
       { %>
    
        <% using (Html.BeginForm())
           { %>
            <%= Html.EditorForModel() %>
        <% }
       }
       else 
       {
           Writer.Write(Html.DisplayForModel().ToHtmlString());
       } %>


</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

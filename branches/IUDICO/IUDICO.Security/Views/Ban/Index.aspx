<%@ Assembly Name="IUDICO.Security" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Security.ViewModels.Ban.IndexViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Ban/Unban user
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<h2><%= Model.GetMessage("Ban / Unban") %></h2>
    
    <h4><%= Model.GetMessage("Computer") %></h4>
    <ul>
        <li> <%= Html.ActionLink(Model.GetMessage("Edit computers"), "EditComputers", "Ban") %> </li>
    </ul>
            
    <h4><%= Model.GetMessage("Room") %></h4>
    <ul>
        <li> <%= Html.ActionLink(Model.GetMessage("Add room"), "AddRoom", "Ban") %> </li>
        <li> <%= Html.ActionLink(Model.GetMessage("Edit room"), "EditRoom", "Ban") %> </li>
        <li> <%= Html.ActionLink(Model.GetMessage("Ban room"), "BanRoom", "Ban") %> </li>
    </ul>    
</asp:Content>
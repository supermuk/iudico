<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Int64>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Play course
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <iframe width="100%" height="100%" frameborder="0" src="<%: "/Plugins/IUDICO.TestingSystem.dll/IUDICO.TestingSystem/Player/Frameset.aspx" %>" id="content" name="content" style="display: block; overflow-x: hidden;"></iframe>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>

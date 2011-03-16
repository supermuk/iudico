<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Int64>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Play course
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <iframe width="100%" height="800px" frameborder="0" src="<%: "/Plugins/IUDICO.TestingSystem.dll/IUDICO.TestingSystem/Player/Frameset.aspx?View=0&AttemptId=" + Model.ToString() %>" id="content" name="content" style="display: inline; overflow-x: hidden;"></iframe>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Int64>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Play course
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Play course...</h2>

    <p><%: Html.ActionLink("Back to list", "Index") %>
    </p>
    <h3><%: Html.ActionLink("Play course", "Play", Model, new { onclick = "javascript:OpenFrameset(" + Model + ");" })%>
    </h3>
    <iframe width="100%" height="100%" frameborder="0" src="<%: "http://localhost:1339/BasicWebPlayer/Frameset/Frameset.aspx?View=0&AttemptId=" + Model %>" id="content" name="content" style="display: block; overflow-x: hidden;"></iframe>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
<script language="javascript" type="text/javascript">
    function OpenFrameset(strAttemptId) {
        // open the frameset for viewing training content; <strAttemptId> is the attempt ID
        window.open("http://localhost:1339/BasicWebPlayer/Frameset/Frameset.aspx?View=0&AttemptId=" + strAttemptId, "_blank");
    }
</script>
</asp:Content>

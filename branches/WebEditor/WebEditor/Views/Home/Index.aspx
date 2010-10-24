<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h3>Welcome to Butterfly - Web Editor for SCORM compatible courses.</h3>

    <script type="text/javascript" src="/Scripts/custom/ckeditor/ckeditor.js"></script>
    <script type="text/javascript" src="/Scripts/custom/ckeditor/adapters/jquery.js"></script>
    <script type="text/javascript">
        $(function () {
            $('textarea').ckeditor();
        });
    </script>

    <textarea name="editor"></textarea>

</asp:Content>

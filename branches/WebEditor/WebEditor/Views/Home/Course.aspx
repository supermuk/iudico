<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Empty.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Course
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../../Content/ui-lightness/jquery-ui-1.8.5.custom.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/Scripts/jquery/jquery.layout.js"></script>
    <script type="text/javascript" src="/Scripts/custom/ckeditor/ckeditor_source.js"></script>
    <script type="text/javascript" src="/Scripts/custom/ckeditor/adapters/jquery.js"></script>
    <script type="text/javascript">
        $(function () {
            $('body').layout({ applyDefaultStyles: true });
            $('textarea').ckeditor();

            CKEDITOR.on('instanceReady',
              function (evt) {
                  var editor = evt.editor;
                  //editor.execCommand('maximize');
              }
            );
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <div class="ui-layout-center ui-layout-content">
            <textarea name="editor" rows="5" cols="5"></textarea>
        </div>
        <div class="ui-layout-north">North</div>
        <div class="ui-layout-south ui-widget-header ui-corner-all">South</div>
        <div class="ui-layout-east">East</div>
        <div class="ui-layout-west">West</div>

</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Common.Models.Shared.Curriculum>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Details</h2>
    
    <% foreach(var chapter in Model.CurriculumChapters) { %>
        <div><%: chapter.Chapter.Name %></div>
        <% foreach(var topic in chapter.CurriculumChapterTopics )  { %>
            <div>
                <%: topic.Topic.Name %>
                <a href="#" onclick="edit(<%: topic.Id %>);">Edit</a>
            </div>
        <% } %>
    <% } %>
    
    <div id="dialog">
        <div id="dialogInner"></div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {

            $("#dialog").dialog({
                autoOpen: false,
                modal: true,
                buttons: {
                    'Submit': function () {
                        $("#dialogInner").find("form").submit();
                    },
                    'Cancel': function () {
                        $(this).dialog('close');
                    }
                }
            });

        });
        function edit(id) {
            $.ajax({
                type: "get",
                url: "/Curriculum",
                data: { id: id },
                success: function (r) {
                    $("#dialogInner").html(r);
                }
            });
            
        }
    </script>
</asp:Content>

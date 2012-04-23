<%@ Assembly Name="IUDICO.CourseManagement" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<IUDICO.CourseManagement.Models.ViewCourseModel>>" %>

<asp:Content ID="Content0" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {

            $('#Courses').dataTable({
                "bJQueryUI": true,
                "sPaginationType": "full_numbers",
                iDisplayLength: 50,
                "bSort": true,
                "aoColumns": [
                { "bSortable": false },
                null,
                null,
                null,
                { "bSortable": false }
                ]
            });

            $("input[id$='CheckAll']").click(function () {
                var $boxes = $("#" + this.id.replace("CheckAll", "")).find(":checkbox");
                if(this.checked) {
                    $boxes.attr("checked", "checked");
                } else {
                    $boxes.removeAttr("checked");
                }
            });

            $("#DeleteMany").click(function () {
                var ids = $("td input:checked:not(id$='CheckAll')").map(function () {
                    return $(this).attr('id');
                });

                if (ids.length == 0) {
                    alert("<%=IUDICO.CourseManagement.Localization.getMessage("PleaseSelectCoursesDelete") %>");
                    
                    return false;
                }

                var answer = confirm("<%=IUDICO.CourseManagement.Localization.getMessage("AreYouSureYouWantDelete") %>" + ids.length + "<%=IUDICO.CourseManagement.Localization.getMessage("selectedCourses") %>");

                if (answer == false) {
                    return false;
                }

                $.ajax({
                    type: "post",
                    url: "/Course/Delete",
                    data: { courseIds: ids },
                    success: function (r) {
                        if (r.success) {
                            $("td input:checked").parents("tr").remove();
                        }
                        else {
                            alert("<%=IUDICO.CourseManagement.Localization.getMessage("ErrorOccuredDuringProccessingRequest") %>");
                        }
                    }
                });
            });

            $("#shareDialog").dialog({
               autoOpen: false,
               modal: true,
               width: 450,
               buttons: {
                   "Share": function () {
                       $("#shareDialog").find("form").submit();
                   },
                   "Close": function () {
                       $(this).dialog("close");
                   }
               }
            });

            $("#dialog").dialog({
               autoOpen: false,
               modal: true,
               buttons: {
                   "Submit": function () {
                       $("#dialog").find("form").submit();
                   },
                   "Close": function () {
                       $(this).dialog("close");
                   }
               }
            });

            $(".courseEditable").click(function () {
                window.location.replace("/Course/" + this.parentNode.id.replace("course", "") + "/Node/Index");
            });
        });
        
        function removeRow(data) {
            window.location = window.location;
        }
        
        function openDialog(title) {
            $("#dialogInner").html("Loading...");
            $("#dialog").dialog("option", "title", title);
            $("#dialog").dialog("open");
        }
        
        function fillDialogInner(html, itemName, itemId) {
            $("#dialogInner").html(html);
            $('<input />').attr('type', 'hidden')
                .attr('name', itemName)
                .attr('value', itemId)
                .appendTo('#dialogInner > form');
        }
        
        function shareCourse(courseId) {
            
            $("#shareDialogInner").html("Loading...");
            $("#shareDialog").dialog("open");

            $.ajax({
               type: "get",
               url: "/Course/Share",
               data: { courseId : courseId },
               success: function (r) {
                   $("#shareDialogInner").html(r);

                   var table = $("#shareUserTable").dataTable({
                       "bJQueryUI": true,
                       "sPaginationType": "full_numbers",
                       iDisplayLength: 8,
                       "bSort": true,
                       "aoColumns": [
                           { "bSortable": false },
                           { "bSortable": false },
                           null
                       ]
                   });

                   //submit rows not visible in table(on other pages)
                   table.closest("form").submit(function() {
                       var hiddenRows = table.fnGetHiddenNodes();
                       $(hiddenRows).css('display', 'none');
                       table.find("tbody").append(hiddenRows);
                   });

                   $('<input />').attr('type', 'hidden')
                       .attr('name', "courseId")
                       .attr('value', courseId)
                       .appendTo('#shareDialog > form');
               }
            });
        }

        function addCourse() {
            openDialog("Create course");

            $.ajax({
                type: "get",
                url: "/Course/Create",
                success: function(r) {
                    $("#dialogInner").html(r);
                }
            });
        }
        
        function editCourse(id) {
            openDialog("Edit course");

            $.ajax({
                type: "get",
                url: "/Course/" + id + "/Edit",
                success: function(r) {
                    $("#dialogInner").html(r);
                }
            });
        }        
        
        function onShareCourseSuccess(r) {
            var resp = eval("(" + r.$2._xmlHttpRequest.responseText + ")");
            if(resp.success) {
                $("#shareDialog").dialog("close");
            } else {
                alert("error");
            }
        }
        
        function onCreateCourseSuccess(r) {
            var resp = eval("(" + r.$2._xmlHttpRequest.responseText + ")");
            if(resp.success) {
                $(".course").after(resp.courseRow);
                $("#dialog").dialog("close");                
            } else {
                fillDialogInner(resp.html, "courseId", resp.courseId);
            }
        }

        function onEditCourseSuccess(r) {
            var resp = eval("(" + r.$2._xmlHttpRequest.responseText + ")");
            if(resp.success) {
                $("#course" + resp.courseId).replaceWith(resp.courseRow);
                $("#dialog").dialog("close");
            } else {
                fillDialogInner(resp.html, "courseId", resp.courseId);
            }
        }

        function onFailure(r) {
            alert("error");
        }
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=IUDICO.CourseManagement.Localization.getMessage("Courses")%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        <a href="#" onclick="addCourse();"><%:IUDICO.CourseManagement.Localization.getMessage("CreateNew")%></a>
        |
        <%: Html.ActionLink(IUDICO.CourseManagement.Localization.getMessage("Import"), "Import")%>
        | <a id="DeleteMany" href="#">
            <%=IUDICO.CourseManagement.Localization.getMessage("DeleteSelected")%></a>
    </p>
    <div style="float: inherit; width: 400px;">
        <h2>
            <%=IUDICO.CourseManagement.Localization.getMessage("Allcourses")%>:</h2>
    </div>
    <div>
        <% if (Model.Count() > 0)
           { %>
        <table id="Courses" class="courseTable">
            <thead>
                <tr>
                    <th>
                        <input type="checkbox" id="CoursesCheckAll" />
                    </th>
                    <th>
                        <%=IUDICO.CourseManagement.Localization.getMessage("Title")%>
                    </th>
                    <th>
                        <%=IUDICO.CourseManagement.Localization.getMessage("Owner")%>
                    </th>
                    <th>
                        <%=IUDICO.CourseManagement.Localization.getMessage("Last modified")%>
                    </th>
                    <th>
                        <%=IUDICO.CourseManagement.Localization.getMessage("Actions")%>
                    </th>
                </tr>
            </thead>
            <tbody>
                <% foreach (var item in Model) { %>
                        <% Html.RenderPartial("CourseRow", item); %>
                <% } %>
            </tbody>
        </table>
        <% }
           else
           {%>
            <%=IUDICO.CourseManagement.Localization.getMessage("NoCourses")%>
        <% } %>
    </div>
    <div id="shareDialog">
        <% using (Ajax.BeginForm("Share", "Course", new { }, new AjaxOptions() { OnFailure = "onFailure", OnSuccess = "onShareCourseSuccess" }))
           {%>
        <div id="shareDialogInner">
        </div>
        <% } %>
    </div>
    <div id="dialog">
        <div id="dialogInner"></div>
    </div>
</asp:Content>

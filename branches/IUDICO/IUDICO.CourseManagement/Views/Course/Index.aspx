<%@ Assembly Name="IUDICO.CourseManagement" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<IUDICO.CourseManagement.Models.ViewCourseModel>>" %>
<%@ Import Namespace="IUDICO.Common" %>
<%@ Import Namespace="System.Web.Mvc.Ajax" %>

<asp:Content ID="Content0" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {

				$('#Courses').dataTable({
                "bJQueryUI": true,
                "sPaginationType": "full_numbers",
                iDisplayLength: 50,
                "bSort": true,
                "bAutoWidth": false,
                "aoColumns": [
                { "bSortable": false },
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

				$("td input:checkbox:not(id$='CheckAll')").click(function(){
					if($("td input:checkbox:not(id$='CheckAll')").length == $("td input:checked:not(id$='CheckAll')").length)	{
						$('input[id$="CheckAll"]').attr('checked', true);
					}else {
						$('input[id$="CheckAll"]').attr('checked', false);
					}
				});

				$("#DeleteMany").click(function () {
                var ids = $("td input:checked:not(id$='CheckAll')").map(function () {
                    return $(this).attr('id');
                });

                if (ids.length == 0) {
                    alert("<%=Localization.GetMessage("PleaseSelectCoursesDelete") %>");
                    
                    return false;
                }

                var answer = confirm("<%=Localization.GetMessage("AreYouSureYouWantDelete") %>" + ids.length + "<%=Localization.GetMessage("selectedCourses") %>");

                if (answer == false) {
                    return false;
                }

					 $.ajax({
                    type: "POST",
						  url: "/Course/Delete",
						  traditional: true,
                    data: {courseIds : ids.toArray()},
                    success: function (r) {
                        if (r.success) {
                            $("td input:checked").parents("tr").remove();
                        }
                        else {
                            alert("<%=Localization.GetMessage("ErrorOccuredDuringProccessingRequest") %>");
                        }
                    }
                });
            });


            $(".courseEditable").click(function () {
                window.location.replace("/Course/" + this.parentNode.id.replace("course", "") + "/Node");
            });
        });
        
        function removeRow(data) {
            window.location = window.location;
        }
        
        
        function shareCourse(courseId) {
            openShareDialog("/Course/Share", { courseId: courseId }, "onShareCourseSuccess");
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
            openDialog("<%=Localization.GetMessage("EditCourse") %>");

            $.ajax({
                type: "get",
                url: "/Course/" + id + "/Edit",
                success: function(r) {
                    fillDialogInner(r, "courseId", id);
                }
            });
        }        
        
        function onShareCourseSuccess(resp) {
            if(resp.success) {
                $("#shareDialog").dialog("close");
            } else {
                alert("error");
            }
        }
        
        function onCreateCourseSuccess(resp) {
            if(resp.success) {
                //$(".course").after(resp.courseRow);
                $("#dialog").dialog("close");
                window.location.reload( true ); // TODO : fix adding course if table is empty.
            } else {
                fillDialogInner(resp.html, "courseId", resp.courseId);
            }
        }

        function onEditCourseSuccess(resp) {
            if(resp.success) {
                $("#course" + resp.courseId).replaceWith(resp.courseRow);
                $("#dialog").dialog("close");
            } else {
                fillDialogInner(resp.html, "courseId", resp.courseId);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=Localization.GetMessage("Courses")%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%=Localization.GetMessage("Courses")%>:</h2>
    
    <p>
        <a href="#" onclick="addCourse();"><%: Localization.GetMessage("CreateNew")%></a>
        |
        <a  href="/Course/Import" ><%: Localization.GetMessage("Import")%></a>
        |
        <a id="DeleteMany" href="#"><%: Localization.GetMessage("DeleteSelected")%></a>
    </p>
    <div>
        <% if (Model.Count() > 0)
           { %>
        <table id="Courses" class="courseTable">
            <thead>
                <tr>
                    <th class="checkboxColumn">
                        <input type="checkbox" id="CoursesCheckAll" />
                    </th>
                    <th>
                        <%=Localization.GetMessage("Title")%>
                    </th>
                    <th class="updatedByColumn">
                        <%=Localization.GetMessage("LastModified")%>
                    </th>
                    <th class="courseActionsColumn">
                        <%=Localization.GetMessage("Actions")%>
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
            <%=Localization.GetMessage("NoCourses")%>
        <% } %>
    </div>
</asp:Content>

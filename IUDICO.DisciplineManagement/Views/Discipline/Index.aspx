<%@ Assembly Name="IUDICO.DisciplineManagement" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<IUDICO.DisciplineManagement.Models.ViewDataClasses.ViewDisciplineModel>>" %>
<%@ Import Namespace="IUDICO.Common" %>

<asp:Content ID="Content0" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" language="javascript">
        var loadingRow = "<tr id='loadingRow'><td></td><td><img src='/Content/Images/wait.gif'/> </td><td></td><td></td><td></td></tr>";
        
        $(document).ready(function () {
            
            $(".disciplineName").click(function() {
                expandDiscipline(this.parentNode);
            });

            $('#disciplines').dataTable({
                "bJQueryUI": true,
                "sPaginationType": "full_numbers",
                iDisplayLength: 200,
                "bSort": false,
                "bPaginate": false,
                "bLengthChange": false,
                "bFilter": false,
                "bAutoWidth": false,
                "aoColumns": [
                { "bSortable": false },
                { "bSortable": false },
                { "bSortable": false },
                { "bSortable": false },
                { "bSortable": false }
                ]
            });
            
            $("#disciplines").treeTable({
                clickableNodeNames: true,
                indent: 0,
                initialState: "collapsed"
            });
            
            $("#DeleteMany").click(function () {
                var ids = $("td input:checked").map(function () {
                    return $(this).attr('id');
                });

                if (ids.length == 0) {
                    alert("<%=Localization.GetMessage("PleaseSelectDisciplinesDelete") %>");
                    return false;
                }

                var answer = confirm("<%=Localization.GetMessage("AreYouSureYouWantDeleteSelectedDisciplines") %>");

                if (answer == false) {
                    return false;
                }

                $.ajax({
                    type: "post",
                    url: "/Discipline/DeleteItems",
                    data: { disciplineIds: ids },
                    success: function (r) {
    		                if (r.success == true) {
    		                    $("td input:checked").parents("tr").remove();
    		                }
    		                else {
    		                    alert("<%=Localization.GetMessage("ErrorOccuredDuringProcessingRequestErrorMessage") %> " + r.message);
    		                }
                    }
                });
            });

        });

        function onCreateTopicSuccess(resp) {
            if(resp.success) {
               $("#chapter" + resp.chapterId).add($(".child-of-chapter" + resp.chapterId)).last().after(resp.topicRow);
                $("#dialog").dialog("close");
            } else {
                fillDialogInner(resp.html, "ChapterId", resp.chapterId);
            }
        }
        
        function onCreateChapterSuccess(resp) {
            if(resp.success) {
                
                var $last = $("#discipline" + resp.disciplineId);
                var $chapters = $(".child-of-discipline" + resp.disciplineId);
                if($chapters.length > 0 ) {
                    var lastId = $chapters.last().attr("id").replace("chapter", "");
                    var $topics = $(".child-of-chapter" + lastId);
                    
                    if($topics.length > 0) {
                        $last = $topics.last();
                    } else {
                        $last = $chapters.last();
                    }
                }
                
                $last.after(resp.chapterRow);
                
                $(".disciplineChapterName").click(function() {
                    expandChapter(this.parentNode);
                });
                $("#dialog").dialog("close");
            } else {
                fillDialogInner(resp.html, "DisciplineRef", resp.disciplineId);
            }
        }
        
        function onEditTopicSuccess(resp) {
            if(resp.success) {
                $("#topic" + resp.topicId).replaceWith(resp.topicRow);
            	 $("#error" + resp.disciplineId).text(resp.error);
                $("#dialog").dialog("close");
            } else {
                fillDialogInner(resp.html, "topicId", resp.topicId);
            }
        }
        
        function onEditChapterSuccess(resp) {
            if(resp.success) {
                $("#chapter" + resp.chapterId).replaceWith(resp.chapterRow);
                $("#dialog").dialog("close");
            } else {
                fillDialogInner(resp.html, "chapterId", resp.chapterId);
            }
        }
        

        function rowClick(rowObj, idPrefix, url, postLoadFunction) {
            var $row = $(rowObj);
            var parentId = $row.attr("id").replace(idPrefix, "");
                
            
            var $icon = $row.find("div").first();
            
            if($icon.hasClass("collapsedIcon")) {
                $icon.removeClass("collapsedIcon").addClass("expandedIcon");
                $row.expand();
            } else {
                $icon.removeClass("expandedIcon").addClass("collapsedIcon");
                $row.collapse();
            }
            
            if(! $row.hasClass("visited")) {
                $row.after(loadingRow);
                $.ajax({
                    type: "post",
                    url: url,
                    data: {parentId : parentId },
                    success: function (r) {
                        if(r.success) {
                            $("#loadingRow").remove();
                                
                            for(var i = r.items.length - 1; i >= 0; i--) {
                                $row.after(r.items[i]);
                            }

                            postLoadFunction();
                        } else {
                            alert('error');
                        }
                    },
                    async: false
                });
                    
                $row.addClass("visited");
            }

        }
        
        function expandRow( id, expandFunction) {
            var $row = $("#" + id);
            if(!$row.hasClass("expanded")) {
                if($row.hasClass("visisted")) {
                    $row.expand();    
                } else {
                    expandFunction($row);
                }
            }
        }
        
        function expandChapter(itemObj) {
            rowClick(itemObj, "chapter", "/TopicAction/ViewTopics", function () { });
        }
        
        function expandDiscipline(itemObj) {
            rowClick(itemObj, "discipline", "/ChapterAction/ViewChapters", function () {
                var selector = ".child-of-" + $(itemObj).attr("id");

                $(selector).find(".disciplineChapterName").each(function(i, val) {
                    $(this).prepend('<span class="disciplineChapterIndex"><%: Localization.GetMessage("Chapter") %> ' + (i + 1) + '.</span>');
                });
                $(".disciplineChapterName").click(function() {
                    expandChapter(this.parentNode);
                });
            });
        }
        
        
        function deleteDiscipline(id) {
            var answer = confirm("<%=Localization.GetMessage("AreYouSureYouWantDeleteSelectedDiscipline") %>");

            if (answer == false) {
                return;
            }

            $.ajax({
                type: "post",
                url: "/Discipline/DeleteItem",
                data: { disciplineId: id },
                success: function (r) {
                    if (r.success == true) {
    		                $("#discipline" + id).remove();
    		                $(".child-of-discipline" + id).remove();
                    }
                    else {
    		                alert("<%=Localization.GetMessage("ErrorOccuredDuringProcessingRequestErrorMessage") %> " + r.message);
                    }
                }
            });
        }
        
        function deleteChapter(id) {
            var answer = confirm("<%=Localization.GetMessage("AreYouSureYouWantDeleteSelectedChapter") %>");

            if (answer == false) {
                return;
            }

            $.ajax({
                type: "post",
                url: "/ChapterAction/DeleteItem",
                data: { chapterId: id },
                success: function (r) {
                    if (r.success == true) {
    		                $("#chapter" + id).remove();
    		                $(".child-of-chapter" + id).remove();
                    }
                    else {
    		                alert("<%=Localization.GetMessage("ErrorOccuredDuringProcessingRequestErrorMessage") %> " + r.message);
                    }
                }
            });
        }
    	
        function deleteTopic(id) {
            var answer = confirm("<%=Localization.GetMessage("AreYouSureYouWantDeleteSelectedTopic") %>");

            if (answer == false) {
                return;
            }

            $.ajax({
                type: "post",
                url: "/TopicAction/DeleteItem",
                data: { topicId: id },
                success: function (r) {
                if (r.success == true) {
    		            $("#topic" + id).remove();
                }
                else {
    		            alert("<%=Localization.GetMessage("ErrorOccuredDuringProcessingRequestErrorMessage") %> " + r.message);
                }
                }
            });
        }
        
        function addTopic(chapterId) {
            openDialog("<%=Localization.GetMessage("CreateTopic") %>");

            expandRow("chapter" + chapterId, expandChapter);
            
            $.ajax({
               type: "get",
               url: "/TopicAction/Create",
               data: { chapterId: chapterId },
               success: function (r) {
                   fillDialogInner(r, "ChapterId", chapterId);
               }
            });
        }
        
        function addChapter(disciplineId) {
            openDialog("<%=Localization.GetMessage("CreateChapter") %>");
            
            expandRow("discipline" + disciplineId, expandDiscipline);

            $.ajax({
               type: "get",
               url: "/ChapterAction/Create",
               data: { disciplineId: disciplineId },
               success: function (r) {
                   fillDialogInner(r, "DisciplineRef", disciplineId);
               }
            });
        }

        function editTopic(topicId) {
            openDialog("<%=Localization.GetMessage("EditTopic") %>");

            $.ajax({
               type: "get",
               url: "/TopicAction/Edit",
               data: { topicId: topicId },
               success: function (r) {
                   fillDialogInner(r, "topicId", topicId);
               }
            });
        }

        function editChapter(chapterId) {
            openDialog("<%=Localization.GetMessage("EditChapter") %>");

            $.ajax({
               type: "get",
               url: "/ChapterAction/Edit",
               data: { chapterId: chapterId },
               success: function (r) {
                   fillDialogInner(r, "chapterId", chapterId);
               }
            });
        }
                
        function moveTopicUp(topicId) {
            $.ajax({
               type: "post",
               url: "/TopicAction/TopicUp",
               data: { topicId: topicId },
               success: function (r) {
                   if(r.success) {
                       var $row = $("#topic" + topicId);
                       var $prev = $row.prev();
                       
                       if($prev.hasClass("disciplineTopic")) {
                           $prev.before($row);
                       }
                   } else {
                       alert(r.message);
                   }
               }
            });
        }
        
        function moveTopicDown(topicId) {
           $.ajax({
               type: "post",
               url: "/TopicAction/TopicDown",
               data: { topicId: topicId },
               success: function (r) {
                   if(r.success) {
                       var $row = $("#topic" + topicId);
                       var $next = $row.next();
                       
                       if($next.hasClass("disciplineTopic")) {
                           $next.after($row);
                       }
                   } else {
                       alert(r.message);
                   }
               }
            });
        }
        
        function shareDiscipline(disciplineId) {
            openShareDialog("/Discipline/Share", {disciplineId: disciplineId}, "onShareDisciplineSuccess");
        }
        
        function onShareDisciplineSuccess(resp) {
            if(resp.success) {
                $("#shareDialog").dialog("close");
            } else {
                alert(r.message);
            }
        }
    </script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=Localization.GetMessage("Disciplines")%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	
	<script src="<%= Html.ResolveUrl("~/Scripts/treetable/javascripts/jquery.treeTable.js")%>" type="text/javascript"></script>
	<link href="<%=  Html.ResolveUrl("~/Scripts/treetable/stylesheets/jquery.treeTable.css")%>" rel="stylesheet" type="text/css" />
	
    <h2>
        <%=Localization.GetMessage("Disciplines")%></h2>
    <div>
        <%: Html.ActionLink(Localization.GetMessage("CreateNew"), "Create") %>
        |
        <% Html.RenderPartial("Import"); %>
        |
        <a id="DeleteMany" href="#" style="display:none"><%=Localization.GetMessage("DeleteSelected")%></a>
    </div>
	  <p></p>
	<table id="disciplines" class="disciplineTable">
    <thead>
		<tr id="tableHeader">
		    <th class="checkboxColumn"></th>
			<th class="disciplineNameColumn"><%=Localization.GetMessage("Name") %></th>
			<th class="disciplineDateColumn" class="datetimeColumn"><%=Localization.GetMessage("Created") %></th>
			<th class="disciplineDateColumn"  class="datetimeColumn"><%=Localization.GetMessage("Updated") %></th>
			<th class="disciplineActionColumn" class="actionsColumn"></th>
		</tr>
    </thead>
    <tbody>
		<% foreach (var item in Model){ %>
			<tr id="discipline<%:item.Discipline.Id%>" class="discipline" >
			    <td><div class="collapsedIcon"></div></td>
				<td class="disciplineName">	<%:item.Discipline.Name %>				</td>
				<td>	<%: DateFormatConverter.DataConvert(item.Discipline.Created) %>		</td>
				<td>	<%: DateFormatConverter.DataConvert(item.Discipline.Updated)%>		</td>
				<td>
					<div style="width: 75%; float: left">
						<a href="#" onclick="addChapter(<%: item.Discipline.Id %>);"><%=Localization.GetMessage("AddChapter")%></a>
						|
							<%: Html.ActionLink(Localization.GetMessage("Edit"), "Edit", new { DisciplineID = item.Discipline.Id })%>
						| 
							<a href="#" onclick="shareDiscipline(<%: item.Discipline.Id %>)"><%=Localization.GetMessage("Share")%></a>
						|
							<%: Html.ActionLink(Localization.GetMessage("Export"), "Export", new { DisciplineID = item.Discipline.Id })%>
						|
						<a href="#" onclick="deleteDiscipline(<%: item.Discipline.Id %>)"><%=Localization.GetMessage("Delete")%></a>
					</div>
					<div style="width: 23%; float: right">
						<span id="error<%:item.Discipline.Id%>" style="color: red; float: right"><%:item.Error%></span>
					</div>
				</td>
			</tr>
		<% } %>
        </tbody>
	</table>

</asp:Content>
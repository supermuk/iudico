<%@ Assembly Name="IUDICO.DisciplineManagement" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<IUDICO.Common.Models.Shared.Discipline>>" %>
<%@ Import Namespace="IUDICO.Common.Models.Shared" %>

<asp:Content ID="Content0" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" language="javascript">
        var loadingRow = "<tr id='loadingRow'><td></td><td>Loading...</td><td></td><td></td><td></td></tr>";
        
        $(document).ready(function () {
            
            $(".disciplineName").click(function() {
                expandDiscipline(this.parentNode);
            });

            $("#dialog").dialog({
               autoOpen: false,
               modal: true,
               buttons: {
                'Submit': function() {
                   $("#dialogInner").find("form").submit();
                },
                'Cancel': function() {
                  $(this).dialog('close');
                }
              }                                
            });
           
            
            $('#disciplines').dataTable({
                "bJQueryUI": true,
                "sPaginationType": "full_numbers",
                iDisplayLength: 200,
                "bSort": false,
                "bPaginate": false,
                "bLengthChange": false,
                "bFilter": false,                
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
    				alert("<%=IUDICO.DisciplineManagement.Localization.getMessage("PleaseSelectDisciplinesDelete") %>");

    				return false;
    			}

    			var answer = confirm("<%=IUDICO.DisciplineManagement.Localization.getMessage("AreYouSureYouWantDeleteSelectedDisciplines") %>");

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
    						alert("<%=IUDICO.DisciplineManagement.Localization.getMessage("ErrorOccuredDuringProcessingRequestErrorMessage") %> " + r.message);
    					}
    				}
    			});
    		});
    	});
        
        function onFailure() {
            alert('error');
        }
        
        function onCreateTopicSuccess(r) {
            var resp = eval("(" + r.$2._xmlHttpRequest.response + ")");
            if(resp.success) {
               $("#chapter" + resp.chapterId).add($(".child-of-chapter" + resp.chapterId)).last().after(resp.topicRow);
                $("#dialog").dialog("close");
            } else {
                fillDialogInner(resp.html, "ChapterId", resp.chapterId);
            }
        }
        
        function onCreateChapterSuccess(r) {
            var resp = eval("(" + r.$2._xmlHttpRequest.response + ")");
            if(resp.success) {
                $("#discipline" + resp.disciplineId).add($(".child-of-discipline" + resp.disciplineId)).last().after(resp.chapterRow);
                $(".disciplineChapterName").click(function() {
                    expandChapter(this.parentNode);
                });
                $("#dialog").dialog("close");
            } else {
                fillDialogInner(resp.html, "DisciplineRef", resp.disciplineId);
            }
        }
        
        function onEditTopicSuccess(r) {
            var resp = eval("(" + r.$2._xmlHttpRequest.response + ")");
            if(resp.success) {
                $("#topic" + resp.topicId).replaceWith(resp.topicRow);
                $("#dialog").dialog("close");
            } else {
                fillDialogInner(resp.html, "topicId", resp.topicId);
            }
        }
        
        function onEditChapterSuccess(r) {
            var resp = eval("(" + r.$2._xmlHttpRequest.response + ")");
            if(resp.success) {
                $("#chapter" + resp.chapterId).replaceWith(resp.chapterRow);
                $("#dialog").dialog("close");
            } else {
                fillDialogInner(resp.html, "chapterId", resp.chapterId);
            }
        }
        
        function fillDialogInner(html, itemName, itemId) {
            $("#dialogInner").html(html);
            $('<input />').attr('type', 'hidden')
                .attr('name', itemName)
                .attr('value', itemId)
                .appendTo('#dialogInner > form');
        }
        
        function openDialog(title) {
            $("#dialogInner").html("Loading...");
            $("#dialog").dialog("option", "title", title);
            $("#dialog").dialog("open");
        }
        
        function rowClick(rowObj, idPrefix, url, postLoadFunction) {
            var $row = $(rowObj);
            var parentId = $row.attr("id").replace(idPrefix, "");
                
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
                    }
                });
                    
                $row.addClass("visited");
            }
                
            $row.toggleBranch();
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
                $(".disciplineChapterName").click(function() {
                    expandChapter(this.parentNode);
                });
            });
        }
        
        
        function deleteDsicipline(id) {
    		var answer = confirm("<%=IUDICO.DisciplineManagement.Localization.getMessage("AreYouSureYouWantDeleteSelectedDiscipline") %>");

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
    					alert("<%=IUDICO.DisciplineManagement.Localization.getMessage("ErrorOccuredDuringProcessingRequestErrorMessage") %> " + r.message);
    				}
    			}
    		});
    	}
        
       function deleteChapter(id) {
    		var answer = confirm("<%=IUDICO.DisciplineManagement.Localization.getMessage("AreYouSureYouWantDeleteSelectedChapter") %>");

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
    					alert("<%=IUDICO.DisciplineManagement.Localization.getMessage("ErrorOccuredDuringProcessingRequestErrorMessage") %> " + r.message);
    				}
    			}
    		});
    	}
    	
       function deleteTopic(id) {
    		var answer = confirm("<%=IUDICO.DisciplineManagement.Localization.getMessage("AreYouSureYouWantDeleteSelectedChapter") %>");

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
    					alert("<%=IUDICO.DisciplineManagement.Localization.getMessage("ErrorOccuredDuringProcessingRequestErrorMessage") %> " + r.message);
    				}
    			}
    		});
    	}
        function addTopic(chapterId) {
            openDialog("Create topic");

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
            openDialog("Create chapter");
            
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
            openDialog("Edit topic");

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
            openDialog("Edit chapter");

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
    </script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=IUDICO.DisciplineManagement.Localization.getMessage("Disciplines")%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	
	<script src="<%= Html.ResolveUrl("~/Scripts/treetable/javascripts/jquery.treeTable.js")%>" type="text/javascript"></script>
	<link href="<%=  Html.ResolveUrl("~/Scripts/treetable/stylesheets/jquery.treeTable.css")%>" rel="stylesheet" type="text/css" />
	
    <h2>
        <%=IUDICO.DisciplineManagement.Localization.getMessage("Disciplines")%></h2>
    <p>
        <%: Html.ActionLink(IUDICO.DisciplineManagement.Localization.getMessage("CreateNew"), "Create") %>
        <a id="DeleteMany" href="#"><%=IUDICO.DisciplineManagement.Localization.getMessage("DeleteSelected")%></a>
    </p>
	
	<table id="disciplines" class="disciplineTable">
    <thead>
		<tr id="tableHeader">
		     <th></th>
			<th class="disciplineNameColumn">Назва</th>
			<th class="disciplineDateColumn">Створено</th>
			<th class="disciplineDateColumn">Оновлено</th>
			<th></th>
		</tr>
    </thead>
    <tbody>
		<% foreach (var item in Model){ %>
			<tr id="discipline<%:item.Id%>" class="discipline" >
			    <td></td>
				<td class="disciplineName">	<%:item.Name %>				</td>
				<td>	<%: String.Format("{0:g}", item.Created) %>		</td>
				<td>	<%: String.Format("{0:g}", item.Updated) %>		</td>
				<td>
						<a href="#" onclick="addChapter(<%: item.Id %>);"><%=IUDICO.DisciplineManagement.Localization.getMessage("Add")%></a>
                        |
						<%: Html.ActionLink(IUDICO.DisciplineManagement.Localization.getMessage("Edit"), "Edit", new { DisciplineID = item.Id })%>
                        |
						<a href="#" onclick="deleteDsicipline(<%: item.Id %>)"><%=IUDICO.DisciplineManagement.Localization.getMessage("Delete")%></a>
				</td>
			</tr>
		<% } %>
        </tbody>
	</table>


    <div id="dialog"><div id="dialogInner"></div></div>
</asp:Content>
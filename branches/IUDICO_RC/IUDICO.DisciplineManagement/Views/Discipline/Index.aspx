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

				 $("#dialog").dialog({
					  autoOpen: false,
					  modal: true,
//					  buttons: [
//							{
//								 text: submitName,
//								 click: function() {
//									  var $form = $("#dialog").find("form");

//									  $.ajax({
//											type: "post",
//											url: $form.attr("action"),
//											data: $form.serialize(),
//											success: function(r) {
//												 window[$form.attr("data-onSuccess")](r);
//											},
//											error: function(r) {
//												 window[$form.attr("data-onFailure")](r);
//											}
//									  });
//								 },
//								 'class': "blueButton",
//								 id: "DialogSubmitButton"
//							},
//							{
//								 text: cancelName,
//								 click: function() {
//									  $(this).dialog("close");
//								 },
//								 'class': "blueButton"
//							}
//					  ]
				 });


        }) ;

        function onCreateDisciplineSuccess(resp) {
            if(resp.success) {
                $("#disciplines > tbody").append(resp.disciplineRow);
                $(".disciplineName").click(function() {
                    expandDiscipline(this.parentNode);
                });
                $("#dialog").dialog("close");
            } else {
                fillDialogInner(resp.html, "", 0);
            }
        }

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
                var disciplineId = "discipline" + resp.disciplineId;
                var $last = $("#" + disciplineId);
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
                setChapterIndex(disciplineId);
                $(".disciplineChapterName").click(function() {
                    expandChapter(this.parentNode);
                });
                $("#dialog").dialog("close");
            } else {
                fillDialogInner(resp.html, "DisciplineRef", resp.disciplineId);
            }
        }
        
        function onEditDisciplineSuccess(resp) {
            if(resp.success) {
                var newRow = $(resp.disciplineRow);
                newRow.find(".disciplineName")
                    .click(function() {
                        expandDiscipline(this.parentNode);
                    });
                $("#discipline" + resp.disciplineId).replaceWith(newRow);
                $("#dialog").dialog("close");
            } else {
                fillDialogInner(resp.html, "disciplineId", resp.disciplineId);
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
                var newRow = $(resp.chapterRow);
                newRow.find(".disciplineChapterName")
                    .click(function() {
                        expandChapter(this.parentNode);
                    });
                $("#chapter" + resp.chapterId).replaceWith(newRow);
                
                var disciplineId = newRow.prevAll('.discipline:first').attr('id');
                setChapterIndex(disciplineId);
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
        
        function expandRow(id, expandFunction) {
            var $row = $("#" + id);
            if(!$row.hasClass("expanded")) {
                if($row.hasClass("visited")) {
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
                setChapterIndex($(itemObj).attr("id"));
                $(".disciplineChapterName").click(function() {
                    expandChapter(this.parentNode);
                });
            });
        }

        /*Add chapter index like 'Chapter1', 'Chapter2'... */
        function setChapterIndex(disciplineId) {
            var selector = ".child-of-" + disciplineId;

            $(selector).find(".disciplineChapterName").each(function(i) {
                $(this).find('span').remove();
                $(this).prepend('<span class="disciplineChapterIndex"><%: Localization.GetMessage("Chapter") %> ' + (i + 1) + '.</span>');
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
        
		  function importDiscipline() {
           // openDialog("<%=Localization.GetMessage("ImportDiscipline") %>");
				    $("#dialog").html('<span class="loading"><img src="/Content/Images/wait.gif"/></span>');
					 $("#dialog").dialog("option", "title", "<%=Localization.GetMessage("ImportDiscipline") %>");
					 $("#dialog").dialog("open");
            $.get(
                "/DisciplineAction/Import",
                function (r) {
                    $("#dialog").html(r);
						  $("#dialog").dialog("open");
                }
            );
        }

        function addDiscipline() {
            openDialog("<%=Localization.GetMessage("CreateDiscipline") %>");

            $.get(
                "/DisciplineAction/Create",
                function (r) {
                    fillDialogInner(r, "", 0);
                }
            );
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
                    fillDialogInner(r, "DisciplineId", disciplineId);
                }
            });
        }

        function editDiscipline(disciplineId) {
            openDialog("<%=Localization.GetMessage("EditDiscipline") %>");

            $.get(
                "/DisciplineAction/Edit",
                { disciplineId: disciplineId },
                function (r) {
                    fillDialogInner(r, "disciplineId", disciplineId);
                }
            );
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
                alert(resp.message);
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
        <a href="#" onclick="addDiscipline();"><%=Localization.GetMessage("CreateNew")%></a>
        |
		  <a id="ImportDiscipline" href="#" onclick="importDiscipline();"><%= Localization.GetMessage("Import") %></a>
        |
        <a id="DeleteMany" href="#" style="display:none"><%=Localization.GetMessage("DeleteSelected")%></a>
    </div>
	  <p></p>
	<table id="disciplines" class="disciplineTable">
    <thead>
		<tr id="tableHeader">
		  <th class="checkboxColumn"></th>
			<th class="disciplineNameColumn"><%=Localization.GetMessage("Name") %></th>
			<th class="disciplineDateColumn"><%=Localization.GetMessage("Created") %></th>
			<th class="disciplineDateColumn"><%=Localization.GetMessage("Updated") %></th>
			<th class="disciplineActionColumn"></th>
		</tr>
    </thead>
    <tbody>
		<% foreach (var item in Model){ %>
        <%Html.RenderPartial("DisciplineRow", item); %>
		<% } %>
        </tbody>
	</table>

</asp:Content>
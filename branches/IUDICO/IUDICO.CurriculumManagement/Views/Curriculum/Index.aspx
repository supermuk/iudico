<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" 
Inherits="System.Web.Mvc.ViewPage<IEnumerable<IUDICO.CurriculumManagement.Models.ViewDataClasses.ViewCurriculumModel>>" %>

<%@ Assembly Name="IUDICO.CurriculumManagement" %>

<%@ Import Namespace="IUDICO.Common" %>
<%@ Import Namespace="IUDICO.CurriculumManagement.Models" %>

<asp:Content ID="Content0" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
	    var loadingRow = "<tr id='loadingRow'><td></td><td><img src='/Content/Images/wait.gif'/> </td><td></td><td></td><td></td></tr>";

	    $(document).ready(function () {

		    $("#errorsDialog").dialog({autoOpen: false, modal: true});
		    setDialogDefaultSettings();

		    $("#warningMessage").click(function(e) {
			    $(this).hide();
		    });

		    $("#curriculumsTable").dataTable({
			    "bJQueryUI": true,
			    "sPaginationType": "full_numbers",
			    iDisplayLength: 200,
			    "bSort": false,
			    "bLengthChange": false,
			    "bFilter": false,
			    "aoColumns": [
				    null,
				    null,
				    null,
				    null,
				    null,
				    null,
				    null
			    ]
		    });

		    $("#dialog").dialog({
			    autoOpen: false,
			    modal: true
		    });

		    $("#curriculumsTable").treeTable({
			    indent: 0,
			    initialState: "collapsed"
		    });

		    $("#DeleteMany").click(function () {
			    var ids = $("td input:checked").map(function () {
				    return $(this).attr('id');
			    });

			    if (ids.length == 0) {
				    alert("<%=Localization.GetMessage("PleaseSelectCurriculumDelete") %>");

				    return false;
			    }

			    var answer = confirm("<%=Localization.GetMessage("AreYouSureYouWantDeleteSelectedCurriculums") %>");

			    if (answer == false) {
				    return false;
			    }

			    $.ajax({
				    type: "post",
				    url: "/Curriculum/DeleteItems",
				    data: { curriculumIds: ids },
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

		    $("#curriculumsTable > tbody tr").click(function(e) {
			    expandCurriculum(this);
			    $("svg").remove();
			    $(".hasSVG").removeClass("hasSVG");
		    });
	    });
	    

	    function CreateCurriculum() {
		    openDialog("<%=Localization.GetMessage("AddCurriculum") %>");

		    $.get(
			    "/Curriculum/Create",
			    function(html) {
				    fillDialogInner(html, "", 0);
			    }
		    );
	    }
	     
	    function OnCurriculumCreate(result) {
		    if (result.success) {
			    var row = $(result.curriculumRow);
			    row.click(function(e) {
				    expandCurriculum(this);
				    $("svg").remove();
				    $(".hasSVG").removeClass("hasSVG");
			    });
			    $("#curriculumsTable > tbody").append(row);
			    $("#dialog").dialog("close");
		    } else {
			    fillDialogInner(result.html, "", 0);
		    }
	    }
		  
	    function CurriculumEditClick(id) {
		    openDialog("<%=Localization.GetMessage("EditCurriculum")%>");

		    $.get(
			    "/Curriculum/Edit",
			    { curriculumId: id },
			    function(res) {
				    fillDialogInner(res, "curriculumId", id);
			    }
		    );
	    }

	    function CurriculumChapterEditClick(id) {
		    openDialog("<%=Localization.GetMessage("EditChapterTimeline")%>");

		    $.get(
			    "/CurriculumChapter/Edit",
			    {curriculumChapterId: id},
			    function(res) {
				    fillDialogInner(res, "curriculumChapterId", id);
			    }
		    );
	    }
	    
		 function CurriculumChapterTopicEditClick(id) {
		    openDialog("<%=Localization.GetMessage("EditTopicAssignment")%>");

		    $.get(
			    "/CurriculumChapterTopic/Edit",
			    {curriculumChapterTopicId: id},
			    function(res) {
				    fillDialogInner(res, "curriculumChapterTopicId", id);
			    }
		    );
	    }

	    function OnCurriculumEdit(result) {
		    if (result.success) {
			    var row = $(result.curriculumRow);
			    row.click(function(e) {
				    expandCurriculum(this);
				    $("svg").remove();
				    $(".hasSVG").removeClass("hasSVG");
			    });
			    var wasExpanded = $("#curriculum" + result.curriculumId).find(".expandedIcon").length != 0;
			    $("#curriculum" + result.curriculumId).removeClass("buged-curriculum");
			    var rowClass = $("#curriculum" + result.curriculumId).attr("class");
			    $("#curriculum" + result.curriculumId).replaceWith(row);
			    if (wasExpanded) $("#curriculum" + result.curriculumId).find(".collapsedIcon").removeClass("collapsedIcon").addClass("expandedIcon");
			    $("#curriculum" + result.curriculumId).attr("class", rowClass);
			    if ($(row).hasClass("buged-curriculum")) {
				     $(".child-of-" + $(row).attr("id")).each(function(i) {
					     $(this).addClass("buged-curriculum");
					     $(".child-of-" + $(this).attr("id")).addClass("buged-curriculum");
				     });
			    } else {
				     $(".child-of-" + $(row).attr("id")).each(function(i) {
					     $(this).removeClass("buged-curriculum");
					     $(".child-of-" + $(this).attr("id")).removeClass("buged-curriculum");
				     });
			    }
			    $("#dialog").dialog("close");
		    } else {
			    fillDialogInner(result.html, "curriculumId", result.curriculumId);
		    }
	    }

	    function OnCurriculumChapterEdit(result) {
		    if (result.success) {
			    var row = $(result.curriculumChapterRow);
			    row.click(function(e) {
				    expandCurriculumChapter(this);
				    $("svg").remove();
				    $(".hasSVG").removeClass("hasSVG");
			    });
			    var wasExpanded = $("#curriculumChapter" + result.curriculumChapterId).find(".expandedIcon").length != 0;
			    var rowClass = $("#curriculumChapter" + result.curriculumChapterId).attr("class");
			    $("#curriculumChapter" + result.curriculumChapterId).replaceWith(row);
			    if(wasExpanded) $("#curriculumChapter" + result.curriculumChapterId).find(".collapsedIcon").removeClass("collapsedIcon").addClass("expandedIcon");
			    $("#curriculumChapter" + result.curriculumChapterId).attr("class", rowClass);
			    if (result.curriculumInfo.IsValid) {
				     $("#curriculum" + result.curriculumInfo.Id).removeClass("buged-curriculum");
				     $("#curriculum" + result.curriculumInfo.Id).find("#showErrorsLink").hide();
				     $(".child-of-curriculum" + result.curriculumInfo.Id).each(function(i) {
					      $(this).removeClass("buged-curriculum");
					      $(".child-of-" + $(this).attr("id")).removeClass("buged-curriculum");
				     });
			    } else {
				     $("#curriculum" + result.curriculumInfo.Id).addClass("buged-curriculum");
				     $("#curriculum" + result.curriculumInfo.Id).find("#showErrorsLink").show();
				     $(".child-of-curriculum" + result.curriculumInfo.Id).each(function(i) {
					      $(this).addClass("buged-curriculum");
					      $(".child-of-" + $(this).attr("id")).addClass("buged-curriculum");
				     });
			    }
			    $("#dialog").dialog("close");
		    } else {
			    fillDialogInner(result.html, "curriculumChapterId", result.curriculumChapterId);
		    }
	    }
	    
		 function OnCurriculumChapterTopicEdit(result) {
		    if (result.success) {
			    var row = $(result.curriculumChapterTopicRow);
			    $("#curriculumChapterTopic" + result.curriculumChapterTopicId).replaceWith(row);
			    if (result.curriculumInfo.IsValid) {
				     $("#curriculum" + result.curriculumInfo.Id).removeClass("buged-curriculum");
				     $("#curriculum" + result.curriculumInfo.Id).find("#showErrorsLink").hide();
				     $(".child-of-curriculum" + result.curriculumInfo.Id).each(function(i) {
					      $(this).removeClass("buged-curriculum");
					      $(".child-of-" + $(this).attr("id")).removeClass("buged-curriculum");
				     });
			    } else {
				     $("#curriculum" + result.curriculumInfo.Id).addClass("buged-curriculum");
				     $("#curriculum" + result.curriculumInfo.Id).find("#showErrorsLink").show();
				     $(".child-of-curriculum" + result.curriculumInfo.Id).each(function(i) {
					      $(this).addClass("buged-curriculum");
					      $(".child-of-" + $(this).attr("id")).addClass("buged-curriculum");
				     });
			    }
			    $("#dialog").dialog("close");
		    } else {
			    fillDialogInner(result.html, "curriculumChapterTopicId", result.curriculumChapterTopicId);
		    }
	    }

		 function RemoveCurriculumChapterTimelines(id) {
			 var answer = confirm("<%=Localization.GetMessage("AreYouSureYouWantDeleteSelectedTimeline") %> ");

		    if (answer == false) {
			    return;
		    }

			 $.ajax({
				    type: "post",
				    url: "/CurriculumChapter/RemoveChapterTimelines",
				    data: { curriculumChapterId: id },
				    success: function (r) {
					    if (r.success == true) {
						    $("#curriculumChapter" + id).find("#removeTimelinesLink").hide();
						    $("#curriculumChapter" + id).find("td:nth-of-type(5)").text("<%=Converter.ToString(null) %>");
						    $("#curriculumChapter" + id).find("td:nth-of-type(6)").text("<%=Converter.ToString(null) %>");
					    }
					    else {
						    alert("<%=Localization.GetMessage("ErrorOccuredDuringProcessingRequestErrorMessage") %> " + r.message);
					    }
				    }
			    });
		 }

		 function encircle(element) {
			  $(element).svg({settings: {class_:'svg'}});
			  var svg = $(element).svg('get');
			  var path = svg.createPath();
			  $(element).find("svg").height($(element).height());
			  $(element).find("svg").width($(element).width() + 4);
			  var width = $(element).find("svg").width();
			  var height = $(element).find("svg").height();
		     svg.path(path.move(0,0).curveC(width/2, 3, width/2, 3, width, 0).close(),{fill: 'none', stroke: 'red', strokeWidth: 2});
			  svg.path(path.move(width, 0).curveC(width - 2, height / 2, width - 2, height / 2, width, height).close(), { fill: 'none', stroke: 'red', strokeWidth: 2 });
			  svg.path(path.move(width, height).curveC(width / 2, height - 3, width / 2, height - 3, 0, height).close(), { fill: 'none', stroke: 'red', strokeWidth: 2 });
			  svg.path(path.move(0, height).curveC(3, height / 2, 3, height / 2, 0, 0).close(), { fill: 'none', stroke: 'red', strokeWidth: 2 });
		 }

		 function expandCurriculum(itemObj) {
		    rowClick(itemObj, "curriculum", "/CurriculumChapter/GetCurriculumChapters", function () {
			    setCurriculumChapterIndex($(itemObj).attr("id"));
			    if ($(itemObj).hasClass("buged-curriculum")) {
				     $(".child-of-" + $(itemObj).attr("id")).addClass("buged-curriculum");
			    }
			    $(".child-of-" + $(itemObj).attr("id")).click(function() {
				    expandCurriculumChapter(this);
			    });
		    });
	    }

	    function expandCurriculumChapter(itemObj) {
		    rowClick(itemObj, "curriculumChapter", "/CurriculumChapterTopic/GetCurriculumChapterTopics", function () {
			    if ($(itemObj).hasClass("buged-curriculum")) {
				     $(".child-of-" + $(itemObj).attr("id")).addClass("buged-curriculum");
			    }
			    setCurriculumChapterTopicIndex($(itemObj).attr("id"));
		    });
	    }
	    
		 function setCurriculumChapterIndex(curriculumId) {
            var selector = ".child-of-" + curriculumId;

            $(selector).find("#chapterIndex").each(function(i) {
                $(this).find('span').remove();
                $(this).append('<span class="disciplineChapterIndex"><%: Localization.GetMessage("Chapter") %> ' + (i + 1) + '.</span>');
            });
        }
		 
		 function setCurriculumChapterTopicIndex(curriculumChapterId) {
            var selector = ".child-of-" + curriculumChapterId;

            $(selector).find("#chapterTopicIndex").each(function(i) {
                $(this).find('span').remove();
                $(this).append('<span class="disciplineChapterIndex"><%: Localization.GetMessage("Topic") %> ' + (i + 1) + '.</span>');
            });
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
        
	    function setDialogDefaultSettings() {
		    $("#errorsDialogInner").html("Loading...");
		    var dialog = $("#errorsDialog");
		    //set to default settings
		    dialog.dialog("option", $.ui.dialog.prototype.options);
		    var settings = {
			    autoOpen: false,
			    modal: true,
			    buttons: {                    
				    'Close': function() {
					    $(this).dialog('close');
				    }
			    }
		    };
		    dialog.dialog("option", settings);
		    dialog.css('overflow', 'hidden');
	    }

	    function openErrorDialog(title, settings) {
		    setDialogDefaultSettings();
		    var dialog = $("#errorsDialog");
		    dialog.dialog("option", "title", title);
		    if(settings) {
			    dialog.dialog("option", settings);
		    }
		    $("#errorsDialog").dialog("open");
	    }

	    function showValidationErrors(id) {
		    $.ajax({
			    type: "post",
			    url: "/Curriculum/"+ id +"/ValidationErrors",
			    success: function(r) {
				    if (r.success) {
//					    $("#tooltipImmitation").text(r.errorsText);
					    for (var i = 0; i < r.errors.length; i++) {
						    switch(r.errors[i].Type) {
						        case (0):
							         encircle($("#curriculum" + id).find("td:nth-of-type(3)"));
							         break;
							     case (1):
								      encircle($("#curriculum" + id).find("td:nth-of-type(4)"));
							         break;
								  case (2):
									   if ($("#curriculum" + id).find(".collapsedIcon").length != 0) {
										    $("#curriculum" + id).click();
									   }
									   while ($("#curriculumChapter" + r.errors[i].ErrorData.CurriculumChapterId).length == 0) {
									   }
									   if (r.errors[i].ErrorData.StartDateErr) {
										    encircle($("#curriculum" + id).find("td:nth-of-type(5)"));
										    encircle($("#curriculumChapter" + r.errors[i].ErrorData.CurriculumChapterId).find("td:nth-of-type(5)"));
									   }
									   if (r.errors[i].ErrorData.EndDateErr) {
										    encircle($("#curriculum" + id).find("td:nth-of-type(6)"));
										    encircle($("#curriculumChapter" + r.errors[i].ErrorData.CurriculumChapterId).find("td:nth-of-type(6)"));
									   }
									   break;
								  case (3):
									   if ($("#curriculum" + id).find(".collapsedIcon").length != 0) {
										    $("#curriculum" + id).click();
									   }
									   while ($("#curriculumChapter" + r.errors[i].ErrorData.CurriculumChapterId).length == 0) {
									   }
									   if ($("#curriculumChapter" + r.errors[i].ErrorData.CurriculumChapterId).find(".collapsedIcon").length != 0) {
										    $("#curriculumChapter" + r.errors[i].ErrorData.CurriculumChapterId).click();
									   }
									   while ($("#curriculumChapterTopic" + r.errors[i].ErrorData.CurriculumChapterTopicId).length == 0) {
									   }
									   if (r.errors[i].ErrorData.StartDateErr) {
										    encircle($("#curriculumChapter" + r.errors[i].ErrorData.CurriculumChapterId).find("td:nth-of-type(5)"));
										    encircle($("#curriculumChapterTopic" + r.errors[i].ErrorData.CurriculumChapterTopicId).find("td:nth-of-type(5)"));
									   }
									   if (r.errors[i].ErrorData.EndDateErr) {
										    encircle($("#curriculumChapter" + r.errors[i].ErrorData.CurriculumChapterId).find("td:nth-of-type(6)"));
										    encircle($("#curriculumChapterTopic" + r.errors[i].ErrorData.CurriculumChapterTopicId).find("td:nth-of-type(6)"));
									   }
									   break;
								  case (4):
									   if ($("#curriculum" + id).find(".collapsedIcon").length != 0) {
										    $("#curriculum" + id).click();
									   }
									   while ($("#curriculumChapter" + r.errors[i].ErrorData.CurriculumChapterId).length == 0) {
									   }
									   if ($("#curriculumChapter" + r.errors[i].ErrorData.CurriculumChapterId).find(".collapsedIcon").length != 0) {
										    $("#curriculumChapter" + r.errors[i].ErrorData.CurriculumChapterId).click();
									   }
									   while ($("#curriculumChapterTopic" + r.errors[i].ErrorData.CurriculumChapterTopicId).length == 0) {
									   }
									   if (r.errors[i].ErrorData.StartDateErr) {
										    encircle($("#curriculum" + id).find("td:nth-of-type(5)"));
										    encircle($("#curriculumChapterTopic" + r.errors[i].ErrorData.CurriculumChapterTopicId).find("td:nth-of-type(5)"));
									   }
									   if (r.errors[i].ErrorData.EndDateErr) {
										    encircle($("#curriculum" + id).find("td:nth-of-type(6)"));
										    encircle($("#curriculumChapterTopic" + r.errors[i].ErrorData.CurriculumChapterTopicId).find("td:nth-of-type(6)"));
									   }
									   break;
						    }
					    }
					    $("*").click(function(e) {
						    $("svg").remove();
						    $(".hasSVG").removeClass("hasSVG");
					    });
				    } else {
					    alert(r.error);
				    }
			    },
			    error: function(xhr, ajaxOptions, thrownError) {
				    alert(xhr.status);
				    alert(thrownError);
			    }
		    });
	    }
	    function deleteItem(id) {
		    var answer = confirm("<%=Localization.GetMessage("AreYouSureYouWantDeleteSelectedCurriculum") %> ");

		    if (answer == false) {
			    return;
		    }

		    $.ajax({
			    type: "post",
			    url: "/Curriculum/DeleteItem",
			    data: { curriculumId: id },
			    success: function (r) {
				    if (r.success == true) {
					    var item = "curriculum" + id;
					    $("tr[id=" + item + "]").remove();
				    }
				    else {
					    alert("<%=Localization.GetMessage("ErrorOccuredDuringProcessingRequestErrorMessage") %> " + r.message);
				    }
			    }
		    });
	    }
    </script>
	 <script src="/Scripts/jquery/jquery.validate.min.js" type="text/javascript"></script>
    <script src="/Scripts/Microsoft/MicrosoftAjax.js" type="text/javascript"></script>
    <script src="/Scripts/Microsoft/MicrosoftMvcAjax.js" type="text/javascript"></script>
    <script src="/Scripts/Microsoft/MicrosoftMvcValidation.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=Localization.GetMessage("Curriculums")%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	
	<script src="<%= Html.ResolveUrl("~/Scripts/svg/jquery.svg.js")%>" type="text/javascript"></script>
	<link href="<%=  Html.ResolveUrl("~/Content/svg/jquery.svg.css")%>" rel="stylesheet" type="text/css"/>
	<script src="<%= Html.ResolveUrl("~/Scripts/treetable/javascripts/jquery.treeTable.js")%>" type="text/javascript"></script>
	<link href="<%=  Html.ResolveUrl("~/Scripts/treetable/stylesheets/jquery.treeTable.css")%>" rel="stylesheet" type="text/css"/>

    <h2>
        <%=Localization.GetMessage("Curriculums")%>
    </h2>
    <p>
	     <a href="javascript:void(0)" onclick="CreateCurriculum();"><%=Localization.GetMessage("AddCurriculum") %></a>
        <a id="DeleteMany" href="#"><%=Localization.GetMessage("DeleteSelected")%></a>
    </p>
    <table id="curriculumsTable">
        <thead>
        <tr>
            <th>
            </th>
				<th>
            </th>
            <th>
                <%=Localization.GetMessage("Group")%>
            </th>
            <th>
                <%=Localization.GetMessage("Discipline")%>
            </th>
            <th>
                <%=Localization.GetMessage("StartDate")%>
            </th>
            <th>
                <%=Localization.GetMessage("EndDate")%>
            </th>
            <th>
            </th>
        </tr>
        </thead>
        <tbody>
        <% int k = 0;
			  foreach (var item in Model) {
				  if (!item.IsValid) k++;%>
        <% Html.RenderPartial("CurriculumRow", item); %>
        <% } %>
        </tbody>
    </table>
	 <div id="tooltipImmitation"></div>
	 <div id="warningMessage" <% if (k==0) {%>style="display: none"<%} %>>
		 <div class="warning-message-back">
		 </div>
		 <div class="warning-message-wrapper">
			 <div class="warning-message"><legend style="color: red"><%=Localization.GetMessage("CurriculumsMarkedInRed") %></legend></div>
		 </div>
	 </div>
	 <div id="errorsDialog">
	 	 <div id="errorsDialogInner"></div>
	 </div>
</asp:Content>
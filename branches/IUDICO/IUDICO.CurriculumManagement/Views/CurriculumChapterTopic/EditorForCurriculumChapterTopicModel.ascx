﻿<%@ Assembly Name="IUDICO.CurriculumManagement" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IUDICO.CurriculumManagement.Models.ViewDataClasses.CreateCurriculumChapterTopicModel>" %>
<%@ Import Namespace="IUDICO.Common" %>

<script type="text/javascript" src="/Scripts/jquery/ui.spinner.min.js"></script>

<link href="/Content/ui.spinner.css" rel="stylesheet" type="text/css" />

<script type="text/javascript">
	$(document).ready(function () {
		if (!$("#SetTestTimeline").attr('checked')) {
			$("#divTestTimeline").hide();
		}
		$("#SetTestTimeline").click(function () {
			$("#divTestTimeline").slideToggle(300);
		});

		if (!$("#SetTheoryTimeline").attr('checked')) {
			$("#divTheoryTimeline").hide();
		}
		$("#SetTheoryTimeline").click(function () {
			$("#divTheoryTimeline").slideToggle(300);
		});

		$("#ThresholdOfSuccess").spinner({ min: 0, max: 100 });
	});
</script>


<fieldset id="">
    <legend>
        <%=Localization.GetMessage("Fields")%></legend>
    <div class="editor-label">
        <%: Html.LabelFor(model => model.ThresholdOfSuccess)%>
    </div>
    <div class="editor-field">
			<%: Html.TextBoxFor(model => model.ThresholdOfSuccess)%>
		   <%: Html.ValidationMessageFor(model => model.ThresholdOfSuccess)%>
    </div>
    <div class="curriculumChapterTopic_timeline1">
        <div class="editor-label">
            <%: Html.LabelFor(model => model.SetTestTimeline)%>
        </div>
        <div class="editor-field">
            <%: Html.CheckBoxFor(model => model.SetTestTimeline)%>
            <%: Html.ValidationMessageFor(model => model.SetTestTimeline)%>
        </div>
        <div id="divTestTimeline">
            <div class="editor-label">
                <%: Html.LabelFor(model => model.TestStartDate) %>
            </div>
            <div class="editor-field">
                <%: Html.EditorFor(model => model.TestStartDate)%>
                <%: Html.ValidationMessageFor(model => model.TestStartDate)%>
            </div>
            <div class="editor-label">
                <%: Html.LabelFor(model => model.TestEndDate)%>
            </div>
            <div class="editor-field">
                <%: Html.EditorFor(model => model.TestEndDate)%>
                <%: Html.ValidationMessageFor(model => model.TestEndDate)%>
            </div>
        </div>
    </div>
    <div class="curriculumChapterTopic_timeline2">
        <div class="editor-label">
            <%: Html.LabelFor(model => model.SetTheoryTimeline)%>
        </div>
        <div class="editor-field">
            <%: Html.CheckBoxFor(model => model.SetTheoryTimeline)%>
            <%: Html.ValidationMessageFor(model => model.SetTheoryTimeline)%>
        </div>
        <div id="divTheoryTimeline">
            <div class="editor-label">
                <%: Html.LabelFor(model => model.TheoryStartDate) %>
            </div>
            <div class="editor-field">
                <%: Html.EditorFor(model => model.TheoryStartDate)%>
                <%: Html.ValidationMessageFor(model => model.TheoryStartDate)%>
            </div>
            <div class="editor-label">
                <%: Html.LabelFor(model => model.TheoryEndDate)%>
            </div>
            <div class="editor-field">
                <%: Html.EditorFor(model => model.TheoryEndDate)%>
                <%: Html.ValidationMessageFor(model => model.TheoryEndDate)%>
            </div>
        </div>
    </div>
    <div class="curriculumChapterTopic_blocking">
        <div class="editor-label">
            <%: Html.LabelFor(model => model.BlockTopicAtTesting)%>
        </div>
        <div class="editor-field">
            <%: Html.CheckBoxFor(model => model.BlockTopicAtTesting)%>
            <%: Html.ValidationMessageFor(model => model.BlockTopicAtTesting)%>
        </div>
        <div class="editor-label">
            <%: Html.LabelFor(model => model.BlockCurriculumAtTesting)%>
        </div>
        <div class="editor-field">
            <%: Html.CheckBoxFor(model => model.BlockCurriculumAtTesting)%>
            <%: Html.ValidationMessageFor(model => model.BlockCurriculumAtTesting)%>
        </div>
    </div>
</fieldset>

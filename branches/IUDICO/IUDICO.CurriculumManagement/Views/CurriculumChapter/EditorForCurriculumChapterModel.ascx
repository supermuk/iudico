<%@ Assembly Name="IUDICO.CurriculumManagement" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IUDICO.CurriculumManagement.Models.ViewDataClasses.CreateCurriculumChapterModel>" %>
<%@ Import Namespace="IUDICO.Common" %>

<fieldset>
    <legend>
        <%=Localization.GetMessage("Fields")%></legend>
    <div id="divTimeline">
        <div class="editor-label">
            <%: Html.LabelFor(model => model.StartDate) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.StartDate)%>
            <%: Html.ValidationMessageFor(model => model.StartDate)%>
        </div>
        <div class="editor-label">
            <%: Html.LabelFor(model => model.EndDate) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.EndDate)%>
            <%: Html.ValidationMessageFor(model => model.EndDate)%>
        </div>
    </div>
</fieldset>

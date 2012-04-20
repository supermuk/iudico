<%@ Assembly Name="IUDICO.CurriculumManagement" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IUDICO.CurriculumManagement.Models.ViewDataClasses.CreateCurriculumModel>" %>
<script type="text/javascript">
    $(document).ready(function () {
        if (!$("#SetTimeline").attr('checked')) {
            $("#divTimeline").hide();
        }
        $("#SetTimeline").click(function () {
            $("#divTimeline").slideToggle(300);
        });
    });
</script>
<fieldset>
    <legend>
        <%=IUDICO.CurriculumManagement.Localization.getMessage("Fields")%></legend>
    <% if (Model.IsCreateModel)
       {%>
        <div class="editor-label">
            <%: Html.LabelFor(model => model.DisciplineId) %>
        </div>
        <div class="editor-field">
            <%: Html.DropDownListFor(model => model.DisciplineId, Model.Disciplines) %>
            <%: Html.ValidationMessageFor(model => model.DisciplineId) %>
        </div>
    <% } %>
    <div class="editor-label">
        <%: Html.LabelFor(model => model.GroupId) %>
    </div>
    <div class="editor-field">
        <%: Html.DropDownListFor(model => model.GroupId, Model.Groups)%>
        <%: Html.ValidationMessageFor(model => model.GroupId)%>
    </div>
    <div class="editor-label">
        <%: Html.LabelFor(model => model.SetTimeline)%>
    </div>
    <div class="editor-field">
        <%: Html.CheckBoxFor(model => model.SetTimeline)%>
        <%: Html.ValidationMessageFor(model => model.SetTimeline)%>
    </div>
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

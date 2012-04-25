<%@ Assembly Name="IUDICO.DisciplineManagement" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<script type="text/javascript">
    $(document).ready(function () {
        $("#linkImport").click(function () {
            $("#inputFileUpload").click();
        });

        $("#inputFileUpload").change(function () {
            $(this).parent().submit();
        });
    })
</script>
<div style="display: inline-block">
    <% using (Html.BeginForm("Import", "Discipline", FormMethod.Post, new { enctype = "multipart/form-data" }))
        {%>
            <input type="file" id="inputFileUpload" name="fileUpload" style="visibility: hidden; position:absolute;"/>
            <a id="linkImport" href="#"><%=IUDICO.DisciplineManagement.Localization.GetMessage("Import")%></a>
    <%} %>
</div>
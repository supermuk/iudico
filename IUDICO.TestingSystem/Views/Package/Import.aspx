<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Import
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <% using (Html.BeginForm("Import", "Package", FormMethod.Post, new { enctype = "multipart/form-data" }))
       {%>
        <fieldset>
            <legend>Import Course</legend>
            <input type="file" id="fileToValidate" name="fileUpload"/>
            <p>
                <input type="submit" value="Import" id="Import" name="action"/>
            </p>
        </fieldset>
    <%} %>

    <div>
        <%: Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="/Scripts/Microsoft/MicrosoftAjax.js" type="text/javascript"></script>
    <script src="/Scripts/Microsoft/MicrosoftMvcAjax.js" type="text/javascript"></script>
    <script type="text/javascript">

    </script>

</asp:Content>

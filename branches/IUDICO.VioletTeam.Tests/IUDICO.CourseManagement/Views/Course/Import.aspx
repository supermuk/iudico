﻿<%@ Assembly Name="IUDICO.CourseManagement" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=IUDICO.CourseManagement.Localization.getMessage("ValidateOrImportCourse")%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <% using (Html.BeginForm("Import", "Course", FormMethod.Post, new { enctype = "multipart/form-data" }))
       {%>
        <fieldset>
            <legend><%=IUDICO.CourseManagement.Localization.getMessage("ValidateOrImportCourse")%></legend>
            <input type="file" id="fileToValidate" name="fileUpload"/>
            <p>
                <input type="submit" value=<%=IUDICO.CourseManagement.Localization.getMessage("Validate")%> id="Validate" name="action"/>
                <input type="submit" value=<%=IUDICO.CourseManagement.Localization.getMessage("Import")%> id="Import" name="action"/>
            </p>
            <div id="validateResult">
                <ul>
                    <% foreach (string result in ViewData["validateResults"] as List<string>)
                       { %>
                        <li>
                            <%: result %>
                        </li>
                    <% } %>
                </ul>
            </div>
        </fieldset>
    <%} %>

    <div>
        <%: Html.ActionLink(IUDICO.CourseManagement.Localization.getMessage("BackToList"), "Index")%>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="/Scripts/Microsoft/MicrosoftAjax.js" type="text/javascript"></script>
    <script src="/Scripts/Microsoft/MicrosoftMvcAjax.js" type="text/javascript"></script>
    <script type="text/javascript">

    </script>

</asp:Content>
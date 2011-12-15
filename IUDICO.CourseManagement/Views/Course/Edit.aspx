<%@ Assembly Name="IUDICO.CourseManagement" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Common.Models.Shared.Course>" %>

<asp:Content ID="Content0" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="<%= Html.ResolveUrl("~/Content/jquery.multiselect2side.css") %>" rel="Stylesheet" type="text/css" />
    <script src="<%= Html.ResolveUrl("/Scripts/jquery/jquery.validate.min.js") %>" type="text/javascript"></script>
    <script src="<%= Html.ResolveUrl("/Scripts/Microsoft/MicrosoftAjax.js") %>" type="text/javascript"></script>
    <script src="<%= Html.ResolveUrl("/Scripts/Microsoft/MicrosoftMvcAjax.js") %>" type="text/javascript"></script>
    <script src="<%= Html.ResolveUrl("/Scripts/Microsoft/MicrosoftMvcValidation.js") %>" type="text/javascript"></script>
    <script src="<%= Html.ResolveUrl("~/Scripts/jquery/jquery.multiselect2side.js") %>" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#sharewith').multiselect2side({
                selectedPosition: 'right',
                moveOptions: false,
                labelsx: '<%=IUDICO.CourseManagement.Localization.getMessage("AllUsers") %>',
                labeldx: '<%=IUDICO.CourseManagement.Localization.getMessage("SharedWith") %>'
            });
        });
    </script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=IUDICO.CourseManagement.Localization.getMessage("Edit")%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%=IUDICO.CourseManagement.Localization.getMessage("Edit")%></h2>

    <% Html.EnableClientValidation(); %>

    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>
        
        <fieldset>
            <legend><%=IUDICO.CourseManagement.Localization.getMessage("Fields") %></legend>
            
            <%= Html.EditorForModel() %>
            
            <select multiple="multiple" id="sharewith" name="sharewith">
            <%  foreach (IUDICO.Common.Models.Shared.User user in ViewData["AllUsers"] as IEnumerable<IUDICO.Common.Models.Shared.User>)
                { %>
                <option value="<%= user.Id %>"><%: user.Name %></option>
            <% }%>
            <%  foreach (IUDICO.Common.Models.Shared.User user in ViewData["SharedUsers"] as IEnumerable<IUDICO.Common.Models.Shared.User>)
                { %>
                <option value="<%= user.Id %>" selected><%: user.Name %></option>
            <% }%>
            </select>

            <p>
                <input type="submit" value=<%=IUDICO.CourseManagement.Localization.getMessage("Save")%> />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%: Html.ActionLink(IUDICO.CourseManagement.Localization.getMessage("BackToList"), "Index")%>
    </div>

</asp:Content>


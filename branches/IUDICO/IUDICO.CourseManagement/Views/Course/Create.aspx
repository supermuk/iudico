<%@ Assembly Name="IUDICO.CourseManagement" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Common.Models.Course>" %>

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
                labelsx: 'All Users',
                labeldx: 'Shared With'
            });
        });
    </script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%=CourseManagRes.CourseManagement.Create %></h2>

    <% Html.EnableClientValidation(); %>

    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>

        <fieldset>
            <legend>Fields</legend>
            
            <%= Html.EditorForModel() %>
            
            <select multiple="multiple" id="sharewith" name="sharewith">
            <%  foreach (IUDICO.Common.Models.User user in ViewData["AllUsers"] as IEnumerable<IUDICO.Common.Models.User>)
                { %>
                <option value="<%= user.Id %>"><%: user.Name %></option>
            <% }%>
            </select>

            <p>
                <input type="submit" value=<%=CourseManagRes.CourseManagement.Create %> />
            </p>
        </fieldset>

    <% } %>
    

    <div>
        <%: Html.ActionLink(CourseManagRes.CourseManagement.BackToList, "Index")%>
    </div>

</asp:Content>


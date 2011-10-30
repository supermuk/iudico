<%@ Assembly Name="IUDICO.Security" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Security.ViewModels.Ban.RoomsViewModel>" %>

<asp:Content ID="Content0" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="<%= Html.ResolveUrl("~/Content/jquery.multiselect2side.css") %>" rel="Stylesheet" type="text/css" />
    <link href="<%= Html.ResolveUrl("~/Content/security.css") %>" rel="Stylesheet" type="text/css" />
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
                labelsx: "Computers at this room",
                labeldx: "Unchoosen computers"
            });

            $("#rooms-list").change(function (event) {
                alert('Rooms changed' + $(event.target).find(":selected").val());
            });
        });
    </script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit Room
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Edit Rooms</h2>

    <select id="rooms-list">
        <%  foreach (var comp in Model.Rooms)
                    { %>
                    <option value="<%= comp %>"> <%= comp %> </option>
                <% }%>
    </select>
    <% using (Html.BeginForm()) {%>
            <select multiple="multiple" id="sharewith" name="sharewith">
                <%  foreach (var comp in Model.UnchoosenComputers)
                    { %>
                    <option value="<%= comp %>" selected="selected"> <%= comp %> </option>
                <% }%>

                <%  foreach (var comp in Model.Computers)
                    { %>
                    <option value="<%= comp %>"> <%= comp %> </option>
                <% }%>
            </select>

            <p>
                <input type="submit" value="Save" />
            </p>

    <% } %>

</asp:Content>

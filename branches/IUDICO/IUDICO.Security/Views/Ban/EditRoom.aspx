<%@ Assembly Name="IUDICO.Security" %>

<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Security.ViewModels.Ban.RoomsViewModel>" %>
<%@ Import Namespace="IUDICO.Common" %>

<asp:Content ID="Content0" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="<%= Html.ResolveUrl("~/Content/jquery.multiselect2side.css") %>" rel="Stylesheet"
        type="text/css" />
    <link href="<%= Html.ResolveUrl("~/Content/security.css") %>" rel="Stylesheet" type="text/css" />
    <script src="<%= Html.ResolveUrl("/Scripts/jquery/jquery.validate.min.js") %>" type="text/javascript"></script>
    <script src="<%= Html.ResolveUrl("/Scripts/Microsoft/MicrosoftAjax.js") %>" type="text/javascript"></script>
    <script src="<%= Html.ResolveUrl("/Scripts/Microsoft/MicrosoftMvcAjax.js") %>" type="text/javascript"></script>
    <script src="<%= Html.ResolveUrl("/Scripts/Microsoft/MicrosoftMvcValidation.js") %>"
        type="text/javascript"></script>
    <script src="<%= Html.ResolveUrl("~/Scripts/jquery/jquery.multiselect2side.js") %>"
        type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#sharewith').multiselect2side({
                selectedPosition: 'left',
                moveOptions: false,
                labelsx: "<%=Localization.GetMessage("Computers at this room") %>",
                labeldx: "<%=Localization.GetMessage("Unchoosen computers") %>"
            });

            $("#rooms-list").change(function (event) {
                var $rooms = $(event.target);
                $rooms.closest('form').submit();
            });
            $("#saveButton").click(function (event) {
                event.preventDefault();
                var arr = $("#sharewith option:not(:checked)");
                var unchecked = '';
                for (var key in arr)
                    if (arr[key].value != undefined)
                        unchecked = unchecked + arr[key].value + ',';

                $.ajax({
                    type: "get",
                    url: "/Ban/UpdateRoom",
                    data: {
                        CurrentRoom: $("#rooms-list").val(),
                        compArray: $("select#sharewith").val().toString(),
                        unchoosenComputers: unchecked.slice(0, unchecked.length - 1)
                    },
                    success: function (r) {
                        alert(r);
                    }
                });
            });            
        });
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=Localization.GetMessage("Edit room") %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <fieldset>
    <legend> <%=Localization.GetMessage("Edit Rooms") %> </legend>
    <% using (Html.BeginForm())
       {%>
    <select id="rooms-list" name="CurrentRoom">
        <%  
            var selected = string.Empty;
            foreach (var room in Model.Rooms)
            {
                if (room == Model.CurrentRoom)
                {
                    selected = "selected='selected'";
                }
                else
                {
                    selected = string.Empty;
                }
        %>
        <option value="<%= room %>" <%= selected %>>
            <%= room %>
        </option>
        <% }%>
                
    </select>

    <select multiple="multiple" id="sharewith" name="sharewith">
        <%  foreach (var comp in Model.UnchoosenComputers)
            { %>
        <option value="<%= comp %>">
            <%= comp %>
        </option>
        <% }%>
        <%  foreach (var comp in Model.Computers)
            { %>
        <option value="<%= comp %>" selected="selected">
            <%= comp %>
        </option>
        <% }%>
    </select>

    <div>
    <input type="submit" value="Save" id="saveButton" />
    </div>
    </fieldset>
    <% } %>
    <%= Html.ActionLink(Localization.GetMessage("BackToList"), "EditRooms", "Ban") %>
</asp:Content>

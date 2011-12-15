<%@ Assembly Name="IUDICO.Security" %>

<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Security.ViewModels.Ban.RoomsViewModel>" %>

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
                labelsx: "Computers at this room",
                labeldx: "Unchoosen computers"
            });

            $("#rooms-list").change(function (event) {
                var $rooms = $(event.target);
                $rooms.closest('form').submit();
            });
        });
        //                $.ajax({
        //                    type: "post",
        //                    url: "/Ban/EditRooms",
        //                    data: { CurrentRoom: $(event.target).find(":selected").val() },
        //                    success: function (r) {
        //                        if (r.success) {
        //                            
        //                        }
        //                        else {
        //                            alert("5");
        //                        }
        //                    }
        //                });
        //            });            
        //      });
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Edit Room
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <fieldset>
    <legend> Edit Rooms </legend>
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
        </fieldset>
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
   <!-- <p>
        <input type="submit" value="Save" name="saveButton" />
    </p> -->
    <% } %>
</asp:Content>

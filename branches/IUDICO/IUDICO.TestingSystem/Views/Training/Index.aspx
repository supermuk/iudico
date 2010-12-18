<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<IUDICO.TestingSystem.Models.Shared.Training>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Availible Trainings
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Availible Trainings</h2>

    <table>
        <tr>
            <th></th>
            <th>
                Package ID
            </th>
            <th>
                Package FileName
            </th>
            <th>
                Organization ID
            </th>
            <th>
                Organization Title
            </th>
            <th>
                Attempt ID
            </th>
            <th>
                Status
            </th>
            <th>
                Upload Time
            </th>
            <th>
                Total Points
            </th>
            <th>
                Play ID
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%: Html.ActionLink("Details", "Details", new { packageID = item.PackageID, attemptID = item.AttemptID })%> |
                <%: item.AttemptStatusProp == null ? Html.ActionLink("Play", "Create", new { id = item.PlayID }) : Html.ActionLink("Play", "Play", new { id = item.PlayID }) %>
            </td>
            <td>
                <%: item.PackageID %>
            </td>
            <td>
                <%: item.PackageFileName %>
            </td>
            <td>
                <%: item.OrganizationID %>
            </td>
            <td>
                <%: item.OrganizationTitle %>
            </td>
            <td>
                <%: item.AttemptID %>
            </td>
            <td>
                <%: item.AttemptStatusProp %>
            </td>
            <td>
                <%: String.Format("{0:g}", item.UploadDateTime) %>
            </td>
            <td>
                <%: String.Format("{0:0.#}", item.TotalPoints) %>
            </td>
            <td>
                <%: item.PlayID %>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%: Html.ActionLink("Upload new", "Add") %>
    </p>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>


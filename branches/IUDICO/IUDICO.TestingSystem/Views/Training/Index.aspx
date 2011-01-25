<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<IUDICO.TestingSystem.Models.VO.Training>>" %>
<%@ Assembly Name="IUDICO.TestingSystem" %>
<%@ Assembly Name="Microsoft.LearningComponents" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Availible Trainings
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Availible Trainings</h2>

    <table>
        <tr>
            <th></th>
            <th>
                Package FileName
            </th>
            <th>
                Organization Title
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
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%: Html.ActionLink("Delete", "Delete", new { id = item.PackageId })%> |
                <%: Html.ActionLink("Details", "Details", new { packageId = item.PackageId, attemptId = item.AttemptId })%> |
                <%: item.AttemptStatusProp == null ? Html.ActionLink("Play", "Create", new { id = item.PlayId }) : Html.ActionLink("Play", "Play", new { id = item.PlayId })%>
            </td>
            <td>
                <%: item.PackageFileName %>
            </td>
            <td>
                <%: item.OrganizationTitle %>
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
        </tr>
    
    <% } %>

    </table>

    <p>
        <%: Html.ActionLink("Upload new", "Import","Package") %>
    </p>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
<script language="javascript" type="text/javascript">
    function OpenFrameset(strAttemptId) {
        // open the frameset for viewing training content; <strAttemptId> is the attempt ID
        window.open("http://localhost:1339/BasicWebPlayer/Frameset/Frameset.aspx?View=0&AttemptId=" + strAttemptId, "_blank");
    }
</script>
</asp:Content>


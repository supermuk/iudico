<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.TestingSystem.Models.VO.Training>" %>
<%@ Assembly Name="IUDICO.TestingSystem" %>
<%@ Assembly Name="Microsoft.LearningComponents" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Training Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Training Details</h2>

    <fieldset>
        <legend>Properties</legend>
        <table>
        <tr>
        <td>PackageId</td>
        <td><%: Model.PackageId %></td>
        </tr>
        <tr>
        <td>PackageFileName</td>
        <td><%: Model.PackageFileName %></td>
        </tr>
        <tr>
        <td>OrganizationId</td>
        <td><%: Model.OrganizationId %></td>
        </tr>
        <tr>
        <td>OrganizationTitle</td>
        <td><%: Model.OrganizationTitle %></td>
        </tr>
        <tr>
        <td>AttemptId</td>
        <td><%: Model.AttemptId %></td>
        </tr>
        <tr>
        <td>UploadDateTime</td>
        <td><%: String.Format("{0:g}", Model.UploadDateTime) %></td>
        </tr>
        <tr>
        <td>TotalPoints</td>
        <td><%: Model.TotalPoints %></td>
        </tr>
        <tr>
        <td>PlayId</td>
        <td><%: Model.PlayId %></td>
        </tr>
        </table>
    </fieldset>
    <p>
        <%: Html.ActionLink("Back to List", "Index") %>
    </p>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>


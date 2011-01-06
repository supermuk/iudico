<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Statistics.Models.Storage.ThemeTestResaultsModel>" %>
<%@ Assembly Name="IUDICO.Statistics" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ThemeTestResaults
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%: Html.ActionLink("<- back", "Index") %>
    <fieldset>
    <legend> Attempt statistic:</legend>
        <h2>Resaults</h2>
        <p>
            Student:  <%: Model.Attempt.User.Username%>
        </p>
        <p>
            Theme:  <%: Model.Attempt.Theme.Name%>
        </p>
        <p>
            Success:  <%: Model.Attempt.SuccessStatus%>
        </p>
        <p>
            Score:  <%: Model.Attempt.Score.ToPercents()%>
        </p>
    </fieldset>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
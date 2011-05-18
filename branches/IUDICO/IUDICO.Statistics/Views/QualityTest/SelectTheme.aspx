<%@ Assembly Name="IUDICO.Statistics" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Statistics.Models.QualityTest.SelectThemeModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	SelectTheme
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>SelectTheme page</h2>
    <fieldset>
    <legend> Select theme</legend>
    <%if (Model.NoData() == false)
      { %>
        <p>
        Teacher: <%: Model.GetTeacherUserName()%>
        </p>
        <p>
        Curriculum: <%: Model.GetCurriculumName()%>
        </p>
        <form action="/QualityTest/SelectGroups/" method="post">
        <% foreach (IUDICO.Common.Models.Theme theme in Model.GetAllowedThemes())
           {%>
                <div>
                <input type="radio" name="selectThemeId" value="<%: theme.Id %>" checked="checked" />
                <%: theme.Name%>
                </div>
            <%} %>
            <input type="submit" value="Next" /> 
        </form>
        <%}
      else
      { %>
      No Data
      <%} %>
    </fieldset>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

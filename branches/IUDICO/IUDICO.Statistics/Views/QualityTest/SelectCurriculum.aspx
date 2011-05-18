<%@ Assembly Name="IUDICO.Statistics" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Statistics.Models.QualityTest.IndexModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>SelectCurriculums page</h2>
    <fieldset>
    <legend> Select one curiculum</legend>
    <%if (Model.NoData() == false)
      { %>
        <p>
        Teacher: <%: Model.GetTeacherUserName()%>
        </p>
        <form action="/QualityTest/SelectTheme/" method="post">
        <% foreach (IUDICO.Common.Models.Curriculum curriculum in Model.GetAllowedCurriculums())
           {%>
                <div>
                <input type="radio" name="selectCurriculumId" value="<%: curriculum.Id %>" checked="checked" />
                <%: curriculum.Name%>
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

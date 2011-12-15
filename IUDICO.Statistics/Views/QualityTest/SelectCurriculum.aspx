<%@ Assembly Name="IUDICO.Statistics" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Statistics.Models.QualityTest.IndexModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=IUDICO.Statistics.Localization.getMessage("SelectCurriculums")%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%=IUDICO.Statistics.Localization.getMessage("SelectCurriculums")%></h2>
    <fieldset>
    <legend><%=IUDICO.Statistics.Localization.getMessage("SelectOneCurriculum")%></legend>
    <%if (Model.NoData() == false)
      { %>
        <p>
        <%=IUDICO.Statistics.Localization.getMessage("Teacher")%>: <%: Model.GetTeacherUserName()%>
        </p>
        <form action="/QualityTest/SelectTheme/" method="post">
        <% foreach (IUDICO.Common.Models.Shared.Curriculum curriculum in Model.GetAllowedCurriculums())
           {%>
                <div>
                <input type="radio" name="selectCurriculumId" value="<%: curriculum.Id %>" checked="checked" />
                <%: curriculum.Name%>
                </div>
            <%} %>
            <input type="submit" value=<%=IUDICO.Statistics.Localization.getMessage("Next")%> /> 
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

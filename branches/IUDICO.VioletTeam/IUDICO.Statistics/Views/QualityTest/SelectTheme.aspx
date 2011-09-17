<%@ Assembly Name="IUDICO.Statistics" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Statistics.Models.QualityTest.SelectThemeModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=IUDICO.Statistics.Localization.getMessage("SelectTheme")%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%=IUDICO.Statistics.Localization.getMessage("SelectTheme")%></h2>
    <fieldset>
    <legend><%=IUDICO.Statistics.Localization.getMessage("SelectOneTheme")%></legend>
    <%if (Model.NoData() == false)
      { %>
        <p>
        <%=IUDICO.Statistics.Localization.getMessage("Teacher")%>: <%: Model.GetTeacherUserName()%>
        </p>
        <p>
        <%=IUDICO.Statistics.Localization.getMessage("Curriculum")%>: <%: Model.GetCurriculumName()%>
        </p>
        <form action="/QualityTest/SelectGroups/" method="post">
        <% foreach (IUDICO.Common.Models.Theme theme in Model.GetAllowedThemes())
           {%>
                <div>
                <input type="radio" name="selectThemeId" value="<%: theme.Id %>" checked="checked" />
                <%: theme.Name%>
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

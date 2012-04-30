<%@ Assembly Name="IUDICO.Statistics" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Statistics.Models.QualityTest.IndexModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=IUDICO.Statistics.Localization.getMessage("SelectDisciplines")%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%=IUDICO.Statistics.Localization.getMessage("SelectDisciplines")%></h2>
    <fieldset>
    <legend><%=IUDICO.Statistics.Localization.getMessage("SelectOneDiscipline")%></legend>
    <%if (Model.NoData() == false)
      { %>
        <p>
        <%=IUDICO.Statistics.Localization.getMessage("Teacher")%>: <%: Model.GetTeacherUserName()%>
        </p>
        <form action="/QualityTest/SelectTopic/" method="post">
        <% foreach (IUDICO.Common.Models.Shared.Discipline discipline in Model.GetAllowedDisciplines())
           {%>
                <div>
                <input type="radio" name="selectDisciplineId" value="<%: discipline.Id %>" checked="checked" />
                <%: discipline.Name%>
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

<%@ Assembly Name="IUDICO.Statistics" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Statistics.Models.QualityTest.SelectTopicModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=IUDICO.Statistics.Localization.getMessage("SelectTopic")%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%=IUDICO.Statistics.Localization.getMessage("SelectTopic")%></h2>
    <fieldset>
    <legend><%=IUDICO.Statistics.Localization.getMessage("SelectOneTopic")%></legend>
    <%if (Model.NoData() == false)
      { %>
        <p>
        <%=IUDICO.Statistics.Localization.getMessage("Teacher")%>: <%: Model.GetTeacherUserName()%>
        </p>
        <p>
        <%=IUDICO.Statistics.Localization.getMessage("Discipline")%>: <%: Model.GetDisciplineName()%>
        </p>
        <form action="/QualityTest/SelectGroups/" method="post">
        <% foreach (IUDICO.Common.Models.Shared.Topic topic in Model.GetAllowedTopics())
           {%>
                <div>
                <input type="radio" name="selectTopicId" value="<%: topic.Id %>" checked="checked" />
                <%: topic.Name%>
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

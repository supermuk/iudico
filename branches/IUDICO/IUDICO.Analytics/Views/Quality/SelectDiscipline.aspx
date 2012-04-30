<%@ Assembly Name="IUDICO.Analytics" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Analytics.Models.Quality.TopicModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Discipline/Topics Quality
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <fieldset>
    <legend>Discipline/Topics Quality</legend>
    <%if (Model.NoData() == false)
      { %>
       
        <h3>
        Discipline: <%: Model.GetDisciplineName()%> , Quality: <%: Model.GetDisciplineQuality()%>
        </h3>
        
        <% for (int i = 1; i < Model.GetAllowedTopics().Count+1;i++ )
           {%>
                <p>
                <%: i%>. <%: Model.GetAllowedTopics()[i-1].Key.Name%> - <%: Model.GetAllowedTopics()[i-1].Value%> 
                </p>
            <%} %>               
        <%}
      else
      { %>
      No Data
      <%} %>
    </fieldset>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

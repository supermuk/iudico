<%@ Assembly Name="IUDICO.Analytics" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Analytics.Models.Quality.DisciplineModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Discipline/Topics Quality
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%: Html.ActionLink("Back", "Index")%>
    <fieldset>
    <legend>Discipline-Topics Quality</legend>
    <%if (Model.NoData() == false)
      { %>
       
        <table>     
     <tr>
        <th>Discipline</th>
        <th>Quality</th>
     </tr>   
        <tr>
            <td>
            <%: Model.GetDisciplineName()%>
            </td>
            <td>
            <%: Model.GetDisciplineQuality()%>
            </td>
        </tr>  
     </table>
        <br/>
     <table>     
     <tr>
        <th>Topic</th>
        <th>Quality</th>
     </tr>
     <%
          foreach (var topic in Model.GetAllowedTopics())
         {%>
        <tr>
            <td>
            <%:topic.Key.Name%>
            </td>
            <td>
            <%:topic.Value%>
            </td>
        </tr>
     <%
         }%>

     </table>
                      
        <%}
      else
      { %>
      No Data
      <%} %>
    </fieldset>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

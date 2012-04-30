<%@ Assembly Name="IUDICO.Analytics" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Analytics.Models.Quality.DisciplineModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
SelectDisciplines
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Discipline Quality</h2>
    <fieldset>
    <legend>Select Discipline</legend>
    <%if (Model.NoData() == false)
      { %>
        
        <form action="/Quality/SelectDiscipline/" method="post">
        <% foreach (var discipline in Model.GetAllowedDisciplines())
           {%>
                <div>
                <input type="radio" name="selectDisciplineId" value="<%: discipline.Id %>" checked="checked" />
                <%: discipline.Name%> 
                </div>
            <%} %>
            <input type="submit" value="Select" /> 
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

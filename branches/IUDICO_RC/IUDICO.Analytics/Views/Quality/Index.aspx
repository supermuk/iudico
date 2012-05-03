<%@ Assembly Name="IUDICO.Analytics" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<IUDICO.Common.Models.Shared.Discipline>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
SelectDisciplines
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Discipline Quality</h2>
    <fieldset>
    <legend>Please, Select Discipline :</legend>
    <%if (Model.Count() != 0 && Model != null )
      { %>
        
        <form action="/Quality/ShowDiscipline/" method="post">
        <% foreach (var discipline in Model)
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

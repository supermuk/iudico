<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<IUDICO.Common.Models.Curriculum>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	SelectCurriculums
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2></h2>

     <fieldset>

     <legend>Please, select one or more curriculum : </legend>

     <form action="/Stats/ShowCurriculumStatistic/" method="post">

     <table>
     
     <tr>
        <th> </th>
        <th>Curriculum id</th>
        <th>Curriculum name</th>
        <th>Created</th>
     </tr>

     <% foreach (IUDICO.Common.Models.Curriculum curr in Model)
        { %>
        <tr>
            <td>
            <input type="checkbox" name="selectCurriculumId" value="<%: curr.Id %>" />
            </td>
            <td>
            <%: curr.Id %>
            </td>
            <td>
            <%: curr.Name %>
            </td>
            <td>
            <%: curr.Created %>
            </td>
        </tr>
     <% } %>

     </table>

     <input type="submit" value="Show" />

     </form>

     </fieldset>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

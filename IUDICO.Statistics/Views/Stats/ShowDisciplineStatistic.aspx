<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Statistics.Models.Storage.AllSpecializedResults>" %>
<%@ Assembly Name="IUDICO.Statistics" %>
<%@ Import Namespace="IUDICO.Common" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=Localization.GetMessage("DisciplinesStatistic")%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <% if(Model.Users.Count() != 0) { %>

    <h2><%=Localization.GetMessage("DisciplinesStatisticForGroup") %>
    <%: (ViewData["selectGroupName"])%>
    </h2>

    <%: Html.ActionLink(Localization.GetMessage("Back"), "Index")%>
    <fieldset>
    <legend><%=Localization.GetMessage("SelectOneDiscipline")%> </legend>

    <table>
    <tr>
        <th><%=Localization.GetMessage("Student")%></th>
        <% int i = 0;
           foreach (IUDICO.Common.Models.Shared.Discipline curr in Model.Curriculums)
           {
               i++;%>
                <th>
                <form name="linkform<%:i%>" action="/Stats/TopicsInfo/" method="post">
                <input type="hidden" name="DisciplineID" value="<%: curr.Id%>"/>
                </form>
                <a href="javascript:document.forms['linkform<%:i%>'].submit();">                     
                    <%:curr.Name%>
                </a>
                </th>
        <% } %>
        <th><%=Localization.GetMessage("Sum")%></th>
        <th><%=Localization.GetMessage("Percent")%></th>
        <th>ECTS</th>
    </tr>
    
    <% foreach (IUDICO.Statistics.Models.Storage.SpecializedResult specializedResult in Model.SpecializedResults)
       { %>
       <tr>
            <td>
           [<%: specializedResult.User.Username %>] <%: specializedResult.User.Name %>
            </td>
           
            <% foreach (IUDICO.Statistics.Models.Storage.DisciplineResult currResult in specializedResult.DisciplineResult)
            { %>
                <td>
                <%: Math.Round(Double.Parse(currResult.Sum.ToString()),2)  %>
                /
                <%: currResult.Max  %>
                </td>     
            <% } %>
           
            <td>
                <%:Math.Round(Double.Parse(specializedResult.Sum.ToString()),2) %>
                /
                <%: specializedResult.Max %>
            </td>
            <td>
                <%: Math.Round(Double.Parse(specializedResult.Percent.ToString()), 2)%>
                %
            </td>
            <td>
                <%: specializedResult.ECTS %>
            </td>
       </tr>
    <% } %>
    </table>
    </fieldset>

    <% } %>
    <% else { %>
        <h2> No student has been added for <%: ViewData["selectGroupName"]%>. </h2>
    <% } %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

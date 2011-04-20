<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Statistics.Models.Storage.AllSpecializedResults>" %>
<%@ Assembly Name="IUDICO.Statistics" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ShowCurriculumStatistic
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%=IUDICO.Statistics.Localization.getMessage("CurriculumsStatisticForGroup") %>
    <%: (ViewData["selectGroupName"])%>
    </h2>

    <%: Html.ActionLink(IUDICO.Statistics.Localization.getMessage("Back"), "Index")%>
    <fieldset>
    <legend><%=IUDICO.Statistics.Localization.getMessage("SelectOneCurriculum")%> </legend>

    <table>
    <tr>
        <th><%=IUDICO.Statistics.Localization.getMessage("Student")%></th>
        <% int i = 0;
           foreach (IUDICO.Common.Models.Curriculum curr in Model.Curriculums)
           {
               i++;%>
                <th>
                <form name="linkform<%:i%>" action="/Stats/ThemesInfo/" method="post">
                <input type="hidden" name="CurriculumID" value="<%: curr.Id%>"/>
                </form>
                <a href="javascript:document.forms['linkform<%:i%>'].submit();">                     
                    <%:curr.Name%>
                </a>
                </th>
        <% } %>
        <th><%=IUDICO.Statistics.Localization.getMessage("Sum")%></th>
        <th><%=IUDICO.Statistics.Localization.getMessage("Percent")%></th>
        <th>ECTS</th>
    </tr>
    
    <% foreach (IUDICO.Statistics.Models.Storage.SpecializedResult specializedResult in Model.SpecializedResult)
       { %>
       <tr>
            <td>
           <%: specializedResult.User.Username %>
            </td>
           
            <% foreach (IUDICO.Statistics.Models.Storage.CurriculumResult currResult in specializedResult.CurriculumResult)
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
                <%: specializedResult.Percent %>
                %
            </td>
            <td>
                <%: specializedResult.ECTS %>
            </td>
       </tr>
    <% } %>
    </table>
    </fieldset>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

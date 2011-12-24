<%@ Assembly Name="IUDICO.Statistics" %>
<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SelectCurriculumsViewModel>" %>
<%@ Import namespace="IUDICO.Statistics" %>
<%@ Import namespace="IUDICO.Statistics.ViewModels" %>

<asp:Content ID="Content0" ContentPlaceHolderID="HeadContent" runat="server">
    <script language="javascript" type="text/javascript">
        function checkBox() {
            if ($('input:checkbox:checked').length == 0) {
                alert('<%=Localization.getMessage("SelectCurriculum")%>')
            }
            else {
                $('#curform').submit();
            }
        }

        $(document).ready(function() {
            $('#submitButton').click(checkBox);
        });
    </script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=Localization.getMessage("Statistics")%>: <%=Localization.getMessage("CurriculumList")%> <%: Model.GroupName%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   
     <% if (Model.Curriculums.Count() != 0)
       { %>
    <h2><%=Localization.getMessage("CurriculumList")%> <%: Model.GroupName%> </h2>


     <%: Html.ActionLink(Localization.getMessage("Back"), "Index")%>
     <fieldset>

     <legend><%=Localization.getMessage("SelectCurriculum")%>: </legend>


    <form id="curform" action="/Stats/ShowCurriculumStatistic/" method="post">
     <table>
     
     <tr>
        <th> </th>
        <th><%=Localization.getMessage("CurriculumName")%></th>
        <th><%=Localization.getMessage("Created")%></th>
     </tr>

     <% foreach (CurriculumViewModel curr in Model.Curriculums)
        { %>
        <tr>
            <td>
            <input type="checkbox" name="selectCurriculumId" value="<%: curr.Id %>" id="<%: curr.Id %>" />
            </td>
            <td>
            <%: curr.Name%>
            </td>
            <td>
            <%: curr.Created%>
            </td>
        </tr>
     <% } %>

     </table>

     <input id="submitButton" type="button" value=<%=Localization.getMessage("Show") %> />

     </form>

     </fieldset>

     <% } %>
     <% else { %>

     <h2> <%=Localization.getMessage("NoCurriculumHasBeenCreated")%> <%: Model.GroupName %>. </h2>

     <p><%: Html.ActionLink(Localization.getMessage("Back"), "Index")%></p>

     <%} %>
</asp:Content>
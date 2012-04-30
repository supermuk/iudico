<%@ Assembly Name="IUDICO.Statistics" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Statistics.Models.QualityTest.SelectGroupsModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=IUDICO.Statistics.Localization.getMessage("SelectGroups")%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%=IUDICO.Statistics.Localization.getMessage("SelectGroups")%></h2>

    <script type="text/javascript" language="javascript">
        function checkBox() {
            if ($('input:checkbox:checked').length == 0) {
                alert('<%=IUDICO.Statistics.Localization.getMessage("SelectOneOrMoreGroups")%>')
            }
            else {
                $('#selectGroupsForm').submit();
            }
        }
    </script>

     <fieldset>
     <legend> <%=IUDICO.Statistics.Localization.getMessage("SelectOneOrMoreGroups")%></legend>
    <%if (Model.NoData() == false)
      { %>
        <p>
         <%=IUDICO.Statistics.Localization.getMessage("Teacher")%>: <%: Model.GetTeacherUserName()%>
        </p>
        <p>
       <%=IUDICO.Statistics.Localization.getMessage("Discipline")%>: <%: Model.GetDisciplineName()%>
        </p>
        <p>
        <%=IUDICO.Statistics.Localization.getMessage("Topic")%>: <%: Model.GetTopicName()%>
        </p>
        <form id = "selectGroupsForm" action="/QualityTest/ShowQualityTest/" method="post">
        <% foreach (IUDICO.Common.Models.Shared.Group group in Model.GetAllowedGroups())
           {%>
                <div>
                <input type="checkbox" name="selectGroupIds" value="<%: group.Id %>" id="<%: group.Id %>"/>
                <%: group.Name%>
                </div>
            <%} %>
            <input type="button" value=<%=IUDICO.Statistics.Localization.getMessage("Next")%> onclick="checkBox();"/> 
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

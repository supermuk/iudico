<%@ Assembly Name="IUDICO.Statistics" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Statistics.Models.QualityTest.SelectGroupsModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	SelectGroups
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>SelectGroups</h2>

    <script type="text/javascript" language="javascript">
        function checkBox() {
            if ($('input:checkbox:checked').length == 0) {
                alert('Please, select one or more group')
            }
            else {
                $('#selectGroupsForm').submit();
            }
        }
    </script>

     <fieldset>
    <%if (Model.NoData() == false)
      { %>
        <div>
        Teacher: <%: Model.GetTeacherUserName()%>
        </div>
        <div>
        Curriculum: <%: Model.GetCurriculumName()%>
        </div>
        <div>
        Theme: <%: Model.GetThemeName()%>
        </div>
        <form id = "selectGroupsForm" action="/QualityTest/ShowQualityTest/" method="post">
        <% foreach (IUDICO.Common.Models.Group group in Model.GetAllowedGroups())
           {%>
                <div>
                <input type="checkbox" name="selectGroupIds" value="<%: group.Id %>" id="<%: group.Id %>"/>
                <%: group.Name%>
                </div>
            <%} %>
            <input type="button" value="Next" onclick="checkBox();"/> 
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

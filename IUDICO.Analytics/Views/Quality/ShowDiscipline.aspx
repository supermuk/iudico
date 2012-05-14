<%@ Assembly Name="IUDICO.Analytics" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Analytics.Models.Quality.DisciplineModel>" %>
<%@ Import Namespace="IUDICO.Common" %>
<%@ Import Namespace="IUDICO.Common.Models.Shared" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Discipline/Topics Quality
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%: Html.ActionLink("Back", "Index")%>
    <fieldset>

    <legend><h2><%=Localization.GetMessage("DTQuality")%></h2></legend>
    <%if (Model.NoData() == false)
      { %>
       
        <table>     
     <tr>
        <th><%=Localization.GetMessage("Discipline")%></th>
        <th><%=Localization.GetMessage("Quality")%></th>
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
        <th><%=Localization.GetMessage("Topic")%></th>
        <th><%=Localization.GetMessage("Quality")%> 1</th>
        <th><%=Localization.GetMessage("Quality")%> 2</th>
        <th><%=Localization.GetMessage("Quality")%> 3</th>
        <th><%=Localization.GetMessage("Quality")%> 4</th>
     </tr>
     <%
          foreach (var topic in Model.GetAllowedTopics())
         {%>
        <tr>
            <td>
            <%:topic.Key.Name%>
            </td>
            <td>
            <%:topic.Value[0]%>
            </td>
            <td>
            <%:topic.Value[1]%>
            </td>
            <td>
            <%:topic.Value[2]%>
            </td>
            <td>
            <%:topic.Value[3]%>
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
    1*. Відсоток студентів з однаковим рівнем навичок по темі, які отримали схожі результати.   <br/>
    2*. Відношення рейтингу студентів по даній темі та їх рейтингу загалом.  <br/>
    3*. Відношення оцінки студентів по даній темі та їх середньої оцінки.  <br/>
    4*. На основі розподілу Гауса вираховується відношення кількості неаномальних оцінок до їх загальної кількісті.  <br/>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<%@ Assembly Name="IUDICO.Analytics" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Analytics.Models.ViewDataClasses.ViewFeatureDetails>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Details of <%=Model.Feature.Name%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Details of <%=Model.Feature.Name%></h2>

    <fieldset>
        <legend><%="Fields"%></legend>
        <%:Html.DisplayForModel()%>
    </fieldset>

    <fieldset>
        <legend><%="Topics"%></legend>

        <table>
        <tr>
            <th>
                <%="Name"%>
            </th>
            <th></th>
        </tr>
        
        <%
            if (Model.Topics.Count() > 0)
            {%>
            <%
                foreach (var topic in Model.Topics)
                {%>
                <tr>
                    <td><%:topic.Name%></td>
                    <td><%:Html.ActionLink("Remove", "Delete", "TopicFeatures",
                                                      new {FeatureId = Model.Feature.Id, TopicId = topic.Id})%></td>
                </tr>
            <%
                }%>
        <%
            }
            else
            {%>
           <tr>
            <td colspan="2">No Data</td>
           </tr>
        <%
            }%>

        </table>
    </fieldset>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<%@ Assembly Name="IUDICO.Security" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Security.ViewModels.UserActivity.OverallViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%= Model.GetMessage("Overall stats") %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%= Model.GetMessage("Overall stats") %></h2>

    <table>
        <thead>
            <tr>
                <td><%= Model.GetMessage("User") %></td>
                <td><%= Model.GetMessage("TotalNumberOfRequests")%></td>
                <td><%= Model.GetMessage("TodayNumberOfRequests")%></td>
                <td><%= Model.GetMessage("LastActivityTime")%></td>
            </tr>
        </thead>

        <tbody>
        <%
            foreach (var stat in Model.GetStats())
            {
                var user = stat.User;

                %>
                <tr>
                    <td>
                    <%
                if (user != null)
                {
                    %><%= Html.ActionLink(user.Username, "Details", new { id = user.Id, controller = "User" }) %><%
                }
                else
                {
                    %><%= Model.GetMessage("Anonymous")%><%
                }
                    %>
                    </td>
                    <td><%= stat.TotalNumberOfRequests%></td>
                    <td><%= stat.TodayNumberOfRequests%></td>
                    <td><%= stat.LastActivityTime.ToString()%></td>
                </tr>
                <%
            }
        %>
        </tbody>
    </table>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

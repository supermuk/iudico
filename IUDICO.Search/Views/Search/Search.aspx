<%@ Assembly Name="IUDICO.Search" %>

<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Search.Models.ViewDataClasses.SearchModel>" %>

<%@ Import Namespace="IUDICO.Common" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=Localization.GetMessage("SearchResults")%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form action="/Search/Search" method="post">
    <%= Html.TextBoxFor(model=> model.SearchText)%>
    <%--    <%= Html.TextBox("query",  ViewData["SearchString"])%>--%>
    <input type="submit" value='<%=Localization.GetMessage("Search")%>' />
    <div>
        <% for (int i = 0; i < Model.CheckBoxes.Count; i++)
           { %>
        <%= Html.HiddenFor(model=>model.CheckBoxes[i].SearchType)  %>
        <%= Html.HiddenFor(model=>model.CheckBoxes[i].Text)  %>
        <%= Html.CheckBoxFor(model=> model.CheckBoxes[i].IsChecked)  %>
        <%= Html.Label(Model.CheckBoxes[i].Text)  %>
        <% } %>
    </div>
    <div>
        Кількість результатів:
        <%= Html.Label(Model.Total.ToString())  %>. Час пошуку:
        <%= Html.Label(Model.Score.ToString())  %>ms.
    </div>
    <br />
    <ul style="margin-bottom: 0em;">
        <% foreach (var result in Model.SearchResult)
           { %>
        <li>
            <h3>
                <a href="<%= result.GetUrl() %>">
                    <%= result.GetName() %></a></h3>
            <div>
                <%= result.GetText() %></div>
        </li>
        <% } %>
    </ul>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

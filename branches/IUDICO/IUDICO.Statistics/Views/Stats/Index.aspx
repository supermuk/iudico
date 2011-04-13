<%@ Assembly Name="IUDICO.Statistics" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<IUDICO.Common.Models.Group>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%=StatisRes.Statistics.GroupList %></h2>

    <fieldset>
    <legend><%=StatisRes.Statistics.SelectOneGroup %></legend>
    <form action="/Stats/SelectCurriculums/" method="post">

    <% foreach (IUDICO.Common.Models.Group item in Model)
       {%>
       <div>
        <input type="radio" name="id" value="<%: item.Id %>" checked="checked" />
            <%: Html.Label(item.Name)%>
       </div>
    <% } %>

    <p>
        <input type="submit" value=<%=StatisRes.Statistics.Show %> />
    </p>
    


    </form>
    
    </fieldset>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>

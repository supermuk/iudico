<%@ Assembly Name="IUDICO.Statistics" %>
<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<GroupViewModel>>" %>
<%@ Import namespace="IUDICO.Statistics" %>
<%@ Import namespace="IUDICO.Statistics.ViewModels" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%= Localization.getMessage("Statistics")%>: <%= Localization.getMessage("GroupList")%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <% if (Model.Count() != 0)
       { %>
    
    <h2><%=Localization.getMessage("GroupList")%></h2>

    <fieldset>
    <legend><%=Localization.getMessage("SelectOneGroup")%></legend>
    <form action="/Stats/SelectCurriculums/" method="post">

    <% foreach (GroupViewModel item in Model)
       {%>
       <div>
        <input type="radio" name="id" value="<%: item.Id %>" checked="checked" />
            <%: Html.Label(item.Name)%>
       </div>
    <% } %>

    <p>
        <input type="submit" value=<%=Localization.getMessage("Show") %> />
    </p>
    
    </form>
    
    </fieldset>

    <% } %>
    <% else {%>
    <h2> <%=Localization.getMessage("NoGroupHasBeenCreated")%> <%: Html.ActionLink(Localization.getMessage("CreateGroup"), "Create", "Group")%>. </h2>
    <% } %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>

<%@ Assembly Name="IUDICO.Statistics" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<IUDICO.Common.Models.Group>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <% if (Model.Count() != 0)
       { %>
    
    <h2><%=IUDICO.Statistics.Localization.getMessage("GroupList")%></h2>

    <fieldset>
    <legend><%=IUDICO.Statistics.Localization.getMessage("SelectOneGroup")%></legend>
    <form action="/Stats/SelectCurriculums/" method="post">

    <% foreach (IUDICO.Common.Models.Group item in Model)
       {%>
       <div>
        <input type="radio" name="id" value="<%: item.Id %>" checked="checked" />
            <%: Html.Label(item.Name)%>
       </div>
    <% } %>

    <p>
        <input type="submit" value=<%=IUDICO.Statistics.Localization.getMessage("Show") %> />
    </p>
    


    </form>
    
    </fieldset>

    <% } %>
    <% else {%>
    <h2> <%=IUDICO.Statistics.Localization.getMessage("NoGroupHasBeenCreated")%> <%: Html.ActionLink(IUDICO.Statistics.Localization.getMessage("CreateGroup"), "Create", "Group")%>. </h2>
    <% } %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>

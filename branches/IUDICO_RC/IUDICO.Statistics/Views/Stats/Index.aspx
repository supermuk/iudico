<%@ Assembly Name="IUDICO.Statistics" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<IUDICO.Common.Models.Shared.Group>>" %>
<%@ Import Namespace="IUDICO.Common" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=Localization.GetMessage("Statistics")%>: <%=Localization.GetMessage("GroupList")%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <% if (Model.Count() != 0)
       { %>
    
    <h2><%=Localization.GetMessage("GroupList")%></h2>

    <fieldset>
    <legend><%=Localization.GetMessage("SelectOneGroup")%></legend>
    <form action="/Stats/SelectDisciplines/" method="post">

    <% foreach (IUDICO.Common.Models.Shared.Group item in Model)
       {%>
       <div>
        <input type="radio" name="id" value="<%: item.Id %>" checked="checked" />
            <%: Html.Label(item.Name)%>
       </div>
    <% } %>

    <p>
        <input type="submit" value=<%=Localization.GetMessage("Show") %> />
    </p>
    


    </form>
    
    </fieldset>

    <% } %>
    <% else {%>
    <h2> <%=Localization.GetMessage("NoGroupHasBeenCreated")%> <%: Html.ActionLink(Localization.GetMessage("CreateGroup"), "Create", "Group")%>. </h2>
    <% } %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>

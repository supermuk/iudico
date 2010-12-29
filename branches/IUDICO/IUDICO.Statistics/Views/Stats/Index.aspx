<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<IUDICO.Common.Models.Group>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Group list</h2>

    <fieldset>
    <legend>Please, select one group : </legend>
    <form action="/Stats/SelectCurriculums/" method="post">

    <% foreach (IUDICO.Common.Models.Group item in Model)
       {%>
       <div>
        <input type="checkbox" name="id" value="<%: item.Id %>" />
            <%: Html.Label(item.Name)%>
       </div>
    <% } %>

    <div class="style5">
    <input type="submit" value="Show" />
    </div>

    </form>
    
    </fieldset>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    
     <style type="text/css">
        .style5
        {
        	cursor: inherit;
        	text-align: center;
            font-family: Century;       
        }
        
    </style>

</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Statistics.Models.InfoOnFirstPage>" %>



<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div><h1 style="text-align: center" class="style4"> Виберіть групу і навчальні плани для перегляду статистики </h1></div>
    <div>
    <fieldset>
    <legend>Виберіть групу: </legend>
    <h2> 
    <%= Html.DropDownList("Groups", ViewData["GroupsList"] as SelectList)%>
    </h2>
    </fieldset>
    </div>
    <div>
    <fieldset>
        <legend>Виберіть навчальні плани:</legend>

        <form action="/Stats/CurriculumInfoID/" method="post">

           <% foreach (IUDICO.Statistics.Models.Curriculum curr in Model.curriculums)  { %>
        <div>
        <input type="checkbox" name="IdList" value="<%: curr.CurriculumId - 1 %>" />
            <%: Html.Label(curr.CurriculumName)%>
            <%= Html.ActionLink("detalis", "CurriculumInfo", new { id = curr.CurriculumId - 1 })%>
        </div>
        <% } %>

        <div class="style5">
        <input type="submit" value="Show" class="style5" />
        </div>

        </form>
        
    </fieldset>
    </div>


</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">

    <style type="text/css">
        .style4
        {
            font-family: Century;
        }
        .style5
        {
        	cursor: inherit;
        	text-align: center;
            font-family: Century;
            
        }
        
    </style>

</asp:Content>

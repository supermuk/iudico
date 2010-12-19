﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Statistics.Models.InfoOnFirstPage>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	CurriculumInfoID
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%: Html.ActionLink("<- back", "Index") %>
    <fieldset>
    <legend>Статистика :</legend>

        <table border="4" cellpadding="4" cellspacing="4">

        <tr>
        <th>Студент </th>
        <% foreach (int i in ViewData["IDs"] as Int32[])
           { %>
        <th> 
        <%: 
            Html.ActionLink(Model.Curriculums[i].CurriculumName, "ThemesInfo", new { CurriculumID = i + 1, SelectedGroupID = 1 }) 
            %>
        </th>
        <% } %>
        <th> Загальні бали </th>
        <th> Відсотки </th>
        <th> Оцінка ECTS</th>
<%--        <th>Total</th>
        <th>Відсотки</th>
        <th>ECTS</th>--%>
        </tr>

        
        <% foreach (IUDICO.Statistics.Models.Student stud in Model.Group[0].Students)
           {  %>

            <tr> 
            <td><%: stud.Name%></td>
            <% foreach (int i in ViewData["IDs"] as Int32[])  { %>
                <td>
                    <% foreach (IUDICO.Statistics.Models.StudentCurriculumResult stCurrRes in Model.StudentCurriculumResult)
                       { %>
                           <% if (stCurrRes.Stud == stud && stCurrRes.CurriculumId == i+1)
                           {%>
                                <%: stCurrRes.StudentResult%> / <%: Model.Curriculums[i].GetMaxPointsFromCurriculum()%>
                           <% } %>
                    <% } %>
                
                </td>
            <% } %>
            <td> <%: Model.GetCurrentPointsFromAllCurriculums(stud, ViewData["IDs"] as Int32[])%> / <%: Model.GetMaxPointsFromAllCurriculums(ViewData["IDs"] as Int32[])%> </td>
            <td> <%: Math.Round(((Convert.ToDouble(Model.GetCurrentPointsFromAllCurriculums(stud, ViewData["IDs"] as Int32[])) / Convert.ToDouble(Model.GetMaxPointsFromAllCurriculums(ViewData["IDs"] as Int32[]))) * 100.0),2)%> % </td>
            <td> <%: Model.Ects(Math.Round(((Convert.ToDouble(Model.GetCurrentPointsFromAllCurriculums(stud, ViewData["IDs"] as Int32[])) / Convert.ToDouble(Model.GetMaxPointsFromAllCurriculums(ViewData["IDs"] as Int32[]))) * 100.0), 2))%> </td>
            </tr>

        <% } %>

        
        <%--<th> <%: Model.GetMaxPointsFromAllCurriculums() %> </th>--%>

        </table>

    </fieldset>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

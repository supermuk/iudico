<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Statistics.Models.ThemeTestResaultsModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ThemeTestResaults
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%: Html.ActionLink("<- back", "Index") %>
    <h2>Резльтати тесту</h2>
    <p>
        Студент:  <%: Model.SelectStudent.Name %>
    </p>
    <p>
        Назва теми:  <%: Model.SelectTheme.Name %>
    </p>
    <fieldset>
    <legend>Відповіді :</legend>

        <table border="4" cellpadding="4" cellspacing="4">

        <tr>
        <th> Номер питання </th>
        <th> Відповідь студента </th>
        <th> Правильна відповідь </th>
        <th> Вартість питання </th>
        <th> Кількість отриманих балів </th>
        </tr>

        
        <%  int i = 1;
            foreach (IUDICO.Statistics.Models.StudentQuestionResault questionResault in Model.Answers)
           {  %>

            <tr> 
            <td><%: i %></td>
            <% i++; %>
            <td><%: questionResault.StudentAnswer %></td>
            <td><%: questionResault.RightAnswer %></td>
            <td><%: questionResault.Costs %></td>
            <td>
            <% if (questionResault.StudentAnswer == questionResault.RightAnswer)
                { %>
                <%: questionResault.Costs %>
                <%}
                else
                {%>
                0
                <%}%>
            </td>
            </tr>
        <% } %>
        <tr>
        <td> </td>
        <td> </td>
        <td> </td>
        <td> Максимальна кількість балів <%: Model.SelectTheme.MaxPoint%> </td>
        <td> Кількість отримаих балів <%: Model.themeResault.StudentResult%> </td>
        </tr>
        </table>

    </fieldset>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
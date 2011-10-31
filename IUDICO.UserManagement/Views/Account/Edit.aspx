<%@ Assembly Name="IUDICO.UserManagement" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.UserManagement.Models.EditModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=IUDICO.Common.Localization.getMessage("Edit")%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%=IUDICO.Common.Localization.getMessage("Edit")%></h2>

    <form action="../Account/UploadAvatar/<%= Model.Id %>/" method="post" enctype="multipart/form-data">
        <table>
                <tr>
                    <th><%: IUDICO.Common.Localization.getMessage("DisplayAvatar")%></th>
                    <th><%: IUDICO.Common.Localization.getMessage("UploadAvatar")%></th>
                </tr>
                <tr>
                    <td><%= Html.Image("avatar", Model.Id, new {width = 100, height = 150}) %></td>
                    <td>
                        <input type="file" name="file" id="file" />
                        <input type="submit" value="Upload" />
                    </td>
                </tr>
            </table>
    </form>

    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(IUDICO.Common.Localization.getMessage("CorrectFollowingErrorAndTryAgain") + ":")%>
        
        <fieldset>
            <legend><%=IUDICO.UserManagement.Localization.getMessage("Fields")%></legend>
            
            <%: Html.EditorForModel() %>
            
            <p>
                <input type="submit" value=<%=IUDICO.Common.Localization.getMessage("Save") %> />
            </p>
        </fieldset>
        
    <% } %>


    <div>
        <%: Html.ActionLink(IUDICO.Common.Localization.getMessage("UpgradeToAdmin"), "TeacherToAdminUpgrade", new { id = Model.Id })%>|
        <%: Html.ActionLink(IUDICO.Common.Localization.getMessage("BackToAccount"), "Index")%>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

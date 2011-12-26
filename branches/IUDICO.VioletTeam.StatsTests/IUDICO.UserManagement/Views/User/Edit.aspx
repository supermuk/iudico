<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.UserManagement.Models.EditUserModel>" %>
<%@ Assembly Name="IUDICO.UserManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=IUDICO.UserManagement.Localization.getMessage("EditUser")%> <%= Model.Username %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%=IUDICO.UserManagement.Localization.getMessage("EditUser")%> <%= Model.Username %></h2>

    <form action="../User/UploadAvatar/<%= Model.Id %>/" method="post" enctype="multipart/form-data">
        <table>
                <tr>
                    <th>Avatar</th>
                    <th>Upload new Avatar</th>
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
        <%: Html.ValidationSummary(IUDICO.UserManagement.Localization.getMessage("CorrectFollowingErrorAndTryAgain") + ":")%>
        
        <fieldset>
            <legend><%=IUDICO.UserManagement.Localization.getMessage("Fields")%></legend>

            <%: Html.EditorForModel() %>
            
            <p>
                <input type="submit" value=<%=IUDICO.UserManagement.Localization.getMessage("Save")%> />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%: Html.ActionLink(IUDICO.UserManagement.Localization.getMessage("BackToList"), "Index")%>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

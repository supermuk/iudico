<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.UserManagement.Models.EditUserModel>" %>
<%@ Assembly Name="IUDICO.UserManagement" %>
<%@ Import Namespace="IUDICO.UserManagement" %>
<%@ Import Namespace="IUDICO.Common" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=Localization.GetMessage("EditUser")%> <%=Model.Username%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%=Localization.GetMessage("EditUser")%> <%=Model.Username%></h2>

    <form action="../User/UploadAvatar?id=<%=Model.Id%>" method="post" enctype="multipart/form-data">
        <table>
                <tr>
                    <th>Avatar</th>
                    <th>Upload new Avatar</th>
                </tr>
                <tr>
                    <td><%=Html.Image("avatar", Model.Id, new {width = 100, height = 150})%><br />
                    <%:Html.ActionLink(Localization.GetMessage("DeleteAvatar"), "DeleteAvatar", new {Id = Model.Id})%></td>
                    <td>
                        <input type="file" name="file" id="file" />
                        <input type="submit" value="Upload" />
                    </td>
                </tr>
            </table>
    </form>

    <%
        using (Html.BeginForm())
        {%>
        <%:Html.ValidationSummary(Localization.GetMessage("CorrectFollowingErrorAndTryAgain") + ":")%>
        
        <fieldset>
            <legend><%=Localization.GetMessage("Fields")%></legend>

            <%:Html.EditorForModel()%>
            
            <p>
                <input type="submit" value=<%=Localization.GetMessage("Save")%> />
            </p>
        </fieldset>

    <%
        }%>

    <div>
        <%:Html.ActionLink(Localization.GetMessage("BackToList"), "Index")%>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

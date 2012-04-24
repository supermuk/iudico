<%@ Assembly Name="IUDICO.UserManagement" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.UserManagement.Models.EditModel>" %>
<%@ Import Namespace="IUDICO.UserManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=Localization.GetMessage("Edit")%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%=Localization.GetMessage("Edit")%></h2>

    <form action="../Account/UploadAvatar/" method="post" enctype="multipart/form-data">
        <table>
                <tr>
                    <th><%:Localization.GetMessage("DisplayAvatar")%></th>
                    <th><%:Localization.GetMessage("UploadAvatar")%></th>
                </tr>
                <tr>
                    <td><%=Html.Image("avatar", Model.Id, new {width = 100, height = 150})%></td>
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
        <%:Html.ActionLink(Localization.GetMessage("BackToAccount"), "Index")%>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="MyInfo.aspx.cs" Inherits="User_MyInfo" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <table>
        <tr>
            <td>
                <asp:Label ID="Label_PageCaption" runat="server" Font-Bold="True" Font-Size="XX-Large"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label_PageDescription" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label_PageMessage" runat="server" ForeColor="#CC0000"></asp:Label>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <asp:Label ID="Label_FirstName" runat="server" Text="First name:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBox_FirstName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label_SecondName" runat="server" Text="Second name:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBox_SecondName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label_Login" runat="server" Text="Login:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBox_Login" runat="server" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label_Email" runat="server" Text="Email:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBox_Email" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label_RolesStatic" runat="server" Text="Roles:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label_Roles" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label_Groups" runat="server" Text="Groups:"></asp:Label>
            </td>
            <td>
                <asp:BulletedList ID="BulletedList_Groups" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button_Update" Text="Update" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:ChangePassword ID="ChangePassword" runat="server" ContinueDestinationPageUrl="<%= Request.RawUrl %>" />
            </td>
        </tr>
    </table>
</asp:Content>

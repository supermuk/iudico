<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="MyInfo.aspx.cs" Inherits="User_MyInfo" %>

<%@ Register assembly="BoxOver" namespace="BoxOver" tagprefix="boxover" %>

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
                <boxover:BoxOver ID="BoxOver1" runat="server" Body="Your name!" 
                    ControlToValidate="TextBox_FirstName" Header="Help!" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label_SecondName" runat="server" Text="Second name:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBox_SecondName" runat="server"></asp:TextBox>
                <boxover:BoxOver ID="BoxOver2" runat="server" Body="Your surname!" 
                    ControlToValidate="TextBox_SecondName" Header="Help!" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label_Login" runat="server" Text="Login:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBox_Login" runat="server" ReadOnly="true"></asp:TextBox>
                <boxover:BoxOver ID="BoxOver3" runat="server" Body="Your LogIn!" 
                    ControlToValidate="TextBox_Login" Header="Help!" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label_Email" runat="server" Text="Email:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBox_Email" runat="server"></asp:TextBox>
                <boxover:BoxOver ID="BoxOver4" runat="server" Body="Your E-mail!" 
                    ControlToValidate="TextBox_Email" Header="Help!" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label_RolesStatic" runat="server" Text="Roles:"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label_Roles" runat="server"></asp:Label>
                <boxover:BoxOver ID="BoxOver5" runat="server" Body="Your roles!" 
                    ControlToValidate="Label_Roles" Header="Help!" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label_Groups" runat="server" Text="Groups:"></asp:Label>
            </td>
            <td>
                <boxover:BoxOver ID="BoxOver6" runat="server" Body="Your Groups!" 
                    ControlToValidate="Label_Groups" Header="Help!" />
                <asp:BulletedList ID="BulletedList_Groups" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button_Update" Text="Update" runat="server" />
                <boxover:BoxOver ID="BoxOver7" runat="server" 
                    Body="Click to update information!" ControlToValidate="Button_Update" 
                    Header="Help!" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:ChangePassword ID="ChangePassword" runat="server" ContinueDestinationPageUrl="<%= Request.RawUrl %>" />
            </td>
        </tr>
    </table>
</asp:Content>

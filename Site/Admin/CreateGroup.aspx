<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CreateGroup.aspx.cs" Inherits="CreateGroup" Title="Untitled Page" %>

<asp:Content ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="Server">
    <table>
        <tr>
            <td>
                Group Name:
            </td>
            <td>
                <asp:TextBox ID="tbGroupName" runat="server" />
            </td>
        </tr>
    </table>
    <asp:RequiredFieldValidator 
        ID="GroupNameValidator" 
        ControlToValidate="tbGroupName"
        Display="Dynamic"
        runat="server" />
    <br />
    <asp:Button ID="btnCreate" Text="Create" runat="server" OnClick="btnCreate_Click" />
</asp:Content>


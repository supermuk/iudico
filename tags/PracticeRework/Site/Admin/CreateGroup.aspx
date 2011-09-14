<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CreateGroup.aspx.cs" Inherits="CreateGroup" Title="Untitled Page" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="Server">
    
    <h1>Create IUDICO Group</h1>
    Type desired name for group and press 'Create' button <br />
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


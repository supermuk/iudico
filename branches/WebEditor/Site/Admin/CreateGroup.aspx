<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CreateGroup.aspx.cs" Inherits="CreateGroup" Title="Untitled Page" %>

<%@ Register assembly="BoxOver" namespace="BoxOver" tagprefix="boxover" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="Server">
    
    <h1>Create IUDICO Group</h1>
    <p class="descriptions">Type desired name for group and press 'Create' button</p>
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
    <boxover:BoxOver ID="BoxOver1" runat="server" body="Please, enter group name!" controltovalidate="tbGroupName" header="Help!" />
    <boxover:BoxOver ID="BoxOver2" runat="server" Body="Click to add group!" controltovalidate="btnCreate" Header="Help!" />
</asp:Content>

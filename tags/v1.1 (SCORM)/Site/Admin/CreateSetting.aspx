<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
CodeFile="CreateSetting.aspx.cs" Inherits="Admin_CreateSetting" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <h1>
        Create Setting</h1>
    <p class="descriptions">
        To create setting fill in the form and press 'Create Setting' button
    </p>
    
    <table>
        <tr>
            <td><asp:Label ID="Label1" Text="Name:" runat="server" /></td>
            <td><asp:TextBox ID="tbName" runat="server" /></td>
        </tr>
        <tr>
            <td><asp:Label ID="Label2" Text="Value:" runat="server" /></td>
            <td><asp:TextBox ID="tbValue" runat="server" /></td>
        </tr>
    </table>
    
    <div style="text-align: left">
        <asp:Button runat="server" Text="Create Setting" ID="btnCreate" />
        
        <p>
            <asp:Label runat="server" ID="lbErrors" ForeColor="Red" />
        </p>
    </div>
</asp:Content>

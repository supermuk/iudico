<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EditSetting.aspx.cs" Inherits="Admin_EditSetting" Title="Edit Setting" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

<h1><asp:Label ID="lbName" runat="server" /></h1>

<table>
    <tr>
        <td><asp:Label ID="Label2" Text="Value:" runat="server" /></td>
        <td><asp:TextBox ID="tbValue" runat="server" /></td>
    </tr>
</table>

<asp:Button ID="btnApply" Text="Apply" runat="server" />
<asp:Button ID="btnCancel" Text="Cancel" runat="server" />

</asp:Content>


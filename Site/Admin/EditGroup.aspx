<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EditGroup.aspx.cs" Inherits="Admin_EditGroup" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

<asp:Panel runat="server">
    <asp:Label ID="lbGroupName" Text="Group Name:" runat="server" />
    &nbsp;
    <asp:TextBox ID="tbGroupName" runat="server" />
    
    <br />
    <asp:Button ID="btnApply" Text="Apply" runat="server" />
</asp:Panel>
<asp:Panel runat="server">
    <h2><asp:Label ID="lbGroupUsers" runat="server" Text="test" /></h2>
    <i:UserList ID="UserList" runat="server" />
</asp:Panel>

</asp:Content>


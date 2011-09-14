<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EditGroup.aspx.cs" Inherits="Admin_EditGroup" Title="Untitled Page" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

<asp:Panel runat="server">
    To rename group type new name and press 'Apply' button to submit changes<br />
    <asp:Label ID="lbGroupName" Text="Group Name:" runat="server" />
    &nbsp;
    <asp:TextBox ID="tbGroupName" runat="server" />
    
    <br />
    <asp:Button ID="btnApply" Text="Apply" runat="server" />
</asp:Panel>
<asp:Panel runat="server">
    <h2><asp:Label ID="lbGroupUsers" runat="server" Text="test" /></h2>
    Press 'Exclude' button to remove user from group<br />
    Click on user's login link to reach details screen
    <i:UserList ID="UserList" runat="server" />
</asp:Panel>

</asp:Content>


<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Settings.aspx.cs"
Inherits="Admin_Settings" Title="Settings" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

<h1>Settings available in the system</h1>
<p class="descriptions">
Press 'Add' to add new setting <br />
Press 'Remove' to remove setting <br />
</p>

<div style="text-align:left">
    <asp:TextBox ID="tbSearchPattern" runat="server" /> <asp:Button ID="btnSearch" Text="Search" runat="server" />
</div>

<i:SettingList ID="SettingList" runat="server" />

<div style="text-align: left">
    <asp:Button ID="btnCreateSetting" runat="server" Text="Create" />
</div>

</asp:Content>


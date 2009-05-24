<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Groups.aspx.cs" Inherits="Admin_Groups" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h1>Groups currently available in the system</h1>
<p class="descriptions">
Press 'Remove' button to remove certain group <br />
Press 'Create' button to create new group<br />
Click on group link to see details of a group <br />
</p>
<i:GroupList ID="GroupList" runat="server" />

<div style="text-align:left">
<asp:Button ID="btnCreateGroup" Text="Create" runat="server" />
</div>

</asp:Content>
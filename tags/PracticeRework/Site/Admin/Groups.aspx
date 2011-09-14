<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Groups.aspx.cs" Inherits="Admin_Groups" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h2>Groups currently available in the system:</h2>
Press 'Remove' button to remove certain group <br />
Press 'Create' button to create new group<br />
Click on group link to see details of a group <br /><br />
<i:GroupList ID="GroupList" runat="server" />
<br />

<asp:Button ID="btnCreateGroup" Text="Create" runat="server" />

</asp:Content>
<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Users.aspx.cs" Inherits="Admin_Users" Title="Users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

<h1>Users available in the system:</h1>
<i:UserList ID="UserList" runat="server" />

<br />
<asp:Button ID="btnCreateUser" runat="server" Text="Create" />

</asp:Content>


<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Users.aspx.cs" Inherits="Admin_Users" Title="Users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

<h1>Users available in the system:</h1>
Press 'create' button to create new one <br />
Press 'remove' button for certain user to delete it <br />
Click on link with login of user to reach the screen with user's details <br /> <br />
<i:UserList ID="UserList" runat="server" />

<br />
<asp:Button ID="btnCreateUser" runat="server" Text="Create" />

</asp:Content>


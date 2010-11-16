<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Users.aspx.cs" Inherits="Admin_Users" Title="Users" %>

<%@ Register assembly="BoxOver" namespace="BoxOver" tagprefix="boxover" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

<h1>Users available in the system</h1>
<p class="descriptions">
Press 'create' button to create new one <br />
Press 'remove' button for certain user to delete it <br />
Click on link with login of user to reach the screen with user's details <br />
    <boxover:BoxOver ID="BoxOver3" runat="server" Body="Click to create new user!" 
        ControlToValidate="btnCreateUser" Header="Help!" />
</p>
<div style="text-align:left">
<asp:TextBox ID="tbSearchPattern" runat="server" />
<asp:Button ID="btnSearch" Text="Search" runat="server" />
    <boxover:BoxOver ID="BoxOver1" runat="server" Body="Enter search creteria!" 
        ControlToValidate="tbSearchPattern" Header="Help!" />
    <boxover:BoxOver ID="BoxOver2" runat="server" Body="Click to start search!" 
        ControlToValidate="btnSearch" Header="Help!" />
</div>
<i:UserList ID="UserList" runat="server" />

<div style="text-align: left">
<asp:Button ID="btnCreateUser" runat="server" Text="Create" />
</div>

</asp:Content>


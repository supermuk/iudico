<%@ Page Language="C#" Title="My Permissions" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MyPermissions.aspx.cs" Inherits="User_MyPermissions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">


<asp:Panel runat="server">
    <h2><asp:Label ID="CoursePermissionsLabel" runat="server"/></h2>
    <i:UserPermissions ID="CoursePermissions" runat="server" ObjectType="COURSE" />
</asp:Panel>

<br />
<asp:Panel runat="server">
    <h2><asp:Label ID="ThemePermissionsLabel" runat="server" /></h2>
    <i:UserPermissions ID="ThemePermissions" runat="server" ObjectType="THEME" />
</asp:Panel>

<br />
<asp:Panel ID="Panel1" runat="server">
    <h2><asp:Label ID="GroupPermissionsLabel" runat="server" /></h2>
    <i:UserPermissions ID="GroupPermissions" runat="server" ObjectType="GROUP" />
</asp:Panel>

</asp:Content>


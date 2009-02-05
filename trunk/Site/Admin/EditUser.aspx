<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EditUser.aspx.cs" Inherits="Admin_EditUser" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

<h2><asp:Label ID="lbUserRoles" runat="server" /></h2>

<table border="1">
    <tr>
        <th>Role</th>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lbStudentRoleTitle" runat="server" />
        </td>
        <td>
            <asp:CheckBox AutoPostBack="true" ID="cbStudentRole" runat="server" />
        </td>
    </tr>
    
    <tr>
        <td>
            <asp:Label ID="lbTrainerRoleTitle" runat="server" />
        </td>
        <td>
            <asp:CheckBox AutoPostBack="true" ID="cbTrainerRole" runat="server" />
        </td>
    </tr>
    
    <tr>
        <td>
            <asp:Label ID="lbLectorRoleTitle" runat="server" />
        </td>
        <td>
            <asp:CheckBox AutoPostBack="true" ID="cbLectorRole" runat="server" />
        </td>
    </tr>
    
    <tr>
        <td>
            <asp:Label ID="lbAdminRoleTitle" runat="server" />
        </td>
        <td>
            <asp:CheckBox AutoPostBack="true" ID="cbAdminRole" runat="server" />
        </td>
    </tr>    
    
    <tr>
        <td>
            <asp:Label ID="lbSuperAdminRoleTitle" runat="server" />
        </td>
        <td>
            <asp:CheckBox AutoPostBack="true" ID="cbSuperAdminRole" runat="server" />
        </td>
    </tr>            
</table>
<br />
<asp:Button ID="btnApply" Text="Apply" runat="server" />

<h2><asp:Label ID="lbUserGroups" runat="server" /></h2>
<asp:GridView 
    ID="gvGroups"
    AutoGenerateColumns="false"
    Width="100%"
    OnRowDataBound="gvGroups_OnRowDataBound"
    runat="server">
    <Columns>
        <asp:TemplateField ControlStyle-Width="80%">
            <HeaderTemplate>
                <asp:Label Text="Group" runat="server" />
            </HeaderTemplate>
            <ItemTemplate>
                <asp:LinkButton runat="server" ID="lnkGroupName" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField ControlStyle-Width="20%">
            <ItemTemplate>
                <asp:LinkButton runat="server" Text="Exclude" ID="lnkExclude" />
            </ItemTemplate>
        </asp:TemplateField>       
    </Columns>
</asp:GridView>
<br />

<asp:Button ID="btnInclude" Text="Include..." runat="server" />

</asp:Content>


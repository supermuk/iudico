<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ThemePages.aspx.cs" Inherits="ThemePages" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:Label ID="headerLabel" runat="server" Font-Bold="True" Font-Size="XX-Large"></asp:Label>
    <br />
    <asp:Label ID="descriptionLabel" runat="server"></asp:Label>
    <br />
    <asp:Table ID="pagesTable" runat="server" GridLines="Both">
        <asp:TableRow runat="server">
            <asp:TableCell runat="server" HorizontalAlign="Center"></asp:TableCell>
            <asp:TableCell runat="server" HorizontalAlign="Center">Page Name</asp:TableCell>
            <asp:TableCell runat="server" HorizontalAlign="Center">Page Rank</asp:TableCell>
            <asp:TableCell runat="server" HorizontalAlign="Center"></asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>


<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CourseBehavior.aspx.cs" Inherits="CourseBehavior" Title="Untitled Page" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:Label ID="_headerLabel" runat="server" Font-Bold="True" Font-Size="XX-Large"></asp:Label>
    <br />
    <asp:Label ID="_descriptionLabel" runat="server"></asp:Label>
    <br />
    <asp:Table ID="_courseBehavior" runat="server" GridLines="Both">
        <asp:TableRow runat="server">
            <asp:TableCell runat="server" HorizontalAlign="Center"></asp:TableCell>
            <asp:TableCell runat="server" HorizontalAlign="Center">Name</asp:TableCell>
            <asp:TableCell runat="server" HorizontalAlign="Center">IsControl</asp:TableCell>
            <asp:TableCell runat="server" HorizontalAlign="Center">PageOrder</asp:TableCell>
            <asp:TableCell runat="server" HorizontalAlign="Center">Page Count To Show</asp:TableCell>
            <asp:TableCell runat="server" HorizontalAlign="Center">Max Count To Submit</asp:TableCell>
            <asp:TableCell runat="server" HorizontalAlign="Center"></asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <asp:Button ID="_saveButton" runat="server" Text="Save"/>
</asp:Content>


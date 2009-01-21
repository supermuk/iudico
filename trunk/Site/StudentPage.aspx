<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="StudentPage.aspx.cs" Inherits="StudentPage" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:Button ID="openTest" runat="server" Text="Open Test" />
    <asp:Button ID="showResult" runat="server" Text="Show Result" />
    <asp:TreeView ID="curriculumTreeView" runat="server" ImageSet="XPFileExplorer" 
        NodeIndent="15">
        <ParentNodeStyle Font-Bold="False" />
        <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
        <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" 
            HorizontalPadding="0px" VerticalPadding="0px" />
        <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" 
            HorizontalPadding="2px" NodeSpacing="0px" VerticalPadding="2px" />
    </asp:TreeView>
</asp:Content>


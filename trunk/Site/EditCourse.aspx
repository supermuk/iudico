<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EditCourse.aspx.cs" Inherits="EditCourse" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<title>Edit Course</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:Button ID="moveUp" runat="server" Text="Move Up" />
    <asp:Button ID="moveDown" runat="server" Text="Move Down" />
    <asp:TreeView ID="courseTreeView" runat="server" ImageSet="XPFileExplorer" 
        NodeIndent="15">
        <ParentNodeStyle Font-Bold="False" />
        <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
        <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" 
            HorizontalPadding="0px" VerticalPadding="0px" />
        <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" 
            HorizontalPadding="2px" NodeSpacing="0px" VerticalPadding="2px" />
    </asp:TreeView>
    <asp:Button ID="delete" runat="server" Text="Delete" Enabled = "false"/>
    <asp:Button ID="rename" runat="server" Text="Rename" />
    <asp:TextBox ID="renameTextBox" runat="server" Visible = "false"></asp:TextBox>
    </asp:Content>


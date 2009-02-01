<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="CourseEdit.aspx.cs" Inherits="CourseEdit" %>

<%@ Register TagPrefix="iudico" Namespace="IUDICO.DataModel.Controllers" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label ID="Label_Name" runat="server" Text="Name" Style="position: absolute;
        top: 120px; left: 0px;"></asp:Label>
    <asp:Label ID="Label_Notify" runat="server" Style="position: absolute; top: 280px;
        left: 0px;"></asp:Label>
    <asp:Label ID="Label_Description" runat="server" Text="Description" Style="position: absolute;
        top: 160px; left: 0px;"></asp:Label>
    <asp:TextBox ID="TextBox_Name" runat="server" Style="position: absolute; top: 120px;
        left: 100px; bottom: 470px;"></asp:TextBox>
    <asp:TextBox ID="TextBox_Description" runat="server" Style="position: absolute; top: 160px;
        left: 100px;"></asp:TextBox>
    <asp:Button ID="Button_Import" runat="server" Text="Import Course" Style="position: absolute;
        top: 240px; left: 60px;" />
    <asp:Button ID="Button_Delete" runat="server" Text="Delete" Style="position: absolute;
        top: 120px; left: 460px;" />
    <asp:FileUpload ID="FileUpload_Course" runat="server" Style="position: absolute;
        top: 200px; left: 0px;" />
    <asp:Label ID="Label_Courses" runat="server" Style="position: absolute; top: 120px;
        left: 300px;" Text="Available Courses:"></asp:Label>
    <iudico:IdentityTreeView ID="TreeView_Courses" runat="server" Style="position: absolute;
        top: 140px; left: 300px;" ImageSet="Custom" ShowCheckBoxes="Root">
    </iudico:IdentityTreeView>
</asp:Content>

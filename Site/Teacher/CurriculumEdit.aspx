<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="CurriculumEdit.aspx.cs" Inherits="CurriculumEdit" %>

<%@ Register TagPrefix="iudico" Namespace="IUDICO.DataModel.Controllers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label ID="Label_Notify" runat="server" Style="position: absolute; top: 60px;
        left: 0px;"></asp:Label>
    <asp:TextBox ID="TextBox_Name" runat="server" Style="position: absolute; top: 120px;
        left: 650px;"></asp:TextBox>
    <asp:TextBox ID="TextBox_Description" runat="server" Style="position: absolute; top: 170px;
        left: 650px;"></asp:TextBox>
    <asp:Label ID="Label_Description" runat="server" Style="position: absolute; top: 170px;
        left: 550px;" Text="Description:"></asp:Label>
    <asp:Label ID="Label_Name" runat="server" Style="position: absolute; top: 120px;
        left: 550px;" Text="Name:"></asp:Label>
    <asp:Label ID="Label_Courses" runat="server" Style="position: absolute; top: 120px;
        left: 0px;" Text="Available Courses:"></asp:Label>
    <asp:Label ID="Label_Curriculums" runat="server" Style="position: absolute; top: 120px;
        left: 300px;" Text="Available Curriculums:"></asp:Label>
    <iudico:IdentityTreeView ID="TreeView_Curriculums" runat="server" Style="position: absolute;
        top: 140px; left: 300px;" ImageSet="XPFileExplorer">
        <SelectedNodeStyle BackColor="#00CC00" />
    </iudico:IdentityTreeView>
    <iudico:IdentityTreeView ID="TreeView_Courses" runat="server" Style="position: absolute;
        top: 140px; left: 0px;" ImageSet="XPFileExplorer" ShowCheckBoxes="Leaf">
    </iudico:IdentityTreeView>
    <asp:Button ID="Button_CreateCurriculum" runat="server" Style="position: absolute;
        top: 220px; left: 550px;" Text="Create new curriculum" />
    <asp:Button ID="Button_Modify" runat="server" Style="position: absolute; top: 340px;
        left: 550px;" Text="Modify" />
    <asp:Button ID="Button_AddStage" runat="server" Style="position: absolute; top: 260px;
        left: 550px;" Text="Add new stage" />
    <asp:Button ID="Button_Delete" runat="server" Style="position: absolute; top: 300px;
        left: 550px;" Text="Delete" />
    <asp:Button ID="Button_AddTheme" runat="server" Style="position: absolute; top: 120px;
        left: 170px;" Text="Add theme" />
</asp:Content>

<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="CurriculumAssignment.aspx.cs" Inherits="CurriculumAssignment" %>

<%@ Register TagPrefix="iudico" Namespace="IUDICO.DataModel.Controllers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label ID="Label_Notify" runat="server" Style="position: absolute; top: 60px;
        left: 0px;"></asp:Label>
    <asp:ListBox ID="ListBox_Groups" runat="server" Style="position: absolute; top: 140px;
        left: 250px;"></asp:ListBox>
    <asp:ListBox ID="ListBox_Curriculums" runat="server" Style="position: absolute; top: 140px;
        left: 0px;"></asp:ListBox>
    <asp:Label ID="Label_Assigments" runat="server" Style="position: absolute; top: 120px;
        left: 400px;" Text="My Assigments"></asp:Label>
    <asp:Button ID="Button_Unsign" runat="server" Style="position: absolute; top: 340px;
        left: 130px;" Text="Unsign" />
    <iudico:IdentityTreeView ID="TreeView_Assigments" runat="server" Style="position: absolute;
        top: 140px; left: 400px;">
    </iudico:IdentityTreeView>
    <asp:Label ID="Label_Curriculums" runat="server" Style="position: absolute; top: 120px;
        left: 0px;" Text="My Curriculums"></asp:Label>
    <asp:Label ID="Label_Groups" runat="server" Style="position: absolute; top: 120px;
        left: 250px;" Text="My Groups"></asp:Label>
    <asp:Button ID="Button_Assign" runat="server" Style="position: absolute; top: 300px;
        left: 130px;" Text="Assign" />
    <asp:Button ID="Button_SwitchView" runat="server" Style="position: absolute; top: 140px;
        left: 550px;" Text="Switch View" />
</asp:Content>

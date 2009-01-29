<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="CurriculumAssigment.aspx.cs" Inherits="CurriculumAssigment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ListBox ID="ListBox_Groups" runat="server" Style="position: absolute; top: 140px;
        left: 250px;"></asp:ListBox>
    <asp:ListBox ID="ListBox_Curriculums" runat="server" Style="position: absolute; top: 140px;
        left: 0px;"></asp:ListBox>
    <asp:Label ID="Label_Assigments" runat="server" Style="position: absolute; top: 120px;
        left: 400px;" Text="My Assigments"></asp:Label>
    <asp:Button ID="Button_Unsign" runat="server" Style="position: absolute; top: 340px;
        left: 130px;" Text="Unsign" />
    <asp:TreeView ID="TreeView_Assigments" runat="server" Style="position: absolute;
        top: 140px; left: 400px;" ShowCheckBoxes="Leaf">
    </asp:TreeView>
    <asp:Label ID="Label_Curriculums" runat="server" Style="position: absolute; top: 120px; left: 0px;"
        Text="My Curriculums"></asp:Label>
    <asp:Label ID="Label_Groups" runat="server" Style="position: absolute; top: 120px;
        left: 250px;" Text="My Groups"></asp:Label>
    <asp:Button ID="Button_Assign" runat="server" Style="position: absolute; top: 300px;
        left: 130px;" Text="Assign" />
    <asp:Button ID="Button_SwitchView" runat="server" Style="position: absolute; top: 140px;
        left: 550px;" Text="Switch View" />
</asp:Content>

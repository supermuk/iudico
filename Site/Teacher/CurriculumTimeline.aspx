<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="CurriculumTimeline.aspx.cs" Inherits="CurriculumTimeline" %>

<%@ Register TagPrefix="iudico" Namespace="IUDICO.DataModel.Controllers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <iudico:IdentityTreeView ID="TreeView_Curriculum" runat="server" Style="position: absolute;
        top: 160px; left: 0px;" ImageSet="XPFileExplorer">
        <SelectedNodeStyle BackColor="#00CC00" />
    </iudico:IdentityTreeView>
    <asp:Label ID="Label_Notify" runat="server" Style="position: absolute; top: 120px;
        left: 0px;"></asp:Label>
    <asp:Label ID="Label_Since" runat="server" Style="position: absolute; top: 160px;
        left: 300px;">Since</asp:Label>
    <asp:Label ID="Label_Till" runat="server" Style="position: absolute; top: 160px;
        left: 480px;">Till</asp:Label>
    <asp:Button ID="Button_Remove" runat="server" Style="position: absolute; top: 240px;
        left: 480px;" Text="Remove"></asp:Button>
    <asp:Button ID="Button_Grant" runat="server" Style="position: absolute; top: 240px;
        left: 300px;" Text="Grant"></asp:Button>
    <asp:TextBox ID="TextBox_Since" runat="server" Style="position: absolute; top: 200px;
        left: 300px">
    </asp:TextBox>
    <asp:TextBox ID="TextBox_Till" runat="server" Style="position: absolute; top: 200px;
        left: 480px">
    </asp:TextBox>
</asp:Content>

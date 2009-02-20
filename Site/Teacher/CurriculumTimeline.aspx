<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="CurriculumTimeline.aspx.cs" Inherits="CurriculumTimeline" %>

<%@ Register TagPrefix="iudico" Namespace="IUDICO.DataModel.Controllers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Table ID="Table_Time" runat="server" Style="position: absolute; top: 160px; left: 300px;">
    </asp:Table>
    <iudico:IdentityTreeView ID="TreeView_Curriculum" runat="server" Style="position: absolute;
        top: 160px; left: 0px;" ImageSet="XPFileExplorer">
        <SelectedNodeStyle BackColor="#00CC00" />
    </iudico:IdentityTreeView>
    <asp:Label ID="Label_Notify" runat="server" Style="position: absolute; top: 120px;
        left: 0px;"></asp:Label>
</asp:Content>

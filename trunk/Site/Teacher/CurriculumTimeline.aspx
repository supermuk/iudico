<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="CurriculumTimeline.aspx.cs" Inherits="CurriculumTimeline" %>

<%@ Register TagPrefix="iudico" Namespace="IUDICO.DataModel.Controllers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <iudico:IdentityTreeView ID="TreeView_Curriculum" runat="server" Style="position: absolute;
        top: 160px; left: 0px;" ShowCheckBoxes="All">
    </iudico:IdentityTreeView>
    <asp:Label ID="Label_CurriculumForGroup" runat="server" Style="position: absolute;
        top: 120px; left: 0px;">Select assigment:</asp:Label>
    <asp:TextBox ID="TextBox_TimeTill" runat="server" Style="position: absolute; top: 200px;
        left: 480px;"></asp:TextBox>
    <asp:TextBox ID="TextBox_DateSince" runat="server" Style="position: absolute; top: 150px;
        left: 300px;"></asp:TextBox>
    <asp:TextBox ID="TextBox_DateTill" runat="server" Style="position: absolute; top: 150px;
        left: 480px;"></asp:TextBox>
    <asp:TextBox ID="TextBox_TimeSince" runat="server" Style="position: absolute; top: 200px;
        left: 300px;"></asp:TextBox>
    <asp:Label ID="Label_Time" runat="server" Style="position: absolute; top: 200px;
        left: 250px;">Time</asp:Label>
    <asp:Label ID="Label_Date" runat="server" Style="position: absolute; top: 150px;
        left: 250px;">Date</asp:Label>
    <asp:Label ID="Label_Since" runat="server" Style="position: absolute; top: 130px;
        left: 300px;">Since</asp:Label>
    <asp:Label ID="Label_Till" runat="server" Style="position: absolute; top: 130px;
        left: 480px;">Till</asp:Label>
    <asp:DropDownList ID="DropDownList_Assigments" runat="server" Style="position: absolute;
        top: 120px; left: 130px; height: 22px;">
    </asp:DropDownList>
    <asp:DropDownList ID="DropDownList_Operation" runat="server" Style="position: absolute;
        top: 250px; left: 300px;">
    </asp:DropDownList>
    <asp:Button ID="Button_Grant" runat="server" Style="position: absolute; top: 250px;
        left: 480px;" Text="Grant;">
    </asp:Button>
</asp:Content>

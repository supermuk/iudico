<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CurriculumTimeline.aspx.cs"
    Inherits="CurriculumTimeline" %>
<%@ Reference Page="~/CurriculumAssigment.aspx" %>
<%@ Register TagPrefix="iudico" Namespace="IUDICO.DataModel.Controllers" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Button ID="Button_Grant" runat="server" Style="position: absolute; top: 247px; left: 457px;
        height: 22px;" Text="Grant" />
    <iudico:IdentityTreeView ID="TreeView_Curriculum" runat="server" Style="position: absolute;
        top: 76px; left: 23px; height: 202px; width: 156px;" ShowCheckBoxes="All">
    </iudico:IdentityTreeView>
    <asp:Label ID="Label_CurriculumForGroup" runat="server" Style="position: absolute;
        top: 39px; left: 20px; height: 25px; width: 150px; bottom: 378px;"></asp:Label>
    <asp:TextBox ID="TextBox_TimeTill" runat="server" Style="position: absolute; top: 194px;
        left: 479px;"></asp:TextBox>
    <asp:TextBox ID="TextBox_DateSince" runat="server" Style="position: absolute; top: 149px;
        left: 309px; height: 22px;"></asp:TextBox>
    <asp:TextBox ID="TextBox_DateTill" runat="server" Style="position: absolute; top: 149px;
        left: 479px; height: 22px;"></asp:TextBox>
    <asp:TextBox ID="TextBox_TimeSince" runat="server" Style="position: absolute; top: 194px;
        left: 309px; right: 274px;"></asp:TextBox>
    <asp:Label ID="Label_Time" runat="server" Style="position: absolute; top: 193px;
        left: 247px; height: 18px; width: 45px; bottom: 231px;">Time</asp:Label>
    <asp:Label ID="Label_Date" runat="server" Style="position: absolute; top: 148px;
        left: 247px; height: 19px; width: 45px;">Date</asp:Label>
    <asp:Label ID="Label_Since" runat="server" Style="position: absolute; top: 98px;
        left: 310px; height: 20px; width: 65px; bottom: 324px;">Since</asp:Label>
    <asp:Label ID="Label_Till" runat="server" Style="position: absolute; top: 96px; left: 483px;
        height: 19px; width: 37px; bottom: 327px;">Till</asp:Label>
    <asp:DropDownList ID="DropDownList_Operation" runat="server" Style="position: absolute; top: 246px;
        left: 339px; height: 22px;" Enabled="False">
    </asp:DropDownList>
    </form>
</body>
</html>

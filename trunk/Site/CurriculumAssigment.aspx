<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CurriculumAssigment.aspx.cs"
    Inherits="CurriculumAssigment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #form1
        {
            height: 333px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ListBox ID="ListBox_Groups" runat="server" Style="position: absolute; top: 82px; left: 183px;
        height: 138px; width: 114px;"></asp:ListBox>
    <asp:ListBox ID="ListBox_Curriculums" runat="server" Style="position: absolute; top: 81px; left: 21px;
        height: 138px; width: 121px;"></asp:ListBox>
    <asp:Label ID="Label_Assigments" runat="server" Style="position: absolute; top: 34px; left: 371px;
        height: 18px;" Text="My Assigments"></asp:Label>
    <asp:Button ID="Button_Timeline" runat="server" Style="position: absolute; top: 285px; left: 487px;
        height: 24px;" Text="Timelene managment" 
        PostBackUrl="~/CurriculumTimeline.aspx" />
    <asp:TreeView ID="TreeView_Assigments" runat="server" Style="position: absolute; top: 87px; left: 361px;
        height: 164px; width: 139px;" ShowCheckBoxes="Leaf">
    </asp:TreeView>
    <asp:Label ID="ulums" runat="server" Style="position: absolute; top: 27px; left: 31px;
        height: 24px; width: 105px;" Text="My Curriculums"></asp:Label>
    <asp:Label ID="Label_Groups" runat="server" Style="position: absolute; top: 27px; left: 184px; height: 27px; right: 444px;"
        Text="My Groups"></asp:Label>
    <asp:Button ID="Button_Assign" runat="server" Style="position: absolute; top: 277px; left: 121px;
        height: 24px;" Text="Assign" />
    <asp:Button ID="Button_SwitchView" runat="server" Style="position: absolute; top: 287px; left: 355px;
        height: 24px;" Text="Switch View" />
    </form>
</body>
</html>

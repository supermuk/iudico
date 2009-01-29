<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CurriculumEdit.aspx.cs" Inherits="CurriculumEdit" %>

<%@ Register TagPrefix="iudico" Namespace="IUDICO.DataModel.Controllers" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" style="position: absolute; top: 31px; left: 22px;
    height: 483px; width: 695px;">
    <asp:TextBox ID="TextBox_Name" runat="server" Style="position: absolute; top: 104px;
        left: 540px;"></asp:TextBox>
    <asp:Label ID="Label_Description" runat="server" Style="position: absolute; top: 149px;
        left: 452px;" Text="Description:"></asp:Label>
    <asp:Label ID="Label_Name" runat="server" Style="position: absolute; top: 105px;
        left: 472px;" Text="Name:"></asp:Label>
    <asp:Label ID="Label_Courses" runat="server" Style="position: absolute; top: 13px;
        left: 11px;" Text="Available Courses:"></asp:Label>
    <asp:Label ID="Label_Curriculums" runat="server" Style="position: absolute; top: 19px;
        left: 292px;" Text="Available Curriculums:"></asp:Label>
    <asp:HyperLink ID="HyperLink_CourseManagment" runat="server" Style="position: absolute;
        top: 308px; left: 8px;" NavigateUrl="~/ImportCourse.aspx">Don&#39;t have courses? Create new!</asp:HyperLink>
    <iudico:IdentityTreeView ID="TreeView_Curriculums" runat="server" Style="position: absolute;
        top: 56px; left: 284px; height: 95px; width: 151px;" ShowCheckBoxes="All">
    </iudico:IdentityTreeView>
    <iudico:IdentityTreeView ID="TreeView_Courses" runat="server" Style="position: absolute;
        top: 55px; left: 39px;" ShowLines="True" ShowCheckBoxes="Leaf">
    </iudico:IdentityTreeView>
    <asp:TextBox ID="TextBox_Description" runat="server" Style="position: absolute; top: 149px;
        left: 537px;"></asp:TextBox>
    <asp:Button ID="Button_CreateCurriculum" runat="server" Style="position: absolute;
        top: 230px; left: 472px;" Text="Create new curriculum" />
    <asp:Button ID="Button_Modify" runat="server" Style="position: absolute; top: 331px;
        left: 533px; bottom: 126px;" Text="Modify" />
    <asp:Button ID="Button_AddStage" runat="server" Style="position: absolute; top: 192px;
        left: 533px; bottom: 265px;" Text="Add new stage" />
    <asp:Button ID="Button_Delete" runat="server" Style="position: absolute; top: 278px;
        left: 531px; bottom: 179px;" Text="Delete" />
    <asp:Button ID="Button_AddTheme" runat="server" Style="position: absolute; top: 251px;
        left: 190px;" EnableTheming="True" Text="Add theme" />
    <asp:HyperLink ID="HyperLink_CourseManagment0" runat="server" Style="position: absolute;
        top: 432px; left: 288px;">Curriculum managment</asp:HyperLink>
    </form>
</body>
</html>

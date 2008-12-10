<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="ImportCourse.aspx.cs" Inherits="UploadCourse" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
</head>
<body>
    <form id="form1" runat="server">
    <asp:FileUpload ID="courseUpload" runat="server" style = "position : absolute; top: 227px; left: 308px; width: 208px; bottom: 322px;"/>
    <asp:TextBox ID="descriptionTextBox" runat="server" style = "position : absolute; top: 171px; left: 402px;"></asp:TextBox>
    <asp:Label ID="nameLabel" runat="server" Text="Name" style = "position : absolute; top: 79px; left: 314px; height: 17px; right: 373px;"></asp:Label>
    <asp:Label ID="descriptionLabel" runat="server" Text="Description" style = "position : absolute; top: 173px; left: 307px;"></asp:Label>
    <asp:TextBox ID="nameTextBox" runat="server" style = "position : absolute; top: 79px; left: 402px; bottom: 470px;"></asp:TextBox>
    <asp:TreeView ID="courseTree" runat="server" 
            style = "position : absolute; top: 77px; left: 7px; height: 340px; width: 217px;" 
            ImageSet="XPFileExplorer" NodeIndent="15">
            <ParentNodeStyle Font-Bold="False" />
            <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
            <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" HorizontalPadding="0px" VerticalPadding="0px" />
            <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" HorizontalPadding="2px" NodeSpacing="0px" VerticalPadding="2px" />
    </asp:TreeView>
    <asp:Button ID="importButton" runat="server" Text="Import Course" style = "position : absolute; top: 424px; left: 98px; width: 91px;"/>
    <asp:Button ID="openButton" runat="server" Text="Open Course" style = "position : absolute; top: 424px; left: 7px; width: 89px;"/>
    <asp:Button ID="editButton" runat="server" Text="Edit" style = "position : absolute; top: 42px; left: 10px; width: 53px; right: 684px; height: 24px;"/>
    <asp:Button ID="deleteButton" runat="server" Text="Delete" style = "position : absolute; top: 42px; left: 68px; width: 53px; height: 24px;"/>
    </form>
</body>
</html>

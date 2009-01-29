<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="CourseImport.aspx.cs" Inherits="CourseImport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>Import Course</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:FileUpload ID="courseUpload" runat="server" style = "position : absolute; top: 227px; left: 65px; width: 208px; bottom: 322px;"/>
    <asp:TextBox ID="descriptionTextBox" runat="server" style = "position : absolute; top: 171px; left: 160px;"></asp:TextBox>
    <asp:Label ID="nameLabel" runat="server" Text="Name" style = "position : absolute; top: 79px; left: 65px; height: 17px; right: 373px;"></asp:Label>
    <asp:Label ID="descriptionLabel" runat="server" Text="Description" style = "position : absolute; top: 173px; left: 65px;"></asp:Label>
    <asp:TextBox ID="nameTextBox" runat="server" style = "position : absolute; top: 79px; left: 160px; bottom: 470px;"></asp:TextBox>
    <asp:HyperLink ID = "editCourseLink" runat="server" style = "position : absolute; top: 79px; left: 320px;" Visible = "false" Text = "Edit"></asp:HyperLink>
    <asp:Button ID="importButton" runat="server" Text="Import Course" style = "position : absolute; top: 300px; left: 65px;"/>
    </form>
</body>
</html>

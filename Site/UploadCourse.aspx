<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="UploadCourse.aspx.cs" Inherits="UploadCourse" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Upload Course</title>
</head>
<body>
    <form id="form1" runat="server">
    <p>
        <asp:FileUpload ID="CourseUpload" runat="server" 
            style = "position : absolute; top: 256px; left: 22px;"/>
    </p>
    <p>
        &nbsp;</p>
    <p>
        <asp:Label ID="UploadCourseLabel" runat="server" Text="UploadCourse" 
            
            style = "position : absolute; top: 26px; left: 282px; margin-bottom: 0px; height: 25px; width: 95px;"></asp:Label>
    </p>
    <asp:Button ID="submitButton" runat="server" Text="Submit" 
        style = "position : absolute; top: 343px; left: 25px;"/>
    <asp:TextBox ID="nameTextBox" runat="server" 
        style = "position : absolute; top: 103px; left: 111px; bottom: 446px;"></asp:TextBox>
    <asp:TextBox ID="descriptionTextBox" runat="server" 
        style = "position : absolute; top: 177px; left: 110px;"></asp:TextBox>
    <asp:Label ID="nameLabel" runat="server" Text="Name" 
        style = "position : absolute; top: 105px; left: 28px; height: 17px;"></asp:Label>
    <asp:Label ID="descriptionLabel" runat="server" Text="Description" 
        style = "position : absolute; top: 179px; left: 28px;"></asp:Label>
    </form>
</body>
</html>

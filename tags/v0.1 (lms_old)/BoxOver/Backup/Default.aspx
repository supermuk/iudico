<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>
<%@ register assembly="BoxOver" namespace="BoxOver" tagprefix="boxover" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:label id="Label1" runat="server" text="Label1"></asp:label><br />
        <asp:label id="Label2" runat="server" text="Label2"></asp:label><br />
        <asp:label id="Label3" runat="server" text="Label3"></asp:label><br />
        <asp:textbox id="TextBox1" runat="server"></asp:textbox><br />
        <asp:radiobuttonlist id="RadioButtonList1" runat="server">
            <asp:listitem>Item 1</asp:listitem>
            <asp:listitem>Item 2</asp:listitem>
            <asp:listitem>Item 3</asp:listitem>
        </asp:radiobuttonlist><br />
        <boxover:boxover id="BoxOver1" runat="server" body="Test body..." controltovalidate="Label2" header="Test Header" />
        &nbsp;</div>
    </form>
</body>
</html>
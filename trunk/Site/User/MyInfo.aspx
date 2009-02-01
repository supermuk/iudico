<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="MyInfo.aspx.cs" Inherits="User_MyInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label ID="Label_Greeting" runat="server" Text="Hello dear User, here you can look up and change your personal info"
        Style="position: absolute; top: 120px; left: 0px;"></asp:Label>
    <asp:Label ID="Label_FirstName" runat="server" Text="Your first name:" Style="position: absolute;
        top: 160px; left: 0px;"></asp:Label>
    <asp:TextBox ID="TextBox_FirstName" runat="server" Style="position: absolute; top: 160px;
        left: 120px;"></asp:TextBox>
    <asp:Label ID="Label_SecondName" runat="server" Text="Your second name:" Style="position: absolute;
        top: 200px; left: 0px;"></asp:Label>
    <asp:TextBox ID="TextBox_SecondName" runat="server" Style="position: absolute; top: 200px;
        left: 120px;"></asp:TextBox>
    <asp:Label ID="Label_Login" runat="server" Text="Your login:" Style="position: absolute;
        top: 240px; left: 0px;"></asp:Label>
    <asp:TextBox ID="TextBox_Login" runat="server" Enabled="false" Style="position: absolute;
        top: 240px; left: 120px;"></asp:TextBox>
    <asp:Label ID="Label_Email" runat="server" Text="Your email:" Style="position: absolute;
        top: 280px; left: 0px;"></asp:Label>
    <asp:TextBox ID="TextBox_Email" runat="server" Style="position: absolute; top: 280px;
        left: 120px;"></asp:TextBox>
    <asp:Label ID="Label_Roles" runat="server" Text="Your roles:" Style="position: absolute;
        top: 320px; left: 0px;"></asp:Label>
    <asp:TextBox ID="TextBox_Roles" runat="server" Style="position: absolute; top: 320px;
        left: 120px;" Width="300" Enabled="false"></asp:TextBox>
    <asp:Label ID="Label_Groups" runat="server" Text="Your groups:" Style="position: absolute;
        top: 360px; left: 0px;"></asp:Label>
    <asp:TextBox ID="TextBox_Groups" runat="server" Style="position: absolute; top: 360px;
        left: 120px;" Width="300" Enabled="false"></asp:TextBox>
    <asp:Button ID="Button_Update" Text="Update" runat="server" Style="position: absolute;
        top: 560px; left: 100px;"></asp:Button>
    <asp:ChangePassword ID="ChangePassword" runat="server" Style="position: absolute;
        top: 400px; left: 0px; right: 301px;">
    </asp:ChangePassword>
</asp:Content>

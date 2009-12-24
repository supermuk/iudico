<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="AddNews.aspx.cs" Inherits="NEWS.AddNews" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContent">
    <asp:Label ID="Label1" runat="server" Text="Title:" />
    <asp:TextBox ID="TitleText" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="Label2" runat="server" Text="Content:" />
    <br />
    <textarea ID="ContentText" cols="50" rows="15" runat="server"></textarea>
    <br />
    <asp:Button runat="server" Text="Add" OnClick="Add_Click" />
</asp:Content>
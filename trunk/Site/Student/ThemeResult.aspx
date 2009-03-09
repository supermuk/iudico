<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ThemeResult.aspx.cs" Inherits="ThemeResult" Title="Theme Results" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<asp:Label runat = "server" ID = "headerLabel" Font-Bold="True" Font-Size="XX-Large"></asp:Label>
<br />
<i:ThemeResult ID = "themeResult" runat = "server"></i:ThemeResult>
</asp:Content>


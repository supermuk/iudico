<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="TestDetails.aspx.cs" Inherits="TestDetails" Title="Test Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<asp:Label runat = "server" ID = "headerLabel" Font-Bold="True" Font-Size="XX-Large"></asp:Label>
<br />
<IFRAME ID="testDetailsFrame" scrolling="auto" Runat="Server" width="100%" height="500px"></IFRAME>
<br />
<asp:Label id="pageRankLabel" Runat="Server"></asp:Label>
<br />
<asp:Label id="questionCountLabel" Runat="Server"></asp:Label>
<br />
<asp:Label id="maximumRankLabel" Runat="Server"></asp:Label>
</asp:Content>


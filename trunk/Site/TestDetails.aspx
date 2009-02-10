<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="TestDetails.aspx.cs" Inherits="TestDetails" Title="Test Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<IFRAME ID="testDetailsFrame" scrolling="auto" Runat="Server" width="500px" height="100%"></IFRAME>
<br />
<asp:Label id="pageRank" Runat="Server"></asp:Label>
<br />
<asp:Label id="questionCount" Runat="Server"></asp:Label>
<br />
<asp:Label id="maximumRank" Runat="Server"></asp:Label>
</asp:Content>


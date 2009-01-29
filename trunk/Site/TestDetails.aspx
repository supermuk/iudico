<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="TestDetails.aspx.cs" Inherits="TestDetails" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<IFRAME ID="testDetailsFrame" scrolling="auto" Runat="Server" width="500px" height="100%"></IFRAME>
<br />
<asp:label id="pageRank" Runat="Server"></asp:label>
<br />
<asp:label id="questionCount" Runat="Server"></asp:label>
<br />
<asp:label id="maximumRank" Runat="Server"></asp:label>
</asp:Content>


<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="TestDetails.aspx.cs" Inherits="TestDetails" Title="Test Details" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<asp:Label runat = "server" ID = "_headerLabel" Font-Bold="True" Font-Size="XX-Large"></asp:Label>
<br />
<asp:Panel ID="_testDetailsPanel" scrolling="auto" Runat="Server" width="100%" 
        height="500px" BorderStyle="Solid"></asp:Panel>
<br />
<asp:Label id="_pageRankLabel" Runat="Server"></asp:Label>
<br />
<asp:Label id="_questionCountLabel" Runat="Server"></asp:Label>
<br />
<asp:Label id="_maximumRankLabel" Runat="Server"></asp:Label>
</asp:Content>


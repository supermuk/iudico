<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SearchPageSolr.aspx.cs" Inherits="Admin_SearchPageSolr"   %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        #TextArea1
        {
            height: 241px;
            width: 489px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:TextBox ID="_searchQuery" runat="server"></asp:TextBox>
    <p style="height: 28px; width: 917px;">
        <asp:Button ID="Button1" runat="server" Text="Search" 
            onclick="Button1_Click1" />
        <asp:Button ID="_openResult" runat="server" Text="Open" Height="22px" />
    <p style="height: 320px; width: 926px;">
        &nbsp;<asp:Panel ID="_resultsPanel" runat="server">
        </asp:Panel>
</asp:Content>


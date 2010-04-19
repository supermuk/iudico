<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SearchPageSolr.aspx.cs" Inherits="Admin_SearchPageSolr" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        #TextArea1
        {
            height: 51px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:TextBox ID="_searchQuery" runat="server"></asp:TextBox>
    <p style="height: 45px">
        <asp:Button ID="Button1" runat="server" Text="Search" />
    <p style="height: 45px">
        <asp:ListBox ID="_resultsListBox" runat="server"></asp:ListBox>
        <asp:Button ID="_openResult" runat="server" Text="Open" />
</asp:Content>


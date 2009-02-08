<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Groups.aspx.cs" Inherits="Admin_Groups" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h2>Groups currently available in the system:</h2>
<i:GroupList ID="GroupList" runat="server" />
<br />

<asp:Button ID="btnCreateGroup" Text="Create" runat="server" />

</asp:Content>
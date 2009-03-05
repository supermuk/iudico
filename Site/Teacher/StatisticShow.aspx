<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="StatisticShow.aspx.cs" Inherits="StatisticShow" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label runat="server" ID="Label_Notify"></asp:Label>
    <asp:Table runat="server" ID="Table_Statistic" GridLines="Both">
    </asp:Table>
</asp:Content>

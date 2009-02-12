<%@Control Language="C#" AutoEventWireup="true" CodeFile="CompiledQuestionResult.ascx.cs" Inherits="CompiledQuestionResult" %>

<script runat="server"></script>

<asp:Label ID="nameLabel" runat="server" Text="Name:"></asp:Label>
<asp:Label ID="timeLimitLabel" runat="server" Text="  Time Limit:"></asp:Label>
<asp:Label ID="memoryLimitLabel" runat="server" Text="  Memory Limit:"></asp:Label>
<asp:Label ID="languageLabel" runat="server" Text="  Language:"></asp:Label>
<asp:Table ID="compiledAnswerTable" runat="server" GridLines="Both" Height="79px" 
    Width="883px" BorderStyle="Solid">
    <asp:TableRow runat="server">
        <asp:TableCell runat="server">Input</asp:TableCell>
        <asp:TableCell runat="server">Expected Output</asp:TableCell>
        <asp:TableCell runat="server">User Output</asp:TableCell>
        <asp:TableCell runat="server">Time Used</asp:TableCell>
        <asp:TableCell runat="server">Memory Used</asp:TableCell>
        <asp:TableCell runat="server">Status</asp:TableCell>
    </asp:TableRow>
</asp:Table>


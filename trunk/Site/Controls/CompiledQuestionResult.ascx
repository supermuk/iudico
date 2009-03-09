<%@Control Language="C#" AutoEventWireup="true" CodeFile="CompiledQuestionResult.ascx.cs" Inherits="CompiledQuestionResult" %>

<script runat="server"></script>

<asp:Label ID="headerLabel" runat="server"></asp:Label>
<br />
<asp:Label ID="statusLabel" runat="server" Visible="false"></asp:Label>
<asp:Table ID="compiledAnswerTable" runat="server" GridLines="Both" Height="79px" 
    Width="883px" BorderStyle="Solid">
    <asp:TableRow runat="server">
        <asp:TableCell runat="server" HorizontalAlign="Center">Input</asp:TableCell>
        <asp:TableCell runat="server" HorizontalAlign="Center">Expected Output</asp:TableCell>
        <asp:TableCell runat="server" HorizontalAlign="Center">User Output</asp:TableCell>
        <asp:TableCell runat="server" HorizontalAlign="Center">Time Used</asp:TableCell>
        <asp:TableCell runat="server" HorizontalAlign="Center">Memory Used</asp:TableCell>
        <asp:TableCell runat="server" HorizontalAlign="Center">Status</asp:TableCell>
    </asp:TableRow>
</asp:Table>


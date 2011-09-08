<%@Control Language="C#" AutoEventWireup="true" CodeFile="ThemeResultControl.ascx.cs" Inherits="ThemeResultControl" %>
<br />
<asp:Label runat = "server" ID = "_themeName"></asp:Label>
<br />
<asp:Label runat = "server" ID = "_startDateTime"></asp:Label>
<asp:Table ID="_resultTable" runat="server" CellPadding="0" CellSpacing="0" 
    GridLines="Both" Height="123px" Width="627px">
    <asp:TableRow runat="server">
        <asp:TableCell runat="server" HorizontalAlign="Center" VerticalAlign="Top">Question</asp:TableCell>
        <asp:TableCell runat="server" Height="30px" HorizontalAlign="Center" VerticalAlign="Top">User Answer</asp:TableCell>
        <asp:TableCell runat="server" Height="30px" HorizontalAlign="Center" VerticalAlign="Top">Correct Answer</asp:TableCell>
        <asp:TableCell ID="TableCell1" runat="server" Height="30px" HorizontalAlign="Center" VerticalAlign="Top">Status</asp:TableCell>
    </asp:TableRow>
</asp:Table>

<br />
<br />


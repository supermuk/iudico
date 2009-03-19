<%@Control Language="C#" AutoEventWireup="true" CodeFile="ThemeResultControl.ascx.cs" Inherits="ThemeResultControl" %>
<br />
<asp:Label runat = "server" ID = "themeName"></asp:Label>
<asp:Table ID="resultTable" runat="server" CellPadding="0" CellSpacing="0" 
    GridLines="Both" Height="123px" Width="627px">
    <asp:TableRow runat="server">
        <asp:TableCell runat="server" HorizontalAlign="Center" VerticalAlign="Top">PageName</asp:TableCell>
        <asp:TableCell runat="server" Height="30px" HorizontalAlign="Center" 
            VerticalAlign="Top">Status</asp:TableCell>
        <asp:TableCell runat="server" Height="30px" HorizontalAlign="Center" 
            VerticalAlign="Top">UserRank</asp:TableCell>
        <asp:TableCell runat="server" Height="30px" HorizontalAlign="Center" 
            VerticalAlign="Top">PageRank</asp:TableCell>
        <asp:TableCell runat="server"></asp:TableCell>
        <asp:TableCell runat="server"></asp:TableCell>
    </asp:TableRow>
</asp:Table>
<br />
<br />


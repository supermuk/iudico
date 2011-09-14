<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OperationsTable.ascx.cs"
    Inherits="OperationsTable" %>
<asp:Table ID="Table_Operations" runat="server">
    <asp:TableHeaderRow>
        <asp:TableHeaderCell>Operation</asp:TableHeaderCell>
        <asp:TableHeaderCell>Time</asp:TableHeaderCell>
        <asp:TableHeaderCell></asp:TableHeaderCell>
    </asp:TableHeaderRow>
    <asp:TableFooterRow>
        <asp:TableCell>
            <asp:DropDownList ID="DropDownList_CurriculumOperations" runat="server">
            </asp:DropDownList>
            <asp:DropDownList ID="DropDownList_StageOperations" runat="server">
            </asp:DropDownList>
            <asp:Button runat="server" ID="Button_AddOperation" Text="Add operation" />
        </asp:TableCell>
    </asp:TableFooterRow>
</asp:Table>

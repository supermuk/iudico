<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="CurriculumAssignment.aspx.cs" Inherits="Teacher_CurriculumAssignment" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label ID="Label_Notify" runat="server"></asp:Label>
    <asp:Table ID="Table_Main" runat="server">
        <asp:TableRow>
            <asp:TableHeaderCell>
            </asp:TableHeaderCell>
            <asp:TableHeaderCell Text="Groups">
            </asp:TableHeaderCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableHeaderCell Text="Curriculums">
            </asp:TableHeaderCell>
            <asp:TableCell>
                <asp:Table ID="Table_Assignments" runat="server" GridLines="Both" CellPadding="10"
                    CellSpacing="5">
                </asp:Table>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="TeachersList.aspx.cs" Inherits="TeachersList" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <table>
        <tr>
            <td>
                <asp:Label ID="Label_PageCaption" runat="server" Font-Bold="True" Font-Size="XX-Large"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label_PageDescription" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label_PageMessage" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td colspan="2">
                <asp:Label ID="Label_SharedBy" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label_SharedWith" runat="server">Already shared with:</asp:Label>
            </td>
            <td>
                <asp:Table ID="Table_SharedWith" runat="server">
                </asp:Table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label_CanBeShared" runat="server">Can be shared with:</asp:Label>
            </td>
            <td>
                <asp:Table ID="Table_CanBeShared" runat="server">
                </asp:Table>
            </td>
        </tr>
    </table>
</asp:Content>

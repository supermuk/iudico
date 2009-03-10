<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="CourseShare.aspx.cs" Inherits="CourseShare" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label runat="server" ID="Label_Notify"></asp:Label>
    <table>
        <tr>
            <td>
                <asp:Label ID="Label_Operations" runat="server">Available operations:</asp:Label>
            </td>
            <td>
                <asp:Table ID="Table_Operations" runat="server">
                </asp:Table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server">Available teachers:</asp:Label>
            </td>
            <td>
                <asp:Table ID="Table_Teachers" runat="server">
                </asp:Table>
            </td>
        </tr>
    </table>
</asp:Content>

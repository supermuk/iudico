<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="CurriculumDeleteConfirmation.aspx.cs" Inherits="CurriculumDeleteConfirmation" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label ID="Label_Notify" runat="server"></asp:Label>
    <table>
        <tr>
            <td colspan="2">
                <asp:BulletedList ID="BulletedList_Groups" runat="server">
                </asp:BulletedList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button_Delete" runat="server" Text="Delete" />
            </td>
            <td>
                <asp:Button ID="Button_Back" runat="server" Text="Back" />
            </td>
        </tr>
    </table>
</asp:Content>

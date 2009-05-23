<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="StatisticSelect.aspx.cs" Inherits="StatisticSelect" %>

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
                <asp:Label ID="Label_PageMessage" runat="server" ForeColor="#CC0000"></asp:Label>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <asp:Label ID="Label_Group" runat="server">Group:</asp:Label>
                <asp:DropDownList ID="DropDownList_Groups" runat="server" AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="Label_Curriculums" runat="server">Curriculum:</asp:Label>
                <asp:DropDownList ID="DropDownList_Curriculums" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="Button_Show" runat="server" Text="Show" />
            </td>
        </tr>
    </table>
</asp:Content>

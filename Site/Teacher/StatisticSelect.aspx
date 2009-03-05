<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="StatisticSelect.aspx.cs" Inherits="StatisticSelect" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label ID="Label_Notify" runat="server"></asp:Label>
    <table>
        <tr>
            <td>
                <asp:Label ID="Label_Group" runat="server">Select Group:</asp:Label>
                <asp:DropDownList ID="DropDownList_Groups" runat="server" AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="Label_Curriculums" runat="server">Select Curriculum:</asp:Label>
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

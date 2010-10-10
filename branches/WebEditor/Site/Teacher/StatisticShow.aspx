<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="StatisticShow.aspx.cs" Inherits="StatisticShow" %>

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
                <br />
                <asp:TextBox ID="TextBox_FindStud" runat="server" Width="99px"></asp:TextBox>
                <asp:Button ID="Button_FindStud" runat="server" Text="Find student" />
                <asp:Button ID="Button_Sort" runat="server" Height="26px" Text="Sort" 
                    Width="108px" />
                <asp:Button ID="ButtonShow_all" runat="server" Height="26px" Text="Show all" 
                    Width="108px" />
            </td>
        </tr>
    </table>
    <table>
        <asp:Table runat="server" ID="Table_Statistic" GridLines="Both">
        </asp:Table>
        </table>
</asp:Content>

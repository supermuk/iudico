<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="StatisticShowCurriculums.aspx.cs" Inherits="Teacher_StatisticShowCurriculums" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <table style="width:100%;">
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <asp:Label ID="Label_PageCaption" runat="server" Font-Bold="True" 
                    Font-Size="XX-Large" Text="Label_PageCaption"></asp:Label>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <asp:Label ID="Label_PageDescription" runat="server" 
                    Text="Label_PageDescription"></asp:Label>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <asp:Table ID="TableCurriculums" runat="server">
                </asp:Table>
            </td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>


<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="CurriculumTimeline.aspx.cs" Inherits="CurriculumTimeline" %>

<%@ Register TagPrefix="iudico" Namespace="IUDICO.DataModel.Controllers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
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
            <td valign="top">
                <iudico:IdentityTreeView ID="TreeView_Curriculum" runat="server">
                    <SelectedNodeStyle BackColor="#00CC00" />
                </iudico:IdentityTreeView>
            </td>
            <td valign="top">
                <i:OperationsTable runat="server" ID="OperationsTable_Operations" />
            </td>
        </tr>
    </table>
</asp:Content>

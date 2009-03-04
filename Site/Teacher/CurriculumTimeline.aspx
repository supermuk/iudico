<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="CurriculumTimeline.aspx.cs" Inherits="CurriculumTimeline" %>

<%@ Register TagPrefix="iudico" Namespace="IUDICO.DataModel.Controllers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label ID="Label_Notify" runat="server"></asp:Label>
    <table>
        <tr>
            <td valign="top">
                <iudico:IdentityTreeView ID="TreeView_Curriculum" runat="server" ImageSet="XPFileExplorer">
                    <SelectedNodeStyle BackColor="#00CC00" />
                </iudico:IdentityTreeView>
            </td>
            <td>
                <table>
                    <tr>
                        <td colspan="2">
                            <asp:Table ID="Table_Time" runat="server" GridLines="Both"  BorderColor="Black" BorderWidth="1">
                            </asp:Table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="DropDownList_Operations" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="Button_Add" runat="server" Text="Add operation"></asp:Button>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

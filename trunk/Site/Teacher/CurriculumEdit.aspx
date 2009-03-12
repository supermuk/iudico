<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="CurriculumEdit.aspx.cs" Inherits="CurriculumEdit" %>

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
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Label_Courses" runat="server" Text="Available Courses:"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <iudico:IdentityTreeView ID="TreeView_Courses" runat="server" ImageSet="XPFileExplorer"
                                ShowCheckBoxes="Leaf">
                            </iudico:IdentityTreeView>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                <asp:Button ID="Button_AddTheme" runat="server" Text="Add theme" />
            </td>
            <td valign="top">
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Label_Curriculums" runat="server" Text="Available Curriculums:"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <iudico:IdentityTreeView ID="TreeView_Curriculums" runat="server" ImageSet="XPFileExplorer">
                                <SelectedNodeStyle BackColor="#00CC00" />
                            </iudico:IdentityTreeView>
                        </td>
                    </tr>
                </table>
            </td>
            <td valign="top">
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Label_Name" runat="server" Text="Name:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox_Name" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label_Description" runat="server" Text="Description:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox_Description" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:Button ID="Button_CreateCurriculum" runat="server" Text="Create curriculum" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:Button ID="Button_AddStage" runat="server" Text="Add stage" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:Button ID="Button_Delete" runat="server" Text="Delete" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:Button ID="Button_Modify" runat="server" Text="Modify" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="CourseEdit.aspx.cs" Inherits="CourseEdit" %>

<%@ Register TagPrefix="iudico" Namespace="IUDICO.DataModel.Controllers" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="Server">
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
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label_CourseName" runat="server" Text="Name"></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:TextBox ID="TextBox_CourseName" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label_CourseDescription" runat="server" Text="Description"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox_CourseDescription" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:FileUpload ID="FileUpload_Course" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="Button_ImportCourse" runat="server" Text="Import Course" />
                        </td>
                    </tr>
                </table>
            </td>
            <td valign="top">
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Label_Courses" runat="server" Text="Available Courses:"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <iudico:IdentityTreeView ID="TreeView_Courses" runat="server" ImageSet="XPFileExplorer">
                                <SelectedNodeStyle BackColor="#00CC00" />
                            </iudico:IdentityTreeView>
                        </td>
                    </tr>
                </table>
            </td>
            <td valign="top">
                <asp:Button ID="Button_DeleteCourse" runat="server" Text="Delete" />
            </td>
        </tr>
    </table>
</asp:Content>

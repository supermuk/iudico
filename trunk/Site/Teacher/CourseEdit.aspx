<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="CourseEdit.aspx.cs" Inherits="CourseEdit" %>

<%@ Register TagPrefix="iudico" Namespace="IUDICO.DataModel.Controllers" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label ID="Label_Notify" runat="server"></asp:Label>
    <table>
        <tr>
            <td valign="top">
                <table>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label_Name" runat="server" Text="Name"></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:TextBox ID="TextBox_Name" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label_Description" runat="server" Text="Description"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBox_Description" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:FileUpload ID="FileUpload_Course" runat="server" />
                        </td>
                        <td>
                            <asp:Button ID="Button_Import" runat="server" Text="Import Course" />
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
                <asp:Button ID="Button_Delete" runat="server" Text="Delete" />
            </td>
        </tr>
    </table>
</asp:Content>

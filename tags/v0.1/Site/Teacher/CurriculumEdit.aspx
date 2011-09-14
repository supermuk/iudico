<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="CurriculumEdit.aspx.cs" Inherits="CurriculumEdit" %>

<%@ Register TagPrefix="iudico" Namespace="IUDICO.DataModel.Controllers" %>
<%@ Register assembly="BoxOver" namespace="BoxOver" tagprefix="boxover" %>
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
                <boxover:BoxOver ID="BoxOver1" runat="server" Body="Courses tree!" 
                    ControlToValidate="TreeView_Courses" Header="Help!" />
                <boxover:BoxOver ID="BoxOver2" runat="server" Body="Click to add new theme!" 
                    ControlToValidate="Button_AddTheme" Header="Help!" />
            </td>
            <td valign="top">
                <table>
                    <tr>
                        <td>
                            <boxover:BoxOver ID="BoxOver3" runat="server" Body="Curriculum tree!" 
                                ControlToValidate="TreeView_Curriculums" Header="Help!" />
                            <asp:Label ID="Label_Curriculums" runat="server" Text="Available Curriculums:"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <iudico:IdentityTreeView ID="TreeView_Curriculums" runat="server">
                                <SelectedNodeStyle BackColor="#00CC00" />
                            </iudico:IdentityTreeView>
                            <boxover:BoxOver ID="BoxOver4" runat="server" 
                                Body="Click to create new curriculum!" 
                                ControlToValidate="Button_CreateCurriculum" Header="Help!" />
                        </td>
                    </tr>
                </table>
            </td>
            <td valign="top">
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Label_Name" runat="server" Text="Name:"></asp:Label>
                            <boxover:BoxOver ID="BoxOver8" runat="server" Body="New record name!" 
                                ControlToValidate="TextBox_Name" Header="Help!" />
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox_Name" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label_Description" runat="server" Text="Description:"></asp:Label>
                            <boxover:BoxOver ID="BoxOver9" runat="server" Body="New record description!" 
                                ControlToValidate="TextBox_Description" Header="Help!" />
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
                            <boxover:BoxOver ID="BoxOver5" runat="server" Body="Click to add new stage!" 
                                ControlToValidate="Button_AddStage" Header="Help!" />
                            <asp:Button ID="Button_AddStage" runat="server" Text="Add stage" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:Button ID="Button_Delete" runat="server" Text="Delete" />
                            <boxover:BoxOver ID="BoxOver6" runat="server" Body="Click to delete record!" 
                                ControlToValidate="Button_Delete" Header="Help!" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:Button ID="Button_Modify" runat="server" Text="Modify" />
                            <boxover:BoxOver ID="BoxOver7" runat="server" Body="Click to modify record!" 
                                ControlToValidate="Button_Modify" Header="Help!" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

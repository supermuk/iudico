<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="CourseEdit.aspx.cs" Inherits="CourseEdit" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Register TagPrefix="iudico" Namespace="IUDICO.DataModel.Controllers" %>
<%@ Register assembly="BoxOver" namespace="BoxOver" tagprefix="boxover" %>
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
                                        <boxover:BoxOver ID="BoxOver1" runat="server" Body="Enter course name!" 
                                            ControlToValidate="TextBox_CourseName" Header="Help!" />
                                    </td>
                                    <td colspan="2">
                                        <asp:TextBox ID="TextBox_CourseName" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label_CourseDescription" runat="server" Text="Description"></asp:Label>
                                        <boxover:BoxOver ID="BoxOver2" runat="server" Body="Enter course description!" 
                                            ControlToValidate="TextBox_CourseDescription" Header="Help!" />
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
                            <boxover:BoxOver ID="BoxOver3" runat="server" Body="Choose course file!" 
                                ControlToValidate="FileUpload_Course" Header="Help!" />
                            <asp:FileUpload ID="FileUpload_Course" runat="server" />
                            <boxover:BoxOver ID="BoxOver4" runat="server" Body="Click to find a course!" 
                                ControlToValidate="Button_ImportCourse" Header="Help!" />
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
            <td valign="top" align="center">
                <table>
                    <tr>
                        <td>
                            <boxover:BoxOver ID="BoxOver5" runat="server" Body="Click to delete course!" 
                                ControlToValidate="Button_DeleteCourse" Header="Help!" />
                            <asp:Button ID="Button_DeleteCourse" runat="server" Text="Delete" />
                        </td>
                    </tr>
                    <!--
                    is there a point in it???
                    <tr> 
                        <td>
                            <asp:Button ID="Button_CourseBehaviour" runat="server" Text="Course Behaviour" />
                            <boxover:BoxOver ID="BoxOver6" runat="server" 
                                Body="Click to create course dehevior!" 
                                ControlToValidate="Button_CourseBehaviour" Header="Help!" />
                            <boxover:BoxOver ID="BoxOver7" runat="server" Body="Course tree!" 
                                ControlToValidate="TreeView_Courses" Header="Help!" />
                        </td>
                    </tr>
                    -->
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

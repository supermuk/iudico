<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="TeacherObjects.aspx.cs" Inherits="TeacherObjects" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label runat="server" ID="Label_Notify"></asp:Label>
    <table border="1">
        <tr>
            <td>
                <asp:Label runat="server" ID="Label_Courses">Your courses:</asp:Label>
            </td>
            <td>
                <asp:Label runat="server" ID="Label_Curriculums">Your curriculums:</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Table runat="server" ID="Table_Courses">
                </asp:Table>
            </td>
            <td>
                <asp:Table runat="server" ID="Table_Curriculums">
                </asp:Table>
            </td>
        </tr>
    </table>
</asp:Content>

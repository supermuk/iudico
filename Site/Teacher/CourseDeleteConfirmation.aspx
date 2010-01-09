<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="CourseDeleteConfirmation.aspx.cs" Inherits="CourseDeleteConfirmation" %>

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
                <asp:Label ID="Label_PageMessage" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td colspan="2">
                <asp:GridView ID="GridView_Dependencies" runat="server">
                </asp:GridView>
                <boxover:BoxOver ID="BoxOver2" runat="server" Body="Click to go back!" 
                    ControlToValidate="Button_Back" Header="Help!" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button_Delete" runat="server" Text="Delete" />
                <boxover:BoxOver ID="BoxOver1" runat="server" Body="Click to delete course!" 
                    ControlToValidate="Button_Delete" Header="Help!" />
            </td>
            <td>
                <asp:Button ID="Button_Back" runat="server" Text="Back" />
            </td>
        </tr>
    </table>
</asp:Content>

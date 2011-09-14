<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="StatisticSelect.aspx.cs" Inherits="StatisticSelect" %>

<%@ Register assembly="BoxOver" namespace="BoxOver" tagprefix="boxover" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
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
            <td>
                <asp:Label ID="Label_Group" runat="server">Group:</asp:Label>
                <boxover:BoxOver ID="BoxOver1" runat="server" Body="Choose group!" 
                    ControlToValidate="DropDownList_Groups" Header="Help!" />
                <asp:DropDownList ID="DropDownList_Groups" runat="server" AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="Label_Curriculums" runat="server">Curriculums:</asp:Label>
            </td>
            <td>
                <asp:CheckBoxList ID="CheckBoxCurriculums" runat="server" 
                    onselectedindexchanged="CheckBoxCurriculums_SelectedIndexChanged" 
                    Height="48px">
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="Button_Show" runat="server" Text="Show" />
                <boxover:BoxOver ID="BoxOver3" runat="server" Body="Click to apply filter!" 
                    ControlToValidate="Button_Show" Header="Help!" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;</td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>

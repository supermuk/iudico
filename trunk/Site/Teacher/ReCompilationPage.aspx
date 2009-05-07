<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ReCompilationPage.aspx.cs" Inherits="Teacher_ReCompilationPage" Title="ReCompilation Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:Label ID="_headerLabel" runat="server" Font-Bold="True" Font-Size="XX-Large"></asp:Label>
    <br />
    <asp:Label ID="_descriptionLabel" runat="server"></asp:Label>
    <table style="width:100%;">
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:DropDownList ID="_groupDropDownList" runat="server" AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
            <td>
                <asp:DropDownList ID="_curriculumnDropDownList" runat="server" 
                    AutoPostBack="True">
                </asp:DropDownList>
                <asp:DropDownList ID="_stageDropDownList" runat="server" AutoPostBack="True">
                </asp:DropDownList>
                <asp:DropDownList ID="_themeDropDownList" runat="server" AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <br />
    <asp:Button ID="_reCompileButton" runat="server" Text="ReCompile" />
</asp:Content>


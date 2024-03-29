﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="CurriculumDeleteConfirmation.aspx.cs" Inherits="CurriculumDeleteConfirmation" %>

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
                <asp:BulletedList ID="BulletedList_Groups" runat="server">
                </asp:BulletedList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button_Delete" runat="server" Text="Delete" />
            </td>
            <td>
                <asp:Button ID="Button_Back" runat="server" Text="Back" />
            </td>
        </tr>
    </table>
</asp:Content>

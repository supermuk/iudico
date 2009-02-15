<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="CurriculumDeleteConfirmation.aspx.cs" Inherits="CurriculumDeleteConfirmation" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label ID="Label_Notify" runat="server"></asp:Label>
    <asp:BulletedList ID="BulletedList_Groups" runat="server">
    </asp:BulletedList>
    <asp:Button ID="Button_Delete" runat="server" Text="Delete" />
    <asp:Button ID="Button_Back" runat="server" Text="Back" />
</asp:Content>

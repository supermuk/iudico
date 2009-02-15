<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="CourseDeleteConfirmation.aspx.cs" Inherits="CourseDeleteConfirmation" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label ID="Label_Notify" runat="server"></asp:Label>
    <asp:GridView ID="GridView_Dependencies" runat="server">
    </asp:GridView>
    <asp:Button ID="Button_Delete" runat="server" Text="Delete" />
    <asp:Button ID="Button_Back" runat="server" Text="Back" />
</asp:Content>

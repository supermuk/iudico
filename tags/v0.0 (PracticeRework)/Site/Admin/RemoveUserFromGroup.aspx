<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RemoveUserFromGroup.aspx.cs" Inherits="Admin_RemoveUserFromGroup" Title="Confirm Excluding User" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
 <h2><asp:Label ID="lbConfirmationText" runat="server" /></h2>
 <br />
 <asp:Button Text="YES" ID="btnYes" runat="server" OnClick="DoExclude" /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
 <asp:Button Text="Cancel" ID="btnNo" runat="server" />
</asp:Content>


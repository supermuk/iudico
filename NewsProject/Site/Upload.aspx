<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="Upload.aspx.cs" Inherits="NEWS.Upload" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <asp:DropDownList runat="server" />
    <asp:FileUpload ID="Uploader" runat="server" />
    <br />
    <asp:Button runat="server" Text="Upload" OnClick="Upload_Click" />
</asp:Content>
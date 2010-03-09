<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="InternalServerErrorHandler.aspx.cs" Inherits="ErrorHandling_DefaultErrorHandler" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <img src="../Images/500.jpg" alt="!" />
    <span>
        <h1>
            <font color = "black">We're sorry, but an internal server error occured during requested page processing. Please contact the administrator for help.</font>
        </h1>
    </span>
</asp:Content>

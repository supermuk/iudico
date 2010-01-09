<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PageNotFoundErrorHandler.aspx.cs" Inherits="ErrorHandling_DefaultErrorHandler" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <img src="../Images/404.jpg" alt="!" />
    <span>
        <h1>
            <font color = "black">We're sorry, but the page you requested could not be found. Please check your typing and try again.</font>
        </h1>
    </span>
</asp:Content>


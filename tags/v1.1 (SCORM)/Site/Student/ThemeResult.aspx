﻿<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ThemeResult.aspx.cs" Inherits="ThemeResult" Title="Theme Results" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<asp:Label runat = "server" ID = "_headerLabel" Font-Bold="True" Font-Size="XX-Large"></asp:Label>
<br />
<i:ThemeResult ID = "_themeResult" runat = "server"></i:ThemeResult>
</asp:Content>

﻿<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CompiledQuestionsDetails.aspx.cs" Inherits="CompiledQuestionsDetails" Title="Compiled Question Statistic"%>
<%@ Reference Control="../Controls/CompiledQuestionResult.ascx"%>
<script runat="server">

</script>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:Label runat = "server" ID = "headerLabel" Font-Bold="True" Font-Size="XX-Large"></asp:Label>
<br />
    <asp:Panel ID="panel" runat = "server"></asp:Panel>
</asp:Content>


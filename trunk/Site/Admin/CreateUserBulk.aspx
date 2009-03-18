<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CreateUserBulk.aspx.cs" Inherits="Admin_CreateUserBulk" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

<h1>Create Multiple Users</h1>

Fill in group prefix, desired users count and password and press 'Create' button <br />

<table>
    <tr>
        <th style="text-align:left">
            <asp:Label Text="Prefix:" runat="server" />
        </th>
        <td>
            <asp:TextBox ID="tbPrefix" runat="server" />
        </td>
    </tr>
    <tr>
        <th style="text-align:left">
            <asp:Label Text="Count:" runat="server" />
        </th>
        <td>
            <asp:TextBox ID="tbCount" runat="server" />
        </td>
    </tr>
    <tr>
        <th style="text-align:left">
            <asp:Label Text="Password:" runat="server" />
        </th>
        <td>
            <asp:TextBox ID="tbPassword" runat="server" />
        </td>
    </tr>    
</table>
<asp:Button runat="server" Text="Create" ID="btnCreate" />
<p><asp:Label runat="server" ID="lbErrors" ForeColor="Red" /></p>

    
<%--    <asp:Wizard id="wizCreateUserBulk" runat="server">
        <WizardSteps>
            <asp:WizardStep ID="stepSpecifyData" runat="server" StepType="Start">
                <asp:Label Text="Prefix:" runat="server" />
                <asp:TextBox ID="tbPrefix" runat="server" />
            </asp:WizardStep>            
        </WizardSteps>
    </asp:Wizard>--%>

</asp:Content>


<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="CreateUserBulk.aspx.cs" Inherits="Admin_CreateUserBulk" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <h1>
        Create Multiple Users</h1>
    Fill in group prefix, desired users count and password and press 'Create' button
    <br />
    <table>
        <tr>
            <th style="text-align: left">
                <asp:Label Text="Prefix:" runat="server" />
            </th>
            <td>
                <asp:TextBox ID="tbPrefix" runat="server" />
            </td>
        </tr>
        <tr>
            <th style="text-align: left">
                <asp:Label Text="Count:" runat="server" />
            </th>
            <td>
                <asp:TextBox ID="tbCount" runat="server" />
            </td>
        </tr>
        <tr>
            <th style="text-align: left">
                <asp:Label Text="Password:" runat="server" />
            </th>
            <td>
                <asp:TextBox ID="tbPassword" runat="server" />
            </td>
        </tr>
        <tr>
            <th style="text-align: left">
                <asp:Label Text="Add to Group:" runat="server" />
            </th>
            <td>
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <asp:CheckBox ID="cbAddToGroup" runat="server" AutoPostBack="true" />
                        <asp:DropDownList ID="cbGroups" runat="server" AutoPostBack="true" Width="130px" />
                        <asp:Label ID="lbNewGroup" runat="server" Text="Name:" /> <asp:TextBox ID="tbNewGroup" runat="server" />
                        
                        
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <th style="text-align: left">
                <asp:Label ID="Label1" Text="Make Student:" runat="server" />
            </th>
            <td>
                <asp:CheckBox runat="server" ID="cbMakeStudent" AutoPostBack="true" />
            </td>
        </tr>
    </table>
    <asp:Button runat="server" Text="Create" ID="btnCreate" />
    <p>
        <asp:Label runat="server" ID="lbErrors" ForeColor="Red" /></p>
</asp:Content>

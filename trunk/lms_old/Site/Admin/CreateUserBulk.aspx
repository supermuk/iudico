<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="CreateUserBulk.aspx.cs" Inherits="Admin_CreateUserBulk" %>

<%@ Register assembly="BoxOver" namespace="BoxOver" tagprefix="boxover" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <h1>
        Create Multiple Users</h1>
    <p class="descriptions">
        Fill in group prefix, desired users count and password and press 'Create' button
    </p>
    <table>
        <tr>
            <td style="text-align: left">
                <asp:Label Text="Prefix:" runat="server" />
            </td>
            <td>
                <boxover:BoxOver ID="BoxOver8" runat="server" Body="Click to save users!" 
                    ControlToValidate="btnCreate" Header="Help!" />
                <asp:TextBox ID="tbPrefix" runat="server" />
                <boxover:BoxOver ID="BoxOver1" runat="server" Body="Enter prefix for user!" 
                    ControlToValidate="tbPrefix" Header="Help!" />
            </td>
        </tr>
        <tr>
            <td style="text-align: left">
                <asp:Label Text="Count:" runat="server" />
            </td>
            <td>
                <asp:TextBox ID="tbCount" runat="server" />
                <boxover:BoxOver ID="BoxOver2" runat="server" Body="Enter number of accounts!" 
                    ControlToValidate="tbCount" Header="Help!" />
            </td>
        </tr>
        <tr>
            <td style="text-align: left">
                <asp:Label Text="Password:" runat="server" />
            </td>
            <td>
                <asp:TextBox ID="tbPassword" runat="server" />
                <boxover:BoxOver ID="BoxOver3" runat="server" Body="Enter default password!" 
                    ControlToValidate="tbPassword" Header="Help!" />
            </td>
        </tr>
        <tr>
            <td style="text-align: left">
                <asp:Label Text="Add to Group:" runat="server" />
            </td>
            <td>
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <asp:CheckBox ID="cbAddToGroup" runat="server" AutoPostBack="true" />
                        <asp:DropDownList ID="cbGroups" runat="server" AutoPostBack="true" Width="130px" />
                        <asp:Label ID="lbNewGroup" runat="server" Text="Name:" />
                        <asp:TextBox ID="tbNewGroup" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td style="text-align: left">
                <asp:Label ID="Label1" Text="Make Student:" runat="server" />
            </td>
            <td>
                <boxover:BoxOver ID="BoxOver5" runat="server" Body="Choose group!" 
                    ControlToValidate="cbGroups" Header="Help!" />
                <asp:CheckBox runat="server" ID="cbMakeStudent" AutoPostBack="true" />
                <boxover:BoxOver ID="BoxOver7" runat="server" 
                    Body="Check to make user a student!" ControlToValidate="cbMakeStudent" 
                    Header="Help!" />
                <boxover:BoxOver ID="BoxOver4" runat="server" 
                    Body="Check if you want to add user to group!" ControlToValidate="cbAddToGroup" 
                    Header="Help!" />
                <boxover:BoxOver ID="BoxOver6" runat="server" Body="Enter new group name!" 
                    ControlToValidate="tbNewGroup" Header="Help!" />
            </td>
        </tr>
    </table>
    <asp:Button runat="server" Text="Create" ID="btnCreate" />
    <p>
        <asp:Label runat="server" ID="lbErrors" ForeColor="Red" /></p>
</asp:Content>

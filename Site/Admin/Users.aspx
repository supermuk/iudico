<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Users.aspx.cs" Inherits="Admin_Users" Title="Users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

<h1>Users available in the system:</h1>
<asp:GridView 
    ID="gvUsers" 
    AutoGenerateColumns="false"
    OnRowDataBound="gvUsers_OnRowDataBound"
    runat="server">
    <Columns>
        <asp:TemplateField>
            <HeaderTemplate>
                <asp:Label Text="Login" runat="server" />
            </HeaderTemplate>
            <ItemTemplate>
                <asp:LinkButton ID="lbLogin" runat="server" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <HeaderTemplate>
                <asp:Label Text="First Name" runat="server" />
            </HeaderTemplate>
            <ItemTemplate>
                <asp:Label ID="lbFirstName" runat="server" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <HeaderTemplate>
                <asp:Label Text="Second Name" runat="server" />
            </HeaderTemplate>
            <ItemTemplate>
                <asp:Label ID="lbSecondName" runat="server" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <HeaderTemplate>
                <asp:Label Text="Email" runat="server" />
            </HeaderTemplate>
            <ItemTemplate>
                <asp:Label ID="lbEmail" runat="server" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>

</asp:Content>


<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserList.ascx.cs" Inherits="Controls_UserList" %>

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
        <asp:TemplateField>
            <ItemTemplate>
                <asp:Button ID="btnAction" runat="server" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
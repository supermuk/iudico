<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserList.ascx.cs" Inherits="Controls_UserList" %>

<asp:GridView 
    ID="gvUsers" 
    AutoGenerateColumns="false"
    Width="100%"
    OnRowDataBound="gvUsers_OnRowDataBound"
    AllowPaging="true"
    OnPageIndexChanging="UsersPageIndexChanging"
    PageSize="25"
    runat="server">
    <PagerSettings Position="TopAndBottom" Mode="NumericFirstLast" />
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
            <HeaderTemplate>
                <asp:Label ID="lbIPHeader" Text="IP" runat="server" />
            </HeaderTemplate>
            <ItemTemplate>
                <asp:Label ID="lbIP" runat="server" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <HeaderTemplate>
                <asp:Label ID="lbLastLoginHeader" Text="Last login" runat="server" />
            </HeaderTemplate>
            <ItemTemplate>
                <asp:Label ID="lbLastLogin" runat="server" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:Button ID="btnAction" runat="server" />
            </ItemTemplate>
        </asp:TemplateField>
        
    </Columns>
</asp:GridView>
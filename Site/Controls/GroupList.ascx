<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GroupList.ascx.cs" Inherits="Controls_GroupList" %>

<asp:GridView 
    ID="gvGroups"
    AutoGenerateColumns="false"
    Width="100%"
    OnRowDataBound="gvGroups_OnRowDataBound"
    AllowPaging="true"
    OnPageIndexChanging="GroupsPageIndexChanging"
    runat="server">
    <PagerSettings Position="TopAndBottom" Mode="NumericFirstLast" />
    <Columns>
        <asp:TemplateField ControlStyle-Width="80%">
            <HeaderTemplate>
                <asp:Label ID="Label1" Text="Group" runat="server" />
            </HeaderTemplate>
            <ItemTemplate>
                <asp:LinkButton runat="server" ID="lnkGroupName" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField ControlStyle-Width="20%">
            <ItemTemplate>
                <asp:Button runat="server" ID="lnkAction" />
            </ItemTemplate>
        </asp:TemplateField>       
    </Columns>
</asp:GridView>
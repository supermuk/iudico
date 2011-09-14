<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SettingList.ascx.cs" Inherits="Controls_SettingList" %>

<asp:GridView 
    ID="gvSettings" 
    AutoGenerateColumns="false"
    Width="100%"
    OnRowDataBound="gvSettings_OnRowDataBound"
    AllowPaging="true"
    OnPageIndexChanging="SettingsPageIndexChanging"
    PageSize="25"
    runat="server">
    <PagerSettings Position="TopAndBottom" Mode="NumericFirstLast" />
    <Columns>
        <asp:TemplateField>
            <HeaderTemplate><asp:Label Text="Name" runat="server" /></HeaderTemplate>
            <ItemTemplate><asp:LinkButton ID="lbName" runat="server" /></ItemTemplate>
        </asp:TemplateField>
        
        <asp:TemplateField>
            <HeaderTemplate><asp:Label Text="Value" runat="server" /></HeaderTemplate>
            <ItemTemplate><asp:Label ID="lbValue" runat="server" /></ItemTemplate>
        </asp:TemplateField>
        
        <asp:TemplateField>
            <ItemTemplate><asp:Button ID="btnAction" runat="server" /></ItemTemplate>
        </asp:TemplateField>
        
    </Columns>
</asp:GridView>
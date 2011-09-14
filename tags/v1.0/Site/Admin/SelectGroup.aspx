<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SelectGroup.aspx.cs" Inherits="Admin_SelectGroup" Title="Untitled Page" %>

<%@ Register assembly="BoxOver" namespace="BoxOver" tagprefix="boxover" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<h2><asp:Label ID="lbMessage" runat="server" /></h2>
<asp:GridView 
    ID="gvGroups"
    AutoGenerateColumns="false"
    Width="100%"
    OnRowDataBound="gvGroups_OnRowDataBound"
    runat="server">
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
                <asp:Button runat="server" ID="lnkSelect" />
            </ItemTemplate>
        </asp:TemplateField>       
    </Columns>
</asp:GridView>
<br />
<asp:Button ID="btnCancel" Text="Back" runat="server" />
    <boxover:BoxOver ID="BoxOver1" runat="server" Body="Click to get back!" 
        ControlToValidate="btnCancel" Header="Help!" />
</asp:Content>

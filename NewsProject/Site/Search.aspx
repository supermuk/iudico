<%@ Page Language="C#" Title="Search news" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="Search.aspx.cs" Inherits="NEWS.Search" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContent">

<center>
<asp:Label runat="server" Text="Title:" />
<asp:TextBox runat="server" ID="TitleFilter" />
<br />

<asp:Label runat="server" Text = "Content:"/>
<asp:TextBox runat="server" ID="ContentFilter" />
<br />
<br />

<asp:Button runat="server" Text="Search" OnClick="Search_Click" />
<br />
<br />

<asp:GridView 
    runat="server" 
    ID="NewsSearchGrid" 
    AllowPaging="true" 
    ShowFooter="false"
    Visible="false"
    PagerSettings-Mode="NumericFirstLast"
    AutoGenerateColumns="false"
    OnRowDataBound="News_OnRowDataBound">
    <EmptyDataTemplate>
        <asp:Label runat="server" Text="No news found with your criteria" />
    </EmptyDataTemplate>
    <Columns>
        <asp:TemplateField> 
            <HeaderTemplate>
                <asp:Label runat="server" Text="Category" Font-Bold="true" Font-Size="XX-Large" />
            </HeaderTemplate>
            <ItemTemplate>
                <asp:LinkButton ID="CategoryTitle" runat="server" Font-Bold="false" Font-Size="XX-Large" />
            </ItemTemplate>           
        </asp:TemplateField>    
        <asp:TemplateField>
            <HeaderTemplate>
                <asp:Label runat="server" Text="News" Font-Bold="true" Font-Size="XX-Large" />
            </HeaderTemplate>
            <ItemTemplate>
                <asp:LinkButton ID="NewsTitle" runat="server" Font-Bold="false" Font-Size="XX-Large" />
            </ItemTemplate>
        </asp:TemplateField>

    </Columns>
</asp:GridView>

</center>

</asp:Content>


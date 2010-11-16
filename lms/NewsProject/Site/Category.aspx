<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="Category.aspx.cs" Inherits="NEWS.CategoryContent" %>

<asp:Content runat="server" ContentPlaceHolderID="MainContent">
<center>
    <h1><asp:Label runat="server" ID="CategoryTitle" /></h1>
<br />
<asp:GridView 
    runat="server" 
    ID="News" 
    AllowPaging="true" 
    ShowHeader="false"
    ShowFooter="false"
    PagerSettings-Mode="NumericFirstLast"
    AutoGenerateColumns="false"
    OnRowDataBound="News_OnRowDataBound">
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:LinkButton ID="NewsTitle" runat="server" Font-Bold="true" Font-Size="XX-Large"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
</center>
<asp:Button ID="AddNews" runat="server" Text="Add" OnClick="AddNews_Click" />
</asp:Content>
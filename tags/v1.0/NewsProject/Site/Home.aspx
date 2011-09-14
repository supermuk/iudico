<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="Home.aspx.cs" Inherits="NEWS.Home" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<center>
<asp:GridView 
    ShowFooter="false" 
    ShowHeader="false"
    BorderStyle="None"
    ID="CategoriesGrid" 
    DataKeyNames="Name" 
    runat="server" 
    AutoGenerateColumns="false" 
    OnRowDataBound="CategoryGrid_RowDataBound">
    <Columns>
        <asp:TemplateField ItemStyle-BorderWidth="0">
            <ItemTemplate>
                    <asp:LinkButton ID="NameLink" runat="server" Font-Bold="true" Font-Size="XX-Large" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
</center>
</asp:Content>

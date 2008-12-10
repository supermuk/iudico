<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" Inherits="NewsPage" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<center>
    <n:newscontent ID="NewsContent" runat="server" />
</center>
<br />
<asp:Label Text="Comments:" runat="server" />
<br />
<center>
    <asp:GridView 
    ShowFooter="false" 
    ShowHeader="false"
    ID="CommentsGrid" 
    DataKeyNames="ID" 
    runat="server" 
    AutoGenerateColumns="false" 
    OnRowDataBound="Comments_RowDataBound">
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:Label ID="CommentText" runat="server" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
<br />
<textarea id="Comment" runat="server" rows="3" cols="25">
</textarea>
<br />
<asp:Button ID="LeaveComment" runat="server" Text="Leave Comment" OnClick="LeaveComment_Click" />
</center>
</asp:Content>


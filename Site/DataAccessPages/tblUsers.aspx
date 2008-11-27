<%@ Page Language="C#" AutoEventWireup="true" CodeFile="tblUsers.aspx.cs" Inherits="DataAccessPages_tblUsers" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
            AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="ID" 
            DataSourceID="IUDICOSqlDataSource">
            <Columns>
                <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" 
                    ReadOnly="True" SortExpression="ID" />
                <asp:BoundField DataField="FirstName" HeaderText="FirstName" 
                    SortExpression="FirstName" />
                <asp:BoundField DataField="LastName" HeaderText="LastName" 
                    SortExpression="LastName" />
                <asp:BoundField DataField="Login" HeaderText="Login" SortExpression="Login" />
                <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="IUDICOSqlDataSource" runat="server" 
            ConflictDetection="CompareAllValues" 
            ConnectionString="Data Source=MICHA-CORP\SQLEXPRESS;Initial Catalog=IUDICO;Integrated Security=True" 
            DeleteCommand="DELETE FROM [tblUsers] WHERE [ID] = @original_ID AND (([FirstName] = @original_FirstName) OR ([FirstName] IS NULL AND @original_FirstName IS NULL)) AND [LastName] = @original_LastName AND [Login] = @original_Login AND [Email] = @original_Email" 
            InsertCommand="INSERT INTO [tblUsers] ([FirstName], [LastName], [Login], [Email]) VALUES (@FirstName, @LastName, @Login, @Email)" 
            OldValuesParameterFormatString="original_{0}" 
            ProviderName="System.Data.SqlClient" 
            SelectCommand="SELECT [ID], [FirstName], [LastName], [Login], [Email] FROM [tblUsers]" 
            UpdateCommand="UPDATE [tblUsers] SET [FirstName] = @FirstName, [LastName] = @LastName, [Login] = @Login, [Email] = @Email WHERE [ID] = @original_ID AND (([FirstName] = @original_FirstName) OR ([FirstName] IS NULL AND @original_FirstName IS NULL)) AND [LastName] = @original_LastName AND [Login] = @original_Login AND [Email] = @original_Email">
            <DeleteParameters>
                <asp:Parameter Name="original_ID" Type="Int32" />
                <asp:Parameter Name="original_FirstName" Type="String" />
                <asp:Parameter Name="original_LastName" Type="String" />
                <asp:Parameter Name="original_Login" Type="String" />
                <asp:Parameter Name="original_Email" Type="String" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="FirstName" Type="String" />
                <asp:Parameter Name="LastName" Type="String" />
                <asp:Parameter Name="Login" Type="String" />
                <asp:Parameter Name="Email" Type="String" />
                <asp:Parameter Name="original_ID" Type="Int32" />
                <asp:Parameter Name="original_FirstName" Type="String" />
                <asp:Parameter Name="original_LastName" Type="String" />
                <asp:Parameter Name="original_Login" Type="String" />
                <asp:Parameter Name="original_Email" Type="String" />
            </UpdateParameters>
            <InsertParameters>
                <asp:Parameter Name="FirstName" Type="String" />
                <asp:Parameter Name="LastName" Type="String" />
                <asp:Parameter Name="Login" Type="String" />
                <asp:Parameter Name="Email" Type="String" />
            </InsertParameters>
        </asp:SqlDataSource>
    
    </div>
    </form>
</body>
</html>

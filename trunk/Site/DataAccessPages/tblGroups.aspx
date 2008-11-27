<%@ Page Language="C#" AutoEventWireup="true" CodeFile="tblGroups.aspx.cs" Inherits="DataAccessPages_tblGroups" %>

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
                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
            </Columns>
        </asp:GridView>
        <asp:DetailsView ID="DetailsView1" runat="server" AllowPaging="True" 
            AutoGenerateRows="False" DataKeyNames="ID" DataSourceID="IUDICOSqlDataSource" 
            Height="50px" Width="125px">
            <PagerSettings Mode="NumericFirstLast" />
            <Fields>
                <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" 
                    ReadOnly="True" SortExpression="ID" />
                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                <asp:CommandField ShowInsertButton="True" />
            </Fields>
        </asp:DetailsView>
        <asp:SqlDataSource ID="IUDICOSqlDataSource" runat="server" 
            ConflictDetection="CompareAllValues" 
            ConnectionString="Data Source=MICHA-CORP\SQLEXPRESS;Initial Catalog=IUDICO;Integrated Security=True" 
            DeleteCommand="DELETE FROM [tblGroups] WHERE [ID] = @original_ID AND (([Name] = @original_Name) OR ([Name] IS NULL AND @original_Name IS NULL))" 
            InsertCommand="INSERT INTO [tblGroups] ([Name]) VALUES (@Name)" 
            OldValuesParameterFormatString="original_{0}" 
            ProviderName="System.Data.SqlClient" SelectCommand="SELECT * FROM [tblGroups]" 
            UpdateCommand="UPDATE [tblGroups] SET [Name] = @Name WHERE [ID] = @original_ID AND (([Name] = @original_Name) OR ([Name] IS NULL AND @original_Name IS NULL))">
            <DeleteParameters>
                <asp:Parameter Name="original_ID" Type="Int32" />
                <asp:Parameter Name="original_Name" Type="String" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="Name" Type="String" />
                <asp:Parameter Name="original_ID" Type="Int32" />
                <asp:Parameter Name="original_Name" Type="String" />
            </UpdateParameters>
            <InsertParameters>
                <asp:Parameter Name="Name" Type="String" />
            </InsertParameters>
        </asp:SqlDataSource>
    
    </div>
    </form>
</body>
</html>

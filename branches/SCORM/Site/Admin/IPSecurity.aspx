<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="IPSecurity.aspx.cs" Inherits="Admin_IPSecurity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h1>
        IP Security</h1>
    <table style="width:100%" bgcolor="White">
        <tr>
            <td align="left" bgcolor="White" width="200" >
                <asp:CheckBoxList ID="CheckBoxList1" Width="200" Height="200" valign="top" runat="server">
                </asp:CheckBoxList>
            </td>
            <td valign="top">
                <table style="width: 100%;">
                    <tr>
                        <td align="left" width="100">
                            <asp:Label ID="Label1" runat="server" Text="Enter ip:"></asp:Label>
                        </td>
                        <td align="left" width="150">
                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                        </td>
                        <td align="left">
                            <asp:Button ID="Button1" runat="server"  Width="140" Text="Add to black list" onclick="Button1_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                        </td>
                        <td align="left">
                        </td>
                        <td align="left">
                            <asp:Button ID="removeButton" runat="server"  Width="140" Text="Remove from black list" 
                            onclick="removeButton_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    
    
</asp:Content>


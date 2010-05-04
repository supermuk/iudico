<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Settings.aspx.cs"
Inherits="Admin_Settings" Title="Settings" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    <cc1:TabContainer ID="TabContainer1" runat="server" Width="100%" Height="" 
        ActiveTabIndex="1">
        <cc1:TabPanel HeaderText="Settings" runat=server ID="SettingsTab">
            <ContentTemplate>
                <h1>Settings available in the system</h1>
            <p class="descriptions">
            Press 'Add' to add new setting <br />
            Press 'Remove' to remove setting <br />
            </p>

            <div style="text-align:left">
                <asp:TextBox ID="tbSearchPattern" runat="server" /> <asp:Button ID="btnSearch" Text="Search" runat="server" />
            </div>

            <i:SettingList ID="SettingList" runat="server" />

            <div style="text-align: left">
                <asp:Button ID="btnCreateSetting" runat="server" Text="Create" />
            </div>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel HeaderText="IP Security" runat=server ID="IPSecurityTab">
            <ContentTemplate>
                <h1>
        IP Security</h1>
    <table style="width:100%" bgcolor="White">
        <tr>
            <td align="left" bgcolor="White" width="200" >
                <asp:CheckBoxList ID="CheckBoxList1" Width="200" Height="200" valign="top" 
                    runat="server" RepeatLayout="Flow">
                </asp:CheckBoxList>
            </td>
            <td valign="top">
                <table style="width: 100%;">
                    <tr>
                        <td align="left" width="150">
                            <asp:Label ID="Label1" runat="server" Text="Enter ip or range:"></asp:Label>
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
            </ContentTemplate>
        </cc1:TabPanel>
    </cc1:TabContainer>
    
</asp:Content>


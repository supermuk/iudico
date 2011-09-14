<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Settings.aspx.cs"
Inherits="Admin_Settings" Title="Settings" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    <cc1:TabContainer ID="TabContainer1" runat="server" Width="100%" Height="" 
        ActiveTabIndex="1">
        <cc1:TabPanel HeaderText="Settings" runat=server ID="SettingsTab">
            <ContentTemplate>
                <h1>
                    Settings available in the system</h1>
                <p class="descriptions">
            Press 'Add' to add new setting 
                    <br />
            Press 'Remove' to remove setting 
                    <br />
                </p>
                <div style="text-align:left">
                    <asp:TextBox ID="tbSearchPattern" runat="server" />
                    <asp:Button ID="btnSearch" Text="Search" runat="server" />
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
                            <asp:CheckBoxList ID="CheckBoxList1" Width="200px" Height="200px" valign="top" 
                    runat="server" RepeatLayout="Flow">
                            </asp:CheckBoxList>
                        </td>
                        <td valign="top">
                            <table style="width: 100%;">
                            <tr>
                                <td align="left" width="190">
                                    <asp:Label ID="Label3" runat="server" Text="Access for unspecified IPs:"></asp:Label>
                                </td>
                                <td align="left">
                                    <cc1:ComboBox ID="ComboBox1" runat="server" 
                                            DropDownStyle="Simple" 
                                        OnSelectedIndexChanged="ComboBox1_SelectedIndexChanged" AutoPostBack="True" 
                                        MaxLength="0" AutoCompleteMode="Suggest">
                                    <asp:ListItem>Allow</asp:ListItem>
                                    <asp:ListItem>Deny</asp:ListItem>
                                        </cc1:ComboBox>
                                </td>
                                <td align="left">
                                        &nbsp;</td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                    <td align="left" width="150">
                                        <asp:Label ID="Label1" runat="server" Text="Enter IP or range:"></asp:Label>
                                    </td>
                                    <td align="left" width="150">
                            &nbsp;<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:Button ID="Button1" runat="server"  Width="130px" Text="Add to list" 
                                onclick="Button1_Click" />
                                    </td>
                                    <td align=left>
                                        <asp:Button ID="removeButton" runat="server" OnClick="removeButton_Click" 
                                Text="Remove from list" Width="130px" />
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


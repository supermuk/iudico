<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GroupList.ascx.cs" Inherits="Controls_GroupList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:GridView ID="gvGroups" AutoGenerateColumns="false" Width="95%" OnRowDataBound="gvGroups_OnRowDataBound"
    AllowPaging="true" OnPageIndexChanging="GroupsPageIndexChanging" runat="server">
    <PagerSettings Position="TopAndBottom" Mode="NumericFirstLast" />
    <Columns>
        <asp:TemplateField ControlStyle-Width="80%" ItemStyle-Width="80%">
            <HeaderTemplate>
                <asp:Label ID="Label1" Text="Group" runat="server" />
            </HeaderTemplate>
            <ItemTemplate>
                <asp:LinkButton runat="server" ID="lnkGroupName" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField ControlStyle-Width="20%" ItemStyle-HorizontalAlign="Center">
            <ItemTemplate>
                <ajax:ModalPopupExtender ID="popup" CancelControlID="btnCancel" runat="server" TargetControlID="lnkAction"
                    PopupControlID="pnlPopup" BackgroundCssClass="modalBackground" DropShadow="true"
                    PopupDragHandleControlID="pnlPopup">
                </ajax:ModalPopupExtender>
                <asp:Panel ID="pnlPopup" runat="server" Width="300px" Height="70px" BackColor="#cccccc"
                    Style="display: none; width: 320px;">
                    <table style="padding: 5px; width: 100%" cellspacing="5">
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="Label1" BackColor="Transparent" runat="server" Text="Do you really want to delete this group?" />
                                <asp:HiddenField ID="groupID" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnOK" OnClick="btnOKClick" runat="server" Width="100px" Height="20px"
                                    Text="OK" />
                            </td>
                            <td align="right">
                                <asp:Button ID="btnCancel" runat="server" Width="100px" Height="20px" Text="Cancel" />
                            </td>
                        </tr>
                    </table>
                    
                    <br />
                </asp:Panel>
                <asp:Button runat="server" style="Width:100px" ID="lnkAction" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="StudentPage.aspx.cs" Inherits="StudentPage" Title="Student Page" %>
<%@ Register TagPrefix="iudico" Namespace="IUDICO.DataModel.Controllers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    
    <asp:Label runat = "server" ID = "headerLabel" Font-Bold="True" Font-Size="XX-Large"></asp:Label>
    <br />
    <asp:Label runat = "server" ID = "descriptionLabel"></asp:Label>
    <br />
    <table style="width:100%;">
        <tr>
            <td class="style1">
    
    <asp:Button ID="openTest" runat="server" Text="Open Test" />
    
    <asp:Button ID="showResult" runat="server" Text="Show Result"/>
    
            </td>
            <td class="style2">
   
    <asp:Button ID="rebuildTreeButton" runat="server" 
        Text="Refresh Tree" Width="111px"/>
   
    <asp:Button ID="modeChangerButton" runat="server" Text="Show Pass Dates" 
        Width="142px" />
   
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style1">
    
    <iudico:IdentityTreeView ID="curriculumTreeView" runat="server" ImageSet="XPFileExplorer" 
        NodeIndent="15" Width="122px" Height="188px">
        <ParentNodeStyle Font-Bold="False" />
        <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
        <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" 
            HorizontalPadding="0px" VerticalPadding="0px" />
        <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" 
            HorizontalPadding="2px" NodeSpacing="0px" VerticalPadding="2px" />
    </iudico:IdentityTreeView>
   
            </td>
            <td class="style2">
   
    <asp:Calendar ID="curriculumCalendar" runat="server" BackColor="White"
        BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" 
        Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" 
        ShowGridLines="True" Height="247px" Width="252px">
        <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
        <SelectorStyle BackColor="#CCCCCC" />
        <WeekendDayStyle BackColor="#FFFFCC" />
        <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
        <OtherMonthDayStyle ForeColor="#808080" />
        <NextPrevStyle VerticalAlign="Bottom" />
        <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
        <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
    </asp:Calendar>
   
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style1">
                &nbsp;</td>
            <td class="style2">
   
    <asp:Table ID="lastPagesResultTable" runat="server" GridLines="Both" Height="49px" 
        Width="481px">
        <asp:TableRow runat="server">
            <asp:TableCell runat="server" HorizontalAlign="Center">Page</asp:TableCell>
            <asp:TableCell runat="server" HorizontalAlign="Center">Theme Name</asp:TableCell>
            <asp:TableCell runat="server" HorizontalAlign="Center">Result</asp:TableCell>
            <asp:TableCell runat="server" HorizontalAlign="Center">Date</asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    
            </td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
   
    </asp:Content>


<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="StudentPage.aspx.cs" Inherits="StudentPage" Title="Student Page" %>
<%@ Register TagPrefix="iudico" Namespace="IUDICO.DataModel.Controllers" %>

<%@ Register assembly="BoxOver" namespace="BoxOver" tagprefix="boxover" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    
    <asp:Label runat = "server" ID = "_headerLabel" Font-Bold="True" Font-Size="XX-Large"></asp:Label>
    <br />
    <asp:Label runat = "server" ID = "_descriptionLabel"></asp:Label>
    <br />
    <table style="width:100%;">
        <tr>
            <td class="style1">
    
    <asp:Button ID="_openTest" runat="server" Text="Open Test" Enabled="False" />
    
    <asp:Button ID="_showResult" runat="server" Text="Show Result"/>
    
                <boxover:BoxOver ID="BoxOver1" runat="server" Body="Click to open test!" 
                    ControlToValidate="_openTest" Header="Help!" />
    
            </td>
            <td class="style2">
   
    <asp:Button ID="_rebuildTreeButton" runat="server" 
        Text="Refresh Tree" Width="111px" Height="26px"/>
   
    <asp:Button ID="_modeChangerButton" runat="server" Text="Show Pass Dates" 
        Width="129px" />
	<asp:Button ID="_showNotes" runat="server" Text="Show Notes" Width="108px" />
   
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style1">
    
                <boxover:BoxOver ID="BoxOver2" runat="server" Body="Click to show results!" 
                    ControlToValidate="_showResult" Header="Help!" />
    
    <iudico:IdentityTreeView ID="_curriculumTreeView" runat="server" ImageSet="XPFileExplorer" 
        NodeIndent="15" Width="122px" Height="188px">
        <ParentNodeStyle Font-Bold="False" />
        <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
        <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" 
            HorizontalPadding="0px" VerticalPadding="0px" />
        <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" 
            HorizontalPadding="2px" NodeSpacing="0px" VerticalPadding="2px" />
    </iudico:IdentityTreeView>
   
                <boxover:BoxOver ID="BoxOver3" runat="server" Body="Evailable curriculums!" 
                    ControlToValidate="_curriculumTreeView" Header="Help!" />
   
                <br />
                <asp:Label ID="Label3" runat="server" Text="Number of tests:"></asp:Label>
                <asp:TextBox ID="_testCount" runat="server" Width="46px">35</asp:TextBox>
   
            </td>
            <td class="style2">
   
                <asp:ListBox ID="_periodDescription" runat="server" Rows="1" Width="252px"></asp:ListBox>
   
                <boxover:BoxOver ID="BoxOver4" runat="server" Body="Refresh curriculum tree!" 
                    ControlToValidate="_rebuildTreeButton" Header="Help!" />
                <boxover:BoxOver ID="BoxOver5" runat="server" Body="Click to see passed date!" 
                    ControlToValidate="_modeChangerButton" Header="Help!" />
   
    <asp:Calendar ID="_curriculumCalendar" runat="server" BackColor="White"
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
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <boxover:BoxOver ID="BoxOver7" runat="server" 
                            Body="Put date decription in here" ControlToValidate="_userDescription" 
                            Header="Help!" />
                        <asp:TextBox ID="_userDescription" runat="server" Height="77px" Width="252px"></asp:TextBox>
                        <asp:Button ID="_descriptionButton" runat="server" Height="28px" Text="OK" 
                            Width="66px" />
                        <boxover:BoxOver ID="BoxOver6" runat="server" Body="Set description" 
                            ControlToValidate="_descriptionButton" Header="Help!" />
                    </ContentTemplate>
                </asp:UpdatePanel>   
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style1">
                &nbsp;</td>
            <td class="style2">
   
    <asp:Table ID="_lastPagesResultTable" runat="server" GridLines="Both" Height="49px" 
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


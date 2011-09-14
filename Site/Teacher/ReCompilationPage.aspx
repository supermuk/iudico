<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ReCompilationPage.aspx.cs" Inherits="Teacher_ReCompilationPage" Title="ReCompilation Page" %>

<%@ Register assembly="BoxOver" namespace="BoxOver" tagprefix="boxover" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:Label ID="_headerLabel" runat="server" Font-Bold="True" Font-Size="XX-Large"></asp:Label>
    <br />
    <asp:Label ID="_descriptionLabel" runat="server"></asp:Label>
    <table style="width:100%;">
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                <boxover:BoxOver ID="BoxOver5" runat="server" Body="Choose theme!" 
                    ControlToValidate="_themeDropDownList" Header="Help!" />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <boxover:BoxOver ID="BoxOver3" runat="server" Body="Click to build course!" 
                    ControlToValidate="_reCompileButton" Header="Help!" />
                <boxover:BoxOver ID="BoxOver1" runat="server" Body="Choose curriculum!" 
                    ControlToValidate="_curriculumnDropDownList" Header="Help!" />
            </td>
            <td>
                <boxover:BoxOver ID="BoxOver6" runat="server" Body="Choose user!" 
                    ControlToValidate="_userDropDownList" Header="Help!" />
                <boxover:BoxOver ID="BoxOver4" runat="server" Body="Choose stage!" 
                    ControlToValidate="_stageDropDownList" Header="Help!" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:DropDownList ID="_groupDropDownList" runat="server" AutoPostBack="True" 
                    Width="200px">
                </asp:DropDownList>
                <br />
                <br />
                <asp:DropDownList ID="_userDropDownList" runat="server" Width="200px">
                </asp:DropDownList>
            </td>
            <td>
                <boxover:BoxOver ID="BoxOver2" runat="server" Body="Choose grop!" 
                    ControlToValidate="_groupDropDownList" Header="Help!" />
            </td>
            <td>
                <asp:DropDownList ID="_curriculumnDropDownList" runat="server" 
                    AutoPostBack="True" Width="200px">
                </asp:DropDownList>
                <asp:DropDownList ID="_stageDropDownList" runat="server" AutoPostBack="True" 
                    Width="200px">
                </asp:DropDownList>
                <asp:DropDownList ID="_themeDropDownList" runat="server" AutoPostBack="True" 
                    Width="200px">
                </asp:DropDownList>
                        </td>
        </tr>
    </table>
    <br />
    <asp:Button ID="_reCompileButton" runat="server" Text="ReCompile" />
</asp:Content>


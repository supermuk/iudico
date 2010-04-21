<%@ Page Language="C#" CodeFile="Login.aspx.cs" Inherits="LoginPage" meta:resourcekey="PageResource1" culture="auto" uiculture="auto"  %>

<%@ Register assembly="BoxOver" namespace="BoxOver" tagprefix="boxover" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="Server">
    <center class="login-placeholder">
        <asp:Login ID="Login1" runat="server" CssClass="login" onload="Login1_Load" 
            meta:resourcekey="Login1Resource1">
            <LayoutTemplate>
                <table border="0" cellpadding="0">
                    <tr>
                        <td style="text-align:center" colspan="2">
                        <asp:Localize ID="Header" runat="server" 
                                Text="Please provide your user name & password:" 
                                meta:resourcekey="HeaderResource1"></asp:Localize>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName" 
                                meta:resourcekey="UserNameLabelResource1" Text="User Name:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="UserName" runat="server" meta:resourcekey="UserNameResource1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                ErrorMessage="User Name is required." ToolTip="User Name is required." 
                                ValidationGroup="Login1" meta:resourcekey="UserNameRequiredResource1" 
                                Text="*"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password" 
                                meta:resourcekey="PasswordLabelResource1" Text="Password:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="Password" runat="server" TextMode="Password" 
                                meta:resourcekey="PasswordResource1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                ErrorMessage="Password is required." ToolTip="Password is required." 
                                ValidationGroup="Login1" meta:resourcekey="PasswordRequiredResource1" 
                                Text="*"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:CheckBox ID="RememberMe" runat="server" Text="Remember me next time." 
                                meta:resourcekey="RememberMeResource1" />
                            <boxover:BoxOver ID="BoxOver2" runat="server" Body="Enter the password!" 
                                ControlToValidate="Password" Header="Help!" CssBody="" CssHeader="" 
                                Delay="0" DoubleClickStop="True" Fade="False" FadeSpeed="0.04" 
                                HideSelects="False" meta:resourcekey="BoxOver2Resource1" OffsetX="10" 
                                OffsetY="10" RequireClick="False" SingleClickStop="False" WindowLock="True" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2" style="color: Red;">
                            <asp:Literal ID="FailureText" runat="server" EnableViewState="False" 
                                meta:resourcekey="FailureTextResource1"></asp:Literal>
                            <boxover:BoxOver ID="BoxOver3" runat="server" Body="Press to Log In!" 
                                ControlToValidate="LoginButton" Header="Help!" CssBody="" CssHeader="" 
                                Delay="0" DoubleClickStop="True" Fade="False" FadeSpeed="0.04" 
                                HideSelects="False" meta:resourcekey="BoxOver3Resource1" OffsetX="10" 
                                OffsetY="10" RequireClick="False" SingleClickStop="False" WindowLock="True" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="text-align:right" colspan="2">
                            <boxover:BoxOver ID="BoxOver4" runat="server" 
                                Body="Check to rememder your password!" ControlToValidate="RememberMe" 
                                Header="Help!" CssBody="" CssHeader="" Delay="0" DoubleClickStop="True" 
                                Fade="False" FadeSpeed="0.04" HideSelects="False" 
                                meta:resourcekey="BoxOver4Resource1" OffsetX="10" OffsetY="10" 
                                RequireClick="False" SingleClickStop="False" WindowLock="True" />
                            <boxover:BoxOver ID="BoxOver1" runat="server" Body="Enter user name!" 
                                ControlToValidate="UserName" Header="Help!" CssBody="" CssHeader="" 
                                Delay="0" DoubleClickStop="True" Fade="False" FadeSpeed="0.04" 
                                HideSelects="False" meta:resourcekey="BoxOver1Resource1" OffsetX="10" 
                                OffsetY="10" RequireClick="False" SingleClickStop="False" WindowLock="True" />
                            <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Log In" 
                                ValidationGroup="Login1" meta:resourcekey="LoginButtonResource1" />
                        </td>
                    </tr>
                </table>
            </LayoutTemplate>
        </asp:Login>
    </center>
</asp:Content>

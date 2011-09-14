<%@ Page Language="C#" CodeFile="Login.aspx.cs" Inherits="LoginPage" %>

<%@ Register assembly="BoxOver" namespace="BoxOver" tagprefix="boxover" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="Server">
    <center class="login-placeholder">
        <asp:Login ID="Login1" runat="server" CssClass="login">
            <LayoutTemplate>
                <table border="0" cellpadding="0">
                    <tr>
                        <td style="text-align:center" colspan="2">
                            Please provide your user name & password:
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">User Name:</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:CheckBox ID="RememberMe" runat="server" Text="Remember me next time." />
                            <boxover:BoxOver ID="BoxOver2" runat="server" Body="Enter the password!" 
                                ControlToValidate="Password" Header="Help!" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2" style="color: Red;">
                            <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                            <boxover:BoxOver ID="BoxOver3" runat="server" Body="Press to Log In!" 
                                ControlToValidate="LoginButton" Header="Help!" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="text-align:right" colspan="2">
                            <boxover:BoxOver ID="BoxOver4" runat="server" 
                                Body="Check to rememder your password!" ControlToValidate="RememberMe" 
                                Header="Help!" />
                            <boxover:BoxOver ID="BoxOver1" runat="server" Body="Enter user name!" 
                                ControlToValidate="UserName" Header="Help!" />
                            <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Log In" ValidationGroup="Login1" />
                        </td>
                    </tr>
                </table>
            </LayoutTemplate>
        </asp:Login>
    </center>
</asp:Content>

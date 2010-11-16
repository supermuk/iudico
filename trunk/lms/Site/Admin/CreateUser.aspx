<%@ Page Title="Create Multiple Users" Language="C#" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeFile="CreateUser.aspx.cs" Inherits="CreateUser" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <h1>
        Create User</h1>
    <p class="descriptions">
        To create IUDICO account fill in the form and press 'Create User' button
        <br />
        To create many users for the time hit 'Create multiple' button<br />
    </p>
    <asp:CreateUserWizard ID="CreateUserWizard1" FinishDestinationPageUrl="~/Login.aspx"
        CancelDestinationPageUrl="~/Login.aspx" ContinueDestinationPageUrl="~/Admin/Users.aspx"
        LoginCreatedUser="False" runat="server">
        <WizardSteps>
            <asp:CreateUserWizardStep ID="CreateUserWizardStep1" runat="server">
                <ContentTemplate>
                    <table border="0" style="font-size: 100%; font-family: Verdana">
                        <tr>
                            <td align="center" colspan="2" style="font-weight: bold; color: white; background-color: #5d7b9d">
                                Sign Up for Your New Account
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">
                        User Name:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="UserNameRequired" Display="Dynamic" Font-Size="Small" runat="server" ControlToValidate="UserName"
                                    ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                                <ajax:ValidatorCalloutExtender runat="Server" ID="PNReqE" TargetControlID="UserNameRequired"
                                    Width="200px" HighlightCssClass="highlight" WarningIconImageUrl="~/Images/warning.png"
                                    CloseImageUrl="~/Images/close.png" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">
                        Password:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="PasswordRequired" Display="Dynamic" runat="server" ControlToValidate="Password"
                                    ErrorMessage="Password is required." Font-Size="Small" ToolTip="Password is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                                <ajax:PasswordStrength ID="PS" runat="server" TargetControlID="Password" DisplayPosition="RightSide"
                                    StrengthIndicatorType="Text" PreferredPasswordLength="8" PrefixText="Strength:"
                                    MinimumNumericCharacters="1" MinimumSymbolCharacters="1" RequiresUpperAndLowerCaseCharacters="false"
                                    TextStrengthDescriptions="Very Poor;Weak;Average;Strong;Excellent" StrengthStyles="cssClass1;cssClass2;cssClass3;cssClass4;cssClass5"
                                    CalculationWeightings="50;15;15;20" />
                                <ajax:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender1" TargetControlID="PasswordRequired"
                                    Width="200px" HighlightCssClass="highlight" WarningIconImageUrl="~/Images/warning.png"
                                    CloseImageUrl="~/Images/close.png" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="ConfirmPasswordLabel" runat="server" AssociatedControlID="ConfirmPassword">
                        Confirm Password:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="ConfirmPassword" runat="server" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" ControlToValidate="ConfirmPassword"
                                    ErrorMessage="Confirm Password is required." Display="Dynamic" Font-Size="Small" ToolTip="Confirm Password is required."
                                    ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                                <ajax:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender2" TargetControlID="ConfirmPasswordRequired"
                                    Width="200px" HighlightCssClass="highlight" WarningIconImageUrl="~/Images/warning.png"
                                    CloseImageUrl="~/Images/close.png" />
                                     <asp:CompareValidator ID="PasswordCompare" Font-Size="Small" runat="server" ControlToCompare="Password"
                                    ControlToValidate="ConfirmPassword" Display="Dynamic" ErrorMessage="The Password and Confirmation Password must match."
                                    ValidationGroup="CreateUserWizard1">*</asp:CompareValidator>
                                     <ajax:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender4" TargetControlID="PasswordCompare"
                                    Width="250px" HighlightCssClass="highlight" WarningIconImageUrl="~/Images/warning.png"
                                    CloseImageUrl="~/Images/close.png" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email">
                        E-mail:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="Email" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="EmailRequired" Display="Dynamic" runat="server" ControlToValidate="Email"
                                    ErrorMessage="E-mail is required." Font-Size="Small" ToolTip="E-mail is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                               
                                <ajax:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender3" TargetControlID="EmailRequired"
                                    Width="200px" HighlightCssClass="highlight" WarningIconImageUrl="~/Images/warning.png"
                                    CloseImageUrl="~/Images/close.png" />
                               
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2" style="color: red">
                                <asp:Literal ID="ErrorMessage" runat="server" EnableViewState="False"></asp:Literal>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:CreateUserWizardStep>
            <asp:CompleteWizardStep runat="server" />
        </WizardSteps>
    </asp:CreateUserWizard>
    <div style="text-align: left">
        <asp:Button runat="server" ID="lbCreateMultiple" Text="Create multiple" />
    </div>
</asp:Content>

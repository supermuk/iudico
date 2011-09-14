<%@ Page Title="Create Multiple Users" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CreateUser.aspx.cs" Inherits="CreateUser" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h1>Create User</h1>
    To create IUDICO account fill in the form and press 'Create User' button <br />
    To create many users for the time click on 'Create multiple' link <br /><br />
    <asp:CreateUserWizard 
        ID="CreateUserWizard1" 
        FinishDestinationPageUrl="~/Login.aspx" 
        CancelDestinationPageUrl="~/Login.aspx" 
        ContinueDestinationPageUrl="~/Admin/Users.aspx"
        LoginCreatedUser="False"
        runat="server" >
        <WizardSteps>
            <asp:CreateUserWizardStep runat="server" />
            <asp:CompleteWizardStep runat="server" />
        </WizardSteps>
    </asp:CreateUserWizard>
    <asp:LinkButton runat="server" ID="lbCreateMultiple" Text="Create multiple" />
</asp:Content>


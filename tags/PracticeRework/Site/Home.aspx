<%@ Page Language="C#" CodeFile="Home.aspx.cs" Inherits="IUDICO.Site.Pages.Home" %>

<asp:Content id="Content2" contentplaceholderid="MainContent" runat="Server">
<asp:Button runat="server" ID="Button1" Text="Test1" />
<br />
<asp:Button runat="server" ID="Button2" Text="Test2" />
<br />
<asp:Button runat="server" ID="Button3" Text="Increment controller value" />
<asp:Label runat="server" ID="lbIncrement" />
<br />

<asp:Button runat="server" ID="TestPersistedListButton" Text="TestPersistedListButton" />
<asp:Label runat="server" ID="PersistedListLabel" />

<i:UserPermissions ID="UserPermissions" runat="server" ObjectType="COURSE" />

</asp:Content>


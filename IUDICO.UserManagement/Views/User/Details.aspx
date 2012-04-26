<%@ Assembly Name="IUDICO.UserManagement" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.UserManagement.Models.AdminDetailsModel>" %>
<%@ Import Namespace="IUDICO.UserManagement" %>
<%@ Import Namespace="IUDICO.Common" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=Localization.GetMessage("DetailsOfUser")%> <%=Model.Username%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%=Localization.GetMessage("DetailsOfUser")%> <%=Model.Username%></h2>

    <fieldset>
        <legend><%=Localization.GetMessage("Fields")%></legend>
        <%=Html.Image("avatar", Model.Id, new {width = 100, height = 150})%>
        <%:Html.DisplayForModel()%>
        
    </fieldset>

    <fieldset>
        <legend><%=Localization.GetMessage("Roles")%></legend>

        <table>
        <tr>
            <th>
                <%=Localization.GetMessage("Name")%>
            </th>
            <th></th>
        </tr>
        
        <%
            if (Model.Roles.Count() > 0)
            {
                foreach (var role in Model.Roles)
                {%>
        <tr>
            <td><%:role.ToString()%></td>
            <td><%:Html.ActionLink(Localization.GetMessage("Remove"), "RemoveFromRole",
                                                      new {id = Model.Id, roleRef = (int)role})%></td>
        </tr>
        <%
                }
            }
            else
            {%>
           <tr>
            <td><%=Localization.GetMessage("NoData")%></td>
            <td><%=Localization.GetMessage("NoActions")%></td>
           </tr>
           <%
            }%>

        </table>
    </fieldset>

    <fieldset>
        <legend><%=Localization.GetMessage("Groups")%></legend>

        <table>
        <tr>
            <th>
                <%=Localization.GetMessage("Name")%>
            </th>
            <th></th>
        </tr>
        
        <%
            if (Model.Groups.Count() > 0)
            {
                foreach (var group in Model.Groups)
                {%>
        <tr>
            <td><%:group.Name%></td>
            <td><%:Html.ActionLink(Localization.GetMessage("Remove"), "RemoveFromGroup",
                                                      new {id = Model.Id, groupRef = group.Id})%></td>
        </tr>
        <%
                }
            }
            else
            {%>
           <tr>
            <td><%=Localization.GetMessage("NoData")%></td>
            <td><%=Localization.GetMessage("NoActions")%></td>
           </tr>
           <%
            }%>

        </table>
    </fieldset>

    <p>
        <%
            if (Model.IsApproved)
            {%>
            <%:Html.ActionLink(Localization.GetMessage("Deactivate"), "Deactivate",
                                                  new {id = Model.Id})%> |
        <%
            }
            else
            {%>
            <%:Html.ActionLink(Localization.GetMessage("Activate"), "Activate", new {id = Model.Id})%> |
        <%
            }%>
        <%:Html.ActionLink(Localization.GetMessage("Edit"), "Edit", new {id = Model.Id})%> |
        <%:Html.ActionLink(Localization.GetMessage("AddToGroup"), "AddToGroup", new {id = Model.Id})%> |
        <%:Html.ActionLink(Localization.GetMessage("AddToRole"), "AddToRole", new {id = Model.Id})%> |
        <%:Html.ActionLink(Localization.GetMessage("Delete"), "Delete", new {id = Model.Id})%> |
        <%:Html.ActionLink(Localization.GetMessage("BackToList"), "Index")%>
    </p>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>


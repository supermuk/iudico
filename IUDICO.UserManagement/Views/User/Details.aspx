<%@ Assembly Name="IUDICO.UserManagement" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.UserManagement.Models.AdminDetailsModel>" %>
<%@ Import Namespace="IUDICO.UserManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=Localization.getMessage("DetailsOfUser")%> <%=Model.Username%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%=Localization.getMessage("DetailsOfUser")%> <%=Model.Username%></h2>

    <fieldset>
        <legend><%=Localization.getMessage("Fields")%></legend>
        <%=Html.Image("avatar", Model.Id, new {width = 100, height = 150})%>
        <%:Html.DisplayForModel()%>
        
    </fieldset>

    <fieldset>
        <legend><%=Localization.getMessage("Roles")%></legend>

        <table>
        <tr>
            <th>
                <%=Localization.getMessage("Name")%>
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
            <td><%:Html.ActionLink(Localization.getMessage("Remove"), "RemoveFromRole",
                                                      new {id = Model.Id, roleRef = (int) role})%></td>
        </tr>
        <%
                }
            }
            else
            {%>
           <tr>
            <td><%=Localization.getMessage("NoData")%></td>
            <td><%=Localization.getMessage("NoActions")%></td>
           </tr>
           <%
            }%>

        </table>
    </fieldset>

    <fieldset>
        <legend><%=Localization.getMessage("Groups")%></legend>

        <table>
        <tr>
            <th>
                <%=Localization.getMessage("Name")%>
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
            <td><%:Html.ActionLink(Localization.getMessage("Remove"), "RemoveFromGroup",
                                                      new {id = Model.Id, groupRef = group.Id})%></td>
        </tr>
        <%
                }
            }
            else
            {%>
           <tr>
            <td><%=Localization.getMessage("NoData")%></td>
            <td><%=Localization.getMessage("NoActions")%></td>
           </tr>
           <%
            }%>

        </table>
    </fieldset>

    <p>
        <%
            if (Model.IsApproved)
            {%>
            <%:Html.ActionLink(Localization.getMessage("Deactivate"), "Deactivate",
                                                  new {id = Model.Id})%> |
        <%
            }
            else
            {%>
            <%:Html.ActionLink(Localization.getMessage("Activate"), "Activate", new {id = Model.Id})%> |
        <%
            }%>
        <%:Html.ActionLink(Localization.getMessage("Edit"), "Edit", new {id = Model.Id})%> |
        <%:Html.ActionLink(Localization.getMessage("AddToGroup"), "AddToGroup", new {id = Model.Id})%> |
        <%:Html.ActionLink(Localization.getMessage("AddToRole"), "AddToRole", new {id = Model.Id})%> |
        <%:Html.ActionLink(Localization.getMessage("Delete"), "Delete", new {id = Model.Id})%> |
        <%:Html.ActionLink(Localization.getMessage("BackToList"), "Index")%>
    </p>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>


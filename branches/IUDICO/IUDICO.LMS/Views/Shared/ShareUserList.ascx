<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<IUDICO.Common.Models.Shared.ShareUser>>" %>
<%@ Import Namespace="IUDICO.Common" %>

<table id="shareUserTable" >
    <thead>
        <tr>
            <th class="checkboxColumn">
            </th>
            <th class="photoColumn"> <%:Localization.GetMessage("Photo") %></th>
            <th class="usernameColumn">
                <%:Localization.GetMessage("Name") %>
            </th>
        </tr>
    </thead>
    <tbody>
        <% foreach (var item in Model)
           { %>
        <tr>
            <td>
                <input type="checkbox" value="<%: item.Id %>" name="sharewith" <%: (item.Shared? "checked='checked'" : "") %> />
            </td>
            <td>
                <%=Html.Image("avatar", item.Id, new {width = 50, height = 50})%>
            </td>
            <td>
                <div class="shareUsername"><%: item.Name %></div>
                <div class="shareRoles">
                    <%: string.Join(", ", item.Roles) %>
                </div>
                
            </td>
        </tr>
        <% } %>
    </tbody>
</table>

<%@ Assembly Name="IUDICO.UserManagement" %>

<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<IUDICO.Common.Models.Shared.User>>" %>

<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="IUDICO.Common.Models" %>
<%@ Import Namespace="IUDICO.UserManagement" %>
<%@ Import Namespace="IUDICO.Common" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=Localization.GetMessage("Users")%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    
    <p>
        <%:Html.ActionLink(Localization.GetMessage("CreateNewUser"), "Create")%>
        |
        <%:Html.ActionLink(Localization.GetMessage("CreateNewUsers"), "CreateMultiple")%>
    </p>

    <div id="catalog" dir="<%=Html.ResolveUrl("/Content/images/status_icon_delete.png")%> ">
        <h2><%:Localization.GetMessage("Roles") %></h2>
        <ul>
            <li class="example1">Student</li>
            <li class="example1">Teacher</li>
            <li class="example1">CourseCreator</li>
            <li class="example1">Admin</li>
        </ul>
    </div>
    <div>
        <h2>
            <%=Localization.GetMessage("Users")%></h2>
    </div>
    <div id="tableContainer">
        <table id="myDataTable" class="display">
            <thead>
                <tr>
                    <th>
                        <%=Localization.GetMessage("FullName")%>
                    </th>
                    <th>
                        <%=Localization.GetMessage("Loginn")%>
                    </th>
                    <th  class="checkboxColumn">
                        <%=Localization.GetMessage("Active")%>
                    </th>
                    <th class="bigColumn">
                        <%=Localization.GetMessage("Roles")%>
                    </th>
                    <th class="normalColumn">
                        <%=Localization.GetMessage("ApprovedBy")%>
                    </th>
                    <th class="normalColumn">
                        <%=Localization.GetMessage("CreationDate")%>
                    </th>
                    <th class="normalColumn">
                        <%=Localization.GetMessage("Groups")%>
                    </th>
                    <th class="actionsColumn">
                    </th>
                </tr>
            </thead>
            <tbody id="cart">
                <%
                    foreach (var item in Model)
                    {%>
                <tr id="<%:item.Id%>">
                    <td>
                        <%:item.Name%>
                    </td>
                    <td>
                        <%:item.Username%>
                    </td>
                    <td>
                        <%:item.IsApproved.ToString()%>
                    </td>
                    <td>
                        <div>
                            <ol class="ui-droppable ui-sortable">
                                <%
            foreach (Role role in item.Roles)
            {%>
                                <li>
                                    <%:role%>
                                    <img src="<%=Html.ResolveUrl("/Content/images/status_icon_delete.png")%>" class="<%:role%>"
                                        alt="Remove role" />
                                </li>
                                <%
            }%>
                            </ol>
                        </div>
                    </td>
                    <td>
                        <%:item.User1 != null ? item.User1.Username : string.Empty%>
                    </td>
                    <td>
                        <%:item.CreationDate.ToString()%>
                    </td>
                    <td>
                        <%:item.GroupsLine%>
                    </td>
                    <td>
                        <%
            if (item.IsApproved)
            {%>
                        <%:Html.ActionLink(Localization.GetMessage("Deactivate"), "Deactivate",
                                                  new {id = item.Id})%>
                        |
                        <%
            }
            else
            {%>
                        <%:Html.ActionLink(Localization.GetMessage("Activate"), "Activate", new {id = item.Id})%>
                        |
                        <%
            }%>
                        <%:Html.ActionLink(Localization.GetMessage("Edit"), "Edit", new {id = item.Id})%>
                        |
                        <%:Html.ActionLink(Localization.GetMessage("Details"), "Details", new {id = item.Id})%>
                        |
                        <%:Html.ActionLink(Localization.GetMessage("AddToRole"), "AddToRole", new {id = item.Id})%>
                        |
                        <%:Html.ActionLink(Localization.GetMessage("AddToGroup"), "AddToGroup", new {id = item.Id})%>
                        |
                        <%:Ajax.ActionLink(Localization.GetMessage("Delete"), "Delete", new {id = item.Id},
                                              new AjaxOptions
                                                  {
                                                      Confirm =
                                                          String.Concat(Localization.GetMessage("AreYouSureYouWantToDelete"),"\"") + item.Username + "\"?",
                                                      HttpMethod = "Delete",
                                                      OnSuccess = "removeRow"
                                                  })%>
                    </td>
                </tr>
                <%
        }%>
            </tbody>
        </table>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script language="javascript" type="text/javascript">


        $(function () {
            $("div li").mousedown(function () {
                $(this).addClass('mouselol');
            });
        });

        $(function () {
            $(this).mouseup(function () {
                $(this).removeClass('mouselol');
                $(this).addClass('mouseolol');
            });
        });

    </script>
    <script language="javascript" type="text/javascript">

        $(function getImagePath() {
            return document.getElementById('catalog').dir;
        });

        $(function () {

            var src = document.getElementById('catalog').dir;

            $("#catalog li").draggable({
                appendTo: "body",
                helper: "clone"
            });
            $("#cart ol").droppable({
                activeClass: "ui-state-default",
                hoverClass: "ui-state-hover",
                accept: ":not(.ui-sortable-helper)",
                drop: function (event, ui) {
                    $(this).find(".placeholder").remove();

                    /*checking*/
                    var double = false;

                    var itemmm = ui.draggable.text();
                    var array = $(this).find("img");

                    for (var i = 0; i < array.length; i++) {
                        if (array[i].className == itemmm) {
                            alert(itemmm + ' already exist');
                            double = true;
                            break;
                        }
                    }

                    if (!double) {
                        addnewrole($(this).parent().parent().parent().attr('id'), itemmm, 'AddItem', this, ui);
                    }

                }
            }).sortable({
                items: "li:not(.placeholder)",
                sort: function () {
                    $(this).removeClass("ui-state-default");
                }
            });

            function ajaxExecuting(studentId, roleId) {
                alert('Id = ' + studentId + ' role = ' + roleId);
            }

        });

        $(document).ready(function () {
            $('li img').bind('dblclick', function () {
                addnewrole($(this).parent().parent().parent().parent().parent().attr('id'), $(this).attr('class'), 'DeleteItem');
                $(this).parent().remove();
            });
        });

        function addnewrole(userIdVal, roleVal, requesttype, olist, ui) {

            $.ajax({
                type: "post",
                url: "/User/" + requesttype,
                data: { userId: userIdVal, role: roleVal },
                success: function (r) {
                    if (r.success == true) {

                        if (requesttype == 'AddItem') {
                            var src = document.getElementById('catalog').dir;
                            $("<li></li>").text(ui.draggable.text()).appendTo(olist);
                            $(olist).children().last().append('<img src="' + src + '" class="' + $(olist).children().last().text() + '" />');

                            $('li img').bind('dblclick', function () {
                                addnewrole($(this).parent().parent().parent().parent().parent().attr('id'), ui.draggable.text(), 'DeleteItem');
                                $(this).parent().remove();
                            });
                        }
                        alert('Success');
                    }
                    else {

                        alert('Error. Probably you have no permission to change roles');
                    }
                }
            });
        }

    </script>
    <script language="javascript" type="text/javascript">

        $(document).ready(function () {
            $('#myDataTable').dataTable({
                "bJQueryUI": true,
                "sPaginationType": "full_numbers",
                iDisplayLength: 50,
                "bSort": true,
                "bAutoWidth": false,
                "aoColumns": [
                null,
                null,
                { "bSortable": false },
                null,
                null,
                null,
                { "bSortable": false },
                null
                ],
                "fnDrawCallback": function () {

                    $(function () {

                        var src = document.getElementById('catalog').dir;
                        $("#catalog li").draggable({
                            appendTo: "body",
                            helper: "clone"
                        });
                        $("#cart ol").droppable({
                            activeClass: "ui-state-default",
                            hoverClass: "ui-state-hover",
                            accept: ":not(.ui-sortable-helper)",
                            drop: function (event, ui) {
                                $(this).find(".placeholder").remove();

                                /*checking*/
                                var double = false;

                                var itemmm = ui.draggable.text();
                                var array = $(this).find("img");

                                for (var i = 0; i < array.length; i++) {
                                    if (array[i].className == itemmm) {
                                        alert(itemmm + ' already exist');
                                        double = true;
                                        break;
                                    }
                                }

                                if (!double) {
                                    addnewrole($(this).parent().parent().parent().attr('id'), itemmm, 'AddItem', this, ui);
                                }

                            }
                        }).sortable({
                            items: "li:not(.placeholder)",
                            sort: function () {
                                $(this).removeClass("ui-state-default");
                            }
                        });

                        function ajaxExecuting(studentId, roleId) {
                            alert('Id = ' + studentId + ' role = ' + roleId);
                        }

                    });

                }
            });

        });
        
    </script>
        
    <script type="text/javascript" language="javascript">
        function removeRow(data) {
            window.location = window.location;
        }
    </script>

    <style type="text/css">
        #catalog
        {

        }
        #cart ol
        {
            margin: 0;
            padding: 1em 0 1em 3em;
        }
        #magleft
        {
            float: left;
            width: 150px;
            margin-right: 2em;
        }
    </style>
    <style type="text/css">
        .example1
        {
            border-width: 1px;
            border-style: solid;
            border-color: gray;
            border-radius: 7px;
            text-align: center;
            display: inline-block;
            margin: 4px;
            padding: 2px 5px;
        }
        
        .example1:hover
        {
            cursor: pointer;
            background-color: Silver;
        }
        
        .mouselol
        {
            cursor: pointer;
        }
        .mouseolol
        {
            cursor: default;
        }
    </style>
</asp:Content>

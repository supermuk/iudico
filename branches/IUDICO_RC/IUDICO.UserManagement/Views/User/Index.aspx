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
        |
        <a id="ActivateSelected" href="#"><%: Localization.GetMessage("ActivateSelected")%></a>
        |
        <a id="DeactivateSelected" href="#"><%: Localization.GetMessage("DeactivateSelected")%></a>
        |
        <a id="AddToRoleSelected" onclick="addUsersToRole();"><%: Localization.GetMessage("AddToRoleSelected")%></a>
        |
        <a id="AddToGroupSelected" onclick="addUsersToGroup()"><%: Localization.GetMessage("AddToGroupSelected")%></a>
        |
        <a id="DeleteSelected" href="#"><%: Localization.GetMessage("DeleteSelected")%></a>
    </p>

    <div id="catalog" dir="<%=Html.ResolveUrl("/Content/images/status_icon_delete.png")%> ">
        <h2><%:Localization.GetMessage("Roles") %></h2>
        <ul>
            <li class="role" style="cursor:pointer;">Student</li>
            <li class="role" style="cursor:pointer;">Teacher</li>
            <li class="role" style="cursor:pointer;">CourseCreator</li>
            <li class="role" style="cursor:pointer;">Admin</li>
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
                    <th class="checkboxColumn">
                        <input type="checkbox" id="UsersCheckAll" />
                    </th>
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
                     <td class="userCheck">
                        <input type="checkbox" id="<%= item.Id %>" />
                    </td>
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

                        <%:Html.ActionLink(Localization.GetMessage("Edit"), "Edit", new {id = item.Id})%>
                        |
                        <%:Html.ActionLink(Localization.GetMessage("Details"), "Details", new {id = item.Id})%>
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

		     function onAddUsersToRoleSuccess(resp) {
			     if (resp.success) {
				     $("#dialog").dialog("close");
				     window.location.reload(true); // TODO : fix adding course if table is empty.
			     } else {
				     fillDialogInner(resp.html, " ", " ");
			     }
		     }

		     function addUsersToRole() {
        		var usersId = $("input:checked:not(id$='UsersCheckAll')").map(function () {
					return $(this).attr('id');
				});

				if (usersId.length == 0) {
					alert("<%=Localization.GetMessage("PleaseSelectUsers") %>");
                    
					return false;
				}

				window.location = actionUrlForRole.replace('PLACEHOLDER', usersId.toArray());
        }
		     	function addUsersToGroup() {
        		var usersId = $("input:checked:not(id$='UsersCheckAll')").map(function () {
					return $(this).attr('id');
				});

				if (usersId.length == 0) {
					alert("<%=Localization.GetMessage("PleaseSelectUsers") %>");
                    
					return false;
				}

				window.location = actionUrlForGroup.replace('PLACEHOLDER', usersId.toArray());

        }
			 var actionUrlForRole = '<%= Url.Action("AddUsersToRole", "User", new { usersId = "PLACEHOLDER" } ) %>';
			 var actionUrlForGroup = '<%= Url.Action("AddUsersToGroup", "User", new { usersId = "PLACEHOLDER" } ) %>';
			     
    </script>
    <script language="javascript" type="text/javascript">

        $(document).ready(function () {
            var dataTable = $('#myDataTable').dataTable({
                "bJQueryUI": true,
                "sPaginationType": "full_numbers",
                iDisplayLength: 50,
                "bSort": true,
                "bAutoWidth": false,
                "aoColumns": [
                { "bSortable": false },
                null,
                null,
                null,
                null,
                null,
                { "sType": 'date' },
                null,
                { "bSortable": false }
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

            $(".role").click(function () {
                dataTable.fnFilter($(this).text());
            });


            $("input[id$='UsersCheckAll']").click(function () {
                var $boxes = $("td input:checkbox:not(id$='UsersCheckAll')");
                if (this.checked) {
                    $boxes.attr("checked", "checked");
                } else {
                    $boxes.removeAttr("checked");
                }
            });

            $("td input:checkbox:not(id$='UsersCheckAll')").click(function () {
                if ($("td input:checkbox:not(id$='UsersCheckAll')").length == $("td input:checked:not(id$='UsersCheckAll')").length) {
                    $('input[id$="UsersCheckAll"]').attr('checked', true);
                } else {
                    $('input[id$="UsersCheckAll"]').attr('checked', false);
                }
            });

            $("#ActivateSelected").click(function () {
                var ids = $("input:checked:not(id$='UsersCheckAll')").map(function () {
                    return $(this).attr('id');
                });

                if (ids.length == 0) {
                    alert("<%=Localization.GetMessage("PleaseSelectUsers") %>");
                    
                    return false;
                }


                $.ajax({
                    type: "POST",
                    url: "/User/ActivateSelected",
                    traditional: true,
                    data: { usersIds: ids.toArray() },
                    success: function(r) {
                        if (r.success) {
                            window.location = "/User/Index";
                        } else {
                            alert("<%=Localization.GetMessage("ErrorOccuredDuringProccessingRequest") %>");
                        }
                    }
                });
                });
            
              $("#DeactivateSelected").click(function () {
                var ids = $("input:checked:not(id$='UsersCheckAll')").map(function () {
                    return $(this).attr('id');
                });

                if (ids.length == 0) {
                    alert("<%=Localization.GetMessage("PleaseSelectUsers") %>");
                    
                    return false;
                }


                $.ajax({
                    type: "POST",
                    url: "/User/DeactivateSelected",
                    traditional: true,
                    data: { usersIds: ids.toArray() },
                    success: function(r) {
                        if (r.success) {
                            window.location = "/User/Index";
                        } else {
                            alert("<%=Localization.GetMessage("ErrorOccuredDuringProccessingRequest") %>");
                        }
                    }
                });
                });
            
            $("#DeleteSelected").click(function () {
                var ids = $("input:checked:not(id$='UsersCheckAll')").map(function () {
                    return $(this).attr('id');
                });

                if (ids.length == 0) {
                    alert("<%=Localization.GetMessage("PleaseSelectUsers") %>");
                    
                    return false;
                }

                 var answer = confirm("<%=Localization.GetMessage("AreYouSureYouWantDelete") %>" + ids.length + "<%=Localization.GetMessage("selectedUsers") %>");

                if (answer == false) {
                    return false;
                }

                $.ajax({
                    type: "POST",
                    url: "/User/DeleteSelected",
                    traditional: true,
                    data: { usersIds: ids.toArray() },
                    success: function(r) {
                        if (r.success) {
                            window.location = "/User/Index";
                        } else {
                            alert("<%=Localization.GetMessage("ErrorOccuredDuringProccessingRequest") %>");
                        }
                    }
                });
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
        .role
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

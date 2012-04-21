<%@ Assembly Name="IUDICO.UserManagement" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<IUDICO.Common.Models.Shared.User>>" %>
<%@ Import Namespace="IUDICO.UserManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=Localization.getMessage("Users")%>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">



    <script language="javascript" type="text/javascript">
    	    $(function () {
    	        $("#catalog").accordion({
    	            collapsible: true,
    	            autoHeight: true,
    	            active: 2
    	        });
    	    });

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
            $("#catalog").accordion();
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

                    for (var i = 0; i < array.length; i++) 
                    {
                    	if (array[i].className == itemmm) 
                    	{
                            alert(itemmm + ' already exist');
                            double = true;
                            break;
                        }
                    }

                    if (!double) 
                    {
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
	    					$("#catalog").accordion();
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

    <div id="catalog" style="padding: 0 0px; float:right; text-align: left;" dir="<%= Html.ResolveUrl("/Content/images/status_icon_delete.png")%> ">
        <h3><a href="#" style="background-color:Silver;">Roles</a></h3>
        <div class="magleft">
			    <li class="example1">Student</li>
			    <li class="example1">Teacher</li>
			    <li class="example1">CourseCreator</li>
                <li class="example1">Admin</li>
	    </div>
    </div>

    <div>
    <h2><%=Localization.getMessage("Users")%></h2>
    </div>

    <div id="demo">

    <table id="myDataTable" class="display">

    <thead>

        <tr>
            <th>
                <%=Localization.getMessage("FullName")%>
            </th>

            <th>
                <%=Localization.getMessage("Loginn")%>
            </th>

            <th>
                <%=Localization.getMessage("Active")%>
            </th>

            <th>Roles</th>

            <th>
                <%=Localization.getMessage("ApprovedBy")%>
            </th>

            <th>
                <%=Localization.getMessage("CreationDate")%>
            </th>

            <th>
                <%=Localization.getMessage("Groups")%>
            </th>

            <th>
            </th>
        </tr>
    
    </thead>

    <tbody id="cart">

    <%foreach (var item in Model)
        {%>
    
        <tr id="<%: item.Id %>">
            <td>	<%:item.Name%>	</td>
            <td>	<%:item.Username%>	</td>
            <td>	<%:item.IsApproved.ToString()%>	</td>
            <td>
				<div>
					<ol class="ui-droppable ui-sortable">
					<% foreach (IUDICO.Common.Models.Role role in item.Roles) {%>
								<li> <%: role %> <img src="<%= Html.ResolveUrl("/Content/images/status_icon_delete.png")%>" class="<%: role %>" alt="Remove role" /> </li> 
					<%} %>
					</ol>
				</div> 
            </td>
            
            <td>	<%:item.User1 != null ? item.User1.Username : string.Empty%>	</td>
            <td>	<%:item.CreationDate.ToString()%>	</td>
            <td>	<%:item.GroupsLine%>	</td>
            <td>
                <%
				if (item.IsApproved)
				{%>
						<%:Html.ActionLink(Localization.getMessage("Deactivate"), "Deactivate",
													  new {id = item.Id})%> |
					<%
				}
				else
				{%>
						<%:Html.ActionLink(Localization.getMessage("Activate"), "Activate", new {id = item.Id})%> |
					<%
				}%>
					<%:Html.ActionLink(Localization.getMessage("Edit"), "Edit", new {id = item.Id})%> |
					<%:Html.ActionLink(Localization.getMessage("Details"), "Details", new {id = item.Id})%> |
					<%:Html.ActionLink(Localization.getMessage("AddToRole"), "AddToRole", new {id = item.Id})%> |
					<%:Html.ActionLink(Localization.getMessage("AddToGroup"), "AddToGroup", new {id = item.Id})%> |
					<%:Ajax.ActionLink(Localization.getMessage("Delete"), "Delete", new {id = item.Id},
												  new AjaxOptions
													  {
														  Confirm =
															  "Are you sure you want to delete \"" + item.Username + "\"?",
														  HttpMethod = "Delete",
														  OnSuccess = "removeRow"
													  })%>
            </td>
        </tr>
    
    <%}%>

    </tbody>

    </table>

    </div>

    <p>
        <%:Html.ActionLink(Localization.getMessage("CreateNewUser"), "Create")%> | 
        <%:Html.ActionLink(Localization.getMessage("CreateNewUsers"), "CreateMultiple")%>
    </p>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">

    <script type="text/javascript" language="javascript">
        function removeRow(data) {
            window.location = window.location;
        }
    </script>


    <style type="text/css">
	#catalog { width: 150px;}
	#cart ol { margin: 0; padding: 1em 0 1em 3em; }
	#magleft { float:left; width: 150px; margin-right: 2em;}
	</style>

    <style type="text/css">
        .example1 {
            border-width: 2px;
        	border-style: solid;
            border-color: gray; 
            border-radius: 7px;
            text-align:center;
        }
        
        .example1:hover 
        {
            cursor:pointer;
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


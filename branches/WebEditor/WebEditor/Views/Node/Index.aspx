<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<WebEditor.Models.Course>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="/Scripts/jquery/jquery.cookie.js" type="text/javascript"></script>
    <script src="/Scripts/jquery/jquery.hotkeys.js" type="text/javascript"></script>
    <script src="/Scripts/jquery/jquery.jstree.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(function () {
            $("#treeView").jstree({
                "plugins": ["themes", "json_data", "ui", "crrm", "dnd", "types", "hotkeys", "contextmenu"],
                "json_data": {
                    "data": [
                        {"data" : "Root", "attr" : {"rel" : "root", "id": "node_0"}, "state" : "closed"} 
                    ],
		            "ajax": {
                        "type": "post",
		                "url": "<%: Url.Action("List", "Node") %>",
		                "data": function (n) {
		                    return {
		                        "id": n.attr ? n.attr("id").replace("node_", "") : 1
		                    };
		                }
		            }
		        },
		        "types": {
		            "max_depth": -2,
		            "max_children": -2,
		            "valid_children": ["root"],
                    "types": {
		                "default": {
                            "valid_children": "none",
		                    "icon": {
                                "image": "/Content/Tree/file.png"
                            }
		                },
                        "folder": {
		                    "valid_children": ["default", "folder"],
		                    "icon": {
		                        "image": "/Content/Tree/folder.png"
		                    }
		                },
		                "root": {
		                    "valid_children": ["default", "folder"],
		                    "icon": {
		                        "image": "/Content/Tree/root.png"
		                    },
		                    "start_drag": false,
		                    "move_node": false,
		                    "delete_node": false,
		                    "remove": false
		                }
		            }
		        },
                "core" : { 
				    "initially_open" : [ "node_0" ] 
			    },
                "contextmenu" : {
                    "show_at_node" : false,
                    "items" : function(node) {
                        return {
                            "create": {
                                "separator_before": false,
                                "separator_after": true,
                                "label": "Create Node",
                                "action": function (obj) { this.create(obj); },
                                "_disabled": node.attr("rel") == "default"
                            },
                            "create_folder": {
                                "separator_before": false,
                                "separator_after": true,
                                "label": "Create Folder",
                                "action": function (obj) { this.create(obj, "last", {"attr": {"rel": "folder"}}); },
                                "_disabled": node.attr("rel") == "default"
                            },
                            "rename": {
                                "separator_before": false,
                                "separator_after": false,
                                "label": "Rename",
                                "action": function (obj) { this.rename(obj); }
                            },
                            "remove": {
                                "separator_before": false,
                                "icon": false,
                                "separator_after": false,
                                "label": "Delete",
                                "action": function (obj) { if (this.is_selected(obj)) { this.remove(); } else { this.remove(obj); } }
                            },
                            "ccp": {
                                "separator_before": true,
                                "icon": false,
                                "separator_after": false,
                                "label": "Edit",
                                "action": false,
                                "submenu": {
                                    "cut": {
                                        "separator_before": false,
                                        "separator_after": false,
                                        "label": "Cut",
                                        "action": function (obj) { this.cut(obj); }
                                    },
                                    "copy": {
                                        "separator_before": false,
                                        "icon": false,
                                        "separator_after": false,
                                        "label": "Copy",
                                        "action": function (obj) { this.copy(obj); }
                                    },
                                    "paste": {
                                        "separator_before": false,
                                        "icon": false,
                                        "separator_after": false,
                                        "label": "Paste",
                                        "action": function (obj) { this.paste(obj); },
                                        "_disabled" : !(this.data.crrm.ct_nodes || this.data.crrm.cp_nodes) || node.attr("rel") == "default"
                                    }
                                }
                            }
                        }
                    }
                }

		    })
            .bind("create.jstree", function (e, data) {
                $.post("<%: Url.Action("Create", "Node") %>", {
				    "parentId": data.rslt.parent.attr("id").replace("node_", ""),
				    "position": data.rslt.position,
				    "data": data.rslt.name,
				    "type": data.rslt.obj.attr("rel")
				},
                function (r) {
				    if (r.status) {
				        $(data.rslt.obj).attr("id", "node_" + r.id);
				    }
				    else {
				        $.jstree.rollback(data.rlbk);
				    }
				});
		    })
            .bind("remove.jstree", function (e, data) {
		        var ids = [];
                
                data.rslt.obj.each(function () {
                    ids.push($(this).attr('id').replace('node_', ''));
                });
                
                $.ajax({
                    type: 'post',
		            url: "<%: Url.Action("Remove", "Node") %>",
                    traditional: true,
		            data: {
		                "ids": ids
		            },
		            success: function (r) {
		                if (!r.status)
                        {
		                    data.inst.refresh();
		                }
		            }
		        });
		    })
            .bind("rename.jstree", function (e, data) {
				$.ajax({
                    type: 'post',
		            url: "<%: Url.Action("Rename", "Node") %>",
					data: {
						"id": data.rslt.obj.attr("id").replace("node_", ""),
						"data": data.rslt.new_name
					},
					success: function (r) {
						if (!r.status) {
							$.jstree.rollback(data.rlbk);
						}
					}
				});
			})
            .bind("move_node.jstree", function (e, data) {
				data.rslt.o.each(function (i) {
					$.ajax({
						async: false,
						type: 'post',
						url: "<%: Url.Action("Move", "Node") %>",
						data: {
							"id": $(this).attr("id").replace("node_", ""),
							"parentId": data.rslt.np.attr("id").replace("node_", ""),
							"position": data.rslt.cp + i,
							"data": data.rslt.name,
							"copy": data.rslt.cy ? true : false
						},
						success: function (r) {
							if (!r.status) {
								$.jstree.rollback(data.rlbk);
							}
							else {
								$(data.rslt.oc).attr("id", "node_" + r.id);

								if (data.rslt.cy && $(data.rslt.oc).children("UL").length) {
									data.inst.refresh(data.inst._get_parent(data.rslt.oc));
								}
							}
						}
					});
				});
			});
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="treeView"></div>
</asp:Content>


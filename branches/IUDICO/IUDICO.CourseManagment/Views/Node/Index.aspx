<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Empty.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Common.Models.Course>" %>

<asp:Content ID="TitleContent1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="HeadContent2" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="<%= Html.ResolveUrl("/Content/ui-lightness/jquery-ui-1.8.5.custom.css") %>" rel="stylesheet" type="text/css" />

    <script src="<%= Html.ResolveUrl("~/Scripts/jquery/jquery.layout.js") %>" type="text/javascript"></script>
    <script src="<%= Html.ResolveUrl("~/Scripts/ckeditor/ckeditor.js") %>" type="text/javascript"></script>
    <script src="<%= Html.ResolveUrl("~/Scripts/ckeditor/adapters/jquery.js") %>" type="text/javascript"></script>
    <script src="<%= Html.ResolveUrl("~/Scripts/jquery/jquery.cookie.js") %>" type="text/javascript"></script>
    <script src="<%= Html.ResolveUrl("~/Scripts/jquery/jquery.hotkeys.js") %>" type="text/javascript"></script>
    <script src="<%= Html.ResolveUrl("~/Scripts/jquery/jquery.jstree.js") %>" type="text/javascript"></script>

    <script type="text/javascript">
        var $editor;

        $(function () {
            $('body').layout({ applyDefaultStyles: true });

            $editor = $('#editor');
            $editor.ckeditor().hide();
            $editor.parent('form').bind('save', function (e) {
                //e.preventDefault();

                id = $.data($editor, 'node-id');

                $ckEditor = $editor.ckeditorGet();

                $ckEditor.updateElement();
                data = $ckEditor.getData();

                $.post("<%: Url.Action("Edit", "Node") %>", { id: id, data: data });
            }).hide();

            $.jstree._themes = pluginPath + "/Content/Tree/themes/";
        });
    </script>

    

    <script type="text/javascript">
        $(function () {
            $("#treeView").jstree({
                "plugins": ["themes", "json_data", "ui", "crrm", "cookies", "dnd", "types", "hotkeys", "contextmenu"],
                "json_data": {
                    "data": [
                        {"data" : "Root", "attr" : {"rel" : "root", "id": "node_0"}, "state" : "closed"} 
                    ],
		            "ajax": {
                        "type": "post",
		                "url": "<%: Url.Action("List", "Node") %>",
		                "data": function (n) {
		                    var id = n.attr("id").replace("node_", "");
                            
                            return {
		                        "id": (id > 0 ? id : null)
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
                                "image": pluginPath + "/Content/Tree/file.png"
                            }
		                },
                        "folder": {
		                    "valid_children": ["default", "folder"],
		                    "icon": {
		                        "image": pluginPath + "/Content/Tree/folder.png"
		                    }
		                },
		                "root": {
		                    "valid_children": ["default", "folder"],
		                    "icon": {
		                        "image": pluginPath + "/Content/Tree/root.png"
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
                                "separator_after": false,
                                "label": "Create Node",
                                "action": function (obj) { this.create(obj); },
                                "_disabled": node.attr("rel") == "default"
                            },
                            "create_folder": {
                                "separator_before": false,
                                "separator_after": false,
                                "label": "Create Folder",
                                "action": function (obj) { this.create(obj, "last", {"attr": {"rel": "folder"}}); },
                                "_disabled": node.attr("rel") == "default"
                            },
                            "edit": {
                                "separator_before": true,
                                "separator_after": false,
                                "label": "Edit",
                                "action": function (obj) {
                                    this.get_container().triggerHandler("edit_node.jstree", { "obj": obj });
                                },
                                "_disabled": node.attr("rel") != "default"
                            },
                            "preview": {
                                "separator_before": false,
                                "separator_after": false,
                                "label": "Preview",
                                "action": function (obj) {
                                    this.get_container().triggerHandler("preview_node.jstree", { "obj": obj });
                                },
                                "_disabled": node.attr("rel") != "default"
                            },
                            "rename": {
                                "separator_before": true,
                                "separator_after": false,
                                "label": "Rename",
                                "action": function (obj) { this.rename(obj); }
                            },
                            "delete": {
                                "separator_before": false,
                                "separator_after": false,
                                "label": "Delete",
                                "action": function (obj) { if (this.is_selected(obj)) { this.remove(); } else { this.remove(obj); } }
                            },
                            "cut": {
                                "separator_before": true,
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
                            },
                            "pattern": {
                                "separator_before": true,
                                "icon": false,
                                "separator_after": false,
                                "label": "Apply Pattern",
                                "submenu": {
                                		"default" : {
			                                "label" : "Pattern 1",
			                                "action" : function (obj) { 
                                                this.get_container().triggerHandler("pattern.jstree", { "obj": obj, "pattern": 1 });
                                            }
		                                },
		                                "another" : {
			                                "label" : "Pattern 2",
			                                "action" : function (obj) { 
                                                this.get_container().triggerHandler("pattern.jstree", { "obj": obj, "pattern": 2 });
                                            }
		                                },
		                                "yetanother" : {
			                                "label" : "Pattern 3",
			                                "action" : function (obj) {
                                                this.get_container().triggerHandler("pattern.jstree", { "obj": obj, "pattern": 3 });
                                            }
		                                }
                                }
                            }
                        }
                    }
                }
		    })
            .bind("create.jstree", function (e, data) {
                var id = data.rslt.parent.attr("id").replace("node_", "");
                var type = data.rslt.obj.attr("rel");

                $.post("<%: Url.Action("Create", "Node") %>", {
				    "name": data.rslt.name,
                    "parentId": id > 0 ? id : null,
				    "position": data.rslt.position,
				    "isFolder": (type != "default")
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
                var ids = data.rslt.obj.map(function() {
                    return $(this).attr('id').replace('node_', '');
                });

                var answer = confirm("Are you sure you want to delete " + ids.length + " selected nodes?");

                if (answer == false) {
                    return false;
                }
                
                $.ajax({
                    type: 'post',
		            url: "<%: Url.Action("Delete", "Node") %>",
                    /*traditional: true,*/
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
						"name": data.rslt.new_name
					},
					success: function (r) {
						if (!r.status) {
							$.jstree.rollback(data.rlbk);
						}
					}
				});
			})
            .bind("move_node.jstree", function (e, data) {
				//"name": data.rslt.name,
                var parentId = data.rslt.np.attr("id").replace("node_", "");

                data.rslt.o.each(function (i) {
                    $.ajax({
						async: false,
						type: 'post',
						url: "<%: Url.Action("Move", "Node") %>",
						data: {
							"id": $(this).attr("id").replace("node_", ""),
							"parentId": (parentId > 0 ? parentId : null),
							"position": data.rslt.cp + i,
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
			})
            .bind("preview_node.jstree", function(e, data) {
                $.ajax({
                    type: 'post',
		            url: "<%: Url.Action("Data", "Node") %>",
					data: {
						"id": data.obj.attr("id").replace("node_", ""),
					},
					success: function (r) {
					    $('.ui-layout-center').html(r.data);
					}
				});
            })
            .bind("edit_node.jstree", function (e, data) {
                $('#node_' + $.data($editor, 'node-id')).children('a').removeClass('jstree-selected');
                data.obj.children('a').addClass('jstree-selected');

                $.data($editor, 'node-id', data.obj.attr("id").replace("node_", ""));

                $.ajax({
                    type: 'post',
		            url: "<%: Url.Action("Data", "Node") %>",
					data: {
						"id": $.data($editor, 'node-id'),
					},
					success: function (r) {
                        $editor.val(r.data);
                        $editor.parent('form').show();
					}
				});
            })
            .bind("pattern.jstree", function(e, data) {
                $.ajax({
                    type: 'post',
		            url: "<%: Url.Action("ApplyPattern", "Node") %>",
					data: {
						"id": data.obj.attr("id").replace("node_", ""),
                        "pattern": data.pattern
					},
					success: function (r) {
                        alert(r.status);
                    }
				});
            });
        });
    </script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="ui-layout-center">
        <form action="" method="post">
            <textarea name="editor" id="editor" rows="0" cols="0"></textarea>
        </form>
    </div>
    <div class="ui-layout-north"></div>
    <div class="ui-layout-south ui-widget-header ui-corner-all"></div>
    <div class="ui-layout-east"></div>
    <div class="ui-layout-west"><div id="treeView"></div></div>
</asp:Content>

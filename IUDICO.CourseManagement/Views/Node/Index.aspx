<%@ Assembly Name="IUDICO.CourseManagement" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Empty.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Common.Models.Course>" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="IUDICO.CourseManagement.Models.ManifestModels" %>

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
    <script src="<%= Html.ResolveUrl("/Scripts/jquery/jquery-ui-1.8.5.js") %>" type="text/javascript"></script>

    <script type="text/javascript">
        var $editor;

        function removeEditor() {
            if ($('#editor').length != 0) {
                $editor.ckeditorGet().destroy();
                $('.ui-layout-center').empty();

                $editor = null;
            }
            if($("#accordion")[0].style.display != 'none')
            {
                $('#accordion').hide( "blind", {}, 1000);
                clearProperties();
            }
            if($("#patterns")[0].style.display != 'none')
            {
                $('#patterns').hide( "drop", {}, 1000);
            }
        }

        function clearProperties() {
            $("#accordion>div").each(function() {
                this.innerHTML = '<img src="' + pluginPath + '/Content/Tree/ajax-loader.gif" />';
            });
        }
        function isEditorInited() {
            return $('#editor').length != 0;
        }

        function getEditor() {
            if ($('#editor').length == 0) {
                $('.ui-layout-center').empty().append(
                    $('<form/>').attr('method', 'post').attr('action', '').append(
                        $('<textarea/>').attr('name', 'editor').attr('id', 'editor').attr('rows', '0').attr('cols', '0')
                    )
                );

                $editor = $('#editor');
                $editor.ckeditor();

                $editor.parent('form').bind('save', function (e) {
                    //e.preventDefault();

                    id = $.data($editor, 'node-id');

                    var $ckEditor = getEditor().ckeditorGet();

                    $ckEditor.updateElement();
                    data = $ckEditor.getData();

                    $.post("<%: Url.Action("Edit", "Node") %>", { id: id, data: data });
                });
            }

            return $editor;
        }

        $(function () {
            $('body').layout({ applyDefaultStyles: true });

            //getEditor();

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
                                "label": "<%=IUDICO.CourseManagement.Localization.getMessage("CreateNode") %>",
                                "action": function (obj) { this.create(obj); },
                                "_disabled": node.attr("rel") == "default"
                            },
                            "create_folder": {
                                "separator_before": false,
                                "separator_after": false,
                                "label": "<%=IUDICO.CourseManagement.Localization.getMessage("CreateFolder") %>",
                                "action": function (obj) { this.create(obj, "last", {"attr": {"rel": "folder"}}); },
                                "_disabled": node.attr("rel") == "default"
                            },
                            "edit": {
                                "separator_before": true,
                                "separator_after": false,
                                "label": "<%=IUDICO.CourseManagement.Localization.getMessage("Edit") %>",
                                "action": function (obj) {
                                    this.get_container().triggerHandler("edit_node.jstree", { "obj": obj });
                                },
                                "_disabled": node.attr("rel") == "root"
                            },
                            "preview": {
                                "separator_before": false,
                                "separator_after": false,
                                "label": "<%=IUDICO.CourseManagement.Localization.getMessage("Preview") %>",
                                "action": function (obj) {
                                    this.get_container().triggerHandler("preview_node.jstree", { "obj": obj });
                                },
                                "_disabled": node.attr("rel") != "default"
                            },
                            "rename": {
                                "separator_before": true,
                                "separator_after": false,
                                "label": "<%=IUDICO.CourseManagement.Localization.getMessage("Rename") %>",
                                "action": function (obj) { this.rename(obj); }
                            },
                            "delete": {
                                "separator_before": false,
                                "separator_after": false,
                                "label": "<%=IUDICO.CourseManagement.Localization.getMessage("Delete") %>",
                                "action": function (obj) { if (this.is_selected(obj)) { this.remove(); } else { this.remove(obj); } }
                            },
                            "cut": {
                                "separator_before": true,
                                "separator_after": false,
                                "label": "<%=IUDICO.CourseManagement.Localization.getMessage("Cut") %>",
                                "action": function (obj) { this.cut(obj); }
                            },
                            "copy": {
                                "separator_before": false,
                                "icon": false,
                                "separator_after": false,
                                "label": "<%=IUDICO.CourseManagement.Localization.getMessage("Copy") %>",
                                "action": function (obj) { this.copy(obj); }
                            },
                            "paste": {
                                "separator_before": false,
                                "icon": false,
                                "separator_after": false,
                                "label": "<%=IUDICO.CourseManagement.Localization.getMessage("Paste") %>",
                                "action": function (obj) { this.paste(obj); },
                                "_disabled" : !(this.data.crrm.ct_nodes || this.data.crrm.cp_nodes) || node.attr("rel") == "default"
                            }
                        }
                    }
                }
		    })
            .bind("select_node.jstree", function (e, data) {
                data.inst.get_container().triggerHandler("preview_node.jstree", { "obj": data.rslt.obj });
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
                if ($editor != null)                
                    $('#node_' + $.data($editor, 'node-id')).children('a').removeClass('jstree-selected');

                removeEditor();

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

                $("#accordion").accordion( "option", "active", -1 ).show("blind", {}, 1000);
                $("#patterns").show("drop", {}, 1000);

                $('[id^="node_"]').children('a').removeClass('jstree-clicked jstree-selected');
                data.obj.children('a').addClass('jstree-selected');

                if($(data.obj[0]).attr("rel") == "folder") {
                    return;
                }

                var editor = getEditor();

                $.data(editor, 'node-id', data.obj.attr("id").replace("node_", ""));

                $.ajax({
                    type: 'post',
		            url: "<%: Url.Action("Data", "Node") %>",
					data: {
						"id": $.data(editor, 'node-id')
					},
					success: function (r) {
                        var editor = getEditor();
                        editor.val(r.data);
                        editor.parent('form').show();
					}
				});
            });
        });
    </script>

    <script type="text/javascript">
        function onSavePropertiesSuccess() {
            alert("Properties saved successfully");
        }
        function onSavePropertiesFailure() {
            alert("Properties saved successfully");
        }
    </script>
    
    <script type="text/javascript">
        $(function () {

            clearProperties();

            $("#accordion").accordion({
                collapsible: true,
                autoHeight: false,
                active: -1
            });
            $("#accordion a").click(function (e) {
                //e.preventDefault();

                if (!isEditorInited() || !$.data($editor, 'node-id')) {
                    return;
                }

                var currentNodeId = $.data($editor, 'node-id');


                if($("#" + this.id + "Properties")[0].style.display != 'none') {
                    clearProperties();
                    return;
                }

                clearProperties();

                $.ajax({
                    type: 'post',
                    url: "<%: Url.Action("Properties", "Node") %>",
                    data: {
                        "id":  $.data($editor, 'node-id'),
                        "type": this.id
                    },
                    success: function(r) {
                        if(r.status) {
                            $("#" + r.type + "Properties")[0].innerHTML = r.data;
                        }
                    }
                })
            });
            $("#ApplyPattern").click(function () {
                $("#accordion").accordion( "option", "active", -1 );
                $.ajax({
                    type: 'post',
	                url: "<%: Url.Action("ApplyPattern", "Node") %>",
	                data: {
		                "id":  $.data($editor, 'node-id'),
                        "pattern": $("#SequencingPatterns")[0].value,
                        "data": $("#sequencingPatternData")[0].value
	                },
	                success: function (r) {
                        if(r.status) {
                            alert("Pattern successfully  applied");
                        }
                        else {
                            alert("Error! Please try again later");
                        }
                    }
                });
            });
            $("#SequencingPatterns").change(function() {
                if(this.value == "<%= SequencingPattern.RandomSetSequencingPattern %>") {
                    $("#sequencingPatternDataHolder").show("drop", {}, 1000);
                }
                else {
                    if($("#sequencingPatternDataHolder")[0].style.display != "none") {
                        $("#sequencingPatternDataHolder").hide("drop", {}, 1000);
                    }
                }
            });
        });
    </script>

</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="ui-layout-center">
        <!--<form action="" method="post">
            <textarea name="editor" id="editor" rows="0" cols="0"></textarea>
        </form>-->
    </div>
    <div class="ui-layout-north">
        <h1>Editing "<%= Model.Name %>"</h1>
        <%= Html.ActionLink("Go back", "Index", "Course") %>
    </div>
    <div class="ui-layout-south ui-widget-header ui-corner-all"></div>
    <div class="ui-layout-east">

        <div id="patterns" style="display:none;">
            <div>Select Pattern:</div>
            <div>
                <%=  Html.DropDownList("SequencingPatterns", ViewData["SequencingPatternsList"] as List<SelectListItem>)%>
            </div>
            <div id="sequencingPatternDataHolder">
                Count of tests:
                <input type="text" id="sequencingPatternData" value="" style="display:none; width: 50px;" />
            </div>
            <div><input type="button" id="ApplyPattern" value="Apply" /></div>
        </div>

        <div id="accordion" style="display:none;">
            <h3><a href="#" id="ControlMode"><%=IUDICO.CourseManagement.Localization.getMessage("ControlMode") %></a></h3>
            <div id="ControlModeProperties"></div>
            <h3><a href="#" id="LimitConditions"><%=IUDICO.CourseManagement.Localization.getMessage("LimitConditions") %></a></h3>
            <div id="LimitConditionsProperties"></div>
            <h3><a href="#" id="RandomizationControls"><%=IUDICO.CourseManagement.Localization.getMessage("RandomizationControls")%></a></h3>
            <div id="RandomizationControlsProperties"></div>
            <h3><a href="#" id="ConstrainedChoiceConsiderations"><%=IUDICO.CourseManagement.Localization.getMessage("ConstrainedChoiceConsiderations")%></a></h3>
            <div id="ConstrainedChoiceConsiderationsProperties"></div>
            <h3><a href="#" id="DeliveryControls"><%=IUDICO.CourseManagement.Localization.getMessage("DeliveryControls")%></a></h3>
            <div id="DeliveryControlsProperties"></div>
            <h3><a href="#" id="RollupRules"><%=IUDICO.CourseManagement.Localization.getMessage("RollupRules")%></a></h3>
            <div id="RollupRulesProperties"></div>
            <h3><a href="#" id="RollupConsiderations"><%=IUDICO.CourseManagement.Localization.getMessage("RollupConsiderations")%></a></h3>
            <div id="RollupConsiderationsProperties"></div>
        </div>
        <div id="properties"></div>
    </div>
    <div class="ui-layout-west"><div id="treeView"></div></div>
</asp:Content>

<%@ Assembly Name="IUDICO.CourseManagement" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Empty.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Common.Models.Shared.Course>" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="System.Web.Mvc" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="IUDICO.Common.Models" %>
<%@ Import Namespace="IUDICO.CourseManagement.Models.ManifestModels" %>
<%@ Import Namespace="System.Security.Policy" %>
<%@ Import Namespace="IUDICO.Common" %>
<asp:Content ID="TitleContent1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=Localization.GetMessage("EditingCourse")%>
</asp:Content>
<asp:Content ID="HeadContent2" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="<%= Html.ResolveUrl("/Content/ui-lightness/jquery-ui-1.8.20.custom.css") %>" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="<%= Html.ResolveUrl("~/Content/jquery-ui.css") %>" id="theme" />
    <link rel="stylesheet" href="<%= Html.ResolveUrl("~/Content/jquery.fileupload-ui.css") %>" />

    <script src="<%= Html.ResolveUrl("~/Scripts/lms.js") %>" type="text/javascript"></script>
    <script src="<%= Html.ResolveUrl("~/Scripts/jquery/jquery.layout.js") %>" type="text/javascript"></script>
    <script src="<%= Html.ResolveUrl("~/Scripts/ckeditor/ckeditor.js") %>" type="text/javascript"></script>
    <script src="<%= Html.ResolveUrl("~/Scripts/ckeditor/adapters/jquery.js") %>" type="text/javascript"></script>
    <script src="<%= Html.ResolveUrl("~/Scripts/jquery/jquery.cookie.js") %>" type="text/javascript"></script>
    <script src="<%= Html.ResolveUrl("~/Scripts/jquery/jquery.hotkeys.js") %>" type="text/javascript"></script>
    <script src="<%= Html.ResolveUrl("~/Scripts/jquery/jquery.jstree.js") %>" type="text/javascript"></script>
    <script src="<%= Html.ResolveUrl("/Scripts/jquery/jquery-ui-1.8.5.js") %>"></script>

    <script type="text/javascript">

        var $editor;
        var currentNodeId;
        var fillResources;
        var currentCourseId = <%= Model.Id %> ;
        
        function removeEditor() {
            
            currentNodeId = null;

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

        function getEditor(nodeId) {
            if ($('#editor').length == 0) {
                $('.ui-layout-center').empty().append(
                    $('<form/>').attr('method', 'post').attr('action', '').append(
                        $('<textarea/>').attr('name', 'editor').attr('id', 'editor').attr('rows', '1').attr('cols', '1')
                    )
                );

                $editor = $('#editor');
                $editor.ckeditor({
                    language: language,
                        
                    filebrowserBrowseUrl : '<%= Html.ResolveUrl("~/Scripts/ckfinder/ckfinder.html") %>'+ '?courseId=' + currentCourseId + '&nodeId=' + nodeId,
                    filebrowserImageBrowseUrl : '<%= Html.ResolveUrl("~/Scripts/ckfinder/ckfinder.html?Type=Images") %>'+ '&courseId=' + currentCourseId + '&nodeId=' + nodeId,
                    filebrowserFlashBrowseUrl : '<%= Html.ResolveUrl("~/Scripts/ckfinder/ckfinder.html?Type=Flash") %>'+ '&courseId=' + currentCourseId + '&nodeId=' + nodeId,
                    filebrowserUploadUrl : '<%= Html.ResolveUrl("~/Scripts/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files") %>'+ '&courseId=' + currentCourseId + '&nodeId=' + nodeId,
                    filebrowserImageUploadUrl : '<%= Html.ResolveUrl("~/Scripts/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images") %>'+ '&courseId=' + currentCourseId + '&nodeId=' + nodeId,
                    filebrowserFlashUploadUrl : '<%= Html.ResolveUrl("~/Scripts/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash") %>' + '&courseId=' + currentCourseId + '&nodeId=' + nodeId
                    
                });
                $.data($editor, 'node-id', nodeId);
                
                $editor.parent('form').bind('save', function (e) {
                    //e.preventDefault();

                    id = $.data($editor, 'node-id');

                    var $ckEditor = getEditor(id).ckeditorGet();

                    $ckEditor.updateElement();
                    data = $ckEditor.getData();
                    $ckEditor.resetDirty();

                    $.post("<%: Url.Action("Edit", "Node") %>", { id: id, data: data });
                });
            }

            return $editor;
        }

        $(function () {
            $('body').layout({ applyDefaultStyles: true });

            //getEditor();

            $.jstree._themes = pluginPath + "/Content/Tree/themes/";
            
            $(window).bind('beforeunload', function(){
                if ($editor != null && $editor.ckeditorGet().checkDirty()) {
                    return "<%=Localization.GetMessage("ChangesAreNotSaved")%>";
                }
            });
        });

        $(function () {
            var historyObject;

            var changeState = function(state) {
                    $("#treeView").jstree("deselect_all");
                    if (state.type != "edit") {
                        $("#treeView").jstree("select_node", "#node_" + historyObject.nodeId);
                    } else {
                        $.jstree._reference("#treeView").get_container().triggerHandler(historyObject.type + "_node.jstree", { "obj": $("#node_" + historyObject.nodeId) });
                    }
            }

            $(window).bind("popstate", function(e) {
                historyObject = e.originalEvent.state;

                if ($("#treeView").hasClass("jstree")) {
                    changeState(historyObject);
                }
            });

            $("#treeView").bind("loaded.jstree", function(){
                if (historyObject !== undefined) {
                    changeState(historyObject);
                }
            });


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
                                "label": "<%=Localization.GetMessage("CreateNode") %>",
                                "action": function (obj) { this.create(obj); },
                                "_disabled": node.attr("rel") == "default"
                            },
                            "create_folder": {
                                "separator_before": false,
                                "separator_after": false,
                                "label": "<%=Localization.GetMessage("CreateFolder") %>",
                                "action": function (obj) { this.create(obj, "last", {"attr": {"rel": "folder"}}); },
                                "_disabled": node.attr("rel") == "default"
                            },
                            "edit": {
                                "separator_before": true,
                                "separator_after": false,
                                "label": "<%=Localization.GetMessage("Edit") %>",
                                "action": function (obj) {
                                    this.get_container().triggerHandler("edit_node.jstree", { "obj": obj });
                                }                                
                            },
                            "preview": {
                                "separator_before": false,
                                "separator_after": false,
                                "label": "<%=Localization.GetMessage("Preview") %>",
                                "action": function (obj) {
                                    this.get_container().triggerHandler("preview_node.jstree", { "obj": obj });
                                },
                                "_disabled": node.attr("rel") != "default"
                            },
                            "rename": {
                                "separator_before": true,
                                "separator_after": false,
                                "label": "<%=Localization.GetMessage("Rename") %>",
                                "action": function (obj) { this.rename(obj); }
                            },
                            "delete": {
                                "separator_before": false,
                                "separator_after": false,
                                "label": "<%=Localization.GetMessage("Delete") %>",
                                "action": function (obj) { if (this.is_selected(obj)) { this.remove(); } else { this.remove(obj); } }
                            },
                            "cut": {
                                "separator_before": true,
                                "separator_after": false,
                                "label": "<%=Localization.GetMessage("Cut") %>",
                                "action": function (obj) { this.cut(obj); }
                            },
                            "copy": {
                                "separator_before": false,
                                "icon": false,
                                "separator_after": false,
                                "label": "<%=Localization.GetMessage("Copy") %>",
                                "action": function (obj) { this.copy(obj); }
                            },
                            "paste": {
                                "separator_before": false,
                                "icon": false,
                                "separator_after": false,
                                "label": "<%=Localization.GetMessage("Paste") %>",
                                "action": function (obj) { this.paste(obj); },
                                "_disabled" : !(this.data.crrm.ct_nodes || this.data.crrm.cp_nodes) || node.attr("rel") == "default"
                            }
                        };
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
                var answer = confirm("<%=String.Concat(Localization.GetMessage("AreYouSureYouWantToDelete")," ",Localization.GetMessage("SelectedNodes")) %>");
                if (answer == false) {
                    return false;
                }
                
                data.rslt.obj.each(function () {
	                $.ajax({
	                    async : true,
	                    type: 'post',
	                    url: "<%: Url.Action("Delete", "Node") %>",
	                    data : { 
	                        "id" : this.id.replace("node_","")
	                    }, 
	                    success : function (r) {
	                        if(!r.status) {
	                            data.inst.refresh();
	                        }
	                    }
	                });
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
                if ($editor != null && $editor.ckeditorGet().checkDirty()) {
                    if (window.confirm("<%=Localization.GetMessage("SaveChanges")%>?")) {
                        $editor.parent("form").trigger('save');   
                    }
                }

                currentNodeId = data.obj.attr("id").replace("node_", "");
                if (history.state == null || currentNodeId != history.state.nodeId || history.state.type == "edit") {
                    if (history.pushState) {
                        history.pushState({ nodeId: currentNodeId, type: "preview" }, "preview node_" + currentNodeId);
                    } 
                }

                var parentsObjectList = $(data.obj).parents('li');
                var parents = currentNodeId;
                for (var i = 0; i < parentsObjectList.length; ++i)
                    parents = parents + "_" + $(parentsObjectList[i]).attr("id").replace("node_", "");

                if (data.obj.attr("id") == "node_0") {
                    return;
                }
                
                if ($editor != null)
                {
                    $('#node_' + $.data($editor, 'node-id')).children('a').removeClass('jstree-selected');
                }

                removeEditor();

                $.ajax({
                        type: 'post',
                        url: "<%: Url.Action("Preview", "Node") %>?time=" + new Date().getTime(),
                        data: {
                            "id": data.obj.attr("id").replace("node_", "")
                        },
                        success: function(r) {
                            $iframe = $('<iframe width="100%" height="100%"></iframe>');
                            $iframe.attr('src', r.path + "?time=" + new Date().getTime());

                            $('.ui-layout-center').empty();
                            $('.ui-layout-center').append($iframe);

                            //$('.ui-layout-center iframe')[0].contentWindow.API_1484_11 = LMSDebugger;
                            window.API_1484_11 = new LMSDebugger();
                        }
                    });
            })
            .bind("edit_node.jstree", function (e, data) {
                if ($editor != null && $editor.ckeditorGet().checkDirty()) {
                    if (window.confirm("<%=Localization.GetMessage("SaveChanges")%>?")) {
                        $editor.parent("form").trigger('save');   
                    }
                }
                
                currentNodeId = data.obj.attr("id").replace("node_", "");
                if (history.state == null || currentNodeId != history.state.nodeId || history.state.type != "edit") {
                    if (history.pushState) {
                        history.pushState({ nodeId: currentNodeId, type: "edit" }, "preview node_" + currentNodeId);
                    }
                }

                var parentsObjectList = $(data.obj).parents('li');
                var parents = currentNodeId;
                for (var i = 0; i < parentsObjectList.length; ++i)
                    parents = parents + "_" + $(parentsObjectList[i]).attr("id").replace("node_", "");
                
                $("#accordion").accordion( "option", "active", -1 ).show("blind", {}, 1000);
                $("#patterns").show("drop", {}, 1000);

                $('[id^="node_"]').children('a').removeClass('jstree-clicked jstree-selected');
                data.obj.children('a').addClass('jstree-selected');


                if($(data.obj[0]).attr("rel") != "default") {
                    return;
                }

                
                
                $.ajax({
                    type: 'post',
		            url: "<%: Url.Action("Data", "Node") %>",
					data: {
						"id": currentNodeId
					},
					success: function (r) {
                        var editor = getEditor(data.obj.attr("id").replace("node_", ""));
                        editor.ckeditorGet().setData(r.data, function () {
                            setTimeout(function() { $editor.ckeditorGet().resetDirty(); }, 100);
                            editor.parent('form').show();
                        });
					}
				});
            });
        });
        
    </script>
    <script type="text/javascript">
        function onSavePropertiesSuccess() {
            alert("<%=Localization.GetMessage("PropertiesSavedSuccessfully")%>");
        }
        function onSavePropertiesFailure() {
            alert("<%=Localization.GetMessage("PropertiesSavedSuccessfully")%>");
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

                if($("#" + this.id + "Properties")[0].style.display != 'none') {
                    clearProperties();
                    return;
                }

                clearProperties();

                $.ajax({
                    type: 'post',
                    url: "<%: Url.Action("Properties", "Node") %>",
                    data: {
                        "id":  currentNodeId,
                        "type": this.id
                    },
                    success: function(r) {
                        if(r.status) {
                            $("#" + r.type + "Properties")[0].innerHTML = r.data;
                        }
                    }
                });
            });
            $("#ApplyPattern").click(function () {
                $("#accordion").accordion( "option", "active", -1 );
                $.ajax({
                    type: 'post',
	                url: "<%: Url.Action("ApplyPattern", "Node") %>",
	                data: {
		                "id":  currentNodeId,
                        "pattern": $("#SequencingPatterns")[0].value,
                        "data": $("#sequencingPatternData")[0].value
	                },
	                success: function (r) {
                        if(r.status) {
                            alert("<%=Localization.GetMessage("PatternSuccessfullyApplied") %>");
                        }
                        else {
                            alert("<%=Localization.GetMessage("ErrorTryAgainLater") %>");
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
        <h1>
            <%=Localization.GetMessage("EditingCourse")%>
            "<%= Model.Name %>"</h1>
        <%= Html.ActionLink(Localization.GetMessage("BackToList"), "Index", "Course")%>
    </div>
    <div class="ui-layout-south ui-widget-header ui-corner-all">
    </div>
    <div class="ui-layout-east">
        <div id="patterns" style="display: none;">
            <div>
                <%=Localization.GetMessage("SelectPattern")%>:</div>
            <div>
                <%=  Html.DropDownList("SequencingPatterns", ViewData["SequencingPatternsList"] as List<SelectListItem>)%>
            </div>
            <div id="sequencingPatternDataHolder" style="display: none;">
                <%=Localization.GetMessage("CountOfTests")%>:
                <input type="text" id="sequencingPatternData" value="0" style="width: 50px;" />
            </div>
            <div>
                <input type="button" id="ApplyPattern" value="<%=Localization.GetMessage("Apply")%>" /></div>
        </div>
        <div id="accordion" style="display: none;">
            <h3>
                <a href="#" id="ControlMode">
                    <%=Localization.GetMessage("ControlMode") %></a></h3>
            <div id="ControlModeProperties">
            </div>
            <h3>
                <a href="#" id="LimitConditions">
                    <%=Localization.GetMessage("LimitConditions") %></a></h3>
            <div id="LimitConditionsProperties">
            </div>
            <h3>
                <a href="#" id="RandomizationControls">
                    <%=Localization.GetMessage("RandomizationControls")%></a></h3>
            <div id="RandomizationControlsProperties">
            </div>
            <h3>
                <a href="#" id="ConstrainedChoiceConsiderations">
                    <%=Localization.GetMessage("ConstrainedChoiceConsiderations")%></a></h3>
            <div id="ConstrainedChoiceConsiderationsProperties">
            </div>
            <h3>
                <a href="#" id="DeliveryControls">
                    <%=Localization.GetMessage("DeliveryControls")%></a></h3>
            <div id="DeliveryControlsProperties">
            </div>
            <h3>
                <a href="#" id="RollupRules">
                    <%=Localization.GetMessage("RollupRules")%></a></h3>
            <div id="RollupRulesProperties">
            </div>
            <h3>
                <a href="#" id="RollupConsiderations">
                    <%=Localization.GetMessage("RollupConsiderations")%></a></h3>
            <div id="RollupConsiderationsProperties">
            </div>
        </div>
        <div id="properties">
        </div>
    </div>
    <div class="ui-layout-west">
        <div id="treeView">
        </div>
    </div>
</asp:Content>

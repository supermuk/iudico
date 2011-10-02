<%@ Assembly Name="IUDICO.CourseManagement" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Empty.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Common.Models.Course>" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="System.Web.Mvc" %>
<%@ Import Namespace="IUDICO.CourseManagement.Models.ManifestModels" %>
<%@ Import Namespace="System.Security.Policy" %>

<asp:Content ID="TitleContent1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=IUDICO.CourseManagement.Localization.getMessage("EditingCourse")%>
</asp:Content>

<asp:Content ID="HeadContent2" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="<%= Html.ResolveUrl("/Content/ui-lightness/jquery-ui-1.8.5.custom.css") %>" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.13/themes/base/jquery-ui.css" id="theme" />
    <link rel="stylesheet" href="<%= Html.ResolveUrl("~/Content/jquery.fileupload-ui.css") %>" />

    <script type="text/javascript">
        debugger;
    </script>
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

        function getEditor() {
            if ($('#editor').length == 0) {
                $('.ui-layout-center').empty().append(
                    $('<form/>').attr('method', 'post').attr('action', '').append(
                        $('<textarea/>').attr('name', 'editor').attr('id', 'editor').attr('rows', '1').attr('cols', '1')
                    )
                );

                $editor = $('#editor');
                $editor.ckeditor({language: language});

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
                                }                                
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
						"id": data.obj.attr("id").replace("node_", ""),
					},
					success: function (r) {
                        $iframe = $('<iframe width="100%" height="100%"></iframe>');
                        $iframe.attr('src', r.path + "?time="  + new Date().getTime());

					    $('.ui-layout-center').empty();
                        $('.ui-layout-center').append($iframe);

                        //$('.ui-layout-center iframe')[0].contentWindow.API_1484_11 = LMSDebugger;
                        window.API_1484_11 = new LMSDebugger();
					}
				});
            })
            .bind("edit_node.jstree", function (e, data) {

                $("#accordion").accordion( "option", "active", -1 ).show("blind", {}, 1000);
                $("#patterns").show("drop", {}, 1000);

                $('[id^="node_"]').children('a').removeClass('jstree-clicked jstree-selected');
                data.obj.children('a').addClass('jstree-selected');

                currentNodeId = data.obj.attr("id").replace("node_", "");

                if($(data.obj[0]).attr("rel") != "default") {
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
            alert("<%=IUDICO.CourseManagement.Localization.getMessage("PropertiesSavedSuccessfully")%>");
        }
        function onSavePropertiesFailure() {
            alert("<%=IUDICO.CourseManagement.Localization.getMessage("PropertiesSavedSuccessfully")%>");
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
                })
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

    <%--<script type="text/javascript">

        $(document).ready(function () {
            (function () {
                //TODO: test server response
            })();
            
            $("#fileInput").change(function () {
                fileUpload($("#imageUploadForm")[0],
                           $("#uploadStatus")[0],
                           $("#uploadFiles")[0],
                           '<%: Url.Action("FileUploader", "Node") %>');
                
                //this.value
            });
        });

        function fileUpload(form, uploadStatus, uploadFiles, action_url) 
        {
            // Create the iframe...
            var iframe = document.createElement("iframe");
            iframe.setAttribute("id", "upload_iframe");
            iframe.setAttribute("name", "upload_iframe");
            iframe.setAttribute("width", "0");
            iframe.setAttribute("height", "0");
            iframe.setAttribute("border", "0");
            iframe.setAttribute("style", "display: none;");

            // Add to document...
            form.parentNode.appendChild(iframe);
            window.frames['upload_iframe'].name = "upload_iframe";

            iframeId = document.getElementById("upload_iframe");

            // Add event...
            var eventHandler = function () {

                if (iframeId.detachEvent)
                    iframeId.detachEvent("onload", eventHandler);
                else
                    iframeId.removeEventListener("load", eventHandler, false);

                // Message from server...
                if (iframeId.contentDocument) {
                    content = iframeId.contentDocument.body.innerHTML;
                } else if (iframeId.contentWindow) {
                    content = iframeId.contentWindow.document.body.innerHTML;
                } else if (iframeId.document) {
                    content = iframeId.document.body.innerHTML;
                }

                uploadStatus.innerHTML = "Uploaded successfully";
                setTimeout('uploadStatus.innerHTML = ""', 2000);
                uploadFiles.innerHTML += content + "</ br>";

                // Del the iframe...
                setTimeout('iframeId.parentNode.removeChild(iframeId)', 250);
            };

            if (iframeId.addEventListener)
                iframeId.addEventListener("load", eventHandler, true);
            if (iframeId.attachEvent)
                iframeId.attachEvent("onload", eventHandler);

            // Set properties of form...
            form.setAttribute("target", "upload_iframe");
            form.setAttribute("action", action_url);
            form.setAttribute("method", "post");
            form.setAttribute("enctype", "multipart/form-data");
            form.setAttribute("encoding", "multipart/form-data");

            // Submit the form...
            form.submit();

            uploadStatus.innerHTML = "Uploading ...";
        }

    </script>--%>
    <script id="template-upload" type="text/x-jquery-tmpl">
        <tr class="template-upload{{if error}} ui-state-error{{/if}}">
            <td class="preview"></td>
            <td class="name">${name}</td>
            <td class="size">${sizef}</td>
            {{if error}}
                <td class="error" colspan="2">Error:
                    {{if error === 'maxFileSize'}}File is too big
                    {{else error === 'minFileSize'}}File is too small
                    {{else error === 'acceptFileTypes'}}Filetype not allowed
                    {{else error === 'maxNumberOfFiles'}}Max number of files exceeded
                    {{else}}${error}
                    {{/if}}
                </td>
            {{else}}
                <td class="progress"><div></div></td>
                <td class="start"><button>Start</button></td>
            {{/if}}
            <td class="cancel"><button>Cancel</button></td>
        </tr>
    </script>
    <script id="template-download" type="text/x-jquery-tmpl">
        <tr class="template-download{{if error}} ui-state-error{{/if}}">
            {{if error}}
                <td></td>
                <td class="name">${namefdsa}</td>
                <td class="size">${sizef}</td>
                <td class="error" colspan="2">Error:
                    {{if error === 1}}File exceeds upload_max_filesize (php.ini directive)
                    {{else error === 2}}File exceeds MAX_FILE_SIZE (HTML form directive)
                    {{else error === 3}}File was only partially uploaded
                    {{else error === 4}}No File was uploaded
                    {{else error === 5}}Missing a temporary folder
                    {{else error === 6}}Failed to write file to disk
                    {{else error === 7}}File upload stopped by extension
                    {{else error === 'maxFileSize'}}File is too big
                    {{else error === 'minFileSize'}}File is too small
                    {{else error === 'acceptFileTypes'}}Filetype not allowed
                    {{else error === 'maxNumberOfFiles'}}Max number of files exceeded
                    {{else error === 'uploadedBytes'}}Uploaded bytes exceed file size
                    {{else error === 'emptyResult'}}Empty file upload result
                    {{else}}${error}
                    {{/if}}
                </td>
            {{else}}
                <td class="preview">
                    {{if Thumbnail_url}}
                        <a href="${url}" target="_blank"><img src="${Thumbnail_url}"></a>
                    {{/if}}
                </td>
                <td class="name">
                    <a href="${url}"{{if thumbnail_url}} target="_blank"{{/if}}>${Name}</a>
                </td>
                <td class="size">${Length}</td>
                <td colspan="2"></td>
            {{/if}}
            <td class="delete">
                <button data-type="${delete_type}" data-url="${delete_url}">Delete</button>
            </td>
        </tr>
    </script>

</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="ui-layout-center">
        <!--<form action="" method="post">
            <textarea name="editor" id="editor" rows="0" cols="0"></textarea>
        </form>-->
    </div>
    <div class="ui-layout-north">
        <h1><%=IUDICO.CourseManagement.Localization.getMessage("EditingCourse")%> "<%= Model.Name %>"</h1>
        <%= Html.ActionLink(IUDICO.CourseManagement.Localization.getMessage("BackToList"), "Index", "Course")%>
    </div>
    <div class="ui-layout-south ui-widget-header ui-corner-all"></div>
    <div class="ui-layout-east">

        <div id="patterns" style="display:none;">
            <div><%=IUDICO.CourseManagement.Localization.getMessage("SelectPattern")%>:</div>
            <div>
                <%=  Html.DropDownList("SequencingPatterns", ViewData["SequencingPatternsList"] as List<SelectListItem>)%>
            </div>
            <div id="sequencingPatternDataHolder" style="display:none;">
                <%=IUDICO.CourseManagement.Localization.getMessage("CountOfTests")%>:
                <input type="text" id="sequencingPatternData" value="0" style="width: 50px;" />
            </div>
            <div><input type="button" id="ApplyPattern" value="<%=IUDICO.CourseManagement.Localization.getMessage("Apply")%>" /></div>
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

    <div class="ui-layout-west">
        <div id="treeView"></div>
        <%--<form id="imageUploadForm" name="imageUploader">
            <input id="fileInput" type="file" name="datafile" style="opacity: 0.0; position: absolute; width: 60px; z-index: 1; cursor: pointer; height: 25px;"/><br />
            <div style="position: absolute; width: 60px; cursor: pointer; z-index: -1; height: 25px;" ><b>Upload</b></div>
            <div id="uploadStatus" style="margin-top: 20px;"></div>
            <div id="uploadFiles" style="margin-top: 10px;"></div>
        </form>--%>
        <div id="fileupload" style="width: 185px; margin-top: 20px;">
            <form action="<%: Url.Action("FileUploader", "Node") %>" method="POST" enctype="multipart/form-data">
                <div class="fileupload-buttonbar">
                    <label class="fileinput-button">
                        <span>Add files...</span>
                        <input id="file" type="file" name="files[]" multiple>
                    </label>
                    <button type="submit" class="start" >Start upload</button>
                    <button type="reset" class="cancel" >Cancel upload</button>
                    <button type="button" class="delete">Delete files</button>
                </div>
            </form>
            <div class="fileupload-content">
                <table id="files" class="files"></table>
                <div class="fileupload-progressbar"></div>
            </div>
        </div>

       
        <script src="http://ajax.aspnetcdn.com/ajax/jquery.templates/beta1/jquery.tmpl.min.js"></script>
        <script src="<%= Html.ResolveUrl("~/Scripts/jquery/jquery.iframe-transport.js") %>"></script>
        <script src="<%= Html.ResolveUrl("~/Scripts/jquery/jquery.fileupload.js") %>"></script>
        <script src="<%= Html.ResolveUrl("~/Scripts/jquery/jquery.fileupload-ui.js") %>"></script>
        <%--<script src="<%= Html.ResolveUrl("~/Scripts/fileUpload.js") %>"></script>--%>
        <script>
            /*global $ */
            $(function () {
                $('#fileupload').fileupload({
                    url: '<%: Url.Action("FileUploader", "Node") %>',
                    method: 'POST',
                    uploadTable: $('#files'),
                    downloadTable: $('#files'),
                    buildUploadRow: function (files, index) {
                        return $('<tr><td>' + files[index].name + '<\/td>' +
                            '<td class="file_upload_progress"><div><\/div><\/td>' +
                            '<td class="file_upload_cancel">' +
                            '<button class="ui-state-default ui-corner-all" title="Cancel">' +
                            '<span class="ui-icon ui-icon-cancel">Cancel<\/span>' +
                            '<\/button><\/td><\/tr>');
                    },
                    buildDownloadRow: function (file) {
                        return $('<tr><td>' + file.name + '<\/td><\/tr>');
                    }
                });
            });
        </script> 
    
    </div>

    

</asp:Content>

<%@ Assembly Name="IUDICO.TestingSystem" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Frameset.aspx.cs" Inherits="Microsoft.LearningComponents.Frameset.Frameset_Frameset" %>
<%-- Copyright (c) Microsoft Corporation. All rights reserved. --%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
    <!-- MICROSOFT PROVIDES SAMPLE CODE "AS IS" AND WITH ALL FAULTS, AND WITHOUT ANY WARRANTY WHATSOEVER.  
        MICROSOFT EXPRESSLY DISCLAIMS ALL WARRANTIES WITH RESPECT TO THE SOURCE CODE, INCLUDING BUT NOT 
        LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE.  THERE IS 
        NO WARRANTY OF TITLE OR NONINFRINGEMENT FOR THE SOURCE CODE. -->
    <head runat="server">
        <title><%=this.PageTitleHtml%></title>
        <link rel="stylesheet" type="text/css" href="Theme/Styles.css" />

        <% if (!this.ShowError) // don't write script if there is an error on the page
           {
               // if this package does not require the RTE, then don't write the links?
               %>
    
        <script type="text/javascript" src="./Include/Rte1p2Api.js"> </script>
        <script type="text/javascript" src="./Include/parser1p2.js"> </script>
        <script type="text/javascript" src="./Include/typevalidators1p2.js"> </script>
    
        <script type="text/javascript" src="./Include/Rte2004Api.js" > </script> 
        <script type="text/javascript" src="./Include/parser.js"> </script>
        <script type="text/javascript" src="./Include/typevalidators.js"> </script>
    
        <script type="text/javascript" src="./Include/RteApiSite.js"> </script>
        <script type="text/javascript" src="./Include/FramesetMgr.js"> </script>
    
        <script type="text/javascript">
// debugger;

    function SetContentHeight(height) {
        parent.document.getElementById("player").height = height;
        frames["frameLearnTask"].document.getElementById("framesetParentMain").style.height = (height - 53) + "px";
        document.getElementById("frameLearnTask").height = height - 53;
    }

    function OnIFrameContentLoad() {
        /*var taskHeight = frames["frameLearnTask"].document.body.offsetHeight;
        frames["frameLearnTask"].document.body.offsetHeight;*/
        SetContentHeight(parent.viewPortHeight - parent.viewPortOffsetTop - parent.viewPortOffsetBottom);
    }

    // Constants
    SCORM_2004 = "V1p3";
    SCORM_12 = "V1p2";

    // FrameMgr is called from all frames
    var g_frameMgr = new FramesetManager;

    // TODO (M2): The following code is only required if the package is SCORM
    var g_scormVersion = "<%=this.ScormVersionHtml%>";
// Version of current session

    var API_1484_11 = null;
// Name of RTE object for 2004 -- name is determined by SCORM.
    var API = null;
// Name of RTE object for 1.2 -- name is determined by SCORM

    // Internal RTE object -- it's the same object as the api objects, just easier to reference.
    var g_API = g_frameMgr.GetRteApi(g_scormVersion, <%=this.RteRequired%> );

    if (g_scormVersion == SCORM_2004) {
        API_1484_11 = g_API;
    } else {
        API = g_API;
    }
    	
    </script>
        <% }
           else if (this.Completed)
           { %>
        <script type="text/javascript">
    function OnRedirectLoad() {
        parent.SubmitTraining();
    }
    </script>
        <% } %>
        <style type="text/css">
        	body {
        		margin: 0;
        		padding: 0;
        	}
        </style>

    </head>
    <% if (this.ShowError)
       { %>
    <% if (this.Completed)
           { %>
    <body onload="OnRedirectLoad()">
    </body>
    <% }
           else
           { %>
    <body class="ErrorBody">

        <table border="0" width="100%" id="table1" style="border-collapse: collapse">
            <tr>
                <td width="60">
                    <p align="center">
                    <img border="0" src="./Theme/Error.gif" width="49" height="49"></td>
                <td class="ErrorTitle"><% =this.ErrorTitle%></td>
            </tr>
            <tr>
                <td width="61">&nbsp;</td>
                <td><hr></td>
            </tr>
            <tr>
                <td width="61">&nbsp;</td>
                <td class="ErrorMessage"><% =this.ErrorMsg%></td>
            </tr>
        </table>

    </body>
    <% } %>
    <% }
       else // no error, so show frameset	
       { %>
    <body  onload="OnIFrameContentLoad()">
        <iframe name="frameTitle" id="frameTitle"  class="frameTitle" src="Title.htm" marginwidth="0" marginheight="0" scrolling="no" frameborder="0" height="53" width="100%"></iframe>

        <iframe name="frameLearnTask" id="frameLearnTask" class="ShellFrame" src="<%=this.MainFramesUrl%>" scrolling="no" marginwidth="0" marginheight="0" frameborder="0" height="600px" width="100%"></iframe>

    </body>
    <% } %>
</html>
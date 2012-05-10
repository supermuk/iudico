<%-- Copyright (c) Microsoft Corporation. All rights reserved. --%>
<%@ Assembly Name="IUDICO.TestingSystem" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TOC.aspx.cs" Inherits="Microsoft.LearningComponents.Frameset.Frameset_TOC" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
    <!-- MICROSOFT PROVIDES SAMPLE CODE "AS IS" AND WITH ALL FAULTS, AND WITHOUT ANY WARRANTY WHATSOEVER.  
        MICROSOFT EXPRESSLY DISCLAIMS ALL WARRANTIES WITH RESPECT TO THE SOURCE CODE, INCLUDING BUT NOT 
        LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE.  THERE IS 
        NO WARRANTY OF TITLE OR NONINFRINGEMENT FOR THE SOURCE CODE. -->
    <head>
        <link rel="stylesheet" type="text/css" href="Theme/Styles.css" />
        <script type="text/javascript" src="./Include/FramesetMgr.js"> </script>
        <script type="text/javascript" src="./Include/Toc.js"> </script>
        <script type="text/javascript" src="./Include/vernum.js"> </script>

        <script type="text/javascript">
    g_currentActivityId = null;
    g_previousActivityId = null;
    g_frameMgr = API_GetFramesetManager();

    function body_onload(e) {
        // Tell frameMgr to call back when current activity changes
        g_frameMgr.ShowActivityId = SetCurrentElement;
        g_frameMgr.ResetActivityId = ResetToPreviousSelection;

        // Tell frameMgr to call back with TOC active / inactive state changes
        g_frameMgr.SetTocNodes = SetTocNodes;

        // Register with framemanager that loading is complete
        g_frameMgr.RegisterFrameLoad(TOC_FRAME);
    }
</script>
    </head>
    <body class="NavBody" onload="body_onload(event)" onmousedown=" body_onclick(event); ">
        <div class="NavTopShadow"></div>
        <DIV id="divMain" style="visibility: hidden; MARGIN: 5px">
            <DIV noWrap >
                <!-- <p class="NavClosedPreviousBtnGrphic">&nbsp;</p> -->
                <% this.WriteToc();%>		
            </DIV>
        </DIV>
        <script type="text/javascript">
        
  // If the version of the page differs from the version of the script, don't render
  //if ("<%=TocVersion%>" != JsVersion())
  //{
 //   document.writeln("<%=InvalidVersionHtml%>");
 // }
        
</script>
    </body>
</html>
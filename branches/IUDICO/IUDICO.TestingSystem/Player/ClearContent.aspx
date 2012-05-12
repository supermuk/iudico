<%-- Copyright (c) Microsoft Corporation. All rights reserved. --%>
<%@ Assembly Name="IUDICO.TestingSystem" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClearContent.aspx.cs" Inherits="Microsoft.LearningComponents.Frameset.Frameset_ClearContent" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!-- MICROSOFT PROVIDES SAMPLE CODE "AS IS" AND WITH ALL FAULTS, AND WITHOUT ANY WARRANTY WHATSOEVER.  
    MICROSOFT EXPRESSLY DISCLAIMS ALL WARRANTIES WITH RESPECT TO THE SOURCE CODE, INCLUDING BUT NOT 
    LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE.  THERE IS 
    NO WARRANTY OF TITLE OR NONINFRINGEMENT FOR THE SOURCE CODE. -->
<html>
<head>
    <link rel="stylesheet" type="text/css" href="./Theme/Styles.css" />
    <script type="text/javascript" src="./Include/FramesetMgr.js"> </script>
    <script type="text/javascript">

        function PleaseWait() {
            if (frameMgr.ReadyForNavigation()) {
                return;
            }
            
            try {
                // clears content from the window and displays a "Please wait" message
                document.body.innerHTML = "<table width='100%' class='ErrorTitle'><tr><td align='center'><%=this.PleaseWaitHtml%></td></tr></table>";
            } catch (e) {
                // only happens in odd boundary cases. Retry the message after another timeout.
                setTimeout("PleaseWait()", 500);
            }
        }

        function onLoad() {
            frameMgr = API_GetFramesetManager();
            frameMgr.SetPostFrame(HIDDEN_FRAME);
            frameMgr.SetPostableForm(parent.parent.frames[MAIN_FRAME].document.getElementById(HIDDEN_FRAME).contentWindow.document.forms[0]);

            contentIsCleared = frameMgr.ContentIsCleared();

            if (contentIsCleared == true) {
                setTimeout("PleaseWait()", 500);
            } else {
                frameMgr.ShowErrorMessage("<%=this.UnexpectedErrorTitleHtml%>", "<%=this.UnexpectedErrorMsgHtml%>");
            }
        }

	
    </script>
</head>
<body onload='onLoad()' class="ErrorBody">
    &nbsp;
</body>
</html>

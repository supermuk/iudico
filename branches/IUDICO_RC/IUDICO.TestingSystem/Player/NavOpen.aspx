<%-- Copyright (c) Microsoft Corporation. All rights reserved. --%>
<%@ Assembly Name="IUDICO.TestingSystem" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NavOpen.aspx.cs" Inherits="Microsoft.LearningComponents.Frameset.Frameset_NavOpen" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<!-- MICROSOFT PROVIDES SAMPLE CODE "AS IS" AND WITH ALL FAULTS, AND WITHOUT ANY WARRANTY WHATSOEVER.  
        MICROSOFT EXPRESSLY DISCLAIMS ALL WARRANTIES WITH RESPECT TO THE SOURCE CODE, INCLUDING BUT NOT 
        LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE.  THERE IS 
        NO WARRANTY OF TITLE OR NONINFRINGEMENT FOR THE SOURCE CODE. -->
<head runat="server">
    <link rel="stylesheet" type="text/css" href="Theme/Styles.css" />
    <script type="text/javascript" src="./Include/FramesetMgr.js"> </script>
    <script type="text/javascript" src="./Include/Nav.js"> </script>
</head>
<body class="NavOpenBody" tabindex="1" onload="OnLoad( NAVOPEN_FRAME ); ">
    <table cellspacing="0" cellpadding="0" width="100%" border="0" valign="middle" background="Theme/nav_bg.gif">
        <tbody>
            <tr>
                <td align="left">
                    <table cellspacing="0" cellpadding="0" border="0">
                        <tbody>
                            <tr>
                                <td width="7" height="20">
                                    <img height="2" src="Theme/1px.gif" width="7" />
                                </td>
                                <td height="20">
                                    <div id="divPrevious" style="visibility: hidden">
                                        <span class="NavOpenNav" style="white-space: nowrap;">
                                            <img id="imgPrevious" title="<%=PreviousTitleHtml%>" src="Theme/Prev.png" /></span></div>
                                </td>
                                <td height="20">
                                    <div id="divNext" style="visibility: hidden">
                                        <span class="NavOpenNav">
                                            <img id="imgNext" title="<%=NextTitleHtml%>" src="Theme/Next.png" style="border-width: 0;" /></span></div>
                                </td>
                                <td width="6" height="20">
                                    <img height="2" src="Theme/1px.gif" width="6" style="border-width: 0;" />
                                </td>
                                <td height="20">
                                    <div id="divSave" style="visibility: hidden">
                                        <span class="NavOpenNav" style="white-space: nowrap;">
                                            <img id="imgSave" title="<%=SaveTitleHtml%>" src="Theme/Save.png" /></span></div>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
        </tbody>
    </table>
</body>
</html>

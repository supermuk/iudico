<%-- Copyright (c) Microsoft Corporation. All rights reserved. --%>
<%@ Assembly Name="IUDICO.TestingSystem" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainFrames.aspx.cs" Inherits="Microsoft.LearningComponents.Frameset.Frameset_MainFrames" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<!-- MICROSOFT PROVIDES SAMPLE CODE "AS IS" AND WITH ALL FAULTS, AND WITHOUT ANY WARRANTY WHATSOEVER.  
        MICROSOFT EXPRESSLY DISCLAIMS ALL WARRANTIES WITH RESPECT TO THE SOURCE CODE, INCLUDING BUT NOT 
        LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE.  THERE IS 
        NO WARRANTY OF TITLE OR NONINFRINGEMENT FOR THE SOURCE CODE. -->
<head runat="server">
    <link rel="stylesheet" type="text/css" href="Theme/Styles.css" />
</head>
<body>
    <table id="framesetParentMain" cellpadding="0" cellspacing="0" border="0" style="height: 500px;
        width: 100%">
        <tr>
            <td style="height: 100%">
                <table cellpadding="0" cellspacing="0" border="0" style="height: 100%; width: 100%">
                    <tr>
                        <td id="navigationColumn" class="TocWrapper" style="width: 220px; height: 100%;">
                            <iframe id="frameToc" name="frameToc" width="100%" height="100%" marginwidth="0"
                                marginheight="0" src="<%=this.TocFrameUrl%>" frameborder="0"></iframe>
                        </td>
                        <td style="height: 100%">
                            <table cellpadding="0" cellspacing="0" border="0" style="height: 100%; width: 100%">
                                <tr>
                                    <td style="height: 12px">
                                        <iframe id="frameNavClosed" name="frameNavClosed" width="100%" height="100%" marginwidth="0"
                                            marginheight="0" src="NavClosed.aspx" frameborder="0" scrolling="no"></iframe>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 100%">
                                        <table cellpadding="0" cellspacing="0" border="0" style="height: 100%; width: 100%">
                                            <tr>
                                                <td style="height: 100%;">
                                                    <iframe id="frameContent" name="frameContent" width="100%" height="100%" marginwidth="0"
                                                        marginheight="0" src="Content.aspx" frameborder="0"></iframe>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <%--<tr style="height: 14px">
                <td style="height: 14px">
                    <iframe class="BottomFrame" id="frameBottom" name="frameBottom" width="100%" height="100%"
                            marginwidth="0" marginheight="0" src="Bottom.htm" frameborder="0" scrolling="no"></iframe>
                </td>
            </tr>--%>
        <tr style="height: 1px">
            <td style="height: 1px">
                <iframe class="HiddenFrame" id="frameHidden" name="frameHidden" visible="false" width="100%"
                    height="100%" marginwidth="0" marginheight="0" src="<%=this.HiddenFrameUrl%>"
                    frameborder="0" scrolling="no"></iframe>
            </td>
        </tr>
    </table>
</body>
</html>

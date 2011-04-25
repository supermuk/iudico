<%-- Copyright (c) Microsoft Corporation. All rights reserved. --%>
<%@ Assembly Name="IUDICO.TestingSystem" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainFrames.aspx.cs" Inherits="Microsoft.LearningComponents.Frameset.Frameset_MainFrames" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

<!-- MICROSOFT PROVIDES SAMPLE CODE "AS IS" AND WITH ALL FAULTS, AND WITHOUT ANY WARRANTY WHATSOEVER.  
     MICROSOFT EXPRESSLY DISCLAIMS ALL WARRANTIES WITH RESPECT TO THE SOURCE CODE, INCLUDING BUT NOT 
     LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE.  THERE IS 
     NO WARRANTY OF TITLE OR NONINFRINGEMENT FOR THE SOURCE CODE. -->
     
<head runat="server">
    <link rel="stylesheet" type="text/css" href="Theme/Styles.css" />
</head>
<body>
    <table id="framesetParentMain" cellpadding="0" cellspacing="0" border="0" style="height: 500px; width: 100%">
        <tr>
            <td style="height: 100%">
                <table cellpadding="0" cellspacing="0" border="0" style="height: 100%; width: 100%">
                    <tr>
                        <td id="navigationColumn" style="width: 220px">
                            <table cellpadding="0" cellspacing="0" border="0" style="height: 100%; width: 100%">
                                <tr>
                                    <td style="height: 22px">
                                        <iframe class="NavOpenFrame" id="frameNavOpen" name="frameNavOpen" width="100%" height="100%"
			                            marginwidth="0" marginheight="0" src="NavOpen.aspx" frameborder="0" scrolling="no"></iframe>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 100%">
                                        <iframe id="frameToc" name="frameToc" width="100%" height="100%"
                                        marginwidth="0" marginheight="0" src="<%=TocFrameUrl %>" frameborder="0"></iframe>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table cellpadding="0" cellspacing="0" border="0" style="height: 100%; width: 100%">
                                <tr>
                                    <td style="height: 12px">
                                        <iframe id="frameNavClosed" name="frameNavClosed" width="100%" height="100%"
                                        marginwidth="0" marginheight="0" src="NavClosed.aspx" frameborder="0" scrolling="no"></iframe>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 100%">
                                        <table cellpadding="0" cellspacing="0" border="0" style="height: 100%; width: 100%">
                                            <tr>
                                                <td style="height: 100%;">
                                                    <iframe id="frameContent" name="frameContent" width="100%" height="100%"
                                                        marginwidth="0" marginheight="0" src="Content.aspx" frameborder="0"></iframe>
                                                </td>
                                                <td style="width: 16px">
                                                    <iframe id="frameScrollbarReplacement" name="frameScrollbarReplacement" width="100%" height="100%"
                                                    marginwidth="0" marginheight="0" src="NoScroll.htm" frameborder="0" scrolling="no"></iframe>
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
        <tr style="height: 14px">
            <td style="height: 14px">
                <iframe class="BottomFrame" id="frameBottom" name="frameBottom" width="100%" height="100%"
	            marginwidth="0" marginheight="0" src="Bottom.htm" frameborder="0" scrolling="no"></iframe>
            </td>
        </tr>
        <tr style="height: 1px">
            <td style="height: 1px">
                <iframe class="HiddenFrame" id="frameHidden" name="frameHidden" visible="false" width="100%" height="100%"
	            marginwidth="0" marginheight="0" src="<%=HiddenFrameUrl %>" frameborder="0" scrolling="no"></iframe>
            </td>
        </tr>
    </table>
</body>
<!--<FRAMESET id=framesetParentMain border=0 frameSpacing=0 rows=*,14,1 frameBorder=0>
	<FRAMESET id=framesetParentUI cols=180,*>
		<FRAMESET id=framesetToc rows=22,*>
			<FRAME class=NavOpenFrame id=frameNavOpen name=frameNavOpen 
			       marginWidth=0 frameSpacing=0 marginHeight=0 src="NavOpen.aspx" frameBorder=0 scrolling=no>
			                
			<FRAME id=frameToc name=frameToc marginWidth=0 frameSpacing=0 marginHeight=0 src="<%=TocFrameUrl %>" frameBorder=0>
		</FRAMESET>

		<FRAMESET class=ContentFrameLeftBorder id=framesetParentContent border=0 rows=12,*>
			<FRAME id=frameNavClosed tabIndex=-1 name=frameNavClosed marginWidth=0 frameSpacing=0 marginHeight=0 src="NavClosed.aspx" frameBorder=0 scrolling=no>

			<FRAMESET id=framesetContent border=0 cols=*,16>
			
				<FRAME id=frameContent name=frameContent src="Content.aspx" frameBorder=0>
				<FRAME id=frameScrollbarReplacement tabIndex=-1 name=frameScrollbarReplacement marginWidth=0 frameSpacing=0 marginHeight=0 src="NoScroll.htm" frameBorder=0 scrolling=no>
				
			</FRAMESET>

		</FRAMESET>

	</FRAMESET>

	<FRAME class=BottomFrame id=frameBottom tabIndex=-1 name=frameBottom 
	            marginWidth=0 frameSpacing=0 marginHeight=0 src="Bottom.htm" frameBorder=0 noResize scrolling=no>
	
	<FRAME class=HiddenFrame id=frameHidden tabIndex=-1 name=frameHidden visible=false
	            marginWidth=0 frameSpacing=0 marginHeight=0 src="<%=HiddenFrameUrl %>" frameBorder=0 noResize scrolling=no>

</FRAMESET>-->
</html>

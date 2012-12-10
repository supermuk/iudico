<%@ Assembly Name="IUDICO.TestingSystem" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Title.aspx.cs" Inherits="IUDICO.TestingSystem.Player.Title" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head id="Head1" runat="server">
    <link rel="stylesheet" type="text/css" href="Theme/Styles.css" />
    <script type="text/javascript" src="./Include/FramesetMgr.js"> </script>
    <script type="text/javascript" src="./Include/Nav.js"> </script>
</head>
<body onload="OnLoad(NAVOPEN_FRAME);">
	<div class="ShellTitleServices">
        <!--img id="imgPrevious" class="Command" title="<%=PreviousTitleHtml%>" alt="<%=PreviousTitleHtml%>" src="Theme/Prev.png" />
        <img id="imgNext" class="Command" title="<%=NextTitleHtml%>" alt="<%=NextTitleHtml%>" src="Theme/Next.png" />
        <!--img id="imgSave" class="Command" title="<%=SaveTitleHtml%>" alt="<%=SaveTitleHtml%>" src="Theme/Save.png" /-->
    </div>
</body>
</html>

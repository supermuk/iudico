<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.Collections.Generic.Dictionary<string,string>>" %>
<%@ Assembly Name="IUDICO.CurriculumManagement" %>
<%@ Import Namespace="IUDICO.Common" %>

<br/>
<div><%=Localization.GetMessage("ClickErrorToCorrect") %></div>
<ul>
	<% foreach (var error in Model) {%>
			<li><a href="<%: error.Value%>" style="color: red"><%: error.Key%> </a></li>
		<%} %>
</ul>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.Collections.Generic.List<string>>" %>
<%@ Assembly Name="IUDICO.CurriculumManagement" %>

<br/>
<ul>
	<% foreach (var error in Model) {%>
			<li><%: error%></li>
		<%} %>
</ul>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IUDICO.Common.Models.Shared.Chapter>" %>
<%@ Assembly Name="IUDICO.DisciplineManagement" %>
<%@ Import Namespace="IUDICO.Common" %>

<tr id="chapter<%:Model.Id%>" class="child-of-discipline<%:Model.DisciplineRef%> disciplineChapter">
	<td></td>
	<td  class="disciplineChapterName">	<%: Model.Name%>									</td>
	<td>	<%: String.Format("{0:g}", Model.Created)%>		</td>
	<td>	<%: String.Format("{0:g}", Model.Updated)%>		</td>
	<td>
		<a href="#" onclick="addTopic(<%: Model.Id %>);"><%=Localization.GetMessage("Add")%></a>
        |
		<a href="#" onclick="editChapter(<%: Model.Id %>);"><%=Localization.GetMessage("Edit")%></a>
        |
		<a href="#" onclick="deleteChapter(<%: Model.Id %>);"><%=Localization.GetMessage("Delete") %></a>
	</td>
</tr>
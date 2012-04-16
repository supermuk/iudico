<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IUDICO.Common.Models.Shared.Chapter>" %>
<%@ Assembly Name="IUDICO.DisciplineManagement" %>

<tr id="chapter<%:Model.Id%>" class="child-of-discipline<%:ViewData["DisciplineId"]%> disciplineChapter">
	<td></td>
	<td  class="disciplineChapterName">	<%: Model.Name%>									</td>
	<td>	<%: String.Format("{0:g}", Model.Created)%>		</td>
	<td>	<%: String.Format("{0:g}", Model.Updated)%>		</td>
	<td>
		<a href="#" onclick="addTopic(<%: Model.Id %>);"><%=IUDICO.DisciplineManagement.Localization.getMessage("Add")%></a>
        |
		<a href="#" onclick="editChapter(<%: Model.Id %>);"><%=IUDICO.DisciplineManagement.Localization.getMessage("Edit")%></a>
        |
		<a href="#" onclick="deleteChapter(<%: Model.Id %>);"><%=IUDICO.DisciplineManagement.Localization.getMessage("Delete") %></a>
	</td>
</tr>
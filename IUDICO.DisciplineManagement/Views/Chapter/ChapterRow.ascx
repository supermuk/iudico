<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IUDICO.Common.Models.Shared.Chapter>" %>
<%@ Assembly Name="IUDICO.DisciplineManagement" %>

<tr id="chapter<%:Model.Id%>" class="child-of-discipline<%:Model.DisciplineRef%> disciplineChapter">
	<td></td>
	<td  class="disciplineChapterName">	<%: Model.Name%>									</td>
	<td>	<%: String.Format("{0:g}", Model.Created)%>		</td>
	<td>	<%: String.Format("{0:g}", Model.Updated)%>		</td>
	<td>
		<a href="#" onclick="addTopic(<%: Model.Id %>);"><%=IUDICO.DisciplineManagement.Localization.GetMessage("Add")%></a>
        |
		<a href="#" onclick="editChapter(<%: Model.Id %>);"><%=IUDICO.DisciplineManagement.Localization.GetMessage("Edit")%></a>
        |
		<a href="#" onclick="deleteChapter(<%: Model.Id %>);"><%=IUDICO.DisciplineManagement.Localization.GetMessage("Delete") %></a>
	</td>
</tr>
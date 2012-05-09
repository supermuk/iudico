<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IUDICO.Common.Models.Shared.Chapter>" %>
<%@ Assembly Name="IUDICO.DisciplineManagement" %>
<%@ Import Namespace="IUDICO.Common" %>

<tr id="chapter<%:Model.Id%>" class="child-of-discipline<%:Model.DisciplineRef%> disciplineChapter">
	<td><div class="collapsedIcon"></div></td>
	<td  class="disciplineChapterName">	<%: Model.Name%>									</td>
	<td>	<%: DateFormatConverter.DataConvert(Model.Created) %>	</td>
	<td>	<%: DateFormatConverter.DataConvert(Model.Updated)%>	</td>
	<td>
		<a href="#" onclick="addTopic(<%: Model.Id %>);"><%=Localization.GetMessage("AddTopic")%></a>
        |
		<a href="#" onclick="editChapter(<%: Model.Id %>);"><%=Localization.GetMessage("Edit")%></a>
        |
		<a href="#" onclick="deleteChapter(<%: Model.Id %>);"><%=Localization.GetMessage("Delete") %></a>
	</td>
</tr>
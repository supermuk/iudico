<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IUDICO.DisciplineManagement.Models.ViewDataClasses.ViewTopicModel>" %>
<%@ Assembly Name="IUDICO.DisciplineManagement" %>
<%@ Import Namespace="IUDICO.Common" %>

<tr id="topic<%:Model.Id%>" class="child-of-chapter<%: Model.ChapterId%> disciplineTopic">
	<td></td>
	<td class="disciplineTopicName">	<%: Model.TopicName%> 
            <%: !string.IsNullOrEmpty(Model.TestTopicType) ? "[" + Model.TestTopicType + ":" + Model.TestCourseName + "]" : ""%> 
            <%: !string.IsNullOrEmpty(Model.TheoryTopicType) ? "[" + Model.TheoryTopicType  + ":" + Model.TheoryCourseName + "]" : ""%>
    </td>
	<td>	<%: String.Format("{0:g}", Model.Created)%>		</td>
	<td>	<%: String.Format("{0:g}", Model.Updated)%>		</td>
	<td>
        <a href="#" onclick="editTopic(<%: Model.Id %>);"><%=Localization.GetMessage("Edit")%></a>
        |
        <a href="#" onclick="moveTopicUp(<%: Model.Id %>);"><%=Localization.GetMessage("Up")%></a>
        |
        <a href="#" onclick="moveTopicDown(<%: Model.Id %>);"><%=Localization.GetMessage("Down")%></a>
        | 
        <a href="#" onclick="deleteTopic(<%: Model.Id %>);"><%=Localization.GetMessage("Delete")%></a>

	</td>
</tr>
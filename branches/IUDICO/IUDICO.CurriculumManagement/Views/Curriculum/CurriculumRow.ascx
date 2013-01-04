<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IUDICO.CurriculumManagement.Models.ViewDataClasses.ViewCurriculumModel>" %>
<%@ Assembly Name="IUDICO.CurriculumManagement" %>
<%@ Import Namespace="IUDICO.Common" %>

<tr id="curriculum<%: Model.Id %>" <%if(!Model.IsValid) {%> class="buged-curriculum" <% } %>>
            <td>
	             <div class="collapsedIcon" style="display: inline-block;"></div>
            </td>
				<td><input type="checkbox" id="<%= Model.Id %>" onclick="event.stopPropagation();" /></td>
            <td class="clickable curriculum-important-field">
                <%: Model.GroupName %>
            </td>
            <td class="clickable curriculum-important-field">
                <%: Model.DisciplineName %>
            </td>
            <td class="clickable">
                <%: Model.StartDate %>
            </td>
            <td onclick="details(<%: Model.Id %>);" class="clickable">
                <%: Model.EndDate %>
            </td>
            <td>
	             <a href="javascript:void(0)" onclick="CurriculumEditClick(<%=Model.Id %>); event.stopPropagation();"><%=Localization.GetMessage("Edit") %></a>
                |
                <a onclick="deleteItem(<%: Model.Id %>); event.stopPropagation();" href="#"><%=Localization.GetMessage("Delete")%></a>
					 <a id="showErrorsLink" onclick="showValidationErrors(<%: Model.Id %>); event.stopPropagation();" href="#" <% if (Model.IsValid) { %> style=" display: none;" <% } %>><%=Localization.GetMessage("ShowErrors") %></a>
            </td>				
        </tr>		
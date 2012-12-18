<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IUDICO.DisciplineManagement.Models.ViewDataClasses.ViewDisciplineModel>" %>
<%@ Assembly Name="IUDICO.DisciplineManagement" %>
<%@ Import Namespace="IUDICO.Common" %>

<tr id="discipline<%:Model.Discipline.Id%>" class="discipline" >
		<td><div class="collapsedIcon"></div></td>
    <td class="disciplineName">	<%:Model.Discipline.Name %>				</td>
    <td>	<%: DateFormatConverter.DataConvert(Model.Discipline.Created)%> </td>
    <td>	<%: DateFormatConverter.DataConvert(Model.Discipline.Updated) %> </td>
    <td>
		    <div style="width: 75%; float: left">
		    <a href="#" onclick="addChapter(<%: Model.Discipline.Id %>);"><%=Localization.GetMessage("AddChapter")%></a>
		    |
            <a href="#" onclick="editDiscipline(<%: Model.Discipline.Id %>);"><%=Localization.GetMessage("Edit")%></a>
		    | 
				    <a href="#" onclick="shareDiscipline(<%: Model.Discipline.Id %>)"><%=Localization.GetMessage("Share")%></a> 
		    |       
            <%if(Model.Error == string.Empty)
            {%>
    	        <%: Html.ActionLink(Localization.GetMessage("Export"), "Export", new { DisciplineID = Model.Discipline.Id })%>
            <%}
            else
            {%>
                 <a href="javascript:void(0)" onclick="exportInvalidDiscipline()"><%=Localization.GetMessage("Export")%></a>    
            <%}%>
		    |
		    <a href="#" onclick="deleteDiscipline(<%: Model.Discipline.Id %>)"><%=Localization.GetMessage("Delete")%></a>
		    </div>
		    <div style="width: 23%; float: right">
		    <span id="error<%:Model.Discipline.Id%>" style="color: red; float: right"><%:Model.Error%></span>
		    </div>
    </td>
</tr>
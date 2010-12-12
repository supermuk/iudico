<%@ Import Namespace="accunoteclone.Helpers"%>
<%@ Import Namespace="accunoteclone.Helpers.TemplateMetadata"%>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="IUDICO.Common.Models.TemplateMetadata" %>

<script runat="server">
    DropDownListAttribute GetDropDownListAttribute()
    {
        FieldTemplateMetadata metaData = ViewData.ModelMetadata as FieldTemplateMetadata;

        return (metaData != null) ? metaData.Attributes.OfType<DropDownListAttribute>().SingleOrDefault() : null;
    }
</script>
<% 
    DropDownListAttribute attribute = GetDropDownListAttribute();
    
    
    /* ITS ONLY DUMMY FIX! */
    /* You should change the logic of the selectedVallue! */
    object model = ViewData.Model;
    string selectedValue = model as string;
    
%>
<% if (attribute != null) {%>
    <%= Html.DropDownList(string.Empty, new SelectList((attribute.Values ?? ViewData[attribute.DataKey]) as IEnumerable, attribute.DataValueField, attribute.DataTextField, /*attribute.SelectedValue*/ selectedValue), attribute.OptionLabel, attribute.HtmlAttributes) %>
<% }%>
<% else {%>
    <%= Html.EditorForModel()%>
<% }%>
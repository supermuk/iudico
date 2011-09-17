<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="IUDICO.Common.Models.Attributes" %>
<%@ Import Namespace="IUDICO.Common.Models.TemplateMetadata" %>

<script runat="server">
    DropDownListAttribute GetDropDownListAttribute()
    {
        var metaData = ViewData.ModelMetadata as FieldTemplateMetadata;

        return (metaData != null) ? metaData.Attributes.OfType<DropDownListAttribute>().SingleOrDefault() : null;
    }
</script>
<% 
    var attribute = GetDropDownListAttribute();
    var text = attribute.List.Where(s => s.Value == Model.ToString()).Select(s => s.Text).FirstOrDefault();
    
    Response.Write(text);
%>
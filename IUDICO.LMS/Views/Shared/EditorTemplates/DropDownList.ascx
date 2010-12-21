<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<SelectListItem>>" %>
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
    MvcHtmlString rtrn;

    if (attribute != null)
    {
        rtrn = Html.DropDownList(string.Empty, new SelectList(ViewData.Model, "Value", "Text", ViewData.Model.SingleOrDefault(s => s.Selected)), attribute.OptionLabel, new { ID = attribute.TargetProperty, Name = attribute.TargetProperty });
    }
    else
    {
        rtrn = Html.EditorForModel();
    }
    
    Response.Write(rtrn);
%>
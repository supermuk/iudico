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

    if (attribute != null && Model != null)
    {
        ViewData.TemplateInfo.HtmlFieldPrefix = String.Empty;
        
        rtrn = Html.DropDownList(attribute.TargetProperty, new SelectList(Model, "Value", "Text", Model.SingleOrDefault(s => s.Selected).Value), attribute.OptionLabel, new { @id = attribute.TargetProperty, @name = attribute.TargetProperty});
    }
    else
    {
        rtrn = Html.EditorForModel();
    }
    
    Response.Write(rtrn);
%>
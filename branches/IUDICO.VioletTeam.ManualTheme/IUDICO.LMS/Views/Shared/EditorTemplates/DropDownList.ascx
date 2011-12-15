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
    
    MvcHtmlString rtrn;

    if (attribute != null && Model != null)
    {
        ViewData.TemplateInfo.HtmlFieldPrefix = String.Empty;

        rtrn = Html.DropDownList(ViewData.ModelMetadata.PropertyName, attribute.List, attribute.OptionLabel);
    }
    else
    {
        rtrn = Html.EditorForModel();
    }
    
    Response.Write(rtrn);
%>
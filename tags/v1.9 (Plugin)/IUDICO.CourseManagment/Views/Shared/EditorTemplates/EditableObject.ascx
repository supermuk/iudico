<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="accunoteclone.Models" %>

<script runat="server">
    protected string PropertyName
    {
        get
        {
            Type modelType = ViewData.ModelMetadata.ModelType;
            Type attrType = typeof(EditableDisplayAttribute);

            object attr = modelType.GetCustomAttributes(attrType, false).First();

            EditableDisplayAttribute EditableAttr = attr as EditableDisplayAttribute;

            if (EditableAttr != null)
            {
                string propName = EditableAttr.PropertyName;
                return propName;
            }

            return null;
        }
    }
    
    //protected string 
</script>
<%
    string id = "1";
    string propName = this.PropertyName;

    //ViewDataDictionary<object> dict = new ViewDataDictionary<object>();
    //dict.Model = Model;
%>

<div class="editable-value">
    <div id="display_<%=id %>" class="display-for">
        <%=Html.Display(propName) %>
    </div>
    <div id="editor_<%=id %>" class="editor-for">
        <%=Html.Editor(propName) %>
    </div>
</div>
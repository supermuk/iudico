<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.DateTime?>" %>

<%= (Model != null ? ((DateTime)Model).ToShortDateString() : "---")  %>
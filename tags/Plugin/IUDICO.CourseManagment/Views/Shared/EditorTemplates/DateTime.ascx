<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.DateTime?>" %>

<%= Html.TextBox(
    "", 
    (Model != null && Model != DateTime.MinValue ? ((DateTime)Model).ToShortDateString() : DateTime.Now.ToShortDateString()), 
    new { @class = "date-edit-box" }
)%>
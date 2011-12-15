<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<%= Model.RollupObjectiveSatisfied %>
<%= Model.RollupProgressCompletion %>
<%= Model.ObjectiveMeasureWeight %>

<% foreach (var rollupRule in Model._RollupRules)
   { %>
  <div>
  <%= rollupRule.MinimumCount  %>
  <%= rollupRule.MinimumPercent %>
  <%= rollupRule.ChildActivitySet.ToString() %>
  </div>
<% } %>

<input type="button" value="add" />
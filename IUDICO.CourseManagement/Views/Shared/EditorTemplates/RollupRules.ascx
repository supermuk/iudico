<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IUDICO.CourseManagement.Models.ManifestModels.SequencingModels.RollupModels.RollupRules>" %>

<%= Html.EditorForModel(Model.RollupObjectiveSatisfied) %>
<%= Html.EditorForModel(Model.RollupProgressCompletion) %>
<%= Html.EditorForModel(Model.ObjectiveMeasureWeight) %>

<% foreach (var rollupRule in Model._RollupRules)
   { %>
  <div>
  <%= Html.EditorForModel(rollupRule) %>
  </div>
<% } %>

<input type="button" value=<%=IUDICO.CourseManagement.Localization.GetMessage("add")%> />
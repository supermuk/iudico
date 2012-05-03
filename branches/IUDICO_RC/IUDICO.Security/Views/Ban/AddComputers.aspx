<%@ Assembly Name="IUDICO.Security" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Security.ViewModels.Ban.AddComputerViewModel>" %>
<%@ Import Namespace="IUDICO.Common" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="TitleContent" runat="server">
	Add Computer
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <fieldset>
    <legend><%=Localization.GetMessage("Add computer")%></legend>
    <% Html.EnableClientValidation(); %>

    <% if (string.IsNullOrEmpty(Model.ComputerIP) || Model.State == IUDICO.Security.Models.ViewModelState.Edit) 
       { %>
    
        <% using (Html.BeginForm())
           { %>
            
            <%= Html.EditorForModel() %>
        <p>
           <input type="submit" value="<%=Localization.GetMessage("Save") %>" name="saveButton" />
       </p>
        <% }
       }
       else 
       {
           Writer.Write(Html.DisplayForModel().ToHtmlString());
           Writer.Write(Html.ActionLink(Localization.GetMessage("BackToList"), "BanComputer", "Ban"));           
       } %>

       
    </fieldset>

</asp:Content>
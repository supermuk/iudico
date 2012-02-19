<%@ Assembly Name="IUDICO.Analytics" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IUDICO.Common.Models.Shared.Feature>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	New Feature
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>New Feature</h2>

    <%
        using (Html.BeginForm())
        {%>
        <%:Html.ValidationSummary(true)%>

        <fieldset>
            <legend>Fields</legend>
            
            <%:Html.EditorForModel()%>
            
            <p>
                <input type="submit" value="Create" />
            </p>
        </fieldset>

    <%
        }%>

    <div>
        <%:Html.ActionLink("Back To List", "Index")%>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>


<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.DateTime?>" %>
<%@ Import Namespace="System.Threading" %>
<script src="/Scripts/jquery/jquery-ui-1.8.5.js" type="text/javascript"></script>
<script src="/Scripts/jquery/jquery-ui-timepicker-addon.js" type="text/javascript"></script>
<link href="/Content/TimePicker.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
    //make it global in case somebody will use it too
    var dateFormats =
    {
        "en-US": ["hh:mm", "m/d/yy"],
        "uk-UA": ["h:mm", "dd.mm.yy"]
    };

    $(document).ready(function () {
        var culture = "<%:Thread.CurrentThread.CurrentCulture.Name %>";
        if (dateFormats[culture] == null) {
            culture = "en-US";
        }

        $(".datePicker").datetimepicker({
            timeFormat: dateFormats[culture][0],
            dateFormat: dateFormats[culture][1]
        });
    });
</script>

<%=Html.TextBox("", (Model != null && Model != DateTime.MinValue ?
        String.Format("{0:g}", (DateTime)Model): String.Format("{0:g}", DateTime.Now)),
        new { @class = "datePicker" })%>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.DateTime?>" %>

<script src="/Scripts/jquery/jquery-ui-1.8.5.js" type="text/javascript"></script>
<script src="../../../Scripts/jquery/jquery-ui-timepicker-addon.js" type="text/javascript"></script>

<script type="text/javascript">
    $(function () {
        $(".datePicker").datetimepicker({
            timeFormat: 'hh:mm:ss',
            dateFormat: 'm/dd/yy'
        });
    });
</script>

<%=Html.TextBox("", (Model != null && Model != DateTime.MinValue ? ((DateTime)Model).ToString() : DateTime.Now.ToString()),
        new { @class = "datePicker" })%>
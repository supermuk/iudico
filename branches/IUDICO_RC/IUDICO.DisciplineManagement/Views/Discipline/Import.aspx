<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<%@ Import Namespace="IUDICO.Common" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Import
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%=Localization.GetMessage("ImportDiscipline")%></h2>
	 <form id="file_upload" action="/DisciplineAction/UploadFiles" method="POST" enctype="multipart/form-data">
	<div class="fileupload-buttonbar">
		<div class="progressbar fileupload-progressbar">
		</div>
		<span class="fileinput-button"><a href="javascript:void(0)" class="upload-image">
			<%=Localization.GetMessage("Upload")%></a>
			<input type="file" name="files[]" multiple>
		</span>
	</div>
	</form>
	<br />
	<br />
	<div id="validation_message">
	</div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
	<link href="/Content/jquery.fileupload-ui.css" rel="stylesheet" type="text/css" />
<script src="/Scripts/jquery/jquery.fileupload.js" type="text/javascript"></script>
<script type="text/javascript">
	$(document).ready(function () {
		function validationInform() {
		   $.ajax({
				type: "GET",
				url: '/Discipline/ValidationInfo',
				async: true,
				cache: false,
				timeout: 5000,
				success: function (data) {
					// do what you need with the returned data...
					$('#validation_message').html(data.message);
					setTimeout(validationInform, 3000);
				},
			});
		};

		$('.progressbar').progressbar({ 
			value: 0,
			change: function (event, ui) {
			}
		});

		$('#file_upload').fileupload({
			dataType: 'json',
			url: '/Discipline/UploadFiles',
			progressall: function (e, data) {
				$(this).find('.progressbar').progressbar({value: parseInt(data.loaded / data.total * 100, 10)});
				$('#validation_message').html("Please wait, it's can take few minutes");
			},
			done: function (e, data) {
				$('#validation_message').html(data.result.message);
				$(this).find('.progressbar').progressbar({ value: 100 });
				if(data.result.message == "complete") {
					window.location = '/DisciplineAction/Index';
				}
			}
		});
	});
</script>
</asp:Content>

<%@ Assembly Name="IUDICO.DisciplineManagement" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="IUDICO.Common" %>
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
				validationInform();
			}
		});

		$('#file_upload').fileupload({
			dataType: 'json',
			url: '/Discipline/UploadFiles',
			progressall: function (e, data) {
				$(this).find('.progressbar').progressbar({value: parseInt(data.loaded / data.total * 100, 10)});
			},
			done: function (e, data) {
				$('#validation_message').html(data.result.message);
//				$('#file_name').html(data.result.name);
//				$('#file_type').html(data.result.type);
//				$('#file_size').html(data.result.size);
//				$('#show_image').html('<img src="/home/image/' + data.result.name + '" />');
				$(this).find('.progressbar').progressbar({ value: 100 });

			}
		});
	});
</script>
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
<div id="file_name">
</div>
<div id="file_type">
</div>
<div id="file_size">
</div>
<div id="show_image">
</div>

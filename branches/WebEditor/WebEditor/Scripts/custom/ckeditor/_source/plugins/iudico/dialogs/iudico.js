CKEDITOR.dialog.add('iudico', function (editor) {
    //var lang = editor.lang.about;

    return {
        title: 'IUDICO',
        minWidth: 390,
        minHeight: 230,
        contents: [
			{
			    id: 'tab1',
			    label: '',
			    title: 'Question',
			    expand: true,
			    padding: 0,
			    elements:
				[
					{
					    type: 'html',
					    html:
                            '<div class="cke_iudico_container">' +

                            '</div>'
					}
				],
			    onOk: function () { },
			    onCancel: function () { },
			    onLoad: function (data) { },
			}
		],
        buttons: [CKEDITOR.dialog.okButton, CKEDITOR.dialog.cancelButton]
    };
});

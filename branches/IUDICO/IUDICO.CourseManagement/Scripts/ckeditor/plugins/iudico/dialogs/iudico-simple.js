CKEDITOR.dialog.add('iudico-simple', function (editor) {
    //var lang = editor.lang.about;

    return {
        title: editor.lang.iudico.title,
        minWidth: 390,
        minHeight: 230,
        onShow: function () {
            this.getIudicoObject(editor);
        },
        onOk: function () {
            this.saveIudicoObject(editor);
        },
        onCancel: function () { },
        onLoad: function (data) { },
        contents: [
			{
			    id: 'simpleTab',
			    label: editor.lang.iudico.simpleTab,
			    title: editor.lang.iudico.simpleTab,
			    padding: 2,
			    elements:
				[
                    {
                        id: 'question',
                        type: 'text',
                        label: editor.lang.iudico.question,
                        labelLayout: 'horizontal',
                        commit: function (objectNode, paramMap, extraStyles, extraAttributes) {
                            if (paramMap['question']) {
                                paramMap['question'].setAttribute("value", this.getValue());
                            } else {
                                paramMap['question'] = this.getDialog().addParam(objectNode, 'question', this.getValue());
                            }
                        },
                        setup: function (objectNode, embedNode, paramMap) {
                            this.setValue(paramMap['question']);
                        }
                    },
                    {
                        id: 'correctAnswer',
                        type: 'text',
                        label: editor.lang.iudico.correctAnswer,
                        labelLayout: 'horizontal',
                        commit: function (objectNode, paramMap, extraStyles, extraAttributes) {
                            if (paramMap['correctAnswer']) {
                                paramMap['correctAnswer'].setAttribute("value", this.getValue());
                            } else {
                                paramMap['correctAnswer'] = this.getDialog().addParam(objectNode, 'correctAnswer', this.getValue());
                            }
                        },
                        setup: function (objectNode, embedNode, paramMap) {
                            this.setValue(paramMap['correctAnswer']);
                        }
                    }
				]
			}
		],

        buttons: [CKEDITOR.dialog.okButton, CKEDITOR.dialog.cancelButton]
    };
});
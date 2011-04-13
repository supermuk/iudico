CKEDITOR.dialog.add('iudico-choice', function (editor) {
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
                id: 'choiceTab',
                label: editor.lang.iudico.choiceTab,
                title: editor.lang.iudico.choiceTab,
                padding: 2,
                elements:
				[
                    {
                        id: 'question',
                        type: 'text',
                        label: editor.lang.iudico.question,
                        labelLayout: 'horizontal',
                        commit: function (element) {
                            addParam(element, 'question', this.getValue());
                        }

                    },
                    {
                        id: 'correctAnswer',
                        type: 'select',
                        label: editor.lang.iudico.correctAnswer,
                        labelLayout: 'horizontal',
                        items: [],
                        setup: function (element) {

                        },
                        commit: function (element) {
                            addParam(element, 'correctAnswer', this.getValue());
                        }
                    },
                    {
                        type: 'hbox',
                        widths: ['100px', '200px'],
                        children:
                        [
                            {
                                id: 'addChoice',
                                type: 'button',
                                label: editor.lang.iudico.addChoice,
                                onClick: function () {

                                }
                            },
                            {
                                id: 'choice',
                                type: 'text',
                                label: editor.lang.iudico.choice,
                                labelLayout: 'horizontal',
                                items: [],
                                setup: function (element) {

                                },
                                commit: function (element) {
                                    //addParam(element, 'correctAnswer', this.getValue());
                                }
                            }
                        ]
                    },
                    {
                        type: 'hbox',
                        widths: ['50px', '200px'],
                        children:
                        [
                            {
                                id: 'deleteChoice',
                                type: 'button',
                                label: editor.lang.iudico.deleteChoice,
                                onClick: function () {

                                }
                            }/*,
                            {
                                id: 'choices',
                                type: 'html'
                            }*/
                        ]
                    }
				]

            }

		],

        buttons: [CKEDITOR.dialog.okButton, CKEDITOR.dialog.cancelButton]
    };
});
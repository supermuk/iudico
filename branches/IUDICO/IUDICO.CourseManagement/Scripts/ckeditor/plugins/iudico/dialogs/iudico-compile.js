CKEDITOR.dialog.add('iudico-compile', function (editor) {

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
                id: 'compileTab',
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
                        id: 'preCode',
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
                        id: 'postCode',
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
                    }
				]
            },
            {
                id: 'testsTab',
                label: editor.lang.iudico.choiceTab,
                title: editor.lang.iudico.choiceTab,
                padding: 2,
                elements:
				[
                    {
                        type: 'hbox',
                        widths: ['200px', '100px'],
                        children:
                        [
                            {
                                id: 'choice',
                                type: 'text',
                                label: editor.lang.iudico.choice,
                                labelLayout: 'horizontal',
                                items: [],
                                setup: function () {
                                    this.setValue('');
                                }
                            },
                            {
                                id: 'addChoice',
                                type: 'button',
                                label: editor.lang.iudico.addChoice,
                                onClick: function () {
                                    var dialog = this.getDialog(),
										choices = dialog.getContentElement('choiceTab', 'cmbChoices'),
                                        choice = dialog.getContentElement('choiceTab', 'choice'),
                                        correctAnswer = dialog.getContentElement('choiceTab', 'correctAnswer');


                                    addOption(choices, choice.getValue(), choice.getValue(), dialog.getParentEditor().document);
                                    addOption(correctAnswer, choice.getValue(), choice.getValue(), dialog.getParentEditor().document);

                                    choice.setValue('');
                                }
                            }
                        ]
                    },

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
                        id: 'preCode',
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
                        id: 'postCode',
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
                    }
				]
            }

		],

        buttons: [CKEDITOR.dialog.okButton, CKEDITOR.dialog.cancelButton]
    };
});
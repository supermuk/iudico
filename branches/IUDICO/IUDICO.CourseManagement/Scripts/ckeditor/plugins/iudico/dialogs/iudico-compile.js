CKEDITOR.dialog.add('iudico-compile', function (editor) {

    // Add a new option to a SELECT object (combo or list).
    function addOption(combo, optionText, optionValue, documentObject, index) {
        combo = getSelect(combo);
        var oOption;
        if (documentObject)
            oOption = documentObject.createElement("OPTION");
        else
            oOption = document.createElement("OPTION");

        if (combo && oOption && oOption.getName() == 'option') {
            if (CKEDITOR.env.ie) {
                if (!isNaN(parseInt(index, 10)))
                    combo.$.options.add(oOption.$, index);
                else
                    combo.$.options.add(oOption.$);

                oOption.$.innerHTML = optionText.length > 0 ? optionText : '';
                oOption.$.value = optionValue;
            }
            else {
                if (index !== null && index < combo.getChildCount())
                    combo.getChild(index < 0 ? 0 : index).insertBeforeMe(oOption);
                else
                    combo.append(oOption);

                oOption.setText(optionText.length > 0 ? optionText : '');
                oOption.setValue(optionValue);
            }
        }
        else
            return false;

        return oOption;
    }

    // Remove option from a SELECT object.
    function removeOption(combo, index) {
        combo = getSelect(combo);
        combo.getChild(index).remove();
    }

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
                label: editor.lang.iudico.compileTab,
                title: editor.lang.iudico.compileTab,
                padding: 2,
                elements:
				[
                    {
                        id: 'language',
                        type: 'select',
                        label: editor.lang.iudico.language,
                        items: [ ['C#'], ['C++'], ['Java'] ],
                        labelLayout: 'horizontal',
                        commit: function (objectNode, paramMap, extraStyles, extraAttributes) {
                            if (paramMap[this.id]) {
                                paramMap[this.id].setAttribute("value", this.getValue());
                            } else {
                                paramMap[this.id] = this.getDialog().addParam(objectNode, this.id, this.getValue());
                            }
                        },
                        setup: function (objectNode, embedNode, paramMap) {
                            this.setValue(paramMap[this.id]);
                        }
                    },
                    {
                        id: 'memoryLimit',
                        type: 'text',
                        label: editor.lang.iudico.memoryLimit,
                        labelLayout: 'horizontal',
                        commit: function (objectNode, paramMap, extraStyles, extraAttributes) {
                            if (paramMap[this.id]) {
                                paramMap[this.id].setAttribute("value", this.getValue());
                            } else {
                                paramMap[this.id] = this.getDialog().addParam(objectNode, this.id, this.getValue());
                            }
                        },
                        setup: function (objectNode, embedNode, paramMap) {
                            this.setValue(paramMap[this.id]);
                        }
                    },
                    {
                        id: 'outputLimit',
                        type: 'text',
                        label: editor.lang.iudico.outputLimit,
                        labelLayout: 'horizontal',
                        commit: function (objectNode, paramMap, extraStyles, extraAttributes) {
                            if (paramMap[this.id]) {
                                paramMap[this.id].setAttribute("value", this.getValue());
                            } else {
                                paramMap[this.id] = this.getDialog().addParam(objectNode, this.id, this.getValue());
                            }
                        },
                        setup: function (objectNode, embedNode, paramMap) {
                            this.setValue(paramMap[this.id]);
                        }
                    },
                    {
                        id: 'timeLimit',
                        type: 'text',
                        label: editor.lang.iudico.timeLimit,
                        labelLayout: 'horizontal',
                        commit: function (objectNode, paramMap, extraStyles, extraAttributes) {
                            if (paramMap[this.id]) {
                                paramMap[this.id].setAttribute("value", this.getValue());
                            } else {
                                paramMap[this.id] = this.getDialog().addParam(objectNode, this.id, this.getValue());
                            }
                        },
                        setup: function (objectNode, embedNode, paramMap) {
                            this.setValue(paramMap[this.id]);
                        }
                    },
                    {
                        id: 'question',
                        type: 'textarea',
                        label: editor.lang.iudico.question,
                        labelLayout: 'horizontal',
                        commit: function (objectNode, paramMap, extraStyles, extraAttributes) {
                            if (paramMap[this.id]) {
                                paramMap[this.id].setAttribute("value", this.getValue());
                            } else {
                                paramMap[this.id] = this.getDialog().addParam(objectNode, this.id, this.getValue());
                            }
                        },
                        setup: function (objectNode, embedNode, paramMap) {
                            this.setValue(paramMap[this.id]);
                        }
                    },
                    {
                        id: 'preCode',
                        type: 'textarea',
                        label: editor.lang.iudico.preCode,
                        labelLayout: 'horizontal',
                        commit: function (objectNode, paramMap, extraStyles, extraAttributes) {
                            if (paramMap[this.id]) {
                                paramMap[this.id].setAttribute("value", this.getValue());
                            } else {
                                paramMap[this.id] = this.getDialog().addParam(objectNode, this.id, this.getValue());
                            }
                        },
                        setup: function (objectNode, embedNode, paramMap) {
                            this.setValue(paramMap[this.id]);
                        }
                    },
                    {
                        id: 'postCode',
                        type: 'textarea',
                        label: editor.lang.iudico.postCode,
                        labelLayout: 'horizontal',
                        commit: function (objectNode, paramMap, extraStyles, extraAttributes) {
                            if (paramMap[this.id]) {
                                paramMap[this.id].setAttribute("value", this.getValue());
                            } else {
                                paramMap[this.id] = this.getDialog().addParam(objectNode, this.id, this.getValue());
                            }
                        },
                        setup: function (objectNode, embedNode, paramMap) {
                            this.setValue(paramMap[this.id]);
                        }
                    }
				]
            },
            {
                id: 'testsTab',
                label: editor.lang.iudico.testsTab,
                title: editor.lang.iudico.testsTab,
                padding: 2,
                elements:
				[
                    {
                        type: 'hbox',
                        widths: ['200px', '100px'],
                        children:
                        [
                            {
                                type: 'vbox',
                                children: [
                                    {
                                        id: 'testInput',
                                        type: 'text',
                                        label: editor.lang.iudico.input,
                                        labelLayout: 'horizontal',
                                        setup: function () {
                                            this.setValue('');
                                        }
                                    },
                                    {
                                        id: 'testOutput',
                                        type: 'text',
                                        label: editor.lang.iudico.output,
                                        labelLayout: 'horizontal',
                                        setup: function () {
                                            this.setValue('');
                                        }
                                    }
                                ]
                            },
                            {
                                id: 'addTest',
                                type: 'button',
                                label: editor.lang.iudico.addTest,
                                onClick: function () {
                                    var dialog = this.getDialog(),
										tests = dialog.getContentElement('testsTab', 'cmbTests'),
                                        test = dialog.getContentElement('testsTab', 'test');

                                    addOption(tests, test.getValue(), test.getValue(), dialog.getParentEditor().document);

                                    test.setValue('');
                                }
                            }
                        ]
                    },

                    {
                        type: 'select',
                        id: 'cmbTests',
                        label: editor.lang.iudico.tests,
                        title: '',
                        size: 5,
                        style: 'width: 100%; height: 200px',
                        items: [],
                        commit: function (objectNode, paramMap, extraStyles, extraAttributes) {
                            for (var i = 0; paramMap['option' + i]; ++i) {
                                paramMap['option' + i].remove();
                                delete paramMap['option' + i];
                            }

                            var dialog = this.getDialog(),
										choices = dialog.getContentElement('choiceTab', 'cmbChoices'),
                                        choice = dialog.getContentElement('choiceTab', 'choice'),
                                        correctAnswer = dialog.getContentElement('choiceTab', 'correctAnswer');

                            choices = getSelect(choices);
                            correctAnswer = getSelect(correctAnswer);

                            for (var i = 0; i < choices.getChildCount(); ++i) {
                                paramMap['option' + i] = this.getDialog().addParam(objectNode, 'option' + i, choices.getChild(i).getText());
                            }
                        },
                        setup: function (objectNode, embedNode, paramMap) {
                            /*var dialog = this.getDialog(),
                            choices = dialog.getContentElement('choiceTab', 'cmbChoices'),
                            choice = dialog.getContentElement('choiceTab', 'choice'),
                            correctAnswer = dialog.getContentElement('choiceTab', 'correctAnswer');

                            removeAllOptions(choices);
                            removeAllOptions(correctAnswer);

                            for (var i = 0; paramMap['option' + i]; ++i) {
                            addOption(choices, paramMap['option' + i], paramMap['option' + i], dialog.getParentEditor().document);
                            var oOption = addOption(correctAnswer, paramMap['option' + i], paramMap['option' + i], dialog.getParentEditor().document);

                            if (paramMap['option' + i] == paramMap['correct']) {
                            oOption.setAttribute('selected', 'selected');
                            oOption.selected = true;
                            }
                            }*/
                        }
                    }
				]
            }

		],

        buttons: [CKEDITOR.dialog.okButton, CKEDITOR.dialog.cancelButton]
    };
});
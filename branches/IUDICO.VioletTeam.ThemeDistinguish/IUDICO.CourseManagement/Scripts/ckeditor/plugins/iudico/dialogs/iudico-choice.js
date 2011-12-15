CKEDITOR.dialog.add('iudico-choice', function (editor) {

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

    function removeAllOptions(combo) {
        combo = getSelect(combo);
        while (combo.getChild(0) && combo.getChild(0).remove())
        { /*jsl:pass*/ }
    }

    function setSelectedIndex(combo, index) {
        combo = getSelect(combo);
        if (index < 0)
            return null;
        var count = combo.getChildren().count();
        combo.$.selectedIndex = (index >= count) ? (count - 1) : index;
        return combo;
    }

    function getSelectedIndex(combo) {
        combo = getSelect(combo);
        return combo ? combo.$.selectedIndex : -1;
    }

    function getSelect(obj) {
        if (obj && obj.domId && obj.getInputElement().$)				// Dialog element.
            return obj.getInputElement();
        else if (obj && obj.$)
            return obj;
        return false;
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
                        id: 'rank',
                        type: 'text',
                        label: editor.lang.iudico.rank,
                        labelLayout: 'horizontal',
                        commit: function (objectNode, paramMap, extraStyles, extraAttributes) {
                            if (paramMap['rank']) {
                                paramMap['rank'].setAttribute("value", this.getValue());
                            } else {
                                paramMap['rank'] = this.getDialog().addParam(objectNode, 'rank', this.getValue());
                            }
                        },
                        setup: function (objectNode, embedNode, paramMap) {
                            this.setValue(paramMap['rank']);
                        }
                    },
                    {
                        id: 'correctAnswer',
                        type: 'select',
                        label: editor.lang.iudico.correctAnswer,
                        labelLayout: 'horizontal',
                        items: [],
                        commit: function (objectNode, paramMap, extraStyles, extraAttributes) {
                            if (paramMap['correct']) {
                                paramMap['correct'].setAttribute("value", this.getValue());
                            } else {
                                paramMap['correct'] = this.getDialog().addParam(objectNode, 'correct', this.getValue());
                            }
                        },
                        setup: function (objectNode, embedNode, paramMap) {
                            //this.setValue(paramMap['correct']);
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
                                    var dialog = this.getDialog(),
										choices = dialog.getContentElement('choiceTab', 'cmbChoices'),
                                        choice = dialog.getContentElement('choiceTab', 'choice'),
                                        correctAnswer = dialog.getContentElement('choiceTab', 'correctAnswer');

                                    if (choice.getValue() == '') {
                                        alert('Empty field!');
                                        return;
                                    }

                                    addOption(choices, choice.getValue(), choice.getValue(), dialog.getParentEditor().document);
                                    addOption(correctAnswer, choice.getValue(), choice.getValue(), dialog.getParentEditor().document);

                                    choice.setValue('');
                                }
                            },
                            {
                                id: 'choice',
                                type: 'text',
                                label: editor.lang.iudico.choice,
                                labelLayout: 'horizontal',
                                items: [],
                                setup: function () {
                                    this.setValue('');
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
                                    var dialog = this.getDialog(),
										choices = dialog.getContentElement('choiceTab', 'cmbChoices'),
                                        choice = dialog.getContentElement('choiceTab', 'choice'),
                                        correctAnswer = dialog.getContentElement('choiceTab', 'correctAnswer');

                                    index = getSelectedIndex(choices);

                                    removeOption(choices, index);
                                    removeOption(correctAnswer, index);
                                }
                            },
                            {
                                type: 'select',
                                id: 'cmbChoices',
                                label: '',
                                title: '',
                                size: 5,
                                style: 'width:115px;height:75px',
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

                                    if (paramMap['count']) {
                                        paramMap['count'].setAttribute("value", i);
                                    } else {
                                        paramMap['count'] = this.getDialog().addParam(objectNode, "count", i);
                                    }

                                    if (paramMap['multichoice']) {
                                        paramMap['multichoice'].setAttribute("value", "0");
                                    } else {
                                        paramMap['multichoice'] = this.getDialog().addParam(objectNode, "multichoice", "0");
                                    }
                                },
                                setup: function (objectNode, embedNode, paramMap) {
                                    var dialog = this.getDialog(),
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
                                    }
                                }
                            }
                        ]
                    }
				]

            }

		],

        buttons: [CKEDITOR.dialog.okButton, CKEDITOR.dialog.cancelButton]
    };
});
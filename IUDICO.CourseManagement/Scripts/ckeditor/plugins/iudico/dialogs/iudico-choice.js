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
				        type: 'checkbox',
				        id: 'singleCase',
				        label: editor.lang.iudico.singleCase,
				        'default': 'checked',
				        onClick: function () {
				            $('#choise-answers').multichoice('switchInputType', this.getValue() ? 'radio' : 'checkbox');
				        },
				        setup: function (objectNode, embedNode, paramMap) {
				            if (paramMap['multichoice']) {
				                this.setValue(paramMap['multichoice'] == 0);
				            }
				        }
				    },
				    {
				        id: 'answers',
				        type: 'html',
				        html: '<div id="choise-answers" style="width: 400px; height: 200px;"></div>',
				        setup: function (objectNode, embedNode, paramMap) {
				            var inputtype = 'radio';
				            if (paramMap['multichoice']) {
				                inputtype = paramMap['multichoice'] == 0 ? 'radio' : 'checkbox';
				            }

				            $('#choise-answers').multichoice({ inputType: inputtype });
				            $('#choise-answers').multichoice("clearItemList");

				            var correctAnswer = paramMap['correct'] ? paramMap['correct'] : '';
				            for (var i = 'A'; paramMap['option' + i]; i = String.fromCharCode(i.charCodeAt(0) + 1)) {
				                var selected = false;
				                if (correctAnswer.indexOf(i) != -1) {
				                    selected = true;
				                }

				                $('#choise-answers').multichoice("addItem", decodeURIComponent(paramMap['option' + i]), selected);
				            }

                            // support of the old style 
				            for (var i = 0; paramMap['option' + i]; ++i) {
				                var selected = false;
				                if (correctAnswer.indexOf(paramMap['option' + i]) != -1) {
				                    selected = true;
				                }

				                $('#choise-answers').multichoice("addItem", decodeURIComponent(paramMap['option' + i]), selected);
				            }
				        },
				        commit: function (objectNode, paramMap, extraStyles, extraAttributes) {
				            for (var i = 'A'; paramMap['option' + i]; i = String.fromCharCode(i.charCodeAt(0) + 1)) {
				                paramMap['option' + i].remove();
				                delete paramMap['option' + i];
				            }

				            var dialog = this.getDialog();
				            var checkbox = dialog.getContentElement('choiceTab', 'singleCase');
				            var items = $('#choise-answers').multichoice('getItems');
				            var correctAnswers = [];

				            for (var j = 0; j < items.length; ++j) {
				                paramMap['option' + String.fromCharCode(65 + j)] = dialog.addParam(objectNode, 'option' + String.fromCharCode(65 + j), items[j].item);
				                if (items[j].checked) {
				                    correctAnswers.push(String.fromCharCode(65 + j));
				                }
				            }

				            if (paramMap['count']) {
				                paramMap['count'].setAttribute("value", items.length);
				            } else {
				                paramMap['count'] = this.getDialog().addParam(objectNode, "count", items.length);
				            }

				            if (paramMap['multichoice']) {
				                paramMap['multichoice'].setAttribute("value", checkbox.getValue() ? '0' : '1');
				            } else {
				                paramMap['multichoice'] = this.getDialog().addParam(objectNode, "multichoice", checkbox.getValue() ? '1' : '0');
				            }

				            if (paramMap['correct']) {
				                paramMap['correct'].setAttribute("value", correctAnswers.join('[,]'));
				            } else {
				                paramMap['correct'] = this.getDialog().addParam(objectNode, 'correct', correctAnswers.join('[,]'));
				            }
				        }
				    }
				]

            }

		],

        buttons: [CKEDITOR.dialog.okButton, CKEDITOR.dialog.cancelButton]
    };
});
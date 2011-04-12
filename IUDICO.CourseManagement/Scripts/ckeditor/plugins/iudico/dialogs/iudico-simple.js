CKEDITOR.dialog.add('iudico-simple', function (editor) {
    //var lang = editor.lang.about;

    return {
        title: editor.lang.iudico.title,
        minWidth: 390,
        minHeight: 230,
        onShow: function () {
            // Clear previously saved elements.
            this.fakeDiv = this.objectNode = null;

            // Try to detect any embed or object tag that has Flash parameters.
            var fakeDiv = this.getSelectedElement();

            if (fakeDiv && fakeDiv.getAttribute('_cke_real_element_type') && fakeDiv.getAttribute('_cke_real_element_type') == 'iudico') {
                var realElement = editor.restoreRealElement(fakeDiv),
                    paramMap = {};

                if (realElement.getName() == 'cke:object' && realElement.getAttribute('iudico-type') && realElement.getAttribute('iudico-type') == 'simple') {
                    this.fakeDiv = fakeDiv;
                    this.objectNode = realElement;

                    var paramList = this.objectNode.getElementsByTag('param', 'cke');
                    for (var i = 0, length = paramList.count(); i < length; i++) {
                        var item = paramList.getItem(i),
							name = item.getAttribute('name'),
							value = item.getAttribute('value');

                        paramMap[name] = value;
                    }
                }

                this.setupContent(this.objectNode, this.fakeDiv, paramMap);
            }
        },
        onOk: function () {
            // If there's no selected object or embed, create one. Otherwise, reuse the
            // selected object and embed nodes.
            var objectNode = null,
				paramMap = null;

            if (!this.fakeDiv) {
                objectNode = CKEDITOR.dom.element.createFromHtml('<cke:object></cke:object>', editor.document);
                var attributes = {
                    'iudico-question': 'true',
                    'iudico-type': 'simple'
                };
                objectNode.setAttributes(attributes);
            } else {
                objectNode = this.objectNode;
                embedNode = this.embedNode;
            }

            // Produce the paramMap if there's an object tag.
            paramMap = {};
            var paramList = objectNode.getElementsByTag('param', 'cke');
            for (var i = 0, length = paramList.count(); i < length; i++)
                paramMap[paramList.getItem(i).getAttribute('name')] = paramList.getItem(i);

            // A subset of the specified attributes/styles
            // should also be applied on the fake element to
            // have better visual effect. (#5240)
            var extraStyles = { width: '100px', height: '100px' }, extraAttributes = {};
            this.commitContent(objectNode, paramMap, extraStyles, extraAttributes);

            //for (var name in paramMap) {
                //this.getDialog().addParam(objectNode, name, paramMap[name]);
            //}

            console.log(paramMap);

            // Refresh the fake image.
            var newFakeDiv = editor.createFakeElement(objectNode, 'cke_iudico', 'iudico', true);

            newFakeDiv.setAttributes(extraAttributes);
            newFakeDiv.setStyles(extraStyles);
            if (this.fakeDiv) {
                newFakeDiv.replace(this.fakeDiv);
                editor.getSelection().selectElement(newFakeDiv);
            } else {
                editor.insertElement(newFakeDiv);
            }
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
                            //paramMap['question'] = this.getValue();
                            paramMap['question'].setAttribute("value", value);
                            //this.getDialog().addParam(element, 'question', this.getValue());
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
                            paramMap['question'].setAttribute("value", value);
                            //this.getDialog().addParam(element, 'correctAnswer', this.getValue());
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
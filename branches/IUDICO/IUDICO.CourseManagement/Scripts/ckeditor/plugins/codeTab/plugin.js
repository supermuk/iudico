function isTabObject(element) {
    var attributes = element.attributes;
    return (attributes['id'] && attributes['id'].contains('tabs'));
}
CKEDITOR.dialog.prototype.saveTabObject = function (editor) {
    var objectNode = CKEDITOR.dom.element.createFromHtml('<div></div>', editor.document);

    this.commitContent(objectNode);

    // Refresh the fake image.
    var newFakeDiv = editor.createFakeElement(objectNode, 'iudico_tab', 'iudico', true);

    if (this.fakeDiv) {
        newFakeDiv.replace(this.fakeDiv);
        editor.getSelection().selectElement(newFakeDiv);
    } else {
        editor.insertElement(newFakeDiv);
    }
}
CKEDITOR.dialog.prototype.getCodeTabObject = function (editor) {
    // Clear previously saved elements.
    this.fakeDiv = this.objectNode = null;

    var fakeDiv = this.getSelectedElement();

    if (fakeDiv && fakeDiv.getAttribute('_cke_real_element_type') && fakeDiv.getAttribute('_cke_real_element_type') == 'iudico') {
        var realElement = editor.restoreRealElement(fakeDiv),
        paramMap = {};

        if (realElement.getName() == 'div' && realElement.getAttribute('id') && realElement.getAttribute('id').contains('tabs')) {
            this.fakeDiv = fakeDiv;
            this.objectNode = realElement;

            var divList = this.objectNode.getElementsByTag('div');
            var liList = this.objectNode.getElementsByTag('li');
            for (var i = 0, length = liList.count(); i < length; i++) {
                var item = liList.getItem(i);
                var idDiv = item.getElementsByTag('a').getItem(0).getAttribute('href').replace('#', ''),
                    title = item.getElementsByTag('a').getItem(0).getHtml(),
                    content;
                for (var index = 0; index < divList.count(); index++) {
                    if (divList.getItem(index).getAttribute('id') == idDiv) {
                        content = divList.getItem(index).getText();
                        break;
                    }
                }

                paramMap[title] = content;
            }

            this.setupContent(this.objectNode, this.fakeDiv, paramMap);

            return true;
        }
    }

    this.setupContent(null, null, {});

    return false;
}
CKEDITOR.plugins.add('codeTab',
{
    lang: ["en", "uk"],
    afterInit: function (editor) {
        // Register a filter to displaying placeholders after mode change.
        var dataProcessor = editor.dataProcessor,
        dataFilter = dataProcessor && dataProcessor.dataFilter;

        if (dataFilter) {
            dataFilter.addRules({
                elements: {
                    'div': function (element) {
                        if (!isTabObject(element))
                            return null;

                        return editor.createFakeParserElement(element, 'iudico_tab', 'iudico', true);
                    }
                }
            });
        }
    },
    init: function (editor) {
        editor.addCommand('codeTabDialog', new CKEDITOR.dialogCommand('codeTabDialog'));
        editor.ui.addButton('CodeTab',
		{
		    label: editor.lang.codeTab.addTabs,
		    command: 'codeTabDialog',
		    icon: this.path + 'images/icon.png'
		});
        editor.addCss(
		'.iudico_tab' +
		'{' +
			'background-image: url(' + CKEDITOR.getUrl(this.path + 'images/icon.png') + ');' +
			'background-position: center center;' +
			'background-repeat: no-repeat;' +
			'border: 1px solid #a9a9a9;' +
			'width: 16px !important;' +
			'height: 16px !important;' +
		'}');
        CKEDITOR.dialog.add('codeTabDialog', function (editor) {
            var toHtml = function (input) {
                return input.replace(/&/g, "&amp;")
            .replace(/</g, "&lt;")
            .replace(/>/g, "&gt;");
            };
            function getSelect(obj) {
                if (obj && obj.domId && obj.getInputElement().$)				// Dialog element.
                    return obj.getInputElement();
                else if (obj && obj.$)
                    return obj;
                return false;
            }
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
            function removeOption(combo, index) {
                combo = getSelect(combo);
                combo.getChild(index).remove();
            }
            function getSelectedIndex(combo) {
                combo = getSelect(combo);
                return combo ? combo.$.selectedIndex : -1;
            }
            return {
                title: editor.lang.codeTab.addTabs,
                minWidth: 400,
                minHeight: 200,
                onShow: function () {
                    this.options = [];
                    this.getCodeTabObject(editor);
                },
                contents:
				[
					{
					    id: 'general',
					    label: 'Settings',
					    elements:
						[
							{
							    type: 'html',
							    html: editor.lang.codeTab.description
							},
							{
							    type: 'text',
							    id: 'title',
							    label: editor.lang.codeTab.txtTitle,
							    commit: function (data) {
							        data.url = this.getValue();
							    }
							},
							{
							    type: 'textarea',
							    id: 'contents',
							    label: editor.lang.codeTab.txtContent,
							    commit: function (data) {
							        data.contents = this.getValue();
							    }
							},
                            {
                                type: 'hbox',
                                widths: ['300px', '0px'],
                                children:
                                [
                                    {
                                        id: 'addTab',
                                        type: 'button',
                                        label: editor.lang.codeTab.addTab,
                                        onClick: function () {
                                            var dialog = this.getDialog(),
										    tabsElem = dialog.getContentElement('general', 'cmbTabs'),
                                            title = dialog.getContentElement('general', 'title'),
                                            contents = dialog.getContentElement('general', 'contents');

                                            if (title.getValue() == '' || contents.getValue() == '') {
                                                alert('Empty fields!');
                                                return;
                                            }

                                            dialog.options.push([title.getValue(), contents.getValue()]);
                                            addOption(tabsElem, title.getValue(), title.getValue(), dialog.getParentEditor().document);

                                            title.setValue('');
                                            contents.setValue('');
                                        }
                                    },
                                    {
                                        id: 'deleteTest',
                                        type: 'button',
                                        label: editor.lang.codeTab.deleteTab,
                                        onClick: function () {
                                            var dialog = this.getDialog(),
										    tabsElem = dialog.getContentElement('general', 'cmbTabs');
                                            index = getSelectedIndex(tabsElem);
                                            dialog.options.splice(index, 1);
                                            removeOption(tabsElem, index);
                                        }
                                    }
                                ]
                            },
                            {
                                type: 'select',
                                id: 'cmbTabs',
                                label: editor.lang.iudico.tests,
                                title: '',
                                size: 5,
                                style: 'width: 100%; height: 80px',
                                items: [],
                                commit: function (data) {
                                    var dialog = this.getDialog(),
                                        tabsArray = [],
                                        numberRandom = Math.floor((Math.random() * 100) + 1);
                                        ul = editor.document.createElement('ul'),
                                        link = editor.document.createElement('a');
                                    for (var i = 0; i < dialog.options.length; i++) {
                                        var li = editor.document.createElement('li');
                                        var a = editor.document.createElement('a');
                                        a.setAttribute('href', '#fragment-' + numberRandom + (i + 1).toString());
                                        a.setText(dialog.options[i][0]);
                                        li.append(a);
                                        ul.append(li);

                                        var div = editor.document.createElement('div');
                                        div.setAttribute('id', 'fragment-' + numberRandom + (i + 1).toString());
                                        var el = document.createElement("pre");
                                        el.innerHTML = toHtml(dialog.options[i][1]);
                                        if (dialog.options[i][0] == 'C#') {
                                            sh_highlightElement(el, sh_languages['csharp']);
                                        }
                                        else if (dialog.options[i][0] == 'C++') {
                                            sh_highlightElement(el, sh_languages['cpp']);
                                        }
                                        else if (dialog.options[i][0] == 'Java') {
                                            sh_highlightElement(el, sh_languages['java']);
                                        }
                                        else if (dialog.options[i][0] == 'XML/XHTML') {
                                            sh_highlightElement(el, sh_languages['xml']);
                                        }
                                        div.appendHtml(el.outerHTML);
                                        tabsArray.push(div);
                                    }
                                    data.append(ul);
                                    for (var i = 0; i < tabsArray.length; i++) {
                                        data.append(tabsArray[i]);
                                    }
                                    data.setAttribute('id', 'tabs-' + numberRandom);
                                },
                                setup: function (objectNode, embedNode, paramMap) {
                                    var dialog = this.getDialog(),
								    tabs = dialog.getContentElement('general', 'cmbTabs'),
                                    tabs = getSelect(tabs);

                                    while (tabs.getChild(0) && tabs.getChild(0).remove())
                                    { /*jsl:pass*/ }

                                    for (item in paramMap) {
                                        dialog.options.push([item, paramMap[item]]);
                                        addOption(tabs, item, item, dialog.getParentEditor().document);
                                    }
                                }
                            }
						]
					}
				],
                onOk: function () {
                    this.saveTabObject(editor);
                }
            };
        });
    }
});
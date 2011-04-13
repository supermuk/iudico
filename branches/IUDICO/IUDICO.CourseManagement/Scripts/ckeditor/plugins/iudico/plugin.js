/*
Copyright (c) 2010, IUDICO. All rights reserved.
*/

/**
* @file IUDICO plugin
*/


CKEDITOR.dialog.prototype.addParam = function (element, name, value) {
    var paramNode = CKEDITOR.dom.element.createFromHtml('<cke:param></cke:param>', element.getDocument());

    paramNode.setAttribute("name", name);
    paramNode.setAttribute("value", value);

    element.append(paramNode);

    return paramNode;
};

CKEDITOR.dialog.prototype.getIudicoObject = function (editor) {
    // Clear previously saved elements.
    this.fakeDiv = this.objectNode = null;

    // Try to detect any embed or object tag that has Flash parameters.
    var fakeDiv = this.getSelectedElement();

    if (fakeDiv && fakeDiv.getAttribute('_cke_real_element_type') && fakeDiv.getAttribute('_cke_real_element_type') == 'iudico') {
        var realElement = editor.restoreRealElement(fakeDiv),
        paramMap = {};

        if (realElement.getName() == 'cke:object' && realElement.getAttribute('iudico-type') && realElement.getAttribute('iudico-type') == this.getName()) {
            this.fakeDiv = fakeDiv;
            this.objectNode = realElement;

            var paramList = this.objectNode.getElementsByTag('param', 'cke');
            for (var i = 0, length = paramList.count(); i < length; i++) {
                var item = paramList.getItem(i),
							name = item.getAttribute('name'),
							value = item.getAttribute('value');

                paramMap[name] = value;
            }

            this.setupContent(this.objectNode, this.fakeDiv, paramMap);

            return true;
        }
    }

    return false;
}

CKEDITOR.dialog.prototype.saveIudicoObject = function (editor) {
    // If there's no selected object or embed, create one. Otherwise, reuse the
    // selected object and embed nodes.
    var objectNode = null,
		paramMap = {};

    if (!this.fakeDiv) {
        objectNode = CKEDITOR.dom.element.createFromHtml('<cke:object></cke:object>', editor.document);
        var attributes = {
            'iudico-question': 'true',
            'iudico-type': this.getName()
        };
        objectNode.setAttributes(attributes);
    } else {
        objectNode = this.objectNode;
    }

    // Produce the paramMap if there's an object tag.
    var paramList = objectNode.getElementsByTag('param', 'cke');
    for (var i = 0, length = paramList.count(); i < length; i++)
        paramMap[paramList.getItem(i).getAttribute('name')] = paramList.getItem(i);

    var extraStyles = { width: '100px', height: '100px' },
        extraAttributes = {};
    
    this.commitContent(objectNode, paramMap, extraStyles, extraAttributes);

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
}

var numberRegex = /^\d+(?:\.\d+)?$/;

function cssifyLength(length) {
    if (numberRegex.test(length))
        return length + 'px';
    return length;
}

function addQuestionCommand(editor, command, path) {
    var dialogFile = path + 'dialogs/' + command + '.js';

    CKEDITOR.dialog.add(command, dialogFile);

    editor.addCommand(command, {
        exec: function (editor) {
            CKEDITOR.plugins.iudicoCmd(editor, command);
        }
    });

    editor.ui.addButton(command, {
        label: editor.lang.iudico[command],
        icon: path + 'logo_iudico.png',
        command: command
    });

    CKEDITOR.dialog.add(command, dialogFile);
};

function createFakeElement(editor, realElement) {
    var fakeElement = editor.createFakeParserElement(realElement, 'cke_iudico', 'iudico', true),
		fakeStyle = fakeElement.attributes.style || '';

    var width = realElement.attributes.width,
		height = realElement.attributes.height;

    if (typeof width != 'undefined')
        fakeStyle = fakeElement.attributes.style = fakeStyle + 'width:' + cssifyLength(width) + ';';

    if (typeof height != 'undefined')
        fakeStyle = fakeElement.attributes.style = fakeStyle + 'height:' + cssifyLength(height) + ';';

    return fakeElement;
}

function isIudicoObject(element) {
    var attributes = element.attributes;

    return (attributes['iudico-type']);
}

CKEDITOR.plugins.add('iudico', {
    availableLangs: { en: 1 },
    requires: ['dialog', 'fakeobjects'],
    lang: ['en'],

    init: function (editor) {
        var pluginName = 'iudico';
        var lang = editor.lang.iudico;
        var commands = ['iudico-simple', 'iudico-choice', 'iudico-compile'];

        editor.addCss(
		'.cke_iudico' +
		'{' +
			'background-image: url(' + CKEDITOR.getUrl(this.path + 'logo_iudico.png') + ');' +
			'background-position: center center;' +
			'background-repeat: no-repeat;' +
			'border: 1px solid #a9a9a9;' +
			'width: 16px !important;' +
			'height: 16px !important;' +
		'}');

        for (var i in commands) {
            addQuestionCommand(editor, commands[i], this.path);
        }
    },

    afterInit: function (editor) {
        // Register a filter to displaying placeholders after mode change.
        var dataProcessor = editor.dataProcessor,
        dataFilter = dataProcessor && dataProcessor.dataFilter;

        if (dataFilter) {
            dataFilter.addRules({
                elements: {
                    'cke:object': function (element) {
                        if (!isIudicoObject(element))
                            return null;

                        return createFakeElement(editor, element);
                    }
                }
            });
        }
    }
});

CKEDITOR.plugins.iudicoCmd = function (editor, command) {


    editor.openDialog(command);

        // Create the element that represents a print break.
        /*var label = editor.lang.pagebreakAlt;
        var breakObject = CKEDITOR.dom.element.createFromHtml('<div style="page-break-after: always;"><span style="display: none;">&nbsp;</span></div>');

        // Creates the fake image used for this element.
        breakObject = editor.createFakeElement(breakObject, 'cke_pagebreak', 'div');
        breakObject.setAttribute('alt', label);
        breakObject.setAttribute('aria-label', label);

        var ranges = editor.getSelection().getRanges(true);

        editor.fire('saveSnapshot');

        for (var range, i = ranges.length - 1; i >= 0; i--) {
            range = ranges[i];

            if (i < ranges.length - 1)
                breakObject = breakObject.clone(true);

            range.splitBlock('p');
            range.insertNode(breakObject);
            if (i == ranges.length - 1) {
                range.moveToPosition(breakObject, CKEDITOR.POSITION_AFTER_END);
                range.select();
            }

            var previous = breakObject.getPrevious();

            if (previous && CKEDITOR.dtd[previous.getName()].div)
                breakObject.move(previous);
        }

        editor.fire('saveSnapshot');*/
};

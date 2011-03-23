/*
Copyright (c) 2010, IUDICO. All rights reserved.
*/

/**
* @file IUDICO plugin
*/


CKEDITOR.dialog.prototype.addParam = function (element, name, value) {
    var documentObject = element.getDocument(),
        paramNode = documentObject.createElement("param");

    paramNode.setAttribute("name", name);
    paramNode.setAttribute("value", value);

    element.append(paramNode);
};
/*
CKEDITOR.dialog.prototype.getObject = function (editor) {
    var path = new CKEDITOR.dom.elementPath(editor.getSelection().getStartElement()),
        blockLimit = path.blockLimit,
        obj = blockLimit && blockLimit.getAscendant('object', true) && blockLimit.getAttribute('iudico') == true && blockLimit.getAttribute('type') == 'simple';

    return obj;
}
*/
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

function isIudicoObject(element) {
    var attributes = element.attributes;

    return (attributes['iudico-type']);
}

function createFakeElement(editor, realElement) {
    var fakeElement = editor.createFakeParserElement(realElement, 'cke_iudico', 'iudico', true),
		fakeStyle = fakeElement.attributes.style || '';

    var width = realElement.attributes.width,
		height = realElement.attributes.height;

    console.log(width);

    if (typeof width != 'undefined')
        fakeStyle = fakeElement.attributes.style = fakeStyle + 'width:' + cssifyLength(width) + ';';

    if (typeof height != 'undefined')
        fakeStyle = fakeElement.attributes.style = fakeStyle + 'height:' + cssifyLength(height) + ';';

    return fakeElement;
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
                        console.log(element);

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

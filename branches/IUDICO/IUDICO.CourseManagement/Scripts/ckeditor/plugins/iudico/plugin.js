/*
Copyright (c) 2010, IUDICO. All rights reserved.
*/

/**
 * @file IUDICO plugin
 */

(function () {

    CKEDITOR.dialog.prototype.addParam = function (element, name, value) {
        var documentObject = element.getDocument(),
            paramNode = documentObject.createElement("param");

        paramNode.setAttribute("name", name);
        paramNode.setAttribute("value", value);

        element.append(paramNode);
    };

    CKEDITOR.dialog.prototype.getObject = function (editor) {
        var path = new CKEDITOR.dom.elementPath(editor.getSelection().getStartElement()),
            blockLimit = path.blockLimit,
            obj = blockLimit && blockLimit.getAscendant('object', true) && blockLimit.getAttribute('iudico') == true && blockLimit.getAttribute('type') == 'simple';

        return obj;
    }

    CKEDITOR.plugins.addQuestionCommand = function (commandName) {
        dialogFile = this.path + 'dialogs/' + commandName + '.js';

        editor.addCommand(commandName, new CKEDITOR.dialogCommand(commandName));

        editor.ui.addButton(commandName,
		{
			label: lang.iudico[commandName],
			command: commandName
		});

        CKEDITOR.dialog.add(commandName, dialogFile);
    };

    CKEDITOR.plugins.add('iudico',
    {
        availableLangs: { en: 1 },
        requires: ['dialog', 'fakeobjects'],
        lang: ['en'],

        init: function (editor) {
            var pluginName = 'iudico';
            var lang = editor.lang.iudico;

            console.log('iudico - init');

            editor.addCss(
			'.iudico-question' +
			'{' +
				'background-image: url(' + CKEDITOR.getUrl(this.path + 'images/logo_iudico.png') + ');' +
				'background-position: center center;' +
				'background-repeat: no-repeat;' +
				'border: 1px solid #a9a9a9;' +
				'width: 16px !important;' +
				'height: 16px !important;' +
			'}');


            var dialogPath = this.path + 'dialogs/';
            /*this.addButtonCommand('iudico-simple');
            this.addButtonCommand('iudico-choice');
            this.addButtonCommand('iudico-compile');*/

            /*
            // Register simple iudico question
            CKEDITOR.dialog.add('iudico-simple', this.path + 'dialogs/iudico-simple.js' );
            editor.addCommand('iudico-simple', new CKEDITOR.dialogCommand('iudico-simple'));
            editor.ui.addButton('iudico-simple',
            {
            label : editor.lang.iudico.toolbar,
            icon : this.path + 'logo_iudico.png',
            command: 'iudico-simple'
            });

            // Register choice iudico question
            CKEDITOR.dialog.add('iudico-choice', this.path + 'dialogs/iudico-choice.js');
            editor.addCommand('iudico-choice', new CKEDITOR.dialogCommand('iudico-choice'));
            editor.ui.addButton('iudico-choice',
            {
            label: editor.lang.iudico.toolbar,
            icon: this.path + 'logo_iudico.png',
            command: 'iudico-choice'
            });

            // Register compile iudico question
            CKEDITOR.dialog.add('iudico-compile', this.path + 'dialogs/iudico-compile.js');
            editor.addCommand('iudico-compile', new CKEDITOR.dialogCommand('iudico-compile'));
            editor.ui.addButton('iudico-compile',
            {
            label: editor.lang.iudico.toolbar,
            icon: this.path + 'logo_iudico.png',
            command: 'iudico-compile'
            });
            */
        },

        afterInit : function( editor ) {
		// Register a filter to displaying placeholders after mode change.

		var dataProcessor = editor.dataProcessor,
			dataFilter = dataProcessor && dataProcessor.dataFilter;

		if ( dataFilter ) {
			dataFilter.addRules({
				elements : {
					div : function( element ) {
						/*var attributes = element.attributes;
                            cssClass = element.attributes.class;
							//style = attributes && attributes.style;
							//child = style && element.children.length == 1 && element.children[ 0 ],
							//childStyle = child && ( child.name == 'span' ) && child.attributes.style;
                            */
                        if ( element.className == 'iudico-question' ) {
                            var fakeDiv 

                        }

						/*if ( childStyle && ( /page-break-after\s*:\s*always/i ).test( style ) && ( /display\s*:\s*none/i ).test( childStyle ) ) {
							var fakeImg = editor.createFakeParserElement( element, 'cke_pagebreak', 'div' );
							var label = editor.lang.pagebreakAlt;
							fakeImg.attributes[ 'alt' ] = label;
							fakeImg.attributes[ 'aria-label' ] = label;
							return fakeImg;
						}*/
					}
				}
			});
		}
    });

})();
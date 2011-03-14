/*
Copyright (c) 2010, IUDICO. All rights reserved.
*/

/**
 * @file IUDICO plugin
 */

(function() {

CKEDITOR.plugins.add( 'iudico',
{
    availableLangs : { en : 1 },
    requires : [ 'dialog' ],
    lang : ['en'],
        
    init : function (editor)
    {
        var pluginName = 'iudico';
        var lang = editor.lang.iudico;

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
    }
});

})();
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

        // Register the dialog.
		CKEDITOR.dialog.add( pluginName, this.path + 'dialogs/iudico.js' );

		// Register the command.
		editor.addCommand( pluginName, new CKEDITOR.dialogCommand( pluginName ) );

		// Register the toolbar button.
		editor.ui.addButton( pluginName,
		{
			label : editor.lang.iudico.toolbar,
            icon : this.path + 'logo_iudico.png',
			command : pluginName
		});
    }
});

})();
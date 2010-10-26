CKEDITOR.plugins.add('iudico-question',
{
    requires : [ 'dialog' ],
    init: function (editor) {
        var pluginName = 'iudico';
                
        CKEDITOR.dialog.add(pluginName, this.path + 'dialogs/iudico.js'); 
        
        var command = editor.addCommand(pluginName, new CKEDITOR.dialogCommand(pluginName));
        command.modes = { wysiwyg:1, source:1 };

        editor.ui.addButton('iudico', 
        {
            label: 'IUDICO',
            command: pluginName,
        });
    }
});
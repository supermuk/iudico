/**
 * @file Code plugin.
 */

(function()
{
var pluginName = 'spoiler';

CKEDITOR.plugins.add( pluginName,
{
init : function( editor )
{
editor.addCommand( pluginName,new CKEDITOR.dialogCommand( 'spoiler' ));

CKEDITOR.dialog.add( pluginName, this.path + 'dialogs/spoiler.js' );

editor.ui.addButton( 'Spoiler',
{
label : 'Add Spoiler',
command : pluginName,
icon : this.path + 'logo.gif'
});
}
});
})();
/**
 * @file Code plugin.
 */

(function()
{
var pluginName = 'spoiler';

CKEDITOR.plugins.add( pluginName,
{
lang: ["en", "uk"],    
init : function( editor )
{
editor.addCommand( pluginName,new CKEDITOR.dialogCommand( 'spoiler' ));

CKEDITOR.dialog.add( pluginName, this.path + 'dialogs/spoiler.js' );

editor.ui.addButton( 'Spoiler',
{
label : editor.lang.spoiler.SpoilerButtonTitle,
command : pluginName,
icon : this.path + 'logo.gif'
});
}
});
})();
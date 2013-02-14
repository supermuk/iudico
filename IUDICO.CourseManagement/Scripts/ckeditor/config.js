 /*
Copyright (c) 2003-2010, CKSource - Frederico Knabben. All rights reserved.
For licensing, see LICENSE.html or http://ckeditor.com/license
*/

var compileServiceString = "http://server1.abtollc.com:54332/CompileService.asmx/Compile";

CKEDITOR.editorConfig = function (config) {
    // Define changes to default configuration here. For example:
    // config.language = 'fr';
    // config.uiColor = '#AADC6E';

    config.extraPlugins = 'iudico,save,syntaxhighlight,spoiler,switchtoolbar,codeTab';
    config.font_names =
            'Arial/Arial, Helvetica, sans-serif;' +
            'Comic Sans MS/Comic Sans MS, cursive;' +
            'Courier New/Courier New, Courier, monospace;' +
            'Georgia/Georgia, serif;' +
            'Lucida Sans Unicode/Lucida Sans Unicode, Lucida Grande, sans-serif;' +
            'Tahoma/Tahoma, Geneva, sans-serif;' +
            'Times New Roman/Times New Roman, Times, serif;' +
            'Trebuchet MS/Trebuchet MS, Helvetica, sans-serif;' +
            'Calibri/Calibri, Verdana, Geneva, sans-serif;' + /* font added on demand */
            'Verdana/Verdana, Geneva, sans-serif';

    CKEDITOR.config.toolbar_IUDICO =
    [
	    ['Source', '-', 'Save', 'NewPage', 'Preview', '-', 'Templates'],
	    ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Print', 'SpellChecker', 'Scayt'],
	    ['Undo', 'Redo', '-', 'Find', 'Replace', '-', 'SelectAll', 'RemoveFormat'],
	    ['Form', 'Checkbox', 'Radio', 'TextField', 'Textarea', 'Select', 'Button', 'ImageButton', 'HiddenField'],
	    '/',
	    ['Bold', 'Italic', 'Underline', 'Strike', '-', 'Subscript', 'Superscript'],
	    ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', 'Blockquote', 'CreateDiv'],
	    ['JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'],
	    ['BidiLtr', 'BidiRtl'],
	    ['Link', 'Unlink', 'Anchor'],
	    ['Image', 'Flash', 'Table', 'HorizontalRule', 'Smiley', 'SpecialChar', 'PageBreak'],
	    '/',
	    ['Styles', 'Format', 'Font', 'FontSize'],
	    ['TextColor', 'BGColor'],
	    ['Maximize', 'ShowBlocks', '-', 'About'],
        ['iudico-simple', 'iudico-choice', 'iudico-compile'],
        ['Code', 'CodeTab', 'Spoiler', '-', 'SwitchToolbar']
    ];

    CKEDITOR.config.toolbar_IUDICOSmall =
    [
	    ['Source', '-', 'Save', '-', 'Undo', 'Redo', ],
	    ['Bold', 'Italic', 'Underline', 'Strike', '-', 'Subscript', 'Superscript'],
	    ['NumberedList', 'BulletedList'],
	    ['JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'],
	    ['Image', 'Flash', 'Table'],
	    '/',
	    ['Styles', 'Format', 'Font', 'FontSize'],
        ['iudico-simple', 'iudico-choice', 'iudico-compile'],
        ['Code', 'Spoiler', '-', 'SwitchToolbar']
    ];

    config.toolbar = 'IUDICOSmall';
    config.switchToToolbar = 'IUDICO';
    config.fullPage = true;
};

CKEDITOR.on('instanceReady', function (ev) {
    ev.editor.dataProcessor.writer.setRules('pre',
         {
             indent: false,
             breakBeforeOpen: true,
             breakAfterOpen: false,
             breakBeforeClose: false,
             breakAfterClose: true
         });
});
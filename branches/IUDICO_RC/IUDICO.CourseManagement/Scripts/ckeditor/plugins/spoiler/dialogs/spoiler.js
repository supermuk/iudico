CKEDITOR.dialog.add('spoiler', function (editor) {
    return {
        title: editor.lang.spoiler.SpoilerTitle,
        minWidth: 250,
        minHeight: 70,
        contents: [{
            id: 'tab1',
            label: editor.lang.spoiler.SpoilerContentsTitle,
            title: editor.lang.spoiler.SpoilerContentsTitle,
            elements: [{
                id: 'input1',
                type: 'text',
                style: 'width:140px;',
                label: editor.lang.spoiler.SpoilerElementsLabel,
                validate: function ()

                {
                    CKEDITOR.config.text_val = this.getValue();
                    if (!this.getValue()) {
                        alert(editor.lang.spoiler.SpoilerWarning);
                        return false;
                    }

                    var fragment = editor.getSelection().getRanges()[0].extractContents();
                    var container = CKEDITOR.dom.element.createFromHtml("<span />", editor.document);

                    fragment.appendTo(container);

                    var element = editor.document.createElement('div');
                    element.setAttribute('style', 'margin:4px 0px 4px 0px');
                    element.setHtml("<input onclick=\"if(this.parentNode.getElementsByTagName('div')[0].style.display != '') { this.parentNode.getElementsByTagName('div')[0].style.display = ''; } else { this.parentNode.getElementsByTagName('div')[0].style.display = 'none'; }\" type=\"button\" value=" + CKEDITOR.config.text_val + " />");
                    var element2 = editor.document.createElement('div');
                    element2.setAttribute('class', 'spoiler');
                    element2.setAttribute('style', 'display: block;		background: none repeat scroll 0 0 #f4f4f4;		border-top: 1px solid #ccc;		line-height: 17px;		padding: 3px 3px 3px 7px;       color: #343434;');

                    container.appendTo(element2);
                    element2.appendTo(element);
                    editor.insertElement(element);



                    CKEDITOR.ENTER_BR;
                    return true;
                }

            }

            ]
        }]
    };
});
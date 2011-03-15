CKEDITOR.dialog.add('iudico-compile', function (editor) {
    //var lang = editor.lang.about;

    function addParam(element, name, value) {
        documentObject = element.getDocument();
        paramNode = documentObject.createElement("param");

        paramNode.setAttribute("name", name);
        paramNode.setAttribute("value", value);

        element.append(paramNode);
    }

    function getObject(editor) {
        path = new CKEDITOR.dom.elementPath(editor.getSelection().getStartElement());
        blockLimit = path.blockLimit;
        obj = blockLimit && blockLimit.getAscendant('object', true) && blockLimit.getAttribute('iudico') == true && blockLimit.getAttribute('type') == 'simple';

        return obj;
    }

    return {
        title: editor.lang.iudico.title,
        minWidth: 390,
        minHeight: 230,
        onShow: function () {
            delete this.object;

            var element = this.getParentEditor().getSelection().getStartElement();
            var object = element && element.getAscendant('object', true);

            if (object && object.getAttribute('iudico') == true && object.getAttribute('type') == 'simple') {
                this.object = object;
                this.setupContent(object);
            }
        },
        onOk: function () {
            //editor =this.editor;
            element = this.object;
            isInsertMode = !element;

            if (isInsertMode) {
                element = editor.document.createElement('object');
                element.setAttribute('iudico', 'true');
                element.setAttribute('type', 'simple');
                element.setAttribute('class', 'iudico');
            }

            this.commitContent(objectNode);


            if (isInsertMode) {
                editor.insertElement(element);
            }
        },
        onCancel: function () { },
        onLoad: function (data) { },
        contents: [
            {
                id: 'compileTab',
                label: editor.lang.iudico.compileTab,
                title: editor.lang.iudico.compileTab,
                expand: true,
                padding: 0,
                elements:
				[

				]
            }
		],

        buttons: [CKEDITOR.dialog.okButton, CKEDITOR.dialog.cancelButton]
    };
});
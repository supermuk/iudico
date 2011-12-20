CKEDITOR.dialog.add("syntaxhighlight", function (editor) {
    var toHtml = function (input) {
        return input.replace(/&/g, "&amp;")
            .replace(/</g, "&lt;")
            .replace(/>/g, "&gt;");
    };
    var dialog;
    return {
        title: editor.lang.syntaxhighlight.title,
        minWidth: 500,
        minHeight: 400,
        onShow: function () {
            var sel = editor.getSelection(),
						element = sel.getStartElement();
            if (element)
                element = element.getAscendant('pre', true);


            if (!element || element.getName() != 'pre') {
                element = editor.document.createElement('pre');
                this.insertMode = true;
            }
            else
                this.insertMode = false;

            this.element = element;
            dialog = this;

            this.setupContent(this.element);
        },
        onOk: function () {
            var pre = this.element;

            if (this.insertMode)
                editor.insertElement(pre);
            this.commitContent(pre);
        },
        contents: [{
            id: "source",
            label: editor.lang.syntaxhighlight.sourceTab,
            elements: [{
                type: "vbox",
                children: [{
                    id: "lang",
                    type: "select",
                    labelLayout: "horizontal",
                    label: editor.lang.syntaxhighlight.langLbl,
                    "default": "csharp",
                    widths: ["25%", "75%"],
                    items: [
                        ["C#", "csharp"],
                        ["C++", "cpp"],
                        ["Java", "java"],
                        ["XML/XHTML", "xml"]
                    ],
                    setup: function (element) {
                        this.setValue(element.getAttribute("lang"));
                    },
                    commit: function (element) {
                        element.setAttribute("lang", this.getValue());
                    }
                }]
            }, {
                type: "textarea",
                id: "code",
                rows: 22,
                style: "width: 100%",
                setup: function (element) {
                    this.setValue(element.getText());
                },
                commit: function (element) {
                    var el = document.createElement("pre");
                    el.innerHTML = toHtml(this.getValue());
                    var lang = dialog.getValueOf('source', 'lang');
                    sh_highlightElement(el, sh_languages[lang]);
                    element.setText("");
                    element.appendHtml(el.innerHTML);
                }
            }]
        }]
    }
});
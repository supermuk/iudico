CKEDITOR.plugins.add("syntaxhighlight", {
    requires: ["dialog"],
    lang: ["en","uk"],
    init: function (a) {
        var b = "syntaxhighlight";
        var c = a.addCommand(b, new CKEDITOR.dialogCommand(b));
        c.modes = {
            wysiwyg: 1,
            source: 1
        };
        c.canUndo = false;
        a.ui.addButton("Code", {
            label: a.lang.syntaxhighlight.title,
            command: b,
            icon: this.path + "images/syntaxhighlight.gif"
        });
        CKEDITOR.scriptLoader.load(CKEDITOR.getUrl(this.path + "scripts/sh_main.js"));
        CKEDITOR.scriptLoader.load(CKEDITOR.getUrl(this.path + "scripts/sh_style.js"));
        CKEDITOR.scriptLoader.load(CKEDITOR.getUrl(this.path + "scripts/sh_csharp.min.js"));
        CKEDITOR.scriptLoader.load(CKEDITOR.getUrl(this.path + "scripts/sh_cpp.min.js"));
        CKEDITOR.scriptLoader.load(CKEDITOR.getUrl(this.path + "scripts/sh_java.min.js"));
        CKEDITOR.scriptLoader.load(CKEDITOR.getUrl(this.path + "scripts/sh_xml.min.js"));

        CKEDITOR.dialog.add(b, this.path + "dialogs/syntaxhighlight.js");
    }
});
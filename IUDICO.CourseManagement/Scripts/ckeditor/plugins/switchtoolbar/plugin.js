CKEDITOR.plugins.add("switchtoolbar", {
    lang: ["en", "uk"],
    init: function (editor) {
        var name = "switchtoolbar";

        editor.addCommand(name, {
            exec: function (editor) {
                if (!this.oldToolbar)
                    this.oldToolbar = editor.config.toolbar;

                if (editor.config.toolbar === editor.config.switchToToolbar) {
                    editor.setToolbar(this.oldToolbar);
                }
                else {
                    this.oldToolbar = editor.config.toolbar;
                    editor.setToolbar(editor.config.switchToToolbar);
                }
            }
        });

        editor.ui.addButton("SwitchToolbar", {
            label: editor.lang.switchtoolbar.title,
            command: name,
            icon: this.path + "switchtoolbar.gif"
        });
    }
});
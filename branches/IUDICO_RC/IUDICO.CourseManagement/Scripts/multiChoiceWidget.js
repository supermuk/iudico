(function ($) {

    $.widget("ui.multichoice", {
        options: {
            inputType: "checkbox"
        },
        _create: function () {
            this.element.addClass("ui-multichoice ui-widget ui-widget-content");

            this.selectedList = $('<ul class="ui-helper-reset"></ul>')
		.appendTo(this.element);
            this.selectedList.sortable({ revert: true, axis: 'y' });

            var inputBox = $('<div class="inputbox"><div class="cke_dialog_ui_input_text"><input type="text" name="newanswer"></div></div>')
                .css("width", this.element.width()).css("box-sizing", "border-box").prependTo(this.element);
            inputBox.css("width", 2 * this.element.width() - inputBox.outerWidth());
            var plus = $('<a href="#" class="actionicon"><span class="ui-corner-all ui-icon ui-icon-plus"/></a></li>').appendTo(inputBox);
            var that = this;
            plus.click(function () {
                that._addNewItemFromInput(that);
            });
            inputBox.keypress(function (e) {
                if (e.which == 13) {  // on enter
                    that._addNewItemFromInput(that);
                }
            });

            this.selectedList.css("height", this.element.height() - inputBox.outerHeight());
        },
        destroy: function () {
        },
        addItem: function (item, selected) {
            if (item) {
                var li = $('<li class="ui-state-default" title="' + item + '"></li>').appendTo(this.selectedList);
                var input = $('<input type="' + this.options.inputType + '" class="multichoice-inputs" name="answer" value="' + encodeURIComponent(item) + '"/>').appendTo(li);
                if (selected) {
                    input.attr('checked', 'checked');
                }
                li.append(item);
                var minus = $('<a href="#" class="actionicon"><span class="ui-corner-all ui-icon ui-icon-minus"/></a></li>').appendTo(li);
                this._registerRemoveEvents(minus);
            }
        },
        _addNewItemFromInput: function (that) {
            var input = $('input[name="newanswer"]');
            that.addItem(input.val());
            input.val('');
        },
        _registerRemoveEvents: function (elements) {
            elements.click(function () {
                $(this).parent().detach();
            });
        },
        switchInputType: function (type) {
            if (!type) {
                type = this.options.inputType == "radio" ? "checkbox" : "radio";
            }
            this.options.inputType = type;
            $('.ui-multichoice .multichoice-inputs').each(
				function () {
				    var parent = $(this).parent();
				    $(this).detach().attr("type", type).prependTo(parent);
				});
        },
        _setOption: function (key, value) {
            switch (key) {
                case "inputType":
                    this.switchInputType(value);
                    break;
            }
        },
        clearItemList: function () {
            this.selectedList.html('');
        },
        getItems: function () {
            var items = [];
            this.selectedList.find('li').each(function () {
                var input = $(this).find('input');
                items.push({ item: input.val(), checked: !!input.attr('checked') });
            });

            return items;
        }
    });


    $.extend($.ui.multichoice, {
        locale: {
            addAll: 'Add all'
        }
    });


})(jQuery);
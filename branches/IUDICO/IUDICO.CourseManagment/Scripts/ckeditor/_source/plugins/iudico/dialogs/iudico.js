CKEDITOR.dialog.add('iudico', function (editor) {
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
        div = blockLimit && blockLimit.getAscendant('object', true) && blockLimit.getAttribute('iudico') == true;

        return div;
    }

    return {
        title: editor.lang.iudico.title,
        minWidth: 390,
        minHeight: 230,
        onShow: function () {
            delete this.object;

            var element = this.getParentEditor().getSelection().getStartElement();
            var object = element && element.getAscendant('object', true);

            if (object && object.getAttribute('iudico') == true) {
                this.object = object;
                this.setupContent(object);
            }
        },
        onOk: function () {
            //editor =this.editor;
            element = this.object;
            isInsertMode = !element;


            if (isInsertMode) {
                objectNode = editor.document.createElement('object');
                objectNode.setAttribute('iudico', 'true');
            }

            // not needed here?
            // this.commitContent(objectNode);

            if (isInsertMode) {
                editor.insertElement(objectNode);
            }
        },
        //onCancel: function () { }, no cancel button
        onLoad: function (data) { },
        contents: [
			{
			    id: 'simpleTab',
			    label: editor.lang.iudico.simpleTab,
			    title: editor.lang.iudico.simpleTab,
			    padding: 2,
			    elements:
				[
                    {
                        id: 'question',
                        type: 'text',
                        label: editor.lang.iudico.question,
                        labelLayout: 'horizontal',
                        commit: function (element) {
                            addParam(element, 'question', this.getValue());
                        }

                    },
                    {
                        id: 'correctAnswer',
                        type: 'text',
                        label: editor.lang.iudico.correctAnswer,
                        labelLayout: 'horizontal',
                        commit: function (element) {
                            addParam(element, 'correctAnswer', this.getValue());
                        }
                    }
				]
			},
            {
                id: 'choiceTab',
                label: editor.lang.iudico.choiceTab,
                title: editor.lang.iudico.choiceTab,
                padding: 2,
                elements:
				[
                    {
                        id: 'question',
                        type: 'text',
                        label: editor.lang.iudico.question,
                        labelLayout: 'horizontal',
                        commit: function (element) {
                            addParam(element, 'question', this.getValue());
                        }

                    },
                    {
                        id: 'correctAnswer',
                        type: 'select',
                        label: editor.lang.iudico.correctAnswer,
                        labelLayout: 'horizontal',
                        items: [],
                        setup: function (element) {

                        },
                        commit: function (element) {
                            addParam(element, 'correctAnswer', this.getValue());
                        }
                    },
                    {
                        type: 'hbox',
                        widths: ['100px', '200px'],
                        children:
                        [
                            {
                                id: 'addChoice',
                                type: 'button',
                                label: editor.lang.iudico.addChoice,
                                onClick: function () {

                                }
                            },
                            {
                                id: 'choice',
                                type: 'text',
                                label: editor.lang.iudico.choice,
                                labelLayout: 'horizontal',
                                items: [],
                                setup: function (element) {

                                },
                                commit: function (element) {
                                    //addParam(element, 'correctAnswer', this.getValue());
                                }
                            }
                        ]
                    },
                    {
                        type: 'hbox',
                        widths: ['50px', '200px'],
                        children:
                        [
                            {
                                id: 'deleteChoice',
                                type: 'button',
                                label: editor.lang.iudico.deleteChoice,
                                onClick: function () {

                                }
                            }/*,
                            {
                                id: 'choices',
                                type: 'html'
                            }*/
                        ]
                    }
				]

            },
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


/* 'default' : 'simple',
						items :
						[

							[ editor.lang.iudico.simple, 'simple' ],
							[ editor.lang.iudico.choice, 'choice' ],
							[ editor.lang.iudico.compile, 'compile' ]

						],
						//onChange : linkTypeChanged,
						setup : function( data )
						{
							if ( data.type )
							    this.setValue( data.type );
						},
						commit : function( data )
						{
							data.type = this.getValue();
						}                        */
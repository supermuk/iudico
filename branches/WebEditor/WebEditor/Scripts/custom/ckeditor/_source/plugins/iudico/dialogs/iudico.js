CKEDITOR.dialog.add('iudico', function (editor) {
    //var lang = editor.lang.about;

    function addParam(element, name, value) {
        documentObject = element.getDocument();
        paramNode = documentObject.createElement("param");

        paramNode.setAttribute(name, value);

        element.append(paramNode);
    }

    return {
        title: editor.lang.iudico.title,
        minWidth: 390,
        minHeight: 230,
        onOk: function () {
            //if (isInsertMode)
            objectNode = editor.document.createElement('object');
            objectNode.setAttribute('iudico', 'true');

            //param = editor.document.createElement("param");

            //element.append(param);


            // later
            this.commitContent(objectNode);


            //if (isInsertMode)
            editor.insertElement(objectNode);
        },
        onCancel: function () { },
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
                expand: true,
                padding: 0,
                elements:
				[

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
CKEDITOR.dialog.add('iudico', function (editor) {
    //var lang = editor.lang.about;

    return {
        title: editor.lang.iudico.title,
        minWidth: 390,
        minHeight: 230,
        onOk: function () { },
		onCancel: function () { },
		onLoad: function (data) { },
        contents: [
			{
			    id: 'simpleTab',
			    label: editor.lang.iudico.simpleTab,
			    title: editor.lang.iudico.simpleTab,
			    expand: true,
			    padding: 0,
			    elements:
				[
                    {
						id : 'questionType',
						type : 'select',
						label : editor.lang.iudico.type,
						'default' : 'simple',
						items :
						[
                        /*
							[ editor.lang.iudico.simple, 'simple' ],
							[ editor.lang.iudico.choice, 'choice' ],
							[ editor.lang.iudico.compile, 'compile' ]
                        */
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
						}
					},
					{
					    type: 'html',
					    html:
                            '<div class="cke_iudico_container">' +
                                
                            '</div>'
					}
				],			    
			},
            {
			    id: 'choiceTab',
			    label: editor.lang.iudico.simpleTab,
			    title: editor.lang.iudico.simpleTab,
			    expand: true,
			    padding: 0,
			    elements:
				[
                    {
						id : 'questionType',
						type : 'select',
						label : editor.lang.iudico.type,
						'default' : 'simple',
						items :
						[
                        /*
							[ editor.lang.iudico.simple, 'simple' ],
							[ editor.lang.iudico.choice, 'choice' ],
							[ editor.lang.iudico.compile, 'compile' ]
                        */
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
						}
					},
					{
					    type: 'html',
					    html:
                            '<div class="cke_iudico_container">' +
                                
                            '</div>'
					}
				],			    
			}
		],
        
        buttons: [CKEDITOR.dialog.okButton, CKEDITOR.dialog.cancelButton]
    };
});

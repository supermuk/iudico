CKEDITOR.dialog.add( 'spoiler', function( editor )
{
	return {
		title : editor.lang.spoiler.SpoilerTitle,
		minWidth : 250,
		minHeight : 70,
		contents : [
			{
				id : 'tab1',
				label : editor.lang.spoiler.SpoilerContentsTitle,
				title:  editor.lang.spoiler.SpoilerContentsTitle,
				elements :
				[
					{   id : 'input1',
						type : 'text',
						style : 'width:140px;',
						label : editor.lang.spoiler.SpoilerElementsLabel,
                        validate : function()

{
CKEDITOR.config.text_val= this.getValue();
if ( !this.getValue() )
{alert( editor.lang.spoiler.SpoilerWarning );
return false;}

var fragment = editor.getSelection().getRanges()[0].extractContents();
var container = CKEDITOR.dom.element.createFromHtml("<pre/>", editor.document);

fragment.appendTo(container);

var spoilerWrap = editor.document.createElement( 'div' );
	spoilerWrap.setAttribute( 'style', 'border: 1px solid #ccc;		background-color: #e8e8e8;		margin: 0 auto;		width: 97%;' );
	spoilerWrap.setAttribute( 'id', 'spoiler');

var spoilerHead = editor.document.createElement( 'div' );
	spoilerHead.setAttribute( 'style', 'cursor: pointer;        color: #343434;		font-size: 11px;		line-height: 15px;		margin-left: 6px;		padding: 1px 14px 3px;		width: 97%;' );
	spoilerHead.setAttribute( 'onclick',"if(this.parentNode.getElementsByTagName('div')[1].style.display != '') { this.parentNode.getElementsByTagName('div')[1].style.display = ''; } else { this.parentNode.getElementsByTagName('div')[1].style.display = 'none'; }");
	spoilerHead.appendText(CKEDITOR.config.text_val);
	
var spoilerBody = editor.document.createElement( 'div' );
	spoilerBody.setAttribute( 'style', 'display: block;		background: none repeat scroll 0 0 #f4f4f4;		border-top: 1px solid #ccc;		line-height: 17px;		padding: 3px 3px 3px 7px;        font-size: 12px;        color: #343434;' );

	var c = editor.document.createElement( 'div' );
	c.appendText( 'Put text here!');
	container.appendTo(c);
	c.appendTo(spoilerBody);
	spoilerHead.appendTo(spoilerWrap);
	spoilerBody.appendTo(spoilerWrap);

editor.insertElement( spoilerWrap);
	
	
CKEDITOR.ENTER_BR;
return true;
}

}

				]
			}
		]
	};
} );
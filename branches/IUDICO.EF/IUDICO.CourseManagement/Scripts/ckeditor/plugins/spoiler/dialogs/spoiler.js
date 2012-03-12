CKEDITOR.dialog.add( 'spoiler', function( editor )
{
	return {
		title : 'Spoiler',
		minWidth : 250,
		minHeight : 70,
		contents : [
			{
				id : 'tab1',
				label : 'First Tab',
				title : 'First Tab',
				elements :
				[
					{   id : 'input1',
						type : 'text',
						style : 'width:140px;',
						label : 'Enter spoiler title:',
                        validate : function()

{
CKEDITOR.config.text_val= this.getValue();
if ( !this.getValue() )
{alert( 'Spoiler title cannot be empty!' );
return false;}

/**
 * Retrieve HTML presentation of the current selected range, require editor
 * to be focused first.
 */
var getSelectedHtml = function()
{
   var selection = editor.getSelection();
   if( selection )
   {
      var bookmarks = selection.createBookmarks(),
         range = selection.getRanges()[ 0 ],
         fragment = range.clone().cloneContents();

      selection.selectBookmarks( bookmarks );

      var retval = "",
         childList = fragment.getChildren(),
         childCount = childList.count();
      for ( var i = 0; i < childCount; i++ )
      {
         var child = childList.getItem( i );
         retval += ( child.getOuterHtml?
            child.getOuterHtml() : child.getText() );
      }
      return retval;
   }
};
var fragment = editor.getSelection().getRanges()[0].extractContents();
var container = CKEDITOR.dom.element.createFromHtml("<pre/>", editor.document);

fragment.appendTo(container);

var spoilerWrap = editor.document.createElement( 'div' );
	spoilerWrap.setAttribute( 'style', 'border: 1px solid #ccc;		background-color: #e8e8e8;		margin: 0 auto;		width: 97%;' );

var spoilerHead = editor.document.createElement( 'div' );
	spoilerHead.setAttribute( 'style', 'cursor: pointer;        color: #343434;		font-size: 11px;		line-height: 15px;		margin-left: 6px;		padding: 1px 14px 3px;		width: 97%;' );
	spoilerHead.setAttribute( 'onclick',"if(this.parentNode.getElementsByTagName('div')[1].style.display != '') { this.parentNode.getElementsByTagName('div')[1].style.display = ''; } else { this.parentNode.getElementsByTagName('div')[1].style.display = 'none'; }");
	spoilerHead.appendText(CKEDITOR.config.text_val);
	
var spoilerBody = editor.document.createElement( 'div' );
	spoilerBody.setAttribute( 'style', 'display: block;		background: none repeat scroll 0 0 #f4f4f4;		border-top: 1px solid #ccc;		line-height: 17px;		padding: 3px 3px 3px 7px;        font-size: 12px;        color: #343434;' );

	var c = editor.document.createElement( 'div' );;
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
/// <reference path="sco.js" />

$(function () {
    $('body').sco(1, $('object[iudico-type]'));
});

$(document).ready(function(){
	$("#spinner").bind("ajaxSend", function() {
		$(this).show();
	}).bind("ajaxStop", function() {
		$(this).hide();
	}).bind("ajaxError", function() {
		$(this).hide();
	});
    var divList = document.getElementsByTagName('div');
    for (var i = 0; i < divList.length; i++) {
        if (divList[i].attributes['id'] && divList[i].attributes['id'].value.contains('tabs')) {
            $('#'+divList[i].attributes['id'].value).tabs();
        }
     }
});
window.onunload = $.rteTerminate;
window.onbeforeunload = $.rteTerminate;
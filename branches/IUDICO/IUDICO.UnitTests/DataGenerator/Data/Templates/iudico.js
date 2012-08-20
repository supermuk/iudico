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
});
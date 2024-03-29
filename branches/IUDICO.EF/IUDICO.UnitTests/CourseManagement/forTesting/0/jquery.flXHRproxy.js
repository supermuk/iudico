(function( $ ){
	$.flXHRproxy = flensed.flXHR;	// make jQuery.flXHRproxy a reference alias to flensed.flXHR, for convenience
	var _opts = [];
	$.flXHRproxy.registerOptions = function(url,fopts) {	// call to define a set of flXHR options to be applied to a 
															// matching request URL
		// set up flXHR defaults, if not already defined
		if (typeof fopts==="undefined"||fopts===null) fopts = {};
		if (typeof fopts.instancePooling==="undefined"||fopts.instancePooling===null) fopts.instancePooling = true;
		if (typeof fopts.autoUpdatePlayer === "undefined"||fopts.autoUpdatePlayer===null) fopts.autoUpdatePlayer = true;
		_opts.push(function(callUrl) {	// save this set of options with a matching function for the target URL
			if (callUrl.substring(0,url.length)===url) return fopts;
			else return null;
		});
	}
	$.xhr.register('flXHRproxy',function(as) {
		if (as.async&&(as.type==="POST"||as.type==="GET")) {	// flXHR only supports async and GET/POST
			var tmp, useopts = {instancePooling:true,autoUpdatePlayer:true};	// defaults if no URL match is found
			for (var i=0; i<_opts.length; i++) {	// loop through all registered options for flXHR
				if ((tmp=_opts[i](as.url))!==null) useopts = tmp;	// if URL match is found, use those options
			}
			return new $.flXHRproxy(useopts);
		}
		else {	// else, fall back on standard XHR
			return $.xhr.registry['xhr'](as);
		}
	});
	$.ajaxSetup({transport:'flXHRproxy'});
})(jQuery);

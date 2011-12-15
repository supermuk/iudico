/*	flensedCore 1.0 <http://www.flensed.com/>
	Copyright (c) 2008 Kyle Simpson, Getify Solutions, Inc.
	This software is released under the MIT License <http://www.opensource.org/licenses/mit-license.php>

	====================================================================================================
	Portions of this code were extracted and/or derived from:

	SWFObject v2.1 & 2.2a8 <http://code.google.com/p/swfobject/>
	Copyright (c) 2007-2008 Geoff Stearns, Michael Williams, and Bobby van der Sluis
	This software is released under the MIT License <http://www.opensource.org/licenses/mit-license.php>
*/
(function(B){var H=B,L=B.document,N="undefined",M=true,C=false,E="",F="object",D="string",G=null,J=null,I=null,K=B.parseInt,A=B.setTimeout;if(typeof B.flensed===N){B.flensed={}}else{if(typeof B.flensed.ua!==N){return}}G=B.flensed;A(function(){var P="flensed.js",U=C,R=L.getElementsByTagName("script"),T=R.length;try{G.base_path.toLowerCase();U=M}catch(S){G.base_path=E}if((typeof R!==N)&&(R!==null)){if(!U){var O=0;for(var Q=0;Q<T;Q++){if(typeof R[Q].src!==N){if((O=R[Q].src.indexOf(P))>=0){G.base_path=R[Q].src.substr(0,O);break}}}}}},0);G.parseXMLString=function(P){var O=null;if(H.ActiveXObject){O=new B.ActiveXObject("Microsoft.XMLDOM");O.async=C;O.loadXML(P)}else{var Q=new B.DOMParser();O=Q.parseFromString(P,"text/xml")}return O};G.getObjectById=function(O){try{if(L.layers){return L.layers[O]}else{if(L.all){return L.all[O]}else{if(L.getElementById){return L.getElementById(O)}}}}catch(P){}return null};G.createCSS=function(T,P,U,S){if(G.ua.ie&&G.ua.mac){return}var R=L.getElementsByTagName("head")[0];if(!R){return}var O=(U&&typeof U===D)?U:"screen";if(S){J=null;I=null}if(!J||I!==O){var Q=L.createElement("style");Q.setAttribute("type","text/css");Q.setAttribute("media",O);J=R.appendChild(Q);if(G.ua.ie&&G.ua.win&&typeof L.styleSheets!==N&&L.styleSheets.length>0){J=L.styleSheets[L.styleSheets.length-1]}I=O}if(G.ua.ie&&G.ua.win){if(J&&typeof J.addRule===F){J.addRule(T,P)}}else{if(J&&typeof L.createTextNode!==N){J.appendChild(L.createTextNode(T+" {"+P+"}"))}}};G.bindEvent=function(R,O,Q){O=O.toLowerCase();try{if(typeof R.addEventListener!==N){R.addEventListener(O.replace(/^on/,E),Q,C)}else{if(typeof R.attachEvent!==N){R.attachEvent(O,Q)}}}catch(P){}};G.unbindEvent=function(R,O,Q){O=O.toLowerCase();try{if(typeof R.removeEventListener!==N){R.removeEventListener(O.replace(/^on/,E),Q,C)}else{if(typeof R.detachEvent!==N){R.detachEvent(O,Q)}}}catch(P){}};G.throwUnhandledError=function(O){throw new B.Error(O)};G.error=function(R,P,Q,O){return{number:R,name:P,description:Q,message:Q,srcElement:O,toString:function(){return R+", "+P+", "+Q}}};G.ua=function(){var U="Shockwave Flash",O="ShockwaveFlash.ShockwaveFlash",Y="application/x-shockwave-flash",P=B.navigator,V=typeof L.getElementById!==N&&typeof L.getElementsByTagName!==N&&typeof L.createElement!==N,f=[0,0,0],X=null;if(typeof P.plugins!==N&&typeof P.plugins[U]===F){X=P.plugins[U].description;if(X&&!(typeof P.mimeTypes!==N&&P.mimeTypes[Y]&&!P.mimeTypes[Y].enabledPlugin)){X=X.replace(/^.*\s+(\S+\s+\S+$)/,"$1");f[0]=K(X.replace(/^(.*)\..*$/,"$1"),10);f[1]=K(X.replace(/^.*\.(.*)\s.*$/,"$1"),10);f[2]=/r/.test(X)?K(X.replace(/^.*r(.*)$/,"$1"),10):0}}else{if(typeof H.ActiveXObject!==N){try{var Z=new B.ActiveXObject(O);if(Z){X=Z.GetVariable("$version");if(X){X=X.split(" ")[1].split(",");f=[K(X[0],10),K(X[1],10),K(X[2],10)]}}}catch(T){}}}var e=P.userAgent.toLowerCase(),S=P.platform.toLowerCase(),c=/webkit/.test(e)?parseFloat(e.replace(/^.*webkit\/(\d+(\.\d+)?).*$/,"$1")):C,Q=C,R=0,b=S?/win/.test(S):/win/.test(e),W=S?/mac/.test(S):/mac/.test(e);/*@cc_on Q=M;try{R=K(e.match(/msie (\d+)/)[1],10);}catch(e2){}@if(@_win32)b=M;@elif(@_mac)W=M;@end @*/return{w3cdom:V,pv:f,webkit:c,ie:Q,ieVer:R,win:b,mac:W}}()})(window);

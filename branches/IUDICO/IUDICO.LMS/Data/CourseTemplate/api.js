/// <reference path="jquery-1.5.2.min.js" />

var noAPIFound = "false";
var findAPITries = 0;
var apiHandle = null;
var exitPageStatus = false;
var inited = false;

(function ($) {
    
    $.extend({
        _findAPI: function () {
            while ((win.API_1484_11 == null) && (win.parent != null) && (win.parent != win)) {
                findAPITries++;
                
                if (findAPITries > 500) {
                    alert("Error finding API -- too deeply nested.");
                    return null;
                }

                win = win.parent;
            }

            return win.API_1484_11;
        },

        _getAPI : function() {
            var result = $.findAPI( window );    

            if ( (result == null) && (window.opener != null) && (typeof(window.opener) != "undefined") )
            {
                result = $.findAPI(window.opener);
            }    
            if (result == null)
            {
                alert("Unable to locate the LMS's API Implementation.\n" + "Communication with the LMS will not occur.");    
                noAPIFound = "true";
            }

            return result;
        },

        getAPI : function() {
            if ( apiHandle == null )
            {
              if ( noAPIFound == "false" )
              {
                apiHandle = $._getAPI();
              }
            }

           return apiHandle;
        },

        lastError:  function() {
            var errCode = $.getAPI().GetLastError();
            
            if (errCode != 0)
            {
                alert($.getAPI().GetErrorString(errCode).toString());
            }
        },

        rteTerminate: function() {
            if (inited) {
                inited = false;
                
                $.getAPI().Terminate("");
                
                exitPageStatus = true;
                
                $.lastError();
            }
        },

        rteInitialize: function() {
            if (!inited) {
                $.getAPI().Initialize("");

                $.lastError();

                inited = true;
            }
        },

        rteGetValue: function(name) {
            var value = $.getAPI().GetValue(name);
            
            $.lastError();
            
            return value;
        },

        rteSetValue: function(name, value) {
           $.getAPI().SetValue(name, value);

           $.lastError();
        },

        rteCommit: function(){
            var result = $.getAPI().Commit("");

            $.lastError();

            return result;
        }
    });

})(jQuery);
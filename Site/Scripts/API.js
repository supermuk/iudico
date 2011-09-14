API_1484_11 = function(LearnerAttemptId)
{
    this.values = {};
    this.cache = {};
    this.LearnerAttemptId = parseInt(LearnerAttemptId);
    
    this.traceLog = function(message)
    {
        try
		{
			if (typeof eval('console') != 'undefined')
			{
				console.log(message);
			}
		}
		catch(e)
		{
		
		}
    }

    this.Initialize = function(parameter)
    {
        this.traceLog('Initialize(\'' + parameter + '\')');
        
        var response = $.ajax({
          type: "POST",
          url: "/API.asmx/Initialize",
          contentType: "application/json; charset=utf-8",
          dataType: "json",
          async: false
        }).responseText;
        
        return "true";
    }
    
    this.Terminate = function(parameter)
    {
        this.traceLog('Terminate(\'' + parameter + '\')');
        return "true";
    }
    
    this.GetValue = function(name){
        this.traceLog('GetValue(\'' + name + '\')');
        
        var response = eval('(' +
        $.ajax({
          type: "POST",
          url: "/API.asmx/GetValue",
          data: "{name: '"+name+"'}",
          contentType: "application/json; charset=utf-8",
          dataType: "json",
          async: false
        }).responseText + ')');
        
        return response["d"];
    }
    
    this.SetValue = function(name, value){
        this.traceLog('SetValue(\'' + name + '\', \'' + value + '\')');
        
        var response = eval('(' +
        $.ajax({
          type: "POST",
          url: "/API.asmx/SetValue",
          data: "{name: '"+name+"', value: '"+value+"'}",
          contentType: "application/json; charset=utf-8",
          dataType: "json",
          async: false
        }).responseText + ')');
        
        return response["d"];
    }
    
    this.Commit = function(parameter){
        this.traceLog('Commit(\'' + parameter + '\')');       
    }
    
    this.GetLastError = function(){
        return 0;
    }
    
    this.GetErrorString = function(parameter){
        return "No error ocurred";
    }
    
    this.GetDiagnostic = function(parameter){
        return "No diagnostic information available";
    }
}
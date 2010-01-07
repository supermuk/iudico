API_1484_11 = function()
{
    this.values = {};
    this.cache = {};
    
    this.traceLog = function(message)
    {
        if (console)
            console.log(message);
    }

    this.Initialize = function(parameter)
    {
        this.traceLog('Initialize(\'' + parameter + '\')');
        return "true";
    }
    
    this.Terminate = function(parameter)
    {
        this.traceLog('Terminate(\'' + parameter + '\')');
        return "true";
    }
    
    this.GetValue = function(name){
        this.traceLog('GetValue(\'' + name + '\')');
        return 0;
    }
    
    this.SetValue = function(name, value){
        this.traceLog('SetValue(\'' + name + '\', \'' + value + '\')');
        
        //this.cache[name] = value;
        //this.values[name] = value;
        
        $.ajax({
          type: "POST",
          url: "/API.asmx/SetValue",
          data: "{name: '"+name+"', value: '"+value+"'}",
          contentType: "application/json; charset=utf-8",
          dataType: "json",
          success: function(msg) {
            // Do something interesting here.
          }
        });
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
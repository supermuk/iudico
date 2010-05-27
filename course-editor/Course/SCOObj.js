function SCOObj(passRank) {
    this.passRank = passRank;
    this.tests = new Array();
    this.compiled = 0;

    this.length = function() {
        return this.tests.length;
    }

    this.Commit = function() {

        var scoreRaw = 0;
        var scoreMin = 0;
        var scoreMax = 0;

        for (var i = 0; i < this.length(); i++) {
            if (this.tests[i].CompiledTest == true) {
                this.tests[i].processAnswer(this, i);
                this.compiled++;
            }
            else {
                doSetValue("cmi.interactions." + i + ".learner_response", this.tests[i].getAnswer());
                doSetValue("cmi.interactions." + i + ".result", this.tests[i].getResult());
            }
            scoreMin += this.tests[i].getScoreMin();
            scoreMax += this.tests[i].getScoreMax();
            scoreRaw += this.tests[i].getScoreRaw();
        }
        var scoreScaled = (scoreRaw - scoreMin) / (scoreMax - scoreMin);
        var success_status = (scoreRaw >= this.passRank ? "passed" : "failed");
        doSetValue("cmi.score.raw", scoreRaw);
	doSetValue("cmi.score.min", scoreMin);
        doSetValue("cmi.score.max", scoreMax);        
        doSetValue("cmi.score.scaled", scoreScaled);
        doSetValue("cmi.completion_status", "completed");
        doSetValue("cmi.success_status", success_status);        
        
        if (doGetValue("cmi.objectives._count") > 0) {           
            //Sets primary objective scores
            doSetValue("cmi.objectives.0.score.min", scoreMin);
            doSetValue("cmi.objectives.0.score.max", scoreMax);
            doSetValue("cmi.objectives.0.score.raw", scoreRaw);            
            doSetValue("cmi.objectives.0.score.scaled", scoreScaled);
            //Sets primary objective status 'satisfied'.
            doSetValue("cmi.objectives.0.success_status", success_status);
        }
        $('#Button1')[0].disabled = true;

        if (this.compiled == 0) {
            this.FinishUp();
        }
    }

    this.FinishUp = function() {
        this.compiled--;

        if (this.compiled <= 0) {
            doSetValue("cmi.completion_status", "completed");
            doSetValue("cmi.exit", "suspend");

            doCommit();
            doSetValue("adl.nav.request", "continue");
            doTerminate();
        }
    }

    doInitialize();

    var argLength = arguments.length;

    for (var i = 1; i < argLength; i++) {
        this.tests.push(arguments[i]);

        if (arguments[i].getType() == "other") {
            this.compileURL = doGetValue("lnu.settings.compile_service_url");
        }
        numInteractions = doGetValue("cmi.interactions._count");
        //alert(numInteractions);
        if (numInteractions <= argLength - 1) {
            doSetValue("cmi.interactions." + (i - 1) + ".id", this.tests[i - 1].ID);
            doSetValue("cmi.interactions." + (i - 1) + ".type", this.tests[i - 1].getType());
            doSetValue("cmi.interactions." + (i - 1) + ".correct_responses.0.pattern", this.tests[i - 1].getCorrectAnswer());
        }
        else if (numInteractions > argLength-1) {
            learnerResponse = doGetValue("cmi.interactions." + (i - 1) + ".learner_response");
            //alert(learnerResponse);
            if (learnerResponse) {
                $('#Button1')[0].disabled = true;
                this.tests[i - 1].setAnswer(learnerResponse);
                //doGetValue("cmi.interactions." + (i-1) + ".correct_responses.0.pattern");
            }
        }
    }
    if (doGetValue("cmi.completion_status") == "unknown"){
	    doSetValue("cmi.completion_status", "incomplete");
    }
    doCommit();
}

SCOObj.compileURL = null;

function simpleTest(ID, correctAnswer, rank) {
    this.ID = ID;
    this.correctAnswer = correctAnswer;
    this.Rank = rank;

    this.getAnswer = function() {
        var answer = document.getElementById(ID).value;
        return answer;
        //return $('#' + this.ID)[0].value;
    }

    this.setAnswer = function(answer) {
        document.$('#' + this.ID)[0].value = answer;
    }

    this.getCorrectAnswer = function() {
        return this.correctAnswer;
    }

    this.getType = function() {
        return "fill-in";
    }

    this.getResult = function() {
        return (this.getCorrectAnswer() == this.getAnswer() ? "correct" : "incorrect");
    }
    
    this.getScoreRaw = function(){
        return (this.getCorrectAnswer() == this.getAnswer() ? this.Rank : 0);
    }
    
    this.getScoreMin = function(){
        return 0;
    }
    
    this.getScoreMax = function(){
        return this.Rank;
    }
}

function complexTest(ID, correctAnswer, rank) {
    this.ID = ID;
    this.correctAnswer = correctAnswer;
    this.Rank = rank;

    this.getAnswer = function() {
        var result = [];
        var inputArray = $('#' + this.ID + ' input');

        for (var i = 0; i < inputArray.length; i++) {
            result[i] = (inputArray[i].checked ? "1" : "0");
        }

        return result.join('');
    }

    this.setAnswer = function(answer) {
        var result = answer.split(',');
        var inputArray = $('#' + this.ID + ' input');

        for (var i = 0; i < inputArray.length; i++) {
            inputArray[i].checked = (result[i] == "1");
        }
    }

    this.getCorrectAnswer = function() {
        return this.correctAnswer;
    }

    this.getType = function() {
        return "choice";
    }

    this.getResult = function() {
        return (this.getCorrectAnswer() == this.getAnswer() ? "correct" : "incorrect");
    }
    
    this.getScoreRaw = function(){
        var answer = this.getAnswer();
        var correctAnswer = this.getCorrectAnswer();
        if (correctAnswer.length != answer.length)
        {
            alert("Correct answer and answer has different lengths!");
        }
        var count = 0;
        for (var i=0; i<answer.length; i++)
        {
            count += (answer.charAt(i) == correctAnswer.charAt(i) ? 1 : 0);
        }
        var result = count * this.Rank / answer.length;
        return result;
    }
    
    this.getScoreMin = function(){
        return 0;
    }
    
    this.getScoreMax = function(){
        return this.Rank;
    }
}

function compiledTest(IDBefore, IDAfter, ID, url, language, timelimit, memorylimit, input, output, rank) {
    this.CompiledTest = true;
    this.Rank = rank;
    this.Answer = "";

    this.ID = ID;
    this.IDBefore = IDBefore;
    this.IDAfter = IDAfter;
    this.language = language;
    this.timelimit = timelimit;
    this.memorylimit = memorylimit;
    this.input = input;
    this.output = output;
    this.url = url;

    this.processAnswer = function(SCOObj, i) {
        var sourceT = $('#' + this.IDBefore + ' pre').text() + $('#' + this.ID).val() + $('#' + this.IDAfter + ' pre').text();
        var dataT = { 'source': sourceT, 'language': language, 'input': input, 'output': output, 'timelimit': timelimit, 'memorylimit': memorylimit };

        var url = SCOObj.compileURL || this.url;

        jQuery.flXHRproxy.registerOptions(url, { xmlResponseText: false });

        $.ajax({
            type: "POST",
            url: url,
            data: dataT,
            dataType: 'xml',
            transport: 'flXHRproxy',
            complete: function(transport) {
                var response = ($(transport.responseText).text());
                doSetValue("cmi.interactions." + i + ".learner_response", response);
                doSetValue("cmi.interactions." + i + ".result", (response == "Accepted" ? "correct" : "incorrect"));
                SCOObj.FinishUp();
            }
        });
    }

    this.setAnswer = function(answer) {
        $('#' + this.ID).value = answer;
    }

    this.getAnswer = function() {
    //???    return "";
        return this.Answer;
    }

    this.getCorrectAnswer = function() {
        return "Accepted";
    }

    this.getType = function() {
        return "other";
    }

    this.getScoreRaw = function() {
        return (this.getCorrectAnswer() == this.getAnswer() ? this.Rank : 0);
    }
    
    this.getScoreMin = function(){
        return 0;
    }
    
    this.getScoreMax = function(){
        return this.Rank;
    }
}
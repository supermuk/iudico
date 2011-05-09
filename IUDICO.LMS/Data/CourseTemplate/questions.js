/// <reference path="api.js" />

(function ($) {

    $.fn.iudicoQuestion = function () {
        var $this = $(this);

        if ($this.data('scoq') == null) {
            var scoq;
            switch ($this.attr('iudico-type')) {
                case 'iudico-simple':
                    scoq = new simpleTest($this);
                    break;
                case 'iudico-compex':
                    scoq = new complexTest($this);
                case 'iudico-compiled':
                    scoq = new compiledTest($this);
                default:
                    scoq = null;
            }

            $this.data('scoq', scoq);

            return scoq;
        }
    }

    $.fn.params = function () {
        var params = {};

        $(this).find('param').each(function () {
            params[$(this).attr('name')] = $(this).attr('value');
        });

        return params;
    }
})(jQuery);

function simpleTest($object) {
    this.Id = null;
    this.CorrectAnswer = null;
    this.Rank = null;

    this.parse = function ($object) {
        var params = $object.params();

        this.Id = params['id'];
        this.CorrectAnswer = params['correctAnswer'];
        this.Rank = params['rank'];

        var $question = $('<div id="' + params['id'] + '"></div>');
        $question.append('<b>' + params['question'] + '</b>');
        $question.append('<input type="text" name="' + params['id'] + 'Answer" id="' + params['id'] + 'Answer" />');

        $object.replaceWith($question);
    }

    this.getAnswer = function () {
        return $('#' + this.Id + 'Answer')[0].val();
    }

    this.setAnswer = function (answer) {
        $('#' + this.Id + 'Answer').val(answer);
    }

    this.getCorrectAnswer = function () {
        return this.CorrectAnswer;
    }

    this.getType = function () {
        return "fill-in";
    }

    this.getResult = function () {
        return (this.getCorrectAnswer() == this.getAnswer() ? "correct" : "incorrect");
    }

    this.getScoreRaw = function () {
        return (this.getCorrectAnswer() == this.getAnswer() ? this.Rank : 0);
    }

    this.getScoreMin = function () {
        return 0;
    }

    this.getScoreMax = function () {
        return this.Rank;
    }
	
	this.parse($object);
}

function complexTest($object) {
    this.Id = null;
    this.CorrectAnswer = null;
    this.Rank = null;
    this.MultiChoice = null;

    this.parse = function ($object) {
        var params = $object.params();

        this.Id = params['id'];
        this.CorrectAnswer = params['correct'];
        this.Rank = params['rank'];
        this.MultiChoice = params['multichoice'];

        var $question = $('<div id="' + params['id'] + '"></div>');
        $question.append('<b>' + params['question'] + '</b>');

        var answersCount = parseInt(params['count']);

        for (var i = 0; i < answersCount; i++) {
            $question.append('<input type="' + (params['multichoice'] == '1' ? 'check' : 'radio') + '" name="' + params['id'] + 'Answer[]" id="' + params['id'] + 'Answer' + i + '" /> ' + params['option' + i]);
        }

        $obj.replaceWith($question);
    }

    this.getAnswer = function () {
        var result = [];

        $('input[@name="' + this.Id + 'Answer[]"').each(function () {
            result.push($(this).is(':checked') ? '1' : '0');
        });

        return result.join('');
    }

    this.setAnswer = function (answer) {
        var result = answer.split('');
        var inputArray = $('#' + this.ID + ' input');

        $('input[@name="' + this.Id + 'Answer[]"').each(function (i) {
            $(this).attr('checked', (result[i] == '1'));
        });
    }

    this.getCorrectAnswer = function () {
        return this.CorrectAnswer;
    }

    this.getType = function () {
        return "choice";
    }

    this.getResult = function () {
        return (this.getCorrectAnswer() == this.getAnswer() ? "correct" : "incorrect");
    }

    this.getScoreRaw = function () {
        var answer = this.getAnswer();
        var correctAnswer = this.getCorrectAnswer();

        var count = 0;
        var min = Math.min(answer.length, correctAnswer.length);

        for (var i = 0; i < min; i++) {
            count += (answer.charAt(i) == correctAnswer.charAt(i) ? 1 : 0);
        }

        return count * this.Rank / correctAnswer.length;
    }

    this.getScoreMin = function () {
        return 0;
    }

    this.getScoreMax = function () {
        return this.Rank;
    }
	
	this.parse($object);
}

function compiledTest($object) {
    this.Id = null;
    this.IdBefore = null;
    this.IdAfter = null;
    this.Rank = null;
    this.Answer = null;
    this.CompiledTest = null;
    
    this.Language = null;
    this.Timelimit = null;
    this.Memorylimit = null;
    this.Input = null;
    this.Output = null;
    this.Url = null;

    this.parse = function ($object) {
        var params = $object.params();

        this.Id = params['id'];
        this.IdBefore = params['id'] + 'Before';
        this.IdAfter = params['id'] + 'After';
		this.PreCode = params['preCode'];
		this.PostCode = params['postCode'];
        this.Rank = params['rank'];
        this.Answer = null;
        this.CompiledTest = true;

        this.Language = params['language'];
        this.Timelimit = params['timeLimit'];
        this.Memorylimit = params['memoryLimit'];
        this.Url = params['url'];
		
		this.Input = [];
        this.Output = [];
		
		var testsCount = parseInt(params['count']);

        for (var i = 0; i < testsCount; i++) {
			this.Input.push(params['testInput' + i]);
			this.Output.push(params['testOutput' + i]);
		}

        var $question = $('<div id="' + params['id'] + '"></div>');
        $question.append('<b>' + params['question'] + '</b>');

        if (params['before'].length > 0) {
            $question.append('<div id="' + params['id'] + 'Before"><pre>' + params['preCode'] + '</pre></div>');
        }
        
        $question.append('<input type="text" name="' + params['id'] + 'Answer" id="' + params['id'] + 'Answer" />');

        if (params['after'].length > 0) {
            $question.append('<div id="' + params['id'] + 'After"><pre>' + params['postCode'] + '</pre></div>');
        }

        $obj.replaceWith($question);
    }

    this.processAnswer = function (SCOObj, i) {
        var obj = this;
        var sourceCode = this.PreCode + $('#' + this.ID).val() + this.PostCode;
        var sourceCodeData = { 'source': sourceCode, 'language': this.Language, 'input': this.Input, 'output': this.Output, 'timelimit': this.Timelimit, 'memorylimit': this.Memorylimit };

        jQuery.flXHRproxy.registerOptions(url, { xmlResponseText: false });

        $.ajax({
            type: "POST",
            url: this.Url,
            data: sourceCodeData,
            dataType: 'xml',
            transport: 'flXHRproxy',
            complete: function (transport) {
                obj.Answer = ($(transport.responseText).text());

                doSetValue("cmi.interactions." + i + ".learner_response", obj.Answer);
                doSetValue("cmi.interactions." + i + ".result", (obj.Answer == "Accepted" ? "correct" : "incorrect"));

                SCOObj.ScoreRaw += this.getScoreRaw();
                SCOObj.finish(i);
            }
        });
    }

    this.setAnswer = function (answer) {
        $('#' + this.Id + 'Answer').val(answer);
    }

    this.getAnswer = function () {
        return this.Answer;
    }

    this.getCorrectAnswer = function () {
        return "Accepted";
    }

    this.getType = function () {
        return "other";
    }

    this.getScoreRaw = function () {
        return (this.getCorrectAnswer() == this.getAnswer() ? this.Rank : 0);
    }

    this.getScoreMin = function () {
        return 0;
    }

    this.getScoreMax = function () {
        return this.Rank;
    }
	
	this.parser($object);
};
/// <reference path="api.js" />

(function ($) {

    $.fn.iudicoQuestion = function (id) {
        var $this = $(this);

        if ($this.data('scoq') == null) {
            var scoq;
            switch ($this.attr('iudico-type')) {
                case 'iudico-simple':
                    scoq = new simpleTest($this, id);
                    break;
                case 'iudico-choice':
                    scoq = new complexTest($this, id);
                    break;
                case 'iudico-compile':
                    scoq = new compiledTest($this, id);
                    break;
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

function simpleTest($object, id) {
    this.Id = id;
    this.CorrectAnswer = null;
    this.Rank = null;

    this.parse = function ($object) {
        var params = $object.params();

        this.CorrectAnswer = params['correctAnswer'];
        this.Rank = parseInt(params['rank']);

        var $question = $('<div id="' + this.Id + '"></div>');
        $question.append('<b>' + params['question'] + '</b>');
        $question.append('<p><input type="text" name="' + this.Id + 'Answer" id="' + this.Id + 'Answer" /></p>');

        $object.replaceWith($question);
    }

    this.getAnswer = function () {
        return $('#' + this.Id + 'Answer').val();
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

function complexTest($object, id) {
    this.Id = id;
    this.CorrectAnswer = null;
    this.Rank = null;
    this.MultiChoice = null;

    this.parse = function ($object) {
        var params = $object.params();

        this.CorrectAnswer = params['correct'];
        this.Rank = parseInt(params['rank']);
        this.MultiChoice = params['multichoice'];

        var $question = $('<div id="' + this.Id + '"></div>');
        $question.append('<b>' + params['question'] + '</b>');

        var answersCount = parseInt(params['count']);

        for (var i = 0; i < answersCount; i++) {
            $question.append('<p><input type="' + (params['multichoice'] == '1' ? 'check' : 'radio') + '" name="' + this.Id + 'Answer[]" id="' + this.Id + 'Answer' + i + '" value="' + params['option' + i] + '" /> ' + params['option' + i] + '</p>');
        }

        $object.replaceWith($question);
    }

    this.getAnswer = function () {
        /*var result = [];

        $('input[@name="' + this.Id + 'Answer[]"').each(function () {
        result.push($(this).is(':checked') ? '1' : '0');
        });

        return result.join('');*/

        var result = null;

        $('input[name="' + this.Id + 'Answer[]"]').each(function () {
            if ($(this).is(':checked')) {
                result = $(this).val();

                return false;
            }
        });

        return encodeURIComponent(result);
    }

    this.setAnswer = function (answer) {
        /*var result = answer.split('');
        var inputArray = $('#' + this.Id + ' input');

        $('input[@name="' + this.Id + 'Answer[]"').each(function (i) {
            $(this).attr('checked', (result[i] == '1'));
        });*/

        $('input[name="' + this.Id + 'Answer[]"][value="' + answer + '"]').attr('checked', 'checked');
    }

    this.getCorrectAnswer = function () {
        return encodeURIComponent(this.CorrectAnswer);
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

function compiledTest($object, id) {
    this.Id = id;
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

        this.IdBefore = this.Id + 'Before';
        this.IdAfter = this.Id + 'After';
        this.PreCode = params['preCode'];
        this.PostCode = params['postCode'];
        this.Rank = parseInt(params['rank']);
        this.Answer = null;
        this.CompiledTest = true;

        this.Language = params['language'];
        this.Timelimit = params['timeLimit'];
        this.Memorylimit = params['memoryLimit'];
        this.Url = params['compileUrl'];

        this.Input = [];
        this.Output = [];

        var testsCount = parseInt(params['count']);

        for (var i = 0; i < testsCount; i++) {
            this.Input.push(params['testInput' + i]);
            this.Output.push(params['testOutput' + i]);
        }

        var $question = $('<div id="' + this.Id + '"></div>');
        $question.append('<b>' + params['question'] + '</b>');

        if (params['preCode'].length > 0) {
            var preCode = $('<div/>').text(params['preCode']).html();
            $question.append('<div id="' + this.Id + 'Before"><pre>' + preCode + '</pre></div>');
        }

        $question.append('<p><textarea name="' + this.Id + 'Answer" id="' + this.Id + 'Answer"></textarea></p>');

        if (params['postCode'].length > 0) {
            var postCode = $('<div/>').text(params['postCode']).html();
            $question.append('<div id="' + this.Id + 'After"><pre>' + postCode + '</pre></div>');
        }

        if(testsCount > 0)
        {
            var $button = $('<input type="submit" name="check" value="Перевірити на 1 тесті" id="ScoCheck" />');

            $question.append($button);
            var obj = this;
            $question.find('#ScoCheck').click(function () { obj.check(); });
        }

        $object.replaceWith($question);
    }

    this.check = function () {
        var sourceCode = encodeURIComponent(this.PreCode + $('#' + this.Id + 'Answer').val() + this.PostCode);
        var sourceCodeData = { 'source': sourceCode, 'language': this.Language, 'input': this.Input[0], 'output': this.Output[0], 'timelimit': this.Timelimit, 'memorylimit': this.Memorylimit };

        $.flXHRproxy.registerOptions(this.Url, { xmlResponseText: false });

        var request = $.ajax({
            type: "POST",
            beforeSend: function(){
                $('#ScoCheck').after('<p id ="compilationStatus" style="color:red">' + 'Зачекайте доки закінчиться компіляція.' + '</p>');
                $('#ScoCheck').attr('disabled', true);
            },
            url: this.Url,
            data: sourceCodeData,
            dataType: 'xml',
            transport: 'flXHRproxy',
            traditional: true,
            complete: function (transport) {
                var checkAnswer = $.trim($(transport.responseText).text());
                alert('Результат одного тесту: ' + (checkAnswer == "Accepted" ? "Прийнято" : "Трапилася помилка") );
                
                $('#compilationStatus').remove();
                $('#ScoCheck').attr('disabled', false);
            }
        });        
    }

    this.processAnswer = function (SCOObj, i) {
        var obj = this;
        var sourceCode = encodeURIComponent(this.PreCode + $('#' + this.Id + 'Answer').val() + this.PostCode);
        var sourceCodeData = { 'source': sourceCode, 'language': this.Language, 'input': this.Input, 'output': this.Output, 'timelimit': this.Timelimit, 'memorylimit': this.Memorylimit };

        $.flXHRproxy.registerOptions(this.Url, { xmlResponseText: false });

        $.ajax({
            type: "POST",
            url: this.Url,
            data: sourceCodeData,
            dataType: 'xml',
            transport: 'flXHRproxy',
            traditional: true,
            complete: function (transport) {
                obj.Answer = $.trim($(transport.responseText).text());

                $.rteSetValue("cmi.interactions." + i + ".learner_response", obj.Answer);
                $.rteSetValue("cmi.interactions." + i + ".result", (obj.Answer == "Accepted" ? "correct" : "incorrect"));

                SCOObj.ScoreRaw += obj.getScoreRaw();
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
    
    this.parse($object);
};
/// <reference path="questions.js" />

$(function() {
    $.fn.sco = function(passrank, questions)
    {
        var element = $(this);

        if (element.data('scoobj') == null)
        {
            var scoobj = new SCO(this, passrank, questions);
                
            element.data('scoobj', scoobj);
        }
            
        return element.data('scoobj');
    };
});

function SCO(element, passrank, _questions) {
    this.Element = $(element);
    this.Questions = [];
    this.Compiled = 0;
    this.ScoreRaw = 0;
    this.ScoreMin = 0;
    this.ScoreMax = 0;
    this.PassRank = passrank;

    this.init = function (_questions) {
        var obj = this;

        $.rteInitialize();

        var interactions = $.rteGetValue("cmi.interactions._count");

        _questions.each(function (i) {
            obj.Questions.push($(this).iudicoQuestion('q' + i));

            $.rteSetValue("cmi.interactions." + i + ".id", obj.Questions[i].Id);
            $.rteSetValue("cmi.interactions." + i + ".type", obj.Questions[i].getType());
            $.rteSetValue("cmi.interactions." + i + ".correct_responses.0.pattern", obj.Questions[i].getCorrectAnswer());

            /*if (i >= interactions) {
                
            }
            else {
            var response = $.rteGetValue("cmi.interactions." + i + ".learner_response");

            if (learnerResponse) {
            $('#ScoSubmit').attr('disabled', true);

            obj.Questions[i].setAnswer(learnerResponse);
            }
            }*/
        });

        if ($.rteGetValue("cmi.completion_status") == "unknown") {
            $.rteSetValue("cmi.completion_status", "incomplete");
        }

        $.rteCommit();

        var $button = $('<input type="submit" name="submit" value="Submit" id="ScoSubmit" />');

        this.Element.append($button);
        this.Element.find('#ScoSubmit').click(function () { obj.submit(); });

        var spinner = '<div id="spinner" class="spinner" style="display:none;"><img id="img-spinner" src="wait.gif" alt="Loading"/></div>';
        $('#ScoSubmit').after(spinner);
    };

    this.count = function () {
        return this.Questions.length;
    };

    this.submit = function () {
        for (var i = 0; i < this.count(); i++) {
            if (this.Questions[i].CompiledTest == true) {
                this.Questions[i].processAnswer(this, i);

                this.Compiled++;
                if($('#compilationSubmitStatus').length == 0){
                     $('#ScoSubmit').after('<p id ="compilationSubmitStatus" style="color:red">' + 'Зачекайте доки закінчиться компіляція на сервері.' + '</p>');
                }
            }
            else {
                $.rteSetValue("cmi.interactions." + i + ".learner_response", this.Questions[i].getAnswer());
                $.rteSetValue("cmi.interactions." + i + ".result", this.Questions[i].getResult());

                this.ScoreRaw += this.Questions[i].getScoreRaw();
            }

            this.ScoreMin += this.Questions[i].getScoreMin();
            this.ScoreMax += this.Questions[i].getScoreMax();
        }

        if($('#compilationSubmitStatus').length > 0){
            $('#compilationSubmitStatus').text('Код відіслано. Компіляція на сервері завершена.');
            $('#compilationSubmitStatus').attr('style','color:green;');
        }


        $('#ScoSubmit').attr('disabled', true);

        if (this.Compiled == 0) {
            this.finish();
        }
    };

    this.finish = function () {
        this.Compiled--;

        if (this.Compiled <= 0) {
            var scoreScaled = (this.ScoreRaw - this.ScoreMin) / (this.ScoreMax - this.ScoreMin);
            var status = (this.ScoreRaw >= this.PassRank ? "passed" : "failed");

            if ($.rteGetValue("cmi.objectives._count") > 0) {
                $.rteSetValue("cmi.objectives.0.score.min", this.ScoreMin);
                $.rteSetValue("cmi.objectives.0.score.max", this.ScoreMax);
                $.rteSetValue("cmi.objectives.0.score.raw", this.ScoreRaw);
                $.rteSetValue("cmi.objectives.0.score.scaled", scoreScaled);
                $.rteSetValue("cmi.objectives.0.success_status", status);
            }
            
            $.rteSetValue("cmi.score.raw", this.ScoreRaw);
            $.rteSetValue("cmi.score.min", this.ScoreMin);
            $.rteSetValue("cmi.score.max", this.ScoreMax);
            $.rteSetValue("cmi.score.scaled", scoreScaled);
            $.rteSetValue("cmi.completion_status", "completed");
            $.rteSetValue("cmi.success_status", status);

            $.rteSetValue("cmi.exit", "normal");
            //$.rteSetValue("adl.nav.request", "continue");

            $.rteCommit();
            $.rteTerminate();
        }
    };
    
    this.init(_questions);
};
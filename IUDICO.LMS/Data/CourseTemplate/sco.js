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

            $.rteSetValue("cmi.interactions." + (i + 1) + ".id", obj.Questions[i].Id);
            $.rteSetValue("cmi.interactions." + (i + 1) + ".type", obj.Questions[i].getType());
            $.rteSetValue("cmi.interactions." + (i + 1) + ".correct_responses.0.pattern", obj.Questions[i].getCorrectAnswer());

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
    };

    this.count = function () {
        return this.Questions.length;
    };

    this.submit = function () {
        for (var i = 0; i < this.count(); i++) {
            if (this.tests[i].CompiledTest == true) {
                this.tests[i].processAnswer(this, i);

                this.compiled++;
            }
            else {
                $.rteSetValue("cmi.interactions." + (i+1) + ".learner_response", this.tests[i].getAnswer());
                $.rteSetValue("cmi.interactions." + (i+1) + ".result", this.tests[i].getResult());

                this.ScoreRaw += this.tests[i].getScoreRaw();
            }

            this.ScoreMin += this.tests[i].getScoreMin();
            this.ScoreMax += this.tests[i].getScoreMax();
        }

        $('#ScoSubmit').attr('disabled', true);

        if (this.compiled == 0) {
            this.finish();
        }
    };

    this.finish = function () {
        this.compiled--;

        if (this.compiled <= 0) {
            var scoreScaled = (this.ScoreRaw - this.ScoreMin) / (this.ScoreMax - this.ScoreMin);
            var status = (this.ScoreRaw >= this.Passrank ? "passed" : "failed");

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

            $.rteSetValue("cmi.exit", "suspend");
            $.rteSetValue("adl.nav.request", "continue");

            $.rteCommit();
            $.rteTerminate();
        }
    };
	
	this.init(_questions);
};
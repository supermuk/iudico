using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using IUDICO.DataModel.Common.StatisticUtils;
using IUDICO.DataModel.Common.StudentUtils;
using IUDICO.DataModel.Common.TestRequestUtils;
using IUDICO.DataModel.DB;

namespace IUDICO.DataModel.Common.TestingUtils
{
    public class AnswerFiller
    {
        private readonly IList<TblQuestions> _questions;

        private readonly bool _fillUserAnswer;

        private readonly bool _fillCorrectAnswer;

        private readonly int _userId;

        public AnswerFiller(HttpRequest request)
        {
            var page = ServerModel.DB.Load<TblPages>(TestRequestParser.GetPageId(request));

            _questions = ServerModel.DB.Load<TblQuestions>(ServerModel.DB.LookupIds<TblQuestions>(page, null));

            _fillUserAnswer = RequestConditionChecker.DoFillUserAnswers(request);

            _fillCorrectAnswer = RequestConditionChecker.DoFillCorrectAnswers(request);

            _userId = TestRequestParser.GetUserId(request);
        }

        private string GetAnswerForQuestion(string name)
        {
            var q = FindQuestion(name);

            if (_fillUserAnswer && _fillCorrectAnswer)
                throw new Exception("Someone try to cheat");


            if (_fillUserAnswer)
                return FindAnswer(_userId, q);


            if (_fillCorrectAnswer)
                if (ServerModel.User.Current.Islector())
                    return q.CorrectAnswer;

            return string.Empty;
        }

        private TblQuestions FindQuestion(string name)
        {
            foreach (var c in _questions)
            {
                if (c.TestName.Equals(name))
                {
                    _questions.Remove(c);
                    return c;
                }
            }
            return null;
        }

        private static string FindAnswer(int userId, TblQuestions question)
        {
            var answersForQuestion =
                ServerModel.DB.Load<TblUserAnswers>(ServerModel.DB.LookupIds<TblUserAnswers>(question, null));

            var answers = new List<TblUserAnswers>();

            foreach (var ans in answersForQuestion)
            {
                if (ans.UserRef == userId)
                    answers.Add(ans);
            }

            if (answers.Count > 0)
            {
                var lstUserAnswer = StatisticManager.FindLatestUserAnswer(answers);

                if (lstUserAnswer != null)
                    return lstUserAnswer.UserAnswer;
            }

            return string.Empty;
        }

        public void SetAnswer(TextBox control)
        {
            control.Text = GetAnswerForQuestion(control.ID);
        }
       
        public void SetAnswer(string id, params RadioButton[] list)
        {
            var answer = GetAnswerForQuestion(id);

            if (!answer.Equals(string.Empty))
            {
                for (int i = 0; i < list.Length; i++)
                    list[i].Checked = answer[i].Equals('1');
            }
        }
        
        public void SetAnswer(string id, params CheckBox[] list)
        {
            var answer = GetAnswerForQuestion(id);

            if (!answer.Equals(string.Empty))
            {
                for (int i = 0; i < list.Length; i++)
                    list[i].Checked = answer[i].Equals('1');
            }
        }
    }
}
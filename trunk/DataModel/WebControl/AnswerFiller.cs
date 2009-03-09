using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.DB;

namespace IUDICO.DataModel.WebControl
{
    public class AnswerFiller
    {
        private readonly IList<TblQuestions> questions;

        private readonly bool showLatestAnswer;

        public AnswerFiller(int pageId, HttpRequest request)
        {
            var page = ServerModel.DB.Load<TblPages>(pageId);
            questions = ServerModel.DB.Load<TblQuestions>(ServerModel.DB.LookupIds<TblQuestions>(page, null));
            showLatestAnswer = (request["answers"] == "user");
        }

        private string GetAnswerForQuestion(string name)
        {
            var q = FindQuestion(name);

            if (showLatestAnswer)
                return FindAnswer(ServerModel.User.Current.ID, q);

            return q.CorrectAnswer;
        }

        private TblQuestions FindQuestion(string name)
        {
            foreach (var c in questions)
            {
                if (c.TestName.Equals(name))
                {
                    questions.Remove(c);
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
            return (new LatestUserAnswerFinder()).FindUserAnswer(answers).UserAnswer;
        }

        public void SetAnswer(TextBox control)
        {
            control.Text = GetAnswerForQuestion(control.ID);
        }
       
        public void SetAnswer(string id, params RadioButton[] list)
        {
            var answer = GetAnswerForQuestion(id);

            for (int i = 0; i < list.Length; i++)
            {
                list[i].Checked = answer[i].Equals('1');
            }
        }
        
        public void SetAnswer(string id, params CheckBox[] list)
        {
            var answer = GetAnswerForQuestion(id);

            for (int i = 0; i < list.Length; i++)
            {
                list[i].Checked = answer[i].Equals('1');
            }
        }
    }
}

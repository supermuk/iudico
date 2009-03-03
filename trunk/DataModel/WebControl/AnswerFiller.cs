using System.Collections.Generic;
using System.Web.UI.WebControls;
using IUDICO.DataModel.DB;

namespace IUDICO.DataModel.WebControl
{
    public class AnswerFiller
    {
        private readonly IList<TblQuestions> questions;

        public AnswerFiller(int pageId)
        {
            var page = ServerModel.DB.Load<TblPages>(pageId);
            questions = ServerModel.DB.Load<TblQuestions>(ServerModel.DB.LookupIds<TblQuestions>(page, null));
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

        public void SetAnswer(TextBox control)
        {
            control.Text = FindQuestion(control.ID).CorrectAnswer;
        }
       
        public void SetAnswer(string id, params RadioButton[] list)
        {
            var answer = FindQuestion(id).CorrectAnswer;

            for (int i = 0; i < list.Length; i++)
            {
                list[i].Checked = answer[i].Equals('1');
            }
        }
        
        public void SetAnswer(string id, params CheckBox[] list)
        {
            var answer = FindQuestion(id).CorrectAnswer;

            for (int i = 0; i < list.Length; i++)
            {
                list[i].Checked = answer[i].Equals('1');
            }
        }
    }
}

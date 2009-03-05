using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.ImportManagers;
using IUDICO.DataModel.ImportManagers.RemoveManager;
using IUDICO.DataModel.Security;
using System.Data;

namespace IUDICO.DataModel.Controllers
{
    public class StatisticShowController : ControllerBase
    {
        [ControllerParameter]
        public int GroupID;
        [ControllerParameter]
        public int CurriculumID;

        TblCurriculums curriculum;
        TblGroups group;


        public Label NotifyLabel { get; set; }
        public Table StatisticTable { get; set; }

        public void PageLoad(object sender, EventArgs e)
        {
            curriculum = ServerModel.DB.Load<TblCurriculums>(CurriculumID);
            group = ServerModel.DB.Load<TblGroups>(GroupID);

            NotifyLabel.Text = "This is statisic for group: " + group.Name + " based on curriculum: " + curriculum.Name;

            fillStatisticTable();
        }

        private void fillStatisticTable()
        {
            StatisticTable.Rows.Clear();

            TableHeaderRow headerRow = new TableHeaderRow();

            TableHeaderCell headerCell = new TableHeaderCell();
            headerCell.Text = "Student";

            headerRow.Cells.Add(headerCell);


            foreach (TblStages stage in TeacherHelper.StagesForCurriculum(curriculum))
            {
                foreach (TblThemes theme in TeacherHelper.ThemesForStage(stage))
                {
                    headerCell = new TableHeaderCell();
                    headerCell.Text = theme.Name;
                    headerRow.Cells.Add(headerCell);
                }
            }
            headerCell = new TableHeaderCell();
            headerCell.Text = "Total";
            headerRow.Cells.Add(headerCell);

            StatisticTable.Rows.Add(headerRow);

            foreach (TblUsers student in TeacherHelper.GetStudentsOfGroup(group))
            {
                TableRow studentRow = new TableRow();
                TableCell studentCell = new TableHeaderCell();
                studentCell.Text = student.DisplayName;

                studentRow.Cells.Add(studentCell);

                int pasedCurriculum = 0;
                int totalCurriculum = 0;
                foreach (TblStages stage in TeacherHelper.StagesForCurriculum(curriculum))
                {


                    foreach (TblThemes theme in TeacherHelper.ThemesForStage(stage))
                    {
                        studentCell = new TableHeaderCell();
                        int pasedPages = 0;
                        int totalPages = 0;
                        foreach (TblPages page in TeacherHelper.PagesOfTheme(theme))
                        {
                            int userRank = 0;
                            foreach (TblQuestions question in TeacherHelper.QuestionsOfPage(page))
                            {
                                TblUserAnswers userAnswer = TeacherHelper.GetUserAnswerForQuestion(student, question);
                                if (userAnswer != null)
                                {
                                    if (userAnswer.IsCompiledAnswer)
                                    {
                                       
                                    }
                                    else
                                    {
                                        if (userAnswer.UserAnswer == question.CorrectAnswer)
                                        {
                                            userRank += question.Rank.HasValue ? question.Rank.Value : 0;
                                        }
                                    }
                                }
                            }
                            if (page.PageRank.HasValue)
                            {
                                totalPages++;
                                if (userRank >= page.PageRank.Value)
                                {
                                    pasedPages++;
                                }

                            }
                        }

                        studentCell.Text = pasedPages.ToString() + "/" + totalPages.ToString();
                        pasedCurriculum += pasedPages;
                        totalCurriculum += totalPages;
                        studentRow.Cells.Add(studentCell);
                    }
                }

                studentCell = new TableHeaderCell();
                studentCell.Text = pasedCurriculum.ToString() + "/" + totalCurriculum.ToString();
                studentRow.Cells.Add(studentCell);
                StatisticTable.Rows.Add(studentRow);
            }

            if (StatisticTable.Rows.Count == 1)
            {
                StatisticTable.Visible = false;
                NotifyLabel.Text = "Theare are no students in this group.";
            }
        }

    }
}

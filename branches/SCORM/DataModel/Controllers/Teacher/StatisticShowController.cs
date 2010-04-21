using System.Collections.Generic;
using System.Web.UI.WebControls;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.Common.StatisticUtils;
using IUDICO.DataModel.Controllers.Student;
using IUDICO.DataModel.DB;
using System.Linq;
using System;
using System.Collections;
using System.Drawing;
using IUDICO.DataModel.DB.Base;
using LEX.CONTROLS;
using LEX.CONTROLS.Expressions;

namespace IUDICO.DataModel.Controllers
{
    public class StatisticShowController : BaseTeacherController
    {
        [ControllerParameter]
        public int GroupID;
        [ControllerParameter]
        public int CurriculumID;
        [ControllerParameter]
        public int UserId;
        [PersistantField]
        public readonly IVariable<string> Find_StudName = string.Empty.AsVariable();
        TblCurriculums curriculum;
        TblGroups group;
        TblUsers user;

        //"magic words"
        private const string pageCaption = "Statistic.";
        private const string pageDescription = "This is statisic for group: {0} based on curriculum: {1}. This statistaic is based on passed pages count.";
        private const string studentStr = "Student";

        private const string totalStr = "Total";
        private const string noStudents = "Theare are no students in this group.";

        public Label NotifyLabel { get; set; }
        public Table StatisticTable { get; set; }

        public Table StatisticTable_constant { get; set; }

        public override void Loaded()
        {
            base.Loaded();

            curriculum = ServerModel.DB.Load<TblCurriculums>(CurriculumID);
            group = ServerModel.DB.Load<TblGroups>(GroupID);

            Caption.Value = pageCaption;
            Description.Value = pageDescription.
                Replace("{0}", group.Name).
                Replace("{1}", curriculum.Name);
            Title.Value = Caption.Value;

            fillStatisticTable();
        }
        public void Button_Sort_Click()
        {
            StatisticTable_constant.Rows.Clear();
            StatisticTable_constant = TeacherHelper.Sort(StatisticTable, curriculum);
            StatisticTable.Rows.Clear();
            for (int i = 0; i < StatisticTable_constant.Rows.Count; i++)
            {
                StatisticTable.Rows.Add(StatisticTable_constant.Rows[i]);
                i--;
            }
        }
        public void Button_FindStud_Click()
        {


            StatisticTable_constant = TeacherHelper.Search_Function(StatisticTable, Find_StudName.Value, curriculum, null, 0, "");

            StatisticTable.Rows.Clear();
            for (int i = 0; i < StatisticTable_constant.Rows.Count; i++)
            {
                StatisticTable.Rows.Add(StatisticTable_constant.Rows[i]);
                i--;
            }
        }

        private void fillStatisticTable()
        {
            IList<TblUsers> ilistusers;
            ilistusers = TeacherHelper.GetStudentsOfGroup(group);
            if (UserId > 0)
            {
                user = ServerModel.DB.Load<TblUsers>(UserId);
                ilistusers.Clear();
                ilistusers.Add(user);
            }
            StatisticTable.Rows.Clear();

            TableHeaderRow headerRow = new TableHeaderRow();

            TableHeaderCell headerCell = new TableHeaderCell();
            headerCell.Text = studentStr;
            headerRow.Cells.Add(headerCell);

            foreach (TblStages stage in TeacherHelper.StagesOfCurriculum(curriculum))
            {
                foreach (TblThemes theme in TeacherHelper.ThemesOfStage(stage))
                {
                    headerCell = new TableHeaderCell();
                    headerCell.Text = theme.Name;
                    headerRow.Cells.Add(headerCell);
                }
            }
            headerCell = new TableHeaderCell();
            headerCell.Text = totalStr;
            headerRow.Cells.Add(headerCell);

            headerCell = new TableHeaderCell();
            headerCell.Text = "Percent";
            headerRow.Cells.Add(headerCell);

            headerCell = new TableHeaderCell();
            headerCell.Text = "ECTS";
            headerRow.Cells.Add(headerCell);

            StatisticTable.Rows.Add(headerRow);
            foreach (TblUsers student in TeacherHelper.GetStudentsOfGroup(group))
            {
                var studentRow = new TableRow();
                TableCell studentCell = new TableHeaderCell { Text = student.DisplayName };

                studentRow.Cells.Add(studentCell);

                double pasedCurriculum = 0;
                double totalCurriculum = 0;
                foreach (TblStages stage in TeacherHelper.StagesOfCurriculum(curriculum))
                {
                    foreach (TblThemes theme in TeacherHelper.ThemesOfStage(stage))
                    {
                        double result = 0;
                        double totalresult = 0;
                        bool islearnerattempt = false;
                        TblOrganizations organization;
                        organization= ServerModel.DB.Load<TblOrganizations>(theme.OrganizationRef);
                        foreach(TblItems items in TeacherHelper.ItemsOfOrganization(organization))
                        {
                            totalresult +=Convert.ToDouble(items.Rank);
                        }
                       
                        foreach (TblLearnerAttempts attempt in TeacherHelper.AttemptsOfTheme(theme))
                        {
                          if(attempt != null)  islearnerattempt = true;
                            if (attempt.ID == TeacherHelper.GetLastLearnerAttempt(student.ID, theme.ID))
                                foreach (TblLearnerSessions session in TeacherHelper.SessionsOfAttempt(attempt))
                                {
                                    CmiDataModel cmiDataModel = new CmiDataModel(session.ID, student.ID, false);
                                    List<TblVarsInteractions> interactionsCollection = cmiDataModel.GetCollection<TblVarsInteractions>("interactions.*.*");

                                    for (int i = 0, j = 0; i < int.Parse(cmiDataModel.GetValue("interactions._count")); i++)
                                    {
                                        for (; j < interactionsCollection.Count && i == interactionsCollection[j].Number; j++)
                                        {
                                            if (interactionsCollection[j].Name == "result")
                                            {
                                                TblItems itm = ServerModel.DB.Load<TblItems>(session.ItemRef);
                                                if (interactionsCollection[j].Value == "correct") result += Convert.ToDouble(itm.Rank);
                                            }
                                        }

                                    }
                                }
                        }


                       
                        string attmpt = "";
                        if (islearnerattempt == true)
                        {
                            attmpt = "(" + TeacherHelper.GetLastIndexOfAttempts(student.ID, theme.ID).ToString() + " attempt )";
                        }

                        studentCell = new TableCell { HorizontalAlign = HorizontalAlign.Center };
                        studentCell.Controls.Add(new HyperLink
                        {
                            Text = result + "/" + totalresult + attmpt,
                            NavigateUrl = ServerModel.Forms.BuildRedirectUrl(new ThemeResultController
                            {
                                BackUrl = string.Empty,
                                LearnerAttemptId = TeacherHelper.GetLastLearnerAttempt(student.ID, theme.ID),

                            })
                        });

                        if (islearnerattempt == false)
                        {
                            studentCell.Enabled = false;
                            studentCell.BackColor = Color.Yellow;
                        }
                        else studentCell.BackColor = Color.YellowGreen;

                        pasedCurriculum += result;
                        totalCurriculum += totalresult;
                        studentRow.Cells.Add(studentCell);

                    }
                }

                studentCell = new TableCell
                {
                    HorizontalAlign = HorizontalAlign.Center,
                    Text = pasedCurriculum + "/" + totalCurriculum
                };
                studentRow.Cells.Add(studentCell);
                studentCell = new TableCell { HorizontalAlign = HorizontalAlign.Center };
                double temp_total;
                if (totalCurriculum != 0)
                    temp_total = pasedCurriculum / totalCurriculum * 100;
                else temp_total = 0;
                studentCell.Text = (temp_total).ToString("F2");
                studentRow.Cells.Add(studentCell);
                studentCell = new TableCell { HorizontalAlign = HorizontalAlign.Center };
                studentCell.Text = TeacherHelper.ECTS_code(temp_total);

                studentRow.Cells.Add(studentCell);
                StatisticTable.Rows.Add(studentRow);
            }

            if (StatisticTable.Rows.Count == 1)
            {
                StatisticTable.Visible = false;
                Message.Value = noStudents;
            }



        }




    }
}

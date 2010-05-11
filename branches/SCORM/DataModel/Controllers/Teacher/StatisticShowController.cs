using System.Collections.Generic;
using System.Web.UI.WebControls;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.Common.StatisticUtils;
using IUDICO.DataModel.Controllers.Student;
using IUDICO.DataModel.Controllers.Teacher;
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
    /// <summary>
    /// Controller for StatisticShow.aspx page
    /// </summary>
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
        private readonly string pageCaption = Translations.StatisticShowController_pageCaption_Statistic_;
        private readonly string pageDescription = Translations.StatisticShowController_pageDescription_This_is_statisic_for_group___0__based_on_curriculum___1___This_statistaic_is_based_on_passed_pages_count_;
        private readonly string studentStr = Translations.StatisticShowController_studentStr_Student;

        private readonly string totalStr = Translations.StatisticShowController_totalStr_Total;
        private readonly string noStudents = Translations.StatisticShowController_noStudents_Theare_are_no_students_in_this_group_;

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

           StatisticTable_constant = TeacherHelper.Search_Function(StatisticTable, Find_StudName.Value, curriculum, null, GroupID, RawUrl);

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
            headerCell.Text = Translations.StatisticShowController_fillStatisticTable_Percent;
            headerRow.Cells.Add(headerCell);

            headerCell = new TableHeaderCell();
            headerCell.Text = "ECTS";
            headerRow.Cells.Add(headerCell);

            StatisticTable.Rows.Add(headerRow);
            foreach (TblUsers student in ilistusers)
            {
                var studentRow = new TableRow();
                TableCell studentCell = new TableHeaderCell { HorizontalAlign = HorizontalAlign.Center };
                studentCell.Controls.Add(new HyperLink
                {
                    Text = student.DisplayName,
                    NavigateUrl = ServerModel.Forms.BuildRedirectUrl(new StatisticShowGraphController
                    {
                        GroupID = GroupID,
                        CurriculumID = curriculum.ID,
                        UserId = student.ID,
                        BackUrl = RawUrl
                    })
                });


                studentRow.Cells.Add(studentCell);

                double pasedCurriculum = 0;
                double totalCurriculum = 0;
                foreach (TblStages stage in TeacherHelper.StagesOfCurriculum(curriculum))
                {
                    foreach (TblThemes theme in TeacherHelper.ThemesOfStage(stage))
                    {
                        double result = 0;
                        double totalresult = 0;
                        int learnercount = TeacherHelper.GetLastIndexOfAttempts(student.ID, theme.ID);
                        TblOrganizations organization;
                        organization = ServerModel.DB.Load<TblOrganizations>(theme.OrganizationRef);
                        foreach (TblItems items in TeacherHelper.ItemsOfOrganization(organization))
                        {
                            totalresult += Convert.ToDouble(items.Rank);
                        }

                        foreach (TblLearnerAttempts attempt in TeacherHelper.AttemptsOfTheme(theme))
                        {
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
                        if (learnercount > 0)
                        {
                            attmpt = "(" + learnercount.ToString() + " attempt )";
                        }
                        else if (learnercount == 0)
                        {
                            attmpt = "";
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

                        if (learnercount == 0)
                        {
                            studentCell.Enabled = false;
                            studentCell.BackColor = Color.Yellow;
                        }
                        else if (learnercount > 0) studentCell.BackColor = Color.YellowGreen;

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

using System.Collections.Generic;
using System.Web.UI.WebControls;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.Common.StatisticUtils;
using IUDICO.DataModel.Controllers.Student;
using IUDICO.DataModel.DB;
using System.Linq;
using System;
using System.Collections;
using IUDICO.DataModel.DB.Base;
using LEX.CONTROLS;
using LEX.CONTROLS.Expressions;

namespace IUDICO.DataModel.Controllers
{
    public class StatisticShowCurriculumsController : BaseTeacherController
    {
        [ControllerParameter]
        public int GroupID;
        [ControllerParameter]
        public string CurriculumsID;
        [PersistantField]
        public readonly IVariable<string> Find_StudName = string.Empty.AsVariable();
        List<TblCurriculums> curriculums = new List<TblCurriculums>();
        TblGroups group;

        //"magic words"
        private readonly string pageCaption = Translations.StatisticShowCurriculumsController_pageCaption_Statistic_;
        private readonly string pageDescription = Translations.StatisticShowCurriculumsController_pageDescription_This_is_statisic_for_group___0__based_on_curriculums___1__;
        private readonly string studentStr = Translations.StatisticShowCurriculumsController_studentStr_Student;
        private readonly string totalStr = Translations.StatisticShowCurriculumsController_totalStr_Total;
        private readonly string noStudents = Translations.StatisticShowCurriculumsController_noStudents_Theare_are_no_students_in_this_group_;

        public Label NotifyLabel { get; set; }
        public Table StatisticTable { get; set; }
        public Table StatisticTable_constant { get; set; }
        private List<int> ids = new List<int>();

        public List<int> ParsString(string str)
        {
            List<int> lst = new List<int>();
            string tmp = "";
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] != '_')
                {
                    tmp += str[i].ToString();
                }
                else if (str[i] == '_')
                {
                    lst.Add(Convert.ToInt32(tmp));
                    tmp = "";
                }
            }
            return lst;
        }

        public override void Loaded()
        {
            base.Loaded();
            string tmp_curriculums = "";

            group = ServerModel.DB.Load<TblGroups>(GroupID);
            ids = ParsString(CurriculumsID);
            for (int i = 0; i < ids.Count; i++)
            {
                if (ids[i] > 0)
                {
                    curriculums.Add(ServerModel.DB.Load<TblCurriculums>(ids[i]));
                    if(i < ids.Count-1)tmp_curriculums += curriculums[i].Name + ",";
                    if (i == ids.Count - 1) tmp_curriculums += curriculums[i].Name + " ";
                }
            }

           

            Caption.Value = pageCaption;
            Description.Value = pageDescription.
                Replace("{0}", group.Name).
                Replace("{1}", tmp_curriculums);
            Title.Value = Caption.Value;

            fillStatisticTable();
        }
        public void Button_FindStud_Click()
        {
            StatisticTable_constant = TeacherHelper.Search_Function(StatisticTable, Find_StudName.Value, null, curriculums, GroupID, RawUrl);

            StatisticTable.Rows.Clear();
            for (int i = 0; i < StatisticTable_constant.Rows.Count; i++)
            {
                StatisticTable.Rows.Add(StatisticTable_constant.Rows[i]);
                i--;
            }
        }
        public void Button_Sort_Click()
        {
            StatisticTable_constant.Rows.Clear();
            //StatisticTable_constant = TeacherHelper.Sort(StatisticTable, curriculum);
            StatisticTable.Rows.Clear();
            for (int i = 0; i < StatisticTable_constant.Rows.Count; i++)
            {
                StatisticTable.Rows.Add(StatisticTable_constant.Rows[i]);
                i--;
            }
        }
        private void fillStatisticTable()
        {
            StatisticTable.Rows.Clear();

            TableHeaderRow headerRow = new TableHeaderRow();

            TableHeaderCell headerCell = new TableHeaderCell();
            headerCell.Text = studentStr;
            headerRow.Cells.Add(headerCell);


            foreach (TblCurriculums curr in curriculums)
            {
                headerCell = new TableHeaderCell { HorizontalAlign = HorizontalAlign.Center };


                headerCell.Controls.Add(new HyperLink
                {
                    Text = curr.Name,
                    NavigateUrl = ServerModel.Forms.BuildRedirectUrl(new StatisticShowController
                    {
                        GroupID = GroupID,
                        CurriculumID = curr.ID,
                        BackUrl = RawUrl
                    })
                });
                headerRow.Cells.Add(headerCell);
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

            foreach (TblUsers student in TeacherHelper.GetStudentsOfGroup(group))
            {
                var studentRow = new TableRow();
                TableCell studentCell = new TableHeaderCell { Text = student.DisplayName };

                studentRow.Cells.Add(studentCell);

                double pasedCurriculums = 0;
                double totalCurriculums = 0;
                foreach (TblCurriculums curr in curriculums)
                {
                    double pasedCurriculum = 0;
                    double totalCurriculum = 0;
                    foreach (TblStages stage in TeacherHelper.StagesOfCurriculum(curr))
                    {
                        foreach (TblThemes theme in TeacherHelper.ThemesOfStage(stage))
                        {
                            double result = 0;
                            double totalresult = 0;
                            bool islearnerattempt = false;
                            TblOrganizations organization;
                            organization = ServerModel.DB.Load<TblOrganizations>(theme.OrganizationRef);
                            foreach (TblItems items in TeacherHelper.ItemsOfOrganization(organization))
                            {
                                totalresult += Convert.ToDouble(items.Rank);
                            }
                            foreach (TblLearnerAttempts attempt in TeacherHelper.AttemptsOfTheme(theme))
                            {
                                if (attempt != null) islearnerattempt = true;
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
                            pasedCurriculum += result;
                            totalCurriculum += totalresult;
                        }
                    }
                    studentCell = new TableCell { HorizontalAlign = HorizontalAlign.Center };

                    studentCell.Controls.Add(new HyperLink
                    {
                        Text = pasedCurriculum + "/" + totalCurriculum,
                        NavigateUrl = ServerModel.Forms.BuildRedirectUrl(new StatisticShowController
                        {
                            GroupID = GroupID,
                            CurriculumID = curr.ID,
                            UserId = student.ID,
                            BackUrl = RawUrl
                        })
                    });
                    studentRow.Cells.Add(studentCell);

                    pasedCurriculums += pasedCurriculum;
                    totalCurriculums += totalCurriculum;


                }
                studentCell = new TableCell
                {
                    HorizontalAlign = HorizontalAlign.Center,
                    Text = pasedCurriculums + "/" + totalCurriculums
                };
                studentRow.Cells.Add(studentCell);
                studentCell = new TableCell { HorizontalAlign = HorizontalAlign.Center };
                double temp_total;
                if (totalCurriculums != 0)
                    temp_total = pasedCurriculums / totalCurriculums * 100;
                else temp_total = 0;
                studentCell.Text = (temp_total).ToString("F2");
                studentRow.Cells.Add(studentCell);
                studentCell = new TableCell { HorizontalAlign = HorizontalAlign.Center };
                studentCell.Text = TeacherHelper.ECTS_code(temp_total);
                studentRow.Cells.Add(studentCell);
                StatisticTable.Rows.Add(studentRow);
                StatisticTable.HorizontalAlign = HorizontalAlign.Center;
            }

            if (StatisticTable.Rows.Count == 1)
            {
                StatisticTable.Visible = false;
                Message.Value = noStudents;
            }
        }
    }
}

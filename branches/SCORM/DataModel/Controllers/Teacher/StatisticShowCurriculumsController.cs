using System.Collections.Generic;
using System.Web.UI.WebControls;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.Common.StatisticUtils;
using IUDICO.DataModel.Controllers.Student;
using IUDICO.DataModel.DB;
using System.Linq;
using System;
using System.Collections;

namespace IUDICO.DataModel.Controllers
{
   public class StatisticShowCurriculumsController: BaseTeacherController
    {
        [ControllerParameter]
        public int GroupID;
        [ControllerParameter]
        public string CurriculumsID;
 
        List<TblCurriculums> curriculums = new List<TblCurriculums>();
        TblGroups group;

        //"magic words"
        private const string pageCaption = "Statistic.";
        private const string pageDescription = "This is statisic for group: {0} based on curriculums: {1}.";
        private const string studentStr = "Student";
        private const string totalStr = "Total";
        private const string noStudents = "Theare are no students in this group.";

        public Label NotifyLabel { get; set; }
        public Table StatisticTable { get; set; }
        private List<int> ids  = new List<int>();

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
               if(ids[i] > 0)
               {
                curriculums.Add( ServerModel.DB.Load<TblCurriculums>(ids[i]));
                tmp_curriculums += curriculums[i].Name+",";
                }
            }

            if(tmp_curriculums.Length > 0)
            tmp_curriculums.Replace(tmp_curriculums[tmp_curriculums.Length-1],' ');

                Caption.Value = pageCaption;
            Description.Value = pageDescription.
                Replace("{0}", group.Name).
                Replace("{1}", tmp_curriculums);
            Title.Value = Caption.Value;

            fillStatisticTable();
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
                    headerCell = new TableHeaderCell();
                    headerCell.Text = curr.Name;
                    headerRow.Cells.Add(headerCell);
                }
            
            headerCell = new TableHeaderCell();
            headerCell.Text = totalStr;
            headerRow.Cells.Add(headerCell);

            StatisticTable.Rows.Add(headerRow);

            foreach (TblUsers student in TeacherHelper.GetStudentsOfGroup(group))
            {
                var studentRow = new TableRow();
                TableCell studentCell = new TableHeaderCell { Text = student.DisplayName };

                studentRow.Cells.Add(studentCell);

                int pasedCurriculums = 0;
                int totalCurriculums = 0;
                foreach (TblCurriculums curr in curriculums)
                {
                    int pasedCurriculum = 0;
                    int totalCurriculum = 0;
                    foreach (TblStages stage in TeacherHelper.StagesOfCurriculum(curr))
                    {
                        foreach (TblThemes theme in TeacherHelper.ThemesOfStage(stage))
                        {
                            int result = 0;
                            int totalresult = 0;
                            foreach (TblLearnerAttempts attempt in TeacherHelper.AttemptsOfTheme(theme))
                            {

                                if (attempt.ID == TeacherHelper.GetLastLearnerAttempt(student.ID, theme.ID))
                                    foreach (TblLearnerSessions session in TeacherHelper.SessionsOfAttempt(attempt))
                                    {

                                        string res = "";

                                        CmiDataModel cmiDataModel = new CmiDataModel(session.ID, student.ID, false);
                                        for (int i = 0; i < int.Parse(cmiDataModel.GetValue("interactions._count")); i++)
                                        {

                                            res = cmiDataModel.GetValue(String.Format("interactions.{0}.result", i));
                                            totalresult += 1;
                                            if (res == "correct")
                                            {
                                                result += 1;

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

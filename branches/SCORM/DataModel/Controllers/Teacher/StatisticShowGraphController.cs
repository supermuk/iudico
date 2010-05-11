using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.Common.StatisticUtils;
using IUDICO.DataModel.Controllers.Student;
using IUDICO.DataModel.Controllers.Teacher;
using IUDICO.DataModel.DB;
using System.Collections;
using IUDICO.DataModel.DB.Base;
using LEX.CONTROLS;
using LEX.CONTROLS.Expressions;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using Excel = Microsoft.Office.Interop.Excel;





namespace IUDICO.DataModel.Controllers.Teacher
{
    public class StatisticShowGraphController : BaseTeacherController
    {
        [ControllerParameter]
        public int GroupID;
        [ControllerParameter]
        public int CurriculumID;
        [ControllerParameter]
        public int UserId;
        public TblCurriculums curriculum;
        public TblGroups group;
        public TblUsers user;

        public List<string> Name_Stage = new List<string>();
        public List<double> Student_Stage_Count = new List<double>();
        public List<double> Total_Stage_Count = new List<double>();
        private readonly string pageCaption = Translations.StatisticShowController_pageCaption_Statistic_;
        private readonly string pageDescription = Translations.StatisticShowController_pageDescription_This_is_statisic_for_group___0__based_on_curriculum___1___This_statistaic_is_based_on_passed_pages_count_;


        public List<string> Get_Name_Stage()
        {
            return Name_Stage;
        }
        public List<double> Get_Student_Stage_Count()
        {
            return Student_Stage_Count;
        }
        public List<double> Get_Total_Stage_Count()
        {
            return Total_Stage_Count;
        }
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
            fillStatistic();
        }
        public void Saveto_Excel_Click()
        {

            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            // xlApp.Visible = true;
            //xlApp.UserControl = true;
            System.Globalization.CultureInfo oldCI = System.Threading.Thread.CurrentThread.CurrentCulture;
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");

            xlApp = new Excel.ApplicationClass();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            //add data 


            xlWorkSheet.Cells[2, 1] = user.LastName;
            xlWorkSheet.Cells[3, 1] = "Total Points";
            for (int i = 1; i <= Name_Stage.Count; i++)
                xlWorkSheet.Cells[1, i + 1] = Name_Stage[i - 1];
            for (int j = 1; j <= Student_Stage_Count.Count; j++)
                xlWorkSheet.Cells[2, j + 1] = Student_Stage_Count[j - 1].ToString();
            for (int j = 1; j <= Total_Stage_Count.Count; j++)
                xlWorkSheet.Cells[3, j + 1] = Total_Stage_Count[j - 1].ToString();



            Excel.Range chartRange;

            Excel.ChartObjects xlCharts = (Excel.ChartObjects)xlWorkSheet.ChartObjects(Type.Missing);
            Excel.ChartObject myChart = (Excel.ChartObject)xlCharts.Add(10, 80, 300, 250);
            Excel.Chart chartPage = myChart.Chart;

            chartRange = xlWorkSheet.get_Range("g3", "a1");
            chartPage.SetSourceData(chartRange, misValue);
            chartPage.ChartType = Excel.XlChartType.xlColumnClustered;
            string temp_name = "Statistic for studet " + user.LastName + " for curriculum " + curriculum.Name + ".xls";
            xlWorkBook.SaveAs(temp_name, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

        }

        public void fillStatistic()
        {
            double totalstudntresoult = 0;
            double totalstagerank = 0;
            user = ServerModel.DB.Load<TblUsers>(UserId);
            curriculum = ServerModel.DB.Load<TblCurriculums>(CurriculumID);
            foreach (TblStages stage in TeacherHelper.StagesOfCurriculum(curriculum))
            {

                foreach (TblThemes theme in TeacherHelper.ThemesOfStage(stage))
                {
                    double result = 0; Name_Stage.Add(theme.Name);
                    double totalresult = 0;
                    int learnercount = TeacherHelper.GetLastIndexOfAttempts(user.ID, theme.ID);
                    TblOrganizations organization;
                    organization = ServerModel.DB.Load<TblOrganizations>(theme.OrganizationRef);
                    foreach (TblItems items in TeacherHelper.ItemsOfOrganization(organization))
                    {
                        totalresult += Convert.ToDouble(items.Rank);
                    }

                    foreach (TblLearnerAttempts attempt in TeacherHelper.AttemptsOfTheme(theme))
                    {
                        if (attempt.ID == TeacherHelper.GetLastLearnerAttempt(user.ID, theme.ID))

                            foreach (TblLearnerSessions session in TeacherHelper.SessionsOfAttempt(attempt))
                            {
                                CmiDataModel cmiDataModel = new CmiDataModel(session.ID, user.ID, false);
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
                    totalstudntresoult += result;
                    totalstagerank += totalresult;
                    Student_Stage_Count.Add(result);
                    Total_Stage_Count.Add(totalresult);
                }
            }

            Name_Stage.Add("Total");
            Student_Stage_Count.Add(totalstudntresoult);
            Total_Stage_Count.Add(totalstagerank);


            Saveto_Excel_Click();
        }
    }
}

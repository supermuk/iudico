using System;
using System.Collections.Generic;
using System.Linq;
using IUDICO.Common.Models.Services;

namespace IUDICO.Statistics.Models.Storage
{
    public class StatisticsStorage : IStatisticsStorage, IStatisticsService
    {

    }


    //Vitalik
    public class ThemeInfoModel
    {
        private InfoOnFirstPage Info;
        public List<Student> SelectStudents;
        public List<Theme> SelectCurriculumThemes;
        public ThemeInfoModel()
        {
            Info = new InfoOnFirstPage();
            Info.SetFakeData();
            SelectStudents = new List<Student>();
            SelectCurriculumThemes = new List<Theme>();
        }
        public void BuildFrom(Int32 CurriculumID, Int32 SelectedGroupID)
        {
            foreach (Student student in GetGroupByID(Info, SelectedGroupID).Students)
            {
                SelectStudents.Add(student);
            }
            foreach (Theme theme in GetCurriculumByID(Info, CurriculumID).Themes)
            {
                SelectCurriculumThemes.Add(theme);
            }
        }
        public int GetStudentResautForTheme(Student SelectStudent, Theme SelectTheme)
        {
            foreach (StudentThemeResult themeResault in Info.StudentThemeResult)
            {
                if (themeResault.Stud == SelectStudent & themeResault.ThemeId == SelectTheme.ThemeId)
                    return themeResault.StudentResult;
            }
            return 0;
        }
        public int GetStudentResautForAllThemesInSelectedCurriculum(Student SelectStudent)
        {
            int res = 0;
            foreach (StudentThemeResult themeResault in Info.StudentThemeResult)
            {
                foreach (Theme theme in this.SelectCurriculumThemes)
                {
                    if (themeResault.Stud == SelectStudent & themeResault.ThemeId == theme.ThemeId)
                        res += themeResault.StudentResult;
                }
            }
            return res;
        }
        public int GetAllThemesInSelectedCurriculumMaxMark()
        {
            int res = 0;
            foreach (Theme theme in this.SelectCurriculumThemes)
            {
                res += theme.MaxPoint;
            }
            return res;
        }
        private Curriculum GetCurriculumByID(InfoOnFirstPage findInfo, Int32 findCurriculumID)
        {
            foreach (Curriculum i in findInfo.Curriculums)
            {
                if (i.CurriculumId == findCurriculumID)
                    return i;
            }
            return null;
        }
        private Group GetGroupByID(InfoOnFirstPage findInfo, Int32 SelectedGroupID)
        {
            foreach (Group i in findInfo.Group)
            {
                if (i.GroupId == SelectedGroupID)
                    return i;
            }
            return null;
        }
        public char Ects(double percent)
        {
            if (percent > 91.0)
            {
                return 'A';
            }
            else if (percent > 81.0)
            {
                return 'B';
            }
            else if (percent > 71.0)
            {
                return 'C';
            }
            else if (percent > 61.0)
            {
                return 'D';
            }
            else if (percent > 51.0)
            {
                return 'E';
            }
            else
            {
                return 'F';
            }
        }
    }
    public class ThemeTestResaultsModel
    {
        private InfoOnFirstPage Info;
        public Student SelectStudent;
        public Theme SelectTheme;
        public StudentThemeResult themeResault;
        public List<StudentQuestionResault> Answers;

        public ThemeTestResaultsModel()
        {
            Info = new InfoOnFirstPage();
            Info.SetFakeData();
            Answers = new List<StudentQuestionResault>();
            SelectStudent = null;
            SelectTheme = null;
            themeResault = null;
        }
        public void BuildFrom(Int32 StudentID, Int32 ThemeID)
        {
            SelectStudent = GetStudentByID(Info, StudentID);
            SelectTheme = GetThemeByID(Info, ThemeID);
            themeResault = GetThemeResaultByID(Info, SelectStudent, SelectTheme);
            foreach (StudentQuestionResault questResault in Info.QuestionResault)
            {
                if (questResault.Stud == SelectStudent & questResault.ThemeID == SelectTheme.ThemeId)
                {
                    Answers.Add(questResault);
                }
            }
        }
        private StudentThemeResult GetThemeResaultByID(InfoOnFirstPage findInfo, Student student, Theme theme)
        {
            foreach (StudentThemeResult res in findInfo.StudentThemeResult)
            {
                if (res.Stud == student & res.ThemeId == theme.ThemeId)
                    return res;
            }
            return null;
        }
        private Student GetStudentByID(InfoOnFirstPage findInfo, Int32 StudentID)
        {
            foreach (Group Group in findInfo.Group)
            {
                foreach (Student st in Group.Students)
                {
                    if (st.StudentId == StudentID)
                        return st;
                }
            }
            return null;
        }
        private Theme GetThemeByID(InfoOnFirstPage findInfo, Int32 ThemeID)
        {
            foreach (Theme theme in findInfo.Themes)
            {
                if (theme.ThemeId == ThemeID)
                    return theme;
            }
            return null;
        }
    }
    public class StudentQuestionResault
    {
        public Student Stud { get; set; }
        public int ThemeID { get; set; }
        public int Costs { get; set; }
        public string StudentAnswer { get; set; }
        public string RightAnswer { get; set; }
    }
    //Roma
    public class Curriculum
    {
        public string CurriculumName { get; set; }
        public int CurriculumId { get; set; }
        public int[] GroupsId { get; set; }
        public Theme[] Themes { get; set; }

        public int GetMaxPointsFromCurriculum()
        {
            return Themes.Sum(th => th.MaxPoint);
        }
    }

    public class Group
    {
        public int GroupId { get; set; }
        public Student[] Students { get; set; }
    }

    public class Student
    {
        public string Name { get; set; }
        public int StudentId { get; set; }
    }

    public class Theme
    {
        public int ThemeId { get; set; }
        public string Name { get; set; }
        public int MaxPoint { get; set; }
    }

    public class StudentThemeResult
    {
        public Student Stud { get; set; }
        public int ThemeId { get; set; }
        public int StudentResult { get; set; }
    }

    public class StudentCurriculumResult
    {
        public Student Stud { get; set; }
        public int CurriculumId { get; set; }
        public int StudentResult { get; set; }
    }

    public class InfoOnFirstPage
    {
        public List<Theme> Themes;
        public List<Curriculum> Curriculums;
        public Curriculum FirstCurriculum;
        public Curriculum SecondCurriculum;
        public Group[] Group;
        public List<StudentThemeResult> StudentThemeResult;
        public List<StudentCurriculumResult> StudentCurriculumResult;
        public List<string> GroupList;
        public List<StudentQuestionResault> QuestionResault;

        public void SetFakeData()
        {
            //Students creation
            Student st1 = new Student() { StudentId = 1, Name = "Roman (1)" };
            Student st2 = new Student() { StudentId = 2, Name = "Ivan (1)" };
            Student st3 = new Student() { StudentId = 3, Name = "Bogdan (1)" };
            Student st4 = new Student() { StudentId = 4, Name = "Stepan (1)" };
            Student st5 = new Student() { StudentId = 5, Name = "Petro (2)" };
            Student st6 = new Student() { StudentId = 6, Name = "Stas (2)" };
            Student st7 = new Student() { StudentId = 7, Name = "Andrij (2)" };
            Student st8 = new Student() { StudentId = 8, Name = "Sergij (2)" };

            //groups creation
            Group = new Group[]
            {
                new Group() { GroupId = 1, Students = new Student[] { st1, st2, st3, st4 } },
                new Group() { GroupId = 2, Students = new Student[] { st5, st6, st7, st8 } }
            };

            //Themes creation
            Theme th1 = new Theme() { ThemeId = 1, Name = "C++", MaxPoint = 75 };
            Theme th2 = new Theme() { ThemeId = 2, Name = "C#", MaxPoint = 25 };
            Theme th3 = new Theme() { ThemeId = 3, Name = "Algebra", MaxPoint = 100 };
            Theme th4 = new Theme() { ThemeId = 4, Name = "Geometry", MaxPoint = 100 };
            Themes = new List<Theme>();
            Themes.Add(th1);
            Themes.Add(th2);
            Themes.Add(th3);
            Themes.Add(th4);
            //Curriculums creation
            Curriculums = new List<Curriculum>();
            Curriculums.Add(new Curriculum() { CurriculumId = 1, CurriculumName = "Programing", GroupsId = new int[] { 1, 2 }, Themes = new Theme[] { th1, th2 } });
            Curriculums.Add(new Curriculum() { CurriculumId = 2, CurriculumName = "Mathematic", GroupsId = new int[] { 1 }, Themes = new Theme[] { th3, th4 } });

            //StudentCurriculumResaults creation
            StudentCurriculumResult = new List<StudentCurriculumResult>();
            #region StudentsCurriculumResult
            //курс Programing група 1
            StudentCurriculumResult.Add(new StudentCurriculumResult() { CurriculumId = 1, Stud = st1, StudentResult = 95 });
            StudentCurriculumResult.Add(new StudentCurriculumResult() { CurriculumId = 1, Stud = st2, StudentResult = 80 });
            StudentCurriculumResult.Add(new StudentCurriculumResult() { CurriculumId = 1, Stud = st3, StudentResult = 65 });
            StudentCurriculumResult.Add(new StudentCurriculumResult() { CurriculumId = 1, Stud = st4, StudentResult = 51 });
            //курс Programing група 2
            StudentCurriculumResult.Add(new StudentCurriculumResult() { CurriculumId = 1, Stud = st5, StudentResult = 52 });
            StudentCurriculumResult.Add(new StudentCurriculumResult() { CurriculumId = 1, Stud = st6, StudentResult = 70 });
            StudentCurriculumResult.Add(new StudentCurriculumResult() { CurriculumId = 1, Stud = st7, StudentResult = 60 });
            StudentCurriculumResult.Add(new StudentCurriculumResult() { CurriculumId = 1, Stud = st8, StudentResult = 85 });
            //курс Mathematic група 1
            StudentCurriculumResult.Add(new StudentCurriculumResult() { CurriculumId = 2, Stud = st1, StudentResult = 160 });
            StudentCurriculumResult.Add(new StudentCurriculumResult() { CurriculumId = 2, Stud = st2, StudentResult = 140 });
            StudentCurriculumResult.Add(new StudentCurriculumResult() { CurriculumId = 2, Stud = st3, StudentResult = 120 });
            StudentCurriculumResult.Add(new StudentCurriculumResult() { CurriculumId = 2, Stud = st4, StudentResult = 110 });
            #endregion

            //studentThemeResults creation
            StudentThemeResult = new List<StudentThemeResult>();
            #region StudentsThemeResult
            //курс Programing тема "C++" група 1
            StudentThemeResult.Add(new StudentThemeResult() { Stud = st1, StudentResult = 70, ThemeId = 1 });
            StudentThemeResult.Add(new StudentThemeResult() { Stud = st2, StudentResult = 60, ThemeId = 1 });
            StudentThemeResult.Add(new StudentThemeResult() { Stud = st3, StudentResult = 50, ThemeId = 1 });
            StudentThemeResult.Add(new StudentThemeResult() { Stud = st4, StudentResult = 40, ThemeId = 1 });
            //курс Programing тема "C#" група 1
            StudentThemeResult.Add(new StudentThemeResult() { Stud = st1, StudentResult = 25, ThemeId = 2 });
            StudentThemeResult.Add(new StudentThemeResult() { Stud = st2, StudentResult = 20, ThemeId = 2 });
            StudentThemeResult.Add(new StudentThemeResult() { Stud = st3, StudentResult = 15, ThemeId = 2 });
            StudentThemeResult.Add(new StudentThemeResult() { Stud = st4, StudentResult = 11, ThemeId = 2 });
            //курс Programing тема "C++" група 2
            StudentThemeResult.Add(new StudentThemeResult() { Stud = st5, StudentResult = 40, ThemeId = 1 });
            StudentThemeResult.Add(new StudentThemeResult() { Stud = st6, StudentResult = 50, ThemeId = 1 });
            StudentThemeResult.Add(new StudentThemeResult() { Stud = st7, StudentResult = 45, ThemeId = 1 });
            StudentThemeResult.Add(new StudentThemeResult() { Stud = st8, StudentResult = 60, ThemeId = 1 });
            //курс Programing тема "C#" група 2
            StudentThemeResult.Add(new StudentThemeResult() { Stud = st5, StudentResult = 12, ThemeId = 2 });
            StudentThemeResult.Add(new StudentThemeResult() { Stud = st6, StudentResult = 20, ThemeId = 2 });
            StudentThemeResult.Add(new StudentThemeResult() { Stud = st7, StudentResult = 15, ThemeId = 2 });
            StudentThemeResult.Add(new StudentThemeResult() { Stud = st8, StudentResult = 25, ThemeId = 2 });
            //курс Mathematic тема "Algebra" група 1
            StudentThemeResult.Add(new StudentThemeResult() { Stud = st1, StudentResult = 80, ThemeId = 3 });
            StudentThemeResult.Add(new StudentThemeResult() { Stud = st2, StudentResult = 70, ThemeId = 3 });
            StudentThemeResult.Add(new StudentThemeResult() { Stud = st3, StudentResult = 60, ThemeId = 3 });
            StudentThemeResult.Add(new StudentThemeResult() { Stud = st4, StudentResult = 55, ThemeId = 3 });
            //курс Mathematic тема "Geometry" група 1
            StudentThemeResult.Add(new StudentThemeResult() { Stud = st1, StudentResult = 80, ThemeId = 4 });
            StudentThemeResult.Add(new StudentThemeResult() { Stud = st2, StudentResult = 70, ThemeId = 4 });
            StudentThemeResult.Add(new StudentThemeResult() { Stud = st3, StudentResult = 60, ThemeId = 4 });
            StudentThemeResult.Add(new StudentThemeResult() { Stud = st4, StudentResult = 55, ThemeId = 4 });
            #endregion

            //groupList Creation
            GroupList = new List<string>();
            GroupList.Add("Group id: " + Group[0].GroupId);
            GroupList.Add("Group id: " + Group[1].GroupId);

            //QuestionResault Creation
            QuestionResault = new List<StudentQuestionResault>();
            #region Group ID == 1 theme "C++"
            //student "Roman (1)" theme "C++" 
            QuestionResault.Add(new StudentQuestionResault() { Stud = st1, ThemeID = 1, Costs = 25, StudentAnswer = "trueAnswer1", RightAnswer = "trueAnswer1" });
            QuestionResault.Add(new StudentQuestionResault() { Stud = st1, ThemeID = 1, Costs = 20, StudentAnswer = "trueAnswer2", RightAnswer = "trueAnswer2" });
            QuestionResault.Add(new StudentQuestionResault() { Stud = st1, ThemeID = 1, Costs = 25, StudentAnswer = "trueAnswer3", RightAnswer = "trueAnswer3" });
            QuestionResault.Add(new StudentQuestionResault() { Stud = st1, ThemeID = 1, Costs = 5, StudentAnswer = "falseAnswer4", RightAnswer = "trueAnswer4" });
            //student "Ivan (1)" theme "C++" 
            QuestionResault.Add(new StudentQuestionResault() { Stud = st2, ThemeID = 1, Costs = 25, StudentAnswer = "trueAnswer5", RightAnswer = "trueAnswer5" });
            QuestionResault.Add(new StudentQuestionResault() { Stud = st2, ThemeID = 1, Costs = 10, StudentAnswer = "falseAnswer6", RightAnswer = "trueAnswer6" });
            QuestionResault.Add(new StudentQuestionResault() { Stud = st2, ThemeID = 1, Costs = 20, StudentAnswer = "trueAnswer7", RightAnswer = "trueAnswer7" });
            QuestionResault.Add(new StudentQuestionResault() { Stud = st2, ThemeID = 1, Costs = 20, StudentAnswer = "trueAnswer8", RightAnswer = "trueAnswer8" });
            //student "Bogdan (1)" theme "C++" 
            QuestionResault.Add(new StudentQuestionResault() { Stud = st3, ThemeID = 1, Costs = 20, StudentAnswer = "trueAnswer9", RightAnswer = "trueAnswer9" });
            QuestionResault.Add(new StudentQuestionResault() { Stud = st3, ThemeID = 1, Costs = 25, StudentAnswer = "falseAnswer10", RightAnswer = "trueAnswer10" });
            QuestionResault.Add(new StudentQuestionResault() { Stud = st3, ThemeID = 1, Costs = 20, StudentAnswer = "trueAnswer11", RightAnswer = "trueAnswer11" });
            QuestionResault.Add(new StudentQuestionResault() { Stud = st3, ThemeID = 1, Costs = 10, StudentAnswer = "trueAnswer12", RightAnswer = "trueAnswer12" });
            //student "Stepan (1)" theme "C++" 
            QuestionResault.Add(new StudentQuestionResault() { Stud = st4, ThemeID = 1, Costs = 20, StudentAnswer = "trueAnswer13", RightAnswer = "trueAnswer13" });
            QuestionResault.Add(new StudentQuestionResault() { Stud = st4, ThemeID = 1, Costs = 25, StudentAnswer = "falseAnswer14", RightAnswer = "trueAnswer14" });
            QuestionResault.Add(new StudentQuestionResault() { Stud = st4, ThemeID = 1, Costs = 20, StudentAnswer = "trueAnswer15", RightAnswer = "trueAnswer15" });
            QuestionResault.Add(new StudentQuestionResault() { Stud = st4, ThemeID = 1, Costs = 10, StudentAnswer = "falseAnswer16", RightAnswer = "trueAnswer16" });
            #endregion
            #region Group ID == 1 theme "C#"
            //student "Roman (1)" theme "C#" 
            QuestionResault.Add(new StudentQuestionResault() { Stud = st1, ThemeID = 2, Costs = 10, StudentAnswer = "trueAnswer17", RightAnswer = "trueAnswer17" });
            QuestionResault.Add(new StudentQuestionResault() { Stud = st1, ThemeID = 2, Costs = 10, StudentAnswer = "trueAnswer18", RightAnswer = "trueAnswer18" });
            QuestionResault.Add(new StudentQuestionResault() { Stud = st1, ThemeID = 2, Costs = 5, StudentAnswer = "trueAnswer19", RightAnswer = "trueAnswer19" });
            //student "Ivan (1)" theme "C#" 
            QuestionResault.Add(new StudentQuestionResault() { Stud = st2, ThemeID = 2, Costs = 10, StudentAnswer = "trueAnswer20", RightAnswer = "trueAnswer20" });
            QuestionResault.Add(new StudentQuestionResault() { Stud = st2, ThemeID = 2, Costs = 5, StudentAnswer = "falseAnswer21", RightAnswer = "trueAnswer21" });
            QuestionResault.Add(new StudentQuestionResault() { Stud = st2, ThemeID = 2, Costs = 10, StudentAnswer = "trueAnswer22", RightAnswer = "trueAnswer22" });
            //student "Bogdan (1)" theme "C#" 
            QuestionResault.Add(new StudentQuestionResault() { Stud = st3, ThemeID = 2, Costs = 10, StudentAnswer = "trueAnswer23", RightAnswer = "trueAnswer23" });
            QuestionResault.Add(new StudentQuestionResault() { Stud = st3, ThemeID = 2, Costs = 10, StudentAnswer = "falseAnswer24", RightAnswer = "trueAnswer24" });
            QuestionResault.Add(new StudentQuestionResault() { Stud = st3, ThemeID = 2, Costs = 5, StudentAnswer = "trueAnswer25", RightAnswer = "trueAnswer25" });
            //student "Stepan (1)" theme "C#" 
            QuestionResault.Add(new StudentQuestionResault() { Stud = st4, ThemeID = 2, Costs = 10, StudentAnswer = "trueAnswer26", RightAnswer = "trueAnswer26" });
            QuestionResault.Add(new StudentQuestionResault() { Stud = st4, ThemeID = 2, Costs = 14, StudentAnswer = "falseAnswer27", RightAnswer = "trueAnswer27" });
            QuestionResault.Add(new StudentQuestionResault() { Stud = st4, ThemeID = 2, Costs = 1, StudentAnswer = "trueAnswer28", RightAnswer = "trueAnswer28" });
            #endregion
            #region Group ID == 1 theme "Algebra"
            //student "Roman (1)" theme "Algebra" 
            QuestionResault.Add(new StudentQuestionResault() { Stud = st1, ThemeID = 3, Costs = 25, StudentAnswer = "trueAnswer29", RightAnswer = "trueAnswer29" });
            QuestionResault.Add(new StudentQuestionResault() { Stud = st1, ThemeID = 3, Costs = 30, StudentAnswer = "trueAnswer30", RightAnswer = "trueAnswer30" });
            QuestionResault.Add(new StudentQuestionResault() { Stud = st1, ThemeID = 3, Costs = 25, StudentAnswer = "trueAnswer31", RightAnswer = "trueAnswer31" });
            QuestionResault.Add(new StudentQuestionResault() { Stud = st1, ThemeID = 3, Costs = 20, StudentAnswer = "falseAnswer32", RightAnswer = "trueAnswer32" });
            //student "Ivan (1)" theme "Algebra" 
            QuestionResault.Add(new StudentQuestionResault() { Stud = st2, ThemeID = 3, Costs = 25, StudentAnswer = "trueAnswer33", RightAnswer = "trueAnswer33" });
            QuestionResault.Add(new StudentQuestionResault() { Stud = st2, ThemeID = 3, Costs = 30, StudentAnswer = "falseAnswer34", RightAnswer = "trueAnswer34" });
            QuestionResault.Add(new StudentQuestionResault() { Stud = st2, ThemeID = 3, Costs = 25, StudentAnswer = "trueAnswer35", RightAnswer = "trueAnswer35" });
            QuestionResault.Add(new StudentQuestionResault() { Stud = st2, ThemeID = 3, Costs = 20, StudentAnswer = "trueAnswer36", RightAnswer = "trueAnswer36" });
            //student "Bogdan (1)" theme "Algebra"
            QuestionResault.Add(new StudentQuestionResault() { Stud = st3, ThemeID = 3, Costs = 20, StudentAnswer = "falseAnswer37", RightAnswer = "trueAnswer37" });
            QuestionResault.Add(new StudentQuestionResault() { Stud = st3, ThemeID = 3, Costs = 25, StudentAnswer = "trueAnswer38", RightAnswer = "trueAnswer38" });
            QuestionResault.Add(new StudentQuestionResault() { Stud = st3, ThemeID = 3, Costs = 20, StudentAnswer = "falseAnswer39", RightAnswer = "trueAnswer39" });
            QuestionResault.Add(new StudentQuestionResault() { Stud = st3, ThemeID = 3, Costs = 35, StudentAnswer = "trueAnswer40", RightAnswer = "trueAnswer40" });
            //student "Stepan (1)" theme "Algebra"
            QuestionResault.Add(new StudentQuestionResault() { Stud = st4, ThemeID = 3, Costs = 30, StudentAnswer = "trueAnswer41", RightAnswer = "trueAnswer41" });
            QuestionResault.Add(new StudentQuestionResault() { Stud = st4, ThemeID = 3, Costs = 20, StudentAnswer = "falseAnswer42", RightAnswer = "trueAnswer42" });
            QuestionResault.Add(new StudentQuestionResault() { Stud = st4, ThemeID = 3, Costs = 25, StudentAnswer = "trueAnswer43", RightAnswer = "trueAnswer43" });
            QuestionResault.Add(new StudentQuestionResault() { Stud = st4, ThemeID = 3, Costs = 25, StudentAnswer = "falseAnswer44", RightAnswer = "trueAnswer44" });
            #endregion
            #region Group ID == 1 theme "Geometry"
            //student "Roman (1)" theme "Geometry"
            QuestionResault.Add(new StudentQuestionResault() { Stud = st1, ThemeID = 4, Costs = 25, StudentAnswer = "trueAnswer45", RightAnswer = "trueAnswer45" });
            QuestionResault.Add(new StudentQuestionResault() { Stud = st1, ThemeID = 4, Costs = 30, StudentAnswer = "trueAnswer46", RightAnswer = "trueAnswer46" });
            QuestionResault.Add(new StudentQuestionResault() { Stud = st1, ThemeID = 4, Costs = 25, StudentAnswer = "trueAnswer47", RightAnswer = "trueAnswer47" });
            QuestionResault.Add(new StudentQuestionResault() { Stud = st1, ThemeID = 4, Costs = 20, StudentAnswer = "falseAnswer48", RightAnswer = "trueAnswer48" });
            //student "Ivan (1)" theme "Geometry"
            QuestionResault.Add(new StudentQuestionResault() { Stud = st2, ThemeID = 4, Costs = 25, StudentAnswer = "trueAnswer49", RightAnswer = "trueAnswer49" });
            QuestionResault.Add(new StudentQuestionResault() { Stud = st2, ThemeID = 4, Costs = 30, StudentAnswer = "falseAnswer50", RightAnswer = "trueAnswer50" });
            QuestionResault.Add(new StudentQuestionResault() { Stud = st2, ThemeID = 4, Costs = 25, StudentAnswer = "trueAnswer51", RightAnswer = "trueAnswer51" });
            QuestionResault.Add(new StudentQuestionResault() { Stud = st2, ThemeID = 4, Costs = 20, StudentAnswer = "trueAnswer52", RightAnswer = "trueAnswer52" });
            //student "Bogdan (1)" theme "Geometry"
            QuestionResault.Add(new StudentQuestionResault() { Stud = st3, ThemeID = 4, Costs = 20, StudentAnswer = "falseAnswer53", RightAnswer = "trueAnswer53" });
            QuestionResault.Add(new StudentQuestionResault() { Stud = st3, ThemeID = 4, Costs = 25, StudentAnswer = "trueAnswer54", RightAnswer = "trueAnswer54" });
            QuestionResault.Add(new StudentQuestionResault() { Stud = st3, ThemeID = 4, Costs = 20, StudentAnswer = "falseAnswer55", RightAnswer = "trueAnswer55" });
            QuestionResault.Add(new StudentQuestionResault() { Stud = st3, ThemeID = 4, Costs = 35, StudentAnswer = "trueAnswer56", RightAnswer = "trueAnswer56" });
            //student "Stepan (1)" theme "Geometry"
            QuestionResault.Add(new StudentQuestionResault() { Stud = st4, ThemeID = 4, Costs = 30, StudentAnswer = "trueAnswer57", RightAnswer = "trueAnswer57" });
            QuestionResault.Add(new StudentQuestionResault() { Stud = st4, ThemeID = 4, Costs = 20, StudentAnswer = "falseAnswer58", RightAnswer = "trueAnswer58" });
            QuestionResault.Add(new StudentQuestionResault() { Stud = st4, ThemeID = 4, Costs = 25, StudentAnswer = "trueAnswer59", RightAnswer = "trueAnswer59" });
            QuestionResault.Add(new StudentQuestionResault() { Stud = st4, ThemeID = 4, Costs = 25, StudentAnswer = "falseAnswer60", RightAnswer = "trueAnswer60" });
            #endregion
        }

        public Curriculum GetCurriculum(int id)
        {
            return Curriculums[id];
        }

        public int GetMaxPointsFromAllCurriculums(Int32[] curriculumIDs)
        {
            return curriculumIDs.SelectMany(index => Curriculums[index].Themes).Sum(th => th.MaxPoint);
        }

        public int GetCurrentPointsFromAllCurriculums(Student stud, Int32[] curriculumIDs)
        {
            var temp = 0;

            foreach(var index in curriculumIDs)
            {
                temp = StudentCurriculumResult.Where(t => t.Stud == stud && t.CurriculumId == index + 1).Sum(t => t.StudentResult);
            }

            return temp;
        }

        public char Ects(double percent)
        {
            if (percent > 91.0)
            {
                return 'A';
            }
            else if (percent > 81.0)
            {
                return 'B';
            }
            else if (percent > 71.0)
            {
                return 'C';
            }
            else if (percent > 61.0)
            {
                return 'D';
            }
            else if (percent > 51.0)
            {
                return 'E';
            }
            else
            {
                return 'F';
            }
        }
    }

    

}
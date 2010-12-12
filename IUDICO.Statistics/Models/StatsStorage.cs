using System;
using System.Collections.Generic;
using System.Linq;

namespace IUDICO.Statistics.Models
{
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
        public Theme[] Themes;
        public List<Curriculum> Curriculums;
        public Curriculum FirstCurriculum;
        public Curriculum SecondCurriculum;
        public Group Pmi11;
        public StudentThemeResult[] StudentThemeResult;
        public List<StudentCurriculumResult> StudentCurriculumResult;
        public List<string> GroupList;

        public void SetFakeData()
        {
            var st1 = new Student { StudentId = 1, Name = "Roman" };
            var st2 = new Student { StudentId = 2, Name = "Ivan" };
            var st3 = new Student { StudentId = 3, Name = "Bogdan" };
            var st4 = new Student { StudentId = 4, Name = "Stepan" };
            Pmi11 = new Group { GroupId = 1, Students = new[] { st1, st2, st3, st4 } };

            var th1 = new Theme { ThemeId = 1, Name = "C++", MaxPoint = 75 };
            var th2 = new Theme { ThemeId = 2, Name = "C#", MaxPoint = 25 };
            Themes = new[] { th1, th2 };
            var themesForSecondCurriculum = new[] { new Theme { ThemeId = 1, Name = "Algebra", MaxPoint = 100 }, new Theme { ThemeId = 2, Name = "Geometry", MaxPoint = 50 } };


            #region StudentsResult

            var st1C1 = new StudentCurriculumResult() { CurriculumId = 1, Stud = st1, StudentResult = 95 };
            var st2C1 = new StudentCurriculumResult() { CurriculumId = 1, Stud = st2, StudentResult = 80 };
            var st3C1 = new StudentCurriculumResult() { CurriculumId = 1, Stud = st3, StudentResult = 70 };
            var st4C1 = new StudentCurriculumResult() { CurriculumId = 1, Stud = st4, StudentResult = 95 };

            var st1C2 = new StudentCurriculumResult() { CurriculumId = 2, Stud = st1, StudentResult = 145 };
            var st2C2 = new StudentCurriculumResult() { CurriculumId = 2, Stud = st2, StudentResult = 81 };
            var st3C2 = new StudentCurriculumResult() { CurriculumId = 2, Stud = st3, StudentResult = 71 };
            var st4C2 = new StudentCurriculumResult() { CurriculumId = 2, Stud = st4, StudentResult = 125 };

            StudentCurriculumResult = new List<StudentCurriculumResult>
                                          {
                                              st1C1,
                                              st2C1,
                                              st3C1,
                                              st4C1,
                                              st1C2,
                                              st2C2,
                                              st3C2,
                                              st4C2
                                          };

            var st1Tr1 = new StudentThemeResult { Stud = st1, StudentResult = 90, ThemeId = 1 };
            var st1Tr2 = new StudentThemeResult { Stud = st1, StudentResult = 90, ThemeId = 1 };
            var st2Tr1 = new StudentThemeResult { Stud = st2, StudentResult = 80, ThemeId = 2 };
            var st2Tr2 = new StudentThemeResult { Stud = st2, StudentResult = 80, ThemeId = 2 };
            var st3Tr1 = new StudentThemeResult { Stud = st3, StudentResult = 70, ThemeId = 3 };
            var st3Tr2 = new StudentThemeResult { Stud = st3, StudentResult = 70, ThemeId = 3 };
            var st4Tr1 = new StudentThemeResult { Stud = st4, StudentResult = 85, ThemeId = 4 };
            var st4Tr2 = new StudentThemeResult { Stud = st4, StudentResult = 85, ThemeId = 4 };
            StudentThemeResult = new[] { st1Tr1, st1Tr2, st2Tr1, st2Tr2, st3Tr1, st3Tr2, st4Tr1, st4Tr2 };

            #endregion


            FirstCurriculum = new Curriculum { CurriculumId = 1, CurriculumName = "Programing", GroupsId = new int[] { 1, 2}, Themes = Themes };
            SecondCurriculum = new Curriculum { CurriculumId = 2, CurriculumName = "Mathematic", GroupsId = new int[] { 1 }, Themes = themesForSecondCurriculum };


            GroupList = new List<string> {"Group id: " + Pmi11.GroupId};

            Curriculums = new List<Curriculum> {FirstCurriculum, SecondCurriculum};
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

    public class StatsStorage
    {
        
    }

}
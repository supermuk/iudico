using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using IUDICO.Common.Models;
using System.Diagnostics;

namespace IUDICO.Statistics.Models
{
    public class Curriculum
    {
        public string CurriculumName { get; set; }
        public int CurriculumID { get; set; }
        public int[] GroupsID { get; set; }
        public Theme[] Themes { get; set; }

        public int GetMaxPointsFromCurriculum()
        {
            int temp = 0;
            foreach (Theme th in Themes)
            {
                temp += th.MaxPoint;
            }
            return temp;
        }
    }

    public class Group
    {
        public int GroupID { get; set; }
        public Student[] Students { get; set; }
    }

    public class Student
    {
        public string Name { get; set; }
        public int StudentID { get; set; }
    }

    public class Theme
    {
        public int ThemeID { get; set; }
        public string Name { get; set; }
        public int MaxPoint { get; set; }
    }

    public class StudentThemeResult
    {
        public Student Stud { get; set; }
        public int ThemeID { get; set; }
        public int StudentResult { get; set; }
    }

    public class StudentCurriculumResult
    {
        public Student Stud { get; set; }
        public int CurriculumID { get; set; }
        public int StudentResult { get; set; }
    }

    public class InfoOnFirstPage
    {
        public Theme[] themes;
        public List<Curriculum> curriculums;
        public Curriculum firstCurriculum;
        public Curriculum secondCurriculum;
        public Group pmi11;
        public StudentThemeResult[] studentThemeResult;
        public List<StudentCurriculumResult> studentCurriculumResult;
        public List<string> groupList;

        public void SetFakeData()
        {
            Student st1 = new Student() { StudentID = 1, Name = "Roman" };
            Student st2 = new Student() { StudentID = 2, Name = "Ivan" };
            Student st3 = new Student() { StudentID = 3, Name = "Bogdan" };
            Student st4 = new Student() { StudentID = 4, Name = "Stepan" };
            pmi11 = new Group() { GroupID = 1, Students = new Student[] { st1, st2, st3, st4 } };

            Theme th1 = new Theme() { ThemeID = 1, Name = "C++", MaxPoint = 75 };
            Theme th2 = new Theme() { ThemeID = 2, Name = "C#", MaxPoint = 25 };
            themes = new Theme[] { th1, th2 };
            Theme[] themesForSecondCurriculum = new Theme[] { new Theme() { ThemeID = 1, Name = "Algebra", MaxPoint = 100 }, new Theme() { ThemeID = 2, Name = "Geometry", MaxPoint = 50 } };


            #region StudentsResult

            StudentCurriculumResult st1C1 = new StudentCurriculumResult() { CurriculumID = 1, Stud = st1, StudentResult = 95 };
            StudentCurriculumResult st2C1 = new StudentCurriculumResult() { CurriculumID = 1, Stud = st2, StudentResult = 80 };
            StudentCurriculumResult st3C1 = new StudentCurriculumResult() { CurriculumID = 1, Stud = st3, StudentResult = 70 };
            StudentCurriculumResult st4C1 = new StudentCurriculumResult() { CurriculumID = 1, Stud = st4, StudentResult = 95 };

            StudentCurriculumResult st1C2 = new StudentCurriculumResult() { CurriculumID = 2, Stud = st1, StudentResult = 145 };
            StudentCurriculumResult st2C2 = new StudentCurriculumResult() { CurriculumID = 2, Stud = st2, StudentResult = 81 };
            StudentCurriculumResult st3C2 = new StudentCurriculumResult() { CurriculumID = 2, Stud = st3, StudentResult = 71 };
            StudentCurriculumResult st4C2 = new StudentCurriculumResult() { CurriculumID = 2, Stud = st4, StudentResult = 125 };

            studentCurriculumResult = new List<StudentCurriculumResult>();
            studentCurriculumResult.Add(st1C1);
            studentCurriculumResult.Add(st2C1);
            studentCurriculumResult.Add(st3C1);
            studentCurriculumResult.Add(st4C1);
            studentCurriculumResult.Add(st1C2);
            studentCurriculumResult.Add(st2C2);
            studentCurriculumResult.Add(st3C2);
            studentCurriculumResult.Add(st4C2);

            StudentThemeResult st1TR1 = new StudentThemeResult() { Stud = st1, StudentResult = 90, ThemeID = 1 };
            StudentThemeResult st1TR2 = new StudentThemeResult() { Stud = st1, StudentResult = 90, ThemeID = 1 };
            StudentThemeResult st2TR1 = new StudentThemeResult() { Stud = st2, StudentResult = 80, ThemeID = 2 };
            StudentThemeResult st2TR2 = new StudentThemeResult() { Stud = st2, StudentResult = 80, ThemeID = 2 };
            StudentThemeResult st3TR1 = new StudentThemeResult() { Stud = st3, StudentResult = 70, ThemeID = 3 };
            StudentThemeResult st3TR2 = new StudentThemeResult() { Stud = st3, StudentResult = 70, ThemeID = 3 };
            StudentThemeResult st4TR1 = new StudentThemeResult() { Stud = st4, StudentResult = 85, ThemeID = 4 };
            StudentThemeResult st4TR2 = new StudentThemeResult() { Stud = st4, StudentResult = 85, ThemeID = 4 };
            studentThemeResult = new StudentThemeResult[] { st1TR1, st1TR2, st2TR1, st2TR2, st3TR1, st3TR2, st4TR1, st4TR2 };

            #endregion


            firstCurriculum = new Curriculum() { CurriculumID = 1, CurriculumName = "Programing", GroupsID = new int[] { 1, 2}, Themes = themes };
            secondCurriculum = new Curriculum() { CurriculumID = 2, CurriculumName = "Mathematic", GroupsID = new int[] { 1 }, Themes = themesForSecondCurriculum };


            groupList = new List<string>();
            groupList.Add("Group id: " + pmi11.GroupID);

            curriculums = new List<Curriculum>();
            curriculums.Add(firstCurriculum);
            curriculums.Add(secondCurriculum);
        }

        public Curriculum GetCurriculum(int id)
        {
            return curriculums[id];
        }

        public int GetMaxPointsFromAllCurriculums(Int32[] CurriculumIDs)
        {
            int temp = 0;
            foreach(int index in CurriculumIDs)
            {
                foreach (Theme th in curriculums[index].Themes)
                {
                    temp += th.MaxPoint;
                }
            }
            return temp;
        }

        public int GetCurrentPointsFromAllCurriculums(Student stud, Int32[] CurriculumIDs)
        {
            int temp = 0;
            foreach(int index in CurriculumIDs)
            {
                for (int i = 0; i < studentCurriculumResult.Count; i++)
                {
                    if (studentCurriculumResult[i].Stud == stud && studentCurriculumResult[i].CurriculumID == index+1)
                    {
                        temp += studentCurriculumResult[i].StudentResult;
                    }
                }
            }
            return temp;
        }

        public char ECTS(double percent)
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
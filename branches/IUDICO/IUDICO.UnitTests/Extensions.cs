﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IUDICO.Common.Models;

namespace IUDICO.UnitTests
{
    public static class AdvAssert
    {
        public static void AreEqual(Curriculum expected, Curriculum actual)
        {
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.IsDeleted, actual.IsDeleted);
            Assert.AreEqual(expected.Id, actual.Id);
        }

        public static void AreEqual(Stage expected, Stage actual)
        {
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.IsDeleted, actual.IsDeleted);
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.CurriculumRef, actual.CurriculumRef);
        }

        public static void AreEqual(List<Stage> expected,List<Stage> actual)
        {
            Assert.AreEqual(expected.Count, actual.Count);

            int i = 0;
            foreach (Stage item in expected)
            {
                Assert.AreEqual(item.Name, actual[i].Name);
                Assert.AreEqual(item.IsDeleted, actual[i].IsDeleted);
                Assert.AreEqual(item.Id, actual[i].Id);
                Assert.AreEqual(item.CurriculumRef, actual[i].CurriculumRef);
                i++;
            }
        }

        public static void AreEqual(Theme expected, Theme actual)
        {
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.IsDeleted, actual.IsDeleted);
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.CourseRef,expected.CourseRef);
            Assert.AreEqual(expected.ThemeTypeRef, expected.ThemeTypeRef);
            Assert.AreEqual(expected.StageRef, expected.StageRef);
        }
    }
    
    
    public static class Extensions
    {

    }
}

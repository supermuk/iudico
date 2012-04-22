﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using IUDICO.Common.Models.Shared;
using IUDICO.Common.Models.Shared.CurriculumManagement;
using IUDICO.Common.Models.Shared.Statistics;
using IUDICO.Analytics.Models.AnomalyDetectionAlg;
using IUDICO.Analytics.Models.DecisionTrees;

namespace IUDICO.UnitTests.Analytics
{
    [TestFixture]
    public class AnomalyDetectionAlgorithmTest
    {
        [Test]
        [Category("AnomalyDetectionAlgorithmTest")]
        public void AnomalyDetectionAlgorithm()
        {
            AnomalyDetectionAlgorithm test = new AnomalyDetectionAlgorithm();

            var listOfStudents = GetStudentListForPMI43();
            string[] normal = new string[] { "2", "13", "6" };
            string[] anomalies = new string[] { "8", "10" };
            var trainingSets = TrainingSetsCreator.generateTrainingSets(listOfStudents, normal, anomalies);

            var formula = test.trainAlgorithm(trainingSets[0], trainingSets[1], trainingSets[2]);
            Assert.AreEqual(formula.isAnomaly(new double[] { 450, 9 }), false);
            Assert.AreEqual(formula.isAnomaly(new double[] { 550, 9 }), false);
            Assert.AreEqual(formula.isAnomaly(new double[] { 450, 7 }), false);
            Assert.AreEqual(formula.isAnomaly(new double[] { 100, 9 }), true);
            Assert.AreEqual(formula.isAnomaly(new double[] { 150, 9 }), true);
        }

        public IEnumerable<KeyValuePair<User, double[]>> GetStudentListForPMI43()
        {
            List<KeyValuePair<User, double[]>> result = new List<KeyValuePair<User, double[]>>();
            Random rnd = new Random(System.Environment.TickCount);

            #region PMI-43
            User user = new User();
            user.OpenId = "1";
            int score = 6;
            user.Name = "Остап Андрусів(08i301)";
            double[] coef = new double[] { 400, score };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "2";
            score = 9;
            user.Name = "Назар Врублевський(08i302)";
            coef = new double[] { 450, score };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "3";
            score = 4;
            user.Name = "Адмайкін Максим(08i303)";
            coef = new double[] { 350, score };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "4";
            score = 6;
            user.Name = "Михайло Тис(08i304)";
            coef = new double[] { 400, score };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "5";
            score = 5;
            user.Name = "Оля Іванків(08i305)";
            coef = new double[] { 400, score };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "6";
            score = 7;
            user.Name = "Юрій Ожирко(08i306)";
            coef = new double[] { 450, score };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "7";
            score = 5;
            user.Name = "Тарас Бехта(08i307)";
            coef = new double[] { 300, score };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "8";
            score = 9;
            user.Name = "Василь Бодак(08i308)";
            coef = new double[] { 100, score };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "9";
            score = 7;
            user.Name = "Василь Багряк(08i309)";
            coef = new double[] { 350, score };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "10";
            score = 9;
            user.Name = "Максим Гула(08i310)";
            coef = new double[] { 150, score };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "11";
            score = 6;
            user.Name = "Назар Качмарик(08i311)";
            coef = new double[] { 350, score };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "12";
            score = 7;
            user.Name = "Мирослав Голуб(08i312)";
            coef = new double[] { 400, score };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "13";
            score = 9;
            user.Name = "Павло Мартиник(08i313)";
            coef = new double[] { 550, score };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "14";
            score = 6;
            user.Name = "08i314 08i314(08i314)";
            coef = new double[] { 400, score };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "15";
            score = 7;
            user.Name = "08i315 Горячий(08i315)";
            coef = new double[] { 450, score };
            result.Add(new KeyValuePair<User, double[]>(user, coef)); user = new User();

            user = new User();
            user.OpenId = "16";
            score = 4;
            user.Name = "Данило Савчак(08i316)";
            coef = new double[] { 250, score };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "17";
            score = 8;
            user.Name = "Ярослав Пиріг(08i317)";
            coef = new double[] { 400, score };
            result.Add(new KeyValuePair<User, double[]>(user, coef));
            user = new User();

            user = new User();
            user.OpenId = "18";
            score = 7;
            user.Name = "Ірина Харів(08i318)";
            coef = new double[] { 400, score };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "19";
            score = 7;
            user.Name = "Ярослав Мота(08i319)";
            coef = new double[] { 350, score };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "20";
            score = 7;
            user.Name = "08i320 Федорович(08i320)";
            coef = new double[] { 450, score };
            result.Add(new KeyValuePair<User, double[]>(user, coef));
            #endregion

            return result;
        }
    }
}
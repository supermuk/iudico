using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared.Statistics;
using IUDICO.Common.Models.Shared;

namespace IUDICO.Statistics.Models.QualityTest
{
    public static class FakeDataQualityTest
    {
        //Index 
        public static List<Discipline> FakeAllowedDisciplines()
        {
            Discipline fakeDiscipline = new Discipline();
            fakeDiscipline.Name = "Програмування";
            return new List<Discipline>() { fakeDiscipline };
        }
        public static User FakeTeacherUser()
        {
            User fakeTeacherUser = new User();
            fakeTeacherUser.Username = "Викладач1";
            return fakeTeacherUser;
        }
        // SelectTopic
        public static List<Topic> FakeTopicsByDisciplineId()
        {
            Topic fakeTopic = new Topic();
            fakeTopic.Id = 1;
            fakeTopic.Name = "Тема1";
            return new List<Topic>() { fakeTopic };
        }
        public static String FakeDisciplineName(long selectDisciplineId)
        {
            return "Програмування";
        }
        // SelectGroups
        public static String FakeTopicName(int selectTopicId)
        {
            return "Тема1";
        }
        public static List<Group> FakeGroupsByTopicIdOrDisciplineId(int selectTopicId)
        {
            Group fakeGroup1 = new Group();
            fakeGroup1.Id = 1;
            fakeGroup1.Name = "PMI-31";
            Group fakeGroup2 = new Group();
            fakeGroup2.Id = 2;
            fakeGroup2.Name = "PMI-32";
            Group fakeGroup3 = new Group();
            fakeGroup3.Id = 3;
            fakeGroup3.Name = "PMI-33";
            return new List<Group>() { fakeGroup1, fakeGroup2, fakeGroup3 };
        }
        // ShowQualityTest
        
        public static IEnumerable<User> FakeUsersByGroupId(int id)
        {
            List<User> listOfUsers = new List<User>();
            switch (id)
            {
                case 1:
                    listOfUsers.Add(new User() { Username = "Роман Баїк(08i101)" });
                    listOfUsers.Add(new User() { Username = "Катерина Бугай(08i102)" });
                    listOfUsers.Add(new User() { Username = "Олексій Гелей(08i103)" });
                    listOfUsers.Add(new User() { Username = "Карпунь Богдан(08i104)" });
                    listOfUsers.Add(new User() { Username = "08i105 08i105(08i105)" });
                    listOfUsers.Add(new User() { Username = "Олександр Козачук(08i106)" });
                    listOfUsers.Add(new User() { Username = "Аня Кітчак(08i107)" });
                    listOfUsers.Add(new User() { Username = "Кравець Роман(08i108)" });
                    listOfUsers.Add(new User() { Username = "Андрій Крупич(08i109)" });
                    listOfUsers.Add(new User() { Username = "Літинський Ростислав(08i110)" });
                    listOfUsers.Add(new User() { Username = "Христина Макар(08i111)" });
                    listOfUsers.Add(new User() { Username = "Юрко Тимчук(08i112)" });
                    listOfUsers.Add(new User() { Username = "Oleg Papirnyk(08i113)" });
                    listOfUsers.Add(new User() { Username = "Віталій Нобіс(08i114)" });
                    listOfUsers.Add(new User() { Username = "Taras Pelenyo(08i115)" });
                    listOfUsers.Add(new User() { Username = "08i116 08i116(08i116)" });
                    listOfUsers.Add(new User() { Username = "Стадник Богдан(08i117)" });
                    listOfUsers.Add(new User() { Username = "Andriy Pachva(08i118)" });
                    listOfUsers.Add(new User() { Username = "Фай Роман(08i119)" });
                    listOfUsers.Add(new User() { Username = "Мар'яна Хлєбик(08i120)" });
                    listOfUsers.Add(new User() { Username = "08i121 08i121(08i121)" });
                    listOfUsers.Add(new User() { Username = "Руслан Івать(08i122)" });
                    listOfUsers.Add(new User() { Username = "Андрій Сташко(08i123)" });
                    listOfUsers.Add(new User() { Username = "08i124 08i124(08i124)" });
                    listOfUsers.Add(new User() { Username = "Ігор Михалевич(08i125)" });
                    break;
                case 2:
                    listOfUsers.Add(new User() { Username = "Олег Булатовський(08i201)" });
                    listOfUsers.Add(new User() { Username = "Василь Ванівський(08i202)" });
                    listOfUsers.Add(new User() { Username = "Boзняк Максим(08i203)" });
                    listOfUsers.Add(new User() { Username = "Юра Гой(08i204)" });
                    listOfUsers.Add(new User() { Username = "Остап Демків(08i205)" });
                    listOfUsers.Add(new User() { Username = "Юра Дерев`янко(08i206)" });
                    listOfUsers.Add(new User() { Username = "Роман Дроботiй(08i207)" });
                    listOfUsers.Add(new User() { Username = "Дубик Петро(08i208)" });
                    listOfUsers.Add(new User() { Username = "Віталій Засадний(08i209)" });
                    listOfUsers.Add(new User() { Username = "08i210 Андрусишин(08i210)" });
                    listOfUsers.Add(new User() { Username = "Тарас Кміть(08i211)" });
                    listOfUsers.Add(new User() { Username = "Роман Коваль(08i212)" });
                    listOfUsers.Add(new User() { Username = "Маріана Кушла(08i213)" });
                    listOfUsers.Add(new User() { Username = "Юрій Ладанівський(08i214)" });
                    listOfUsers.Add(new User() { Username = "Leskiv Andriy(08i215)" });
                    listOfUsers.Add(new User() { Username = "Юра Лучків(08i216)" });
                    listOfUsers.Add(new User() { Username = "Mamchur Andriy(08i217)" });
                    listOfUsers.Add(new User() { Username = "Христина Мандибур(08i218)" });
                    listOfUsers.Add(new User() { Username = "Андрiй Протасов(08i219)" });
                    listOfUsers.Add(new User() { Username = "08i220 08i220(08i220)" });
                    listOfUsers.Add(new User() { Username = "Стадник Роман(08i221)" });
                    listOfUsers.Add(new User() { Username = "Андрій Столбовой(08i222)" });
                    listOfUsers.Add(new User() { Username = "Ігор Сторянський(08i223)" });
                    listOfUsers.Add(new User() { Username = "Фатич Михайло(08i224)" });
                    break;
                case 3:
                    listOfUsers.Add(new User() { Username = "Остап Андрусів(08i301)" });
                    listOfUsers.Add(new User() { Username = "Назар Врублевський(08i302)" });
                    listOfUsers.Add(new User() { Username = "Адмайкін Максим(08i303)" });
                    listOfUsers.Add(new User() { Username = "Михайло Тис(08i304)" });
                    listOfUsers.Add(new User() { Username = "Оля Іванків(08i305)" });
                    listOfUsers.Add(new User() { Username = "Юрій Ожирко(08i306)" });
                    listOfUsers.Add(new User() { Username = "Тарас Бехта(08i307)" });
                    listOfUsers.Add(new User() { Username = "Василь Бодак(08i308)" });
                    listOfUsers.Add(new User() { Username = "Василь Багряк(08i309)" });
                    listOfUsers.Add(new User() { Username = "Максим Гула(08i310)" });
                    listOfUsers.Add(new User() { Username = "Назар Качмарик(08i311)" });
                    listOfUsers.Add(new User() { Username = "Мирослав Голуб(08i312)" });
                    listOfUsers.Add(new User() { Username = "Павло Мартиник(08i313)" });
                    listOfUsers.Add(new User() { Username = "08i314 08i314(08i314)" });
                    listOfUsers.Add(new User() { Username = "08i315 Горячий(08i315)" });
                    listOfUsers.Add(new User() { Username = "Данило Савчак(08i316)" });
                    listOfUsers.Add(new User() { Username = "Ярослав Пиріг(08i317)" });
                    listOfUsers.Add(new User() { Username = "Ірина Харів(08i318)" });
                    listOfUsers.Add(new User() { Username = "Ярослав Мота(08i319)" });
                    listOfUsers.Add(new User() { Username = "08i320 Федорович(08i320)" });
                    break;
            }
            return listOfUsers;
        }
        
        public static AttemptResult GetFakeAttempt(User user, Topic topic)
        {
            float? attemptScore;
            switch (user.Username)
            {
                    //pmi-33
                case "Остап Андрусів(08i301)":
                    attemptScore = (float?)0.06;
                    return new AttemptResult(1, user, topic, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), DateTime.Now, DateTime.Now.AddMinutes(10), attemptScore);
                case "Назар Врублевський(08i302)":
                    attemptScore = (float?)0.09;
                    return new AttemptResult(1, user, topic, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), DateTime.Now, DateTime.Now.AddMinutes(10), attemptScore);
                case "Адмайкін Максим(08i303)":
                    attemptScore = (float?)0.04;
                    return new AttemptResult(1, user, topic, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), DateTime.Now, DateTime.Now.AddMinutes(10), attemptScore);
                case "Михайло Тис(08i304)":
                    attemptScore = (float?)0.06;
                    return new AttemptResult(1, user, topic, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), DateTime.Now, DateTime.Now.AddMinutes(10), attemptScore);
                case "Оля Іванків(08i305)":
                    attemptScore = (float?)0.05;
                    return new AttemptResult(1, user, topic, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), DateTime.Now, DateTime.Now.AddMinutes(10), attemptScore);
                case "Юрій Ожирко(08i306)":
                    attemptScore = (float?)0.07;
                    return new AttemptResult(1, user, topic, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), DateTime.Now, DateTime.Now.AddMinutes(10), attemptScore);
                case "Тарас Бехта(08i307)":
                    attemptScore = (float?)0.05;
                    return new AttemptResult(1, user, topic, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), DateTime.Now, DateTime.Now.AddMinutes(10), attemptScore);
                case "Василь Бодак(08i308)":
                    attemptScore = (float?)0.09;
                    return new AttemptResult(1, user, topic, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), DateTime.Now, DateTime.Now.AddMinutes(10), attemptScore);
                case "Василь Багряк(08i309)":
                    attemptScore = (float?)0.07;
                    return new AttemptResult(1, user, topic, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), DateTime.Now, DateTime.Now.AddMinutes(10), attemptScore);
                case "Максим Гула(08i310)":
                    attemptScore = (float?)0.05;
                    return new AttemptResult(1, user, topic, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), DateTime.Now, DateTime.Now.AddMinutes(10), attemptScore);
                case "Назар Качмарик(08i311)":
                    attemptScore = (float?)0.06;
                    return new AttemptResult(1, user, topic, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), DateTime.Now, DateTime.Now.AddMinutes(10), attemptScore);
                case "Мирослав Голуб(08i312)":
                    attemptScore = (float?)0.07;
                    return new AttemptResult(1, user, topic, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), DateTime.Now, DateTime.Now.AddMinutes(10), attemptScore);                
                case "Павло Мартиник(08i313)":
                    attemptScore = (float?)0.09;
                    return new AttemptResult(1, user, topic, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), DateTime.Now, DateTime.Now.AddMinutes(10), attemptScore);
                case "08i314 08i314(08i314)":
                    attemptScore = (float?)0.06;
                    return new AttemptResult(1, user, topic, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), DateTime.Now, DateTime.Now.AddMinutes(10), attemptScore);
                case "08i315 Горячий(08i315)":
                    attemptScore = (float?)0.07;
                    return new AttemptResult(1, user, topic, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), DateTime.Now, DateTime.Now.AddMinutes(10), attemptScore);
                case "Данило Савчак(08i316)":
                    attemptScore = (float?)0.04;
                    return new AttemptResult(1, user, topic, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), DateTime.Now, DateTime.Now.AddMinutes(10), attemptScore);
                case "Ярослав Пиріг(08i317)":
                    attemptScore = (float?)0.08;
                    return new AttemptResult(1, user, topic, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), DateTime.Now, DateTime.Now.AddMinutes(10), attemptScore);
                case "Ірина Харів(08i318)":
                    attemptScore = (float?)0.07;
                    return new AttemptResult(1, user, topic, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), DateTime.Now, DateTime.Now.AddMinutes(10), attemptScore);
                case "Ярослав Мота(08i319)":
                    attemptScore = (float?)0.07;
                    return new AttemptResult(1, user, topic, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), DateTime.Now, DateTime.Now.AddMinutes(10), attemptScore);
                case "08i320 Федорович(08i320)":
                    attemptScore = (float?)0.07;
                    return new AttemptResult(1, user, topic, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), DateTime.Now, DateTime.Now.AddMinutes(10), attemptScore);
                    //pmi-32
                case "Олег Булатовський(08i201)":
                    attemptScore = (float?)0.07;
                    return new AttemptResult(1, user, topic, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), DateTime.Now, DateTime.Now.AddMinutes(10), attemptScore);
                case "Василь Ванівський(08i202)":
                    attemptScore = (float?)0.08;
                    return new AttemptResult(1, user, topic, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), DateTime.Now, DateTime.Now.AddMinutes(10), attemptScore);
                case "Boзняк Максим(08i203)":
                    attemptScore = (float?)0.04;
                    return new AttemptResult(1, user, topic, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), DateTime.Now, DateTime.Now.AddMinutes(10), attemptScore);
                case "Юра Гой(08i204)":
                    attemptScore = (float?)0.06;
                    return new AttemptResult(1, user, topic, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), DateTime.Now, DateTime.Now.AddMinutes(10), attemptScore);
                case "Остап Демків(08i205)":
                    attemptScore = (float?)0.05;
                    return new AttemptResult(1, user, topic, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), DateTime.Now, DateTime.Now.AddMinutes(10), attemptScore);
                case "Юра Дерев`янко(08i206)":
                    attemptScore = (float?)0.07;
                    return new AttemptResult(1, user, topic, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), DateTime.Now, DateTime.Now.AddMinutes(10), attemptScore);
                case "Роман Дроботiй(08i207)":
                    attemptScore = (float?)0.04;
                    return new AttemptResult(1, user, topic, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), DateTime.Now, DateTime.Now.AddMinutes(10), attemptScore);
                case "Дубик Петро(08i208)":
                    attemptScore = (float?)0.09;
                    return new AttemptResult(1, user, topic, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), DateTime.Now, DateTime.Now.AddMinutes(10), attemptScore);
                case "Віталій Засадний(08i209)":
                    attemptScore = (float?)0.07;
                    return new AttemptResult(1, user, topic, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), DateTime.Now, DateTime.Now.AddMinutes(10), attemptScore);
                case "08i210 Андрусишин(08i210)":
                    attemptScore = (float?)0.05;
                    return new AttemptResult(1, user, topic, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), DateTime.Now, DateTime.Now.AddMinutes(10), attemptScore);
                case "Тарас Кміть(08i211)":
                    attemptScore = (float?)0.05;
                    return new AttemptResult(1, user, topic, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), DateTime.Now, DateTime.Now.AddMinutes(10), attemptScore);
                case "Роман Коваль(08i212)":
                    attemptScore = (float?)0.06;
                    return new AttemptResult(1, user, topic, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), DateTime.Now, DateTime.Now.AddMinutes(10), attemptScore);
                case "Маріана Кушла(08i213)":
                    attemptScore = (float?)0.08;
                    return new AttemptResult(1, user, topic, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), DateTime.Now, DateTime.Now.AddMinutes(10), attemptScore);
                case "Юрій Ладанівський(08i214)":
                    attemptScore = (float?)0.07;
                    return new AttemptResult(1, user, topic, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), DateTime.Now, DateTime.Now.AddMinutes(10), attemptScore);
                case "Leskiv Andriy(08i215)":
                    attemptScore = (float?)0.06;
                    return new AttemptResult(1, user, topic, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), DateTime.Now, DateTime.Now.AddMinutes(10), attemptScore);
                case "Юра Лучків(08i216)":
                    attemptScore = (float?)0.04;
                    return new AttemptResult(1, user, topic, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), DateTime.Now, DateTime.Now.AddMinutes(10), attemptScore);
                case "Mamchur Andriy(08i217)":
                    attemptScore = (float?)0.06;
                    return new AttemptResult(1, user, topic, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), DateTime.Now, DateTime.Now.AddMinutes(10), attemptScore);
                case "Христина Мандибур(08i218)":
                    attemptScore = (float?)0.05;
                    return new AttemptResult(1, user, topic, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), DateTime.Now, DateTime.Now.AddMinutes(10), attemptScore);
                case "Андрiй Протасов(08i219)":
                    attemptScore = (float?)0.07;
                    return new AttemptResult(1, user, topic, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), DateTime.Now, DateTime.Now.AddMinutes(10), attemptScore);
                case "08i220 08i220(08i220)":
                    attemptScore = (float?)0.07;
                    return new AttemptResult(1, user, topic, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), DateTime.Now, DateTime.Now.AddMinutes(10), attemptScore);
                case "Стадник Роман(08i221)":
                    attemptScore = (float?)0.07;
                    return new AttemptResult(1, user, topic, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), DateTime.Now, DateTime.Now.AddMinutes(10), attemptScore);
                case "Андрій Столбовой(08i222)":
                    attemptScore = (float?)0.05;
                    return new AttemptResult(1, user, topic, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), DateTime.Now, DateTime.Now.AddMinutes(10), attemptScore);
                case "Ігор Сторянський(08i223)":
                    attemptScore = (float?)0.05;
                    return new AttemptResult(1, user, topic, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), DateTime.Now, DateTime.Now.AddMinutes(10), attemptScore);
                case "Фатич Михайло(08i224)":
                    attemptScore = (float?)0.06;
                    return new AttemptResult(1, user, topic, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), DateTime.Now, DateTime.Now.AddMinutes(10), attemptScore);
                //pmi-31
                case "Роман Баїк(08i101)":
                    attemptScore = (float?)0.08;
                    return new AttemptResult(1, user, topic, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), DateTime.Now, DateTime.Now.AddMinutes(10), attemptScore);
                case "Катерина Бугай(08i102)":
                    attemptScore = (float?)0.08;
                    return new AttemptResult(1, user, topic, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), DateTime.Now, DateTime.Now.AddMinutes(10), attemptScore);
                case "Олексій Гелей(08i103)":
                    attemptScore = (float?)0.07;
                    return new AttemptResult(1, user, topic, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), DateTime.Now, DateTime.Now.AddMinutes(10), attemptScore);
                case "Карпунь Богдан(08i104)":
                    attemptScore = (float?)0.09;
                    return new AttemptResult(1, user, topic, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), DateTime.Now, DateTime.Now.AddMinutes(10), attemptScore);
                case "08i105 08i105(08i105)":
                    attemptScore = (float?)0.09;
                    return new AttemptResult(1, user, topic, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), DateTime.Now, DateTime.Now.AddMinutes(10), attemptScore);
                case "Олександр Козачук(08i106)":
                    attemptScore = (float?)0.07;
                    return new AttemptResult(1, user, topic, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), DateTime.Now, DateTime.Now.AddMinutes(10), attemptScore);
                case "Аня Кітчак(08i107)":
                    attemptScore = (float?)0.05;
                    return new AttemptResult(1, user, topic, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), DateTime.Now, DateTime.Now.AddMinutes(10), attemptScore);
                case "Кравець Роман(08i108)":
                    attemptScore = (float?)0.09;
                    return new AttemptResult(1, user, topic, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), DateTime.Now, DateTime.Now.AddMinutes(10), attemptScore);
                case "Андрій Крупич(08i109)":
                    attemptScore = (float?)0.08;
                    return new AttemptResult(1, user, topic, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), DateTime.Now, DateTime.Now.AddMinutes(10), attemptScore);
                case "Літинський Ростислав(08i110)":
                    attemptScore = (float?)0.09;
                    return new AttemptResult(1, user, topic, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), DateTime.Now, DateTime.Now.AddMinutes(10), attemptScore);
                case "Христина Макар(08i111)":
                    attemptScore = (float?)0.06;
                    return new AttemptResult(1, user, topic, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), DateTime.Now, DateTime.Now.AddMinutes(10), attemptScore);
                case "Юрко Тимчук(08i112)":
                    attemptScore = (float?)0.07;
                    return new AttemptResult(1, user, topic, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), DateTime.Now, DateTime.Now.AddMinutes(10), attemptScore);
                case "Oleg Papirnyk(08i113)":
                    attemptScore = (float?)0.09;
                    return new AttemptResult(1, user, topic, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), DateTime.Now, DateTime.Now.AddMinutes(10), attemptScore);
                case "Віталій Нобіс(08i114)":
                    attemptScore = (float?)0.08;
                    return new AttemptResult(1, user, topic, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), DateTime.Now, DateTime.Now.AddMinutes(10), attemptScore);
                case "Taras Pelenyo(08i115)":
                    attemptScore = (float?)0.07;
                    return new AttemptResult(1, user, topic, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), DateTime.Now, DateTime.Now.AddMinutes(10), attemptScore);
                case "08i116 08i116(08i116)":
                    attemptScore = (float?)0.05;
                    return new AttemptResult(1, user, topic, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), DateTime.Now, DateTime.Now.AddMinutes(10), attemptScore);
                case "Стадник Богдан(08i117)":
                    attemptScore = (float?)0.08;
                    return new AttemptResult(1, user, topic, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), DateTime.Now, DateTime.Now.AddMinutes(10), attemptScore);
                case "Andriy Pachva(08i118)":
                    attemptScore = (float?)0.05;
                    return new AttemptResult(1, user, topic, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), DateTime.Now, DateTime.Now.AddMinutes(10), attemptScore);
                case "Фай Роман(08i119)":
                    attemptScore = (float?)0.08;
                    return new AttemptResult(1, user, topic, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), DateTime.Now, DateTime.Now.AddMinutes(10), attemptScore);
                case "Мар'яна Хлєбик(08i120)":
                    attemptScore = (float?)0.07;
                    return new AttemptResult(1, user, topic, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), DateTime.Now, DateTime.Now.AddMinutes(10), attemptScore);
                case "08i121 08i121(08i121)":
                    attemptScore = (float?)0.07;
                    return new AttemptResult(1, user, topic, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), DateTime.Now, DateTime.Now.AddMinutes(10), attemptScore);
                case "Руслан Івать(08i122)":
                    attemptScore = (float?)0.07;
                    return new AttemptResult(1, user, topic, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), DateTime.Now, DateTime.Now.AddMinutes(10), attemptScore);
                case "Андрій Сташко(08i123)":
                    attemptScore = (float?)0.06;
                    return new AttemptResult(1, user, topic, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), DateTime.Now, DateTime.Now.AddMinutes(10), attemptScore);
                case "08i124 08i124(08i124)":
                    attemptScore = (float?)0.07;
                    return new AttemptResult(1, user, topic, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), DateTime.Now, DateTime.Now.AddMinutes(10), attemptScore);
                case "Ігор Михалевич(08i125)":
                    attemptScore = (float?)0.08;
                    return new AttemptResult(1, user, topic, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), DateTime.Now, DateTime.Now.AddMinutes(10), attemptScore);
                default:
                    return new AttemptResult(-1, user, topic, new CompletionStatus(), new AttemptStatus(), new SuccessStatus(), DateTime.Now, DateTime.Now.AddMinutes(10), (float?)1.0);
            }
        }
        public static IEnumerable<AnswerResult> GetFakeAnswers(AttemptResult attempt)
        {
            List<AnswerResult> listOfAnswers = new List<AnswerResult>();
            switch (attempt.User.Username)
            {
                //pmi-33
                case "Остап Андрусів(08i301)":
                    //  1	1	1	0	1	0	1	0	1	0
                    listOfAnswers.Add(new AnswerResult(0, "1", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "2", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "3", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "4", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "5", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "6", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "7", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "8", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "9", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "10", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    return listOfAnswers;
                case "Назар Врублевський(08i302)":
                    //  1	1	1	1	1	0	1	1	1	1
                    listOfAnswers.Add(new AnswerResult(0, "1", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "2", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "3", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "4", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "5", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "6", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "7", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "8", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "9", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "10", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    return listOfAnswers;
                case "Адмайкін Максим(08i303)":
                    //  0	1	0	0	1	0	1	1	0	0
                    listOfAnswers.Add(new AnswerResult(0, "1", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "2", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "3", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "4", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "5", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "6", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "7", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "8", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "9", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "10", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    return listOfAnswers;
                case "Михайло Тис(08i304)":
                    //  1	1	1	0	1	0	1	0	1	0
                    listOfAnswers.Add(new AnswerResult(0, "1", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "2", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "3", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "4", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "5", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "6", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "7", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "8", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "9", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "10", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    return listOfAnswers;
                case "Оля Іванків(08i305)":
                    //  1	1	0	0	1	0	1	1	0	0
                    listOfAnswers.Add(new AnswerResult(0, "1", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "2", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "3", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "4", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "5", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "6", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "7", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "8", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "9", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "10", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    return listOfAnswers;
                case "Юрій Ожирко(08i306)":
                    //  1	1	1	1	1	0	1	0	1	0
                    listOfAnswers.Add(new AnswerResult(0, "1", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "2", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "3", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "4", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "5", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "6", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "7", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "8", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "9", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "10", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    return listOfAnswers;
                case "Тарас Бехта(08i307)":
                    //  1	1	0	1	0	0	1	1	0	0
                    listOfAnswers.Add(new AnswerResult(0, "1", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "2", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "3", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "4", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "5", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "6", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "7", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "8", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "9", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "10", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    return listOfAnswers;
                case "Василь Бодак(08i308)":
                    //  1	1	1	1	1	1	1	0	1	1
                    listOfAnswers.Add(new AnswerResult(0, "1", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "2", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "3", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "4", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "5", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "6", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "7", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "8", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "9", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "10", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    return listOfAnswers;
                case "Василь Багряк(08i309)":
                    //  1	1	1	1	1	1	0	1	0	0
                    listOfAnswers.Add(new AnswerResult(0, "1", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "2", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "3", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "4", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "5", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "6", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "7", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "8", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "9", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "10", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    return listOfAnswers;
                case "Максим Гула(08i310)":
                    //  1	0	0	0	1	0	1	1	1	0
                    listOfAnswers.Add(new AnswerResult(0, "1", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "2", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "3", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "4", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "5", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "6", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "7", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "8", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "9", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "10", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    return listOfAnswers;
                case "Назар Качмарик(08i311)":
                    //  0	1	0	1	1	0	1	1	0	1
                    listOfAnswers.Add(new AnswerResult(0, "1", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "2", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "3", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "4", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "5", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "6", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "7", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "8", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "9", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "10", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    return listOfAnswers;
                case "Мирослав Голуб(08i312)":
                    //  1	1	1	0	1	0	1	1	1	0
                    listOfAnswers.Add(new AnswerResult(0, "1", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "2", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "3", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "4", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "5", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "6", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "7", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "8", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "9", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "10", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    return listOfAnswers;
                case "Павло Мартиник(08i313)":
                    //  1	1	1	1	0	1	1	1	1	1
                    listOfAnswers.Add(new AnswerResult(0, "1", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "2", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "3", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "4", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "5", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "6", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "7", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "8", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "9", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "10", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    return listOfAnswers;
                case "08i314 08i314(08i314)":
                    //  0	1	1	1	1	0	0	1	1	0
                    listOfAnswers.Add(new AnswerResult(0, "1", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "2", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "3", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "4", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "5", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "6", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "7", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "8", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "9", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "10", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    return listOfAnswers;
                case "08i315 Горячий(08i315)":
                    //  1	1	0	1	1	0	1	1	0	1
                    listOfAnswers.Add(new AnswerResult(0, "1", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "2", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "3", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "4", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "5", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "6", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "7", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "8", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "9", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "10", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    return listOfAnswers;
                case "Данило Савчак(08i316)":
                    //  0	0	0	1	1	0	0	1	1	0
                    listOfAnswers.Add(new AnswerResult(0, "1", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "2", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "3", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "4", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "5", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "6", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "7", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "8", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "9", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "10", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    return listOfAnswers;
                case "Ярослав Пиріг(08i317)":
                    //  1	1	1	1	1	0	1	0	1	1
                    listOfAnswers.Add(new AnswerResult(0, "1", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "2", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "3", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "4", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "5", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "6", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "7", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "8", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "9", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "10", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    return listOfAnswers;
                case "Ірина Харів(08i318)":
                    //  0	1	1	1	1	0	1	1	1	0
                    listOfAnswers.Add(new AnswerResult(0, "1", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "2", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "3", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "4", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "5", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "6", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "7", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "8", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "9", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "10", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    return listOfAnswers;
                case "Ярослав Мота(08i319)":
                    //  1	1	1	1	0	0	1	0	1	1
                    listOfAnswers.Add(new AnswerResult(0, "1", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "2", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "3", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "4", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "5", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "6", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "7", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "8", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "9", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "10", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    return listOfAnswers;
                case "08i320 Федорович(08i320)":
                    //  0	1	1	1	1	0	1	1	1	0
                    listOfAnswers.Add(new AnswerResult(0, "1", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "2", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "3", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "4", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "5", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "6", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "7", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "8", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "9", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "10", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    return listOfAnswers;
                //pmi-32
                case "Олег Булатовський(08i201)":
                    //  1	1	1	0	1	0	1	0	1	1
                    listOfAnswers.Add(new AnswerResult(0, "1", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "2", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "3", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "4", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "5", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "6", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "7", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "8", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "9", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "10", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    return listOfAnswers;
                case "Василь Ванівський(08i202)":
                    //  1	1	1	1	1	0	1	1	0	1
                    listOfAnswers.Add(new AnswerResult(0, "1", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "2", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "3", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "4", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "5", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "6", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "7", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "8", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "9", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "10", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    return listOfAnswers;
                case "Boзняк Максим(08i203)":
                    //  0	1	1	0	1	0	1	0	0	0
                    listOfAnswers.Add(new AnswerResult(0, "1", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "2", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "3", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "4", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "5", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "6", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "7", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "8", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "9", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "10", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    return listOfAnswers;
                case "Юра Гой(08i204)":
                    //  1	1	1	1	1	0	0	0	1	0
                    listOfAnswers.Add(new AnswerResult(0, "1", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "2", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "3", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "4", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "5", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "6", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "7", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "8", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "9", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "10", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    return listOfAnswers;
                case "Остап Демків(08i205)":
                    //  1	1	0	0	1	0	1	1	0	0
                    listOfAnswers.Add(new AnswerResult(0, "1", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "2", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "3", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "4", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "5", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "6", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "7", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "8", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "9", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "10", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    return listOfAnswers;
                case "Юра Дерев`янко(08i206)":
                    //  1	1	1	1	0	1	1	0	1	0
                    listOfAnswers.Add(new AnswerResult(0, "1", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "2", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "3", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "4", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "5", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "6", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "7", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "8", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "9", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "10", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    return listOfAnswers;
                case "Роман Дроботiй(08i207)":
                    //  1	1	0	0	0	0	1	1	0	0
                    listOfAnswers.Add(new AnswerResult(0, "1", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "2", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "3", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "4", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "5", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "6", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "7", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "8", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "9", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "10", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    return listOfAnswers;
                case "Дубик Петро(08i208)":
                    //  1	1	0	1	1	1	1	1	1	1
                    listOfAnswers.Add(new AnswerResult(0, "1", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "2", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "3", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "4", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "5", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "6", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "7", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "8", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "9", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "10", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    return listOfAnswers;
                case "Віталій Засадний(08i209)":
                    //  1	0	1	1	1	1	0	1	1	0
                    listOfAnswers.Add(new AnswerResult(0, "1", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "2", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "3", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "4", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "5", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "6", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "7", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "8", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "9", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "10", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    return listOfAnswers;
                case "08i210 Андрусишин(08i210)":
                    //  0	0	0	0	1	0	1	1	1	1
                    listOfAnswers.Add(new AnswerResult(0, "1", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "2", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "3", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "4", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "5", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "6", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "7", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "8", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "9", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "10", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    return listOfAnswers;
                case "Тарас Кміть(08i211)":
                    //  0	1	0	1	1	0	1	1	0	0
                    listOfAnswers.Add(new AnswerResult(0, "1", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "2", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "3", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "4", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "5", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "6", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "7", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "8", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "9", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "10", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    return listOfAnswers;
                case "Роман Коваль(08i212)":
                    //  1	1	1	0	1	0	1	1	0	0
                    listOfAnswers.Add(new AnswerResult(0, "1", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "2", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "3", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "4", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "5", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "6", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "7", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "8", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "9", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "10", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    return listOfAnswers;
                case "Маріана Кушла(08i213)":
                    //  1	1	1	1	0	1	1	0	1	1
                    listOfAnswers.Add(new AnswerResult(0, "1", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "2", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "3", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "4", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "5", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "6", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "7", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "8", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "9", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "10", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    return listOfAnswers;
                case "Юрій Ладанівський(08i214)":
                    //  1	1	1	1	1	0	0	1	1	0
                    listOfAnswers.Add(new AnswerResult(0, "1", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "2", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "3", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "4", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "5", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "6", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "7", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "8", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "9", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "10", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    return listOfAnswers;
                case "Leskiv Andriy(08i215)":
                    //  1	0	0	1	1	0	1	1	0	1
                    listOfAnswers.Add(new AnswerResult(0, "1", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "2", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "3", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "4", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "5", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "6", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "7", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "8", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "9", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "10", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    return listOfAnswers;
                case "Юра Лучків(08i216)":
                    //  0	0	1	1	0	0	0	1	1	0
                    listOfAnswers.Add(new AnswerResult(0, "1", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "2", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "3", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "4", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "5", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "6", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "7", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "8", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "9", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "10", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    return listOfAnswers;
                case "Mamchur Andriy(08i217)":
                    //  1	1	0	0	1	0	1	0	1	1
                    listOfAnswers.Add(new AnswerResult(0, "1", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "2", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "3", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "4", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "5", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "6", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "7", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "8", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "9", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "10", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    return listOfAnswers;
                case "Христина Мандибур(08i218)":
                    //  0	1	0	1	0	0	1	1	1	0
                    listOfAnswers.Add(new AnswerResult(0, "1", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "2", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "3", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "4", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "5", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "6", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "7", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "8", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "9", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "10", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    return listOfAnswers;
                case "Андрiй Протасов(08i219)":
                    //  0	1	1	1	0	1	1	0	1	1
                    listOfAnswers.Add(new AnswerResult(0, "1", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "2", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "3", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "4", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "5", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "6", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "7", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "8", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "9", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "10", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    return listOfAnswers;
                case "08i220 08i220(08i220)":
                    //  1	1	1	1	1	0	0	1	1	0
                    listOfAnswers.Add(new AnswerResult(0, "1", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "2", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "3", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "4", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "5", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "6", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "7", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "8", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "9", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "10", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    return listOfAnswers;
                case "Стадник Роман(08i221)":
                    //  1	0	1	1	0	1	1	0	1	1
                    listOfAnswers.Add(new AnswerResult(0, "1", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "2", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "3", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "4", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "5", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "6", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "7", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "8", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "9", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "10", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    return listOfAnswers;
                case "Андрій Столбовой(08i222)":
                    //  0	1	1	1	1	0	0	1	0	0
                    listOfAnswers.Add(new AnswerResult(0, "1", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "2", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "3", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "4", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "5", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "6", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "7", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "8", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "9", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "10", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    return listOfAnswers;
                case "Ігор Сторянський(08i223)":
                    //  1	1	0	0	1	0	1	1	0	0
                    listOfAnswers.Add(new AnswerResult(0, "1", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "2", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "3", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "4", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "5", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "6", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "7", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "8", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "9", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "10", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    return listOfAnswers;
                case "Фатич Михайло(08i224)":
                    //  0	1	0	1	1	0	1	1	1	0
                    listOfAnswers.Add(new AnswerResult(0, "1", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "2", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "3", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "4", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "5", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "6", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "7", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "8", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "9", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "10", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    return listOfAnswers;
                //pmi-31
                case "Роман Баїк(08i101)":
                    //  1	1	1	0	1	1	1	0	1	1
                    listOfAnswers.Add(new AnswerResult(0, "1", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "2", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "3", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "4", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "5", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "6", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "7", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "8", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "9", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "10", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    return listOfAnswers;
                case "Катерина Бугай(08i102)":
                    //  1	1	1	1	1	0	1	1	0	1
                    listOfAnswers.Add(new AnswerResult(0, "1", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "2", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "3", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "4", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "5", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "6", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "7", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "8", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "9", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "10", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    return listOfAnswers;
                case "Олексій Гелей(08i103)":
                    //  1	1	1	0	1	1	1	1	0	0
                    listOfAnswers.Add(new AnswerResult(0, "1", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "2", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "3", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "4", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "5", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "6", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "7", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "8", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "9", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "10", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    return listOfAnswers;
                case "Карпунь Богдан(08i104)":
                    //  1	1	1	1	1	0	1	1	1	1
                    listOfAnswers.Add(new AnswerResult(0, "1", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "2", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "3", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "4", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "5", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "6", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "7", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "8", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "9", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "10", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    return listOfAnswers;
                case "08i105 08i105(08i105)":
                    //  1	1	1	0	1	1	1	1	1	1
                    listOfAnswers.Add(new AnswerResult(0, "1", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "2", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "3", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "4", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "5", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "6", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "7", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "8", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "9", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "10", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    return listOfAnswers;
                case "Олександр Козачук(08i106)":
                    //  1	1	1	1	0	1	1	0	1	0
                    listOfAnswers.Add(new AnswerResult(0, "1", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "2", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "3", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "4", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "5", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "6", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "7", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "8", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "9", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "10", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    return listOfAnswers;
                case "Аня Кітчак(08i107)":
                    //  1	1	0	0	0	0	1	1	1	0
                    listOfAnswers.Add(new AnswerResult(0, "1", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "2", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "3", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "4", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "5", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "6", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "7", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "8", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "9", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "10", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    return listOfAnswers;
                case "Кравець Роман(08i108)":
                    //  1	1	0	1	1	1	1	1	1	1
                    listOfAnswers.Add(new AnswerResult(0, "1", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "2", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "3", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "4", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "5", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "6", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "7", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "8", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "9", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "10", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    return listOfAnswers;
                case "Андрій Крупич(08i109)":
                    //  1	0	1	1	1	1	1	1	1	0
                    listOfAnswers.Add(new AnswerResult(0, "1", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "2", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "3", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "4", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "5", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "6", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "7", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "8", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "9", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "10", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    return listOfAnswers;
                case "Літинський Ростислав(08i110)":
                    //  1	1	1	1	1	0	1	1	1	1
                    listOfAnswers.Add(new AnswerResult(0, "1", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "2", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "3", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "4", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "5", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "6", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "7", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "8", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "9", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "10", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    return listOfAnswers;
                case "Христина Макар(08i111)":
                    //  0	1	0	1	1	0	1	1	0	1
                    listOfAnswers.Add(new AnswerResult(0, "1", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "2", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "3", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "4", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "5", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "6", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "7", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "8", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "9", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "10", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    return listOfAnswers;
                case "Юрко Тимчук(08i112)":
                    //  1	1	1	0	1	0	1	1	1	0
                    listOfAnswers.Add(new AnswerResult(0, "1", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "2", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "3", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "4", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "5", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "6", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "7", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "8", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "9", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "10", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    return listOfAnswers;
                case "Oleg Papirnyk(08i113)":
                    //  1	1	1	1	0	1	1	1	1	1
                    listOfAnswers.Add(new AnswerResult(0, "1", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "2", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "3", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "4", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "5", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "6", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "7", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "8", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "9", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "10", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    return listOfAnswers;
                case "Віталій Нобіс(08i114)":
                    //  1	1	1	1	1	0	1	1	1	0
                    listOfAnswers.Add(new AnswerResult(0, "1", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "2", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "3", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "4", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "5", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "6", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "7", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "8", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "9", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "10", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    return listOfAnswers;
                case "Taras Pelenyo(08i115)":
                    //  1	0	1	1	1	0	1	1	0	1
                    listOfAnswers.Add(new AnswerResult(0, "1", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "2", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "3", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "4", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "5", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "6", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "7", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "8", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "9", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "10", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    return listOfAnswers;
                case "08i116 08i116(08i116)":
                    //  0	0	1	1	1	0	0	1	1	0
                    listOfAnswers.Add(new AnswerResult(0, "1", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "2", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "3", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "4", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "5", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "6", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "7", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "8", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "9", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "10", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    return listOfAnswers;
                case "Стадник Богдан(08i117)":
                    //  1	1	1	1	1	0	1	0	1	1
                    listOfAnswers.Add(new AnswerResult(0, "1", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "2", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "3", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "4", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "5", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "6", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "7", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "8", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "9", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "10", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    return listOfAnswers;
                case "Andriy Pachva(08i118)":
                    //  0	1	0	1	0	0	1	1	1	0
                    listOfAnswers.Add(new AnswerResult(0, "1", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "2", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "3", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "4", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "5", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "6", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "7", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "8", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "9", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "10", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    return listOfAnswers;
                case "Фай Роман(08i119)":
                    //  1	1	1	1	0	1	1	0	1	1
                    listOfAnswers.Add(new AnswerResult(0, "1", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "2", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "3", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "4", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "5", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "6", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "7", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "8", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "9", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "10", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    return listOfAnswers;
                case "Мар'яна Хлєбик(08i120)":
                    //  1	1	1	1	1	0	0	1	1	0
                    listOfAnswers.Add(new AnswerResult(0, "1", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "2", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "3", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "4", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "5", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "6", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "7", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "8", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "9", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "10", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    return listOfAnswers;
                case "08i121 08i121(08i121)":
                    //  1	0	1	1	0	1	1	0	1	1
                    listOfAnswers.Add(new AnswerResult(0, "1", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "2", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "3", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "4", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "5", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "6", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "7", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "8", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "9", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "10", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    return listOfAnswers;
                case "Руслан Івать(08i122)":
                    //  1	1	1	1	1	0	0	1	0	1
                    listOfAnswers.Add(new AnswerResult(0, "1", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "2", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "3", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "4", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "5", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "6", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "7", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "8", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "9", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "10", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    return listOfAnswers;
                case "Андрій Сташко(08i123)":
                    //  1	1	0	1	1	0	1	1	0	0
                    listOfAnswers.Add(new AnswerResult(0, "1", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "2", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "3", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "4", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "5", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "6", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "7", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "8", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "9", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "10", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    return listOfAnswers;
                case "08i124 08i124(08i124)":
                    //  0	1	1	1	1	0	0	1	1	1
                    listOfAnswers.Add(new AnswerResult(0, "1", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "2", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "3", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "4", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "5", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "6", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "7", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "8", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "9", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "10", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    return listOfAnswers;
                case "Ігор Михалевич(08i125)":
                    //  1	1	1	1	1	0	0	1	1	1
                    listOfAnswers.Add(new AnswerResult(0, "1", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "2", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "3", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "4", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "5", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "6", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "7", 0, attempt, "123", "123", InteractionType.Other, (float?)0.0));
                    listOfAnswers.Add(new AnswerResult(0, "8", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "9", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    listOfAnswers.Add(new AnswerResult(0, "10", 0, attempt, "123", "123", InteractionType.Other, (float?)1.0));
                    return listOfAnswers;
                default:
                    return null;
            }
        }
    }
}
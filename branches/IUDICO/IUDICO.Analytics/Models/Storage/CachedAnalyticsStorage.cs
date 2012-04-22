using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IUDICO.Common.Models.Caching;
using IUDICO.Common.Models.Shared;
using IUDICO.Analytics.Models.ViewDataClasses;

namespace IUDICO.Analytics.Models.Storage
{
    public class CachedAnalyticsStorage: IAnalyticsStorage
    {
        private readonly IAnalyticsStorage _storage;
        private readonly ICacheProvider _cacheProvider;
        private readonly object lockObject = new object();

        public CachedAnalyticsStorage(IAnalyticsStorage storage, ICacheProvider cacheProvider)
        {
            _storage = storage;
            _cacheProvider = cacheProvider;
        }
        /*
        public void RefreshState()
        {
            throw new NotImplementedException();
        }
        */
        public IEnumerable<ForecastingTree> GetAllForecastingTrees()
        {
            return _cacheProvider.Get<IEnumerable<ForecastingTree>>("forecastingtrees", @lockObject, () => _storage.GetAllForecastingTrees(), DateTime.Now.AddDays(1), "forecastingtrees");
        }

        public IEnumerable<ForecastingTree> GetForecastingTrees(Guid userRef)
        {
            return _cacheProvider.Get<IEnumerable<ForecastingTree>>("forecastingtrees-" + userRef, @lockObject, () => _storage.GetAllForecastingTrees().Where(f => f.UserRef == userRef), DateTime.Now.AddDays(1), "forecastingtrees");
        }

        public IEnumerable<TopicStat> GetRecommenderTopics(User user)
        {
            return _cacheProvider.Get<IEnumerable<TopicStat>>("recommendertopics-" + user.Username, @lockObject, () => _storage.GetRecommenderTopics(user), DateTime.Now.AddDays(1), "recommendertopics-" + user.Username);
        }

        public IEnumerable<TopicStat> GetRecommenderTopics(User user, int amount)
        {
            return GetRecommenderTopics(user).Take(amount);

            //return _cacheProvider.Get<IEnumerable<TopicStat>>("recommendertopics-" + user.Username + "", @lockObject, () => _storage.GetRecommenderTopics(user), DateTime.Now.AddDays(1), "recommendertopics-" + user.Username);
        }

        public IEnumerable<Tag> GetTags()
        {
            return _cacheProvider.Get<IEnumerable<Tag>>("tags", @lockObject, () => _storage.GetTags(), DateTime.Now.AddDays(1), "tags");
        }

        public Tag GetTag(int id)
        {
            return _cacheProvider.Get<Tag>("tag-" + id, @lockObject, () => _storage.GetTag(id), DateTime.Now.AddDays(1), "tag-" + id);    
        }

        public ViewTagDetails GetTagDetails(int id)
        {
            return _cacheProvider.Get<ViewTagDetails>("tagdetails-" + id, @lockObject, () => _storage.GetTagDetails(id), DateTime.Now.AddDays(1), "tagdetails-" + id, "tag-" + id);
        }

        public ViewTagDetails GetTagDetailsWithTopics(int id)
        {
            return _cacheProvider.Get<ViewTagDetails>("tagdetails-full-" + id, @lockObject, () => _storage.GetTagDetailsWithTopics(id), DateTime.Now.AddDays(1), "tagdetails-" + id, "tag-" + id);
        }

        public void CreateTag(Tag feature)
        {
            _storage.CreateTag(feature);

            _cacheProvider.Invalidate("tags");
        }

        public void EditTag(int id, Tag feature)
        {
            _storage.EditTag(id, feature);
            
            _cacheProvider.Invalidate("tags", "tag-" + id);
        }

        public void DeleteTag(int id)
        {
            _storage.DeleteTag(id);

            _cacheProvider.Invalidate("tags", "tag-" + id);
        }

        public void EditTags(int id, IEnumerable<int> topics)
        {
            _storage.EditTags(id, topics);

            _cacheProvider.Invalidate("tags", "tag-" + id);
        }

        public Dictionary<int, IEnumerable<TopicScore>> GetTopicScores()
        {
            return _cacheProvider.Get<Dictionary<int, IEnumerable<TopicScore>>>("topicscores", @lockObject, () => _storage.GetTopicScores(), DateTime.Now.AddDays(1), "topicscores"); 
        }

        public Dictionary<Guid, IEnumerable<UserScore>> GetUserScores()
        {
            return _cacheProvider.Get<Dictionary<Guid, IEnumerable<UserScore>>>("userscores", @lockObject, () => _storage.GetUserScores(), DateTime.Now.AddDays(1), "userscores");
        }

        public void UpdateUserScores(Guid id)
        {
            _cacheProvider.Invalidate("userscores");
        }

        public void UpdateTopicScores(int id)
        {
            _cacheProvider.Invalidate("topicscores");
        }

        #region Anomaly detection

        public IEnumerable<Topic> AvailebleTopics()
        {
            //User teacherUser = _LmsService.FindService<IUserService>().GetCurrentUser();
            //return _LmsService.FindService<ICurriculumService>().GetTopicsOwnedByUser(teacherUser);
            Topic t = new Topic();
            t.Id = 99999;
            t.Name = "Test topic";
            List<Topic> res = new List<Topic>();
            res.Add(t);
            return res;
        }

        public IEnumerable<Group> AvailebleGroups(int topicId)
        {
            //User teacherUser = _LmsService.FindService<IUserService>().GetCurrentUser();
            //return _LmsService.FindService<ICurriculumService>().GetTopicsOwnedByUser(teacherUser);
            Group i41 = new Group();
            i41.Id = 1;
            i41.Name = "PMI 41";
            Group i42 = new Group();
            i42.Id = 2;
            i42.Name = "PMI 42";
            Group i43 = new Group();
            i43.Id = 3;
            i43.Name = "PMI 43";
            List<Group> res = new List<Group>();
            res.Add(i41);
            res.Add(i42);
            res.Add(i43);
            return res;
        }

        public IEnumerable<KeyValuePair<User, double[]>> GetAllStudentListForTraining(int topicId)
        {
            //var listOfGroups = _LmsService.FindService<ICurriculumService>().GetGroupsAssignedToTopic(topicId);
            //IEnumerable<User> studentsFromSelectedGroups = new List<User>();
            //foreach (var group in listOfGroups)
            //{
            //    studentsFromSelectedGroups = studentsFromSelectedGroups.Union(_LmsService.FindService<IUserService>().GetUsersByGroup(group));
            //}
            List<KeyValuePair<User, double[]>> result = new List<KeyValuePair<User, double[]>>();
            return result.Concat(GetStudentListForPMI41()).Concat(GetStudentListForPMI42()).Concat(GetStudentListForPMI43());
            //foreach (var student in studentsFromSelectedGroups)
            //{
            //    AttemptResult att = new AttemptResult(1,student,null,new CompletionStatus(),new AttemptStatus(),new SuccessStatus(),null,null,rnd.Next(1,20)/100);
            //    result.Add(new KeyValuePair<User,AttemptResult>(student,att));
            //}
            //return result;
        }

        public IEnumerable<KeyValuePair<User, double[]>> GetStudentListForTraining(int topicId, int groupId)
        {
            //var listOfGroups = _LmsService.FindService<ICurriculumService>().GetGroupsAssignedToTopic(topicId);
            //IEnumerable<User> studentsFromSelectedGroups = new List<User>();
            //foreach (var group in listOfGroups)
            //{
            //    studentsFromSelectedGroups = studentsFromSelectedGroups.Union(_LmsService.FindService<IUserService>().GetUsersByGroup(group));
            //}
            List<KeyValuePair<User, double[]>> result = new List<KeyValuePair<User, double[]>>();
            switch (groupId)
            {
                case 1:
                    return result.Concat(GetStudentListForPMI41());
                    break;
                case 2:
                    return result.Concat(GetStudentListForPMI42());
                    break;
                case 3:
                    return result.Concat(GetStudentListForPMI43());
                    break;
                default:
                    throw new Exception();
            }
            //foreach (var student in studentsFromSelectedGroups)
            //{
            //    AttemptResult att = new AttemptResult(1,student,null,new CompletionStatus(),new AttemptStatus(),new SuccessStatus(),null,null,rnd.Next(1,20)/100);
            //    result.Add(new KeyValuePair<User,AttemptResult>(student,att));
            //}
            return result;
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
            double[] coef = new double[] { 400, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "2";
            score = 9;
            user.Name = "Назар Врублевський(08i302)";
            coef = new double[] { 450, 3 - rnd.Next(10, 100) / 100.0, 2 - rnd.Next(10, 100) / 100.0, 4 - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "3";
            score = 4;
            user.Name = "Адмайкін Максим(08i303)";
            coef = new double[] { 350, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "4";
            score = 6;
            user.Name = "Михайло Тис(08i304)";
            coef = new double[] { 400, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "5";
            score = 5;
            user.Name = "Оля Іванків(08i305)";
            coef = new double[] { 400, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "6";
            score = 7;
            user.Name = "Юрій Ожирко(08i306)";
            coef = new double[] { 450, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "7";
            score = 5;
            user.Name = "Тарас Бехта(08i307)";
            coef = new double[] { 300, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "8";
            score = 9;
            user.Name = "Василь Бодак(08i308)";
            coef = new double[] { 100, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "9";
            score = 7;
            user.Name = "Василь Багряк(08i309)";
            coef = new double[] { 350, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "10";
            score = 9;
            user.Name = "Максим Гула(08i310)";
            coef = new double[] { 150, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "11";
            score = 6;
            user.Name = "Назар Качмарик(08i311)";
            coef = new double[] { 350, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "12";
            score = 7;
            user.Name = "Мирослав Голуб(08i312)";
            coef = new double[] { 400, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "13";
            score = 9;
            user.Name = "Павло Мартиник(08i313)";
            coef = new double[] { 550, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "14";
            score = 6;
            user.Name = "08i314 08i314(08i314)";
            coef = new double[] { 400, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "15";
            score = 7;
            user.Name = "08i315 Горячий(08i315)";
            coef = new double[] { 450, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef)); user = new User();

            user = new User();
            user.OpenId = "16";
            score = 4;
            user.Name = "Данило Савчак(08i316)";
            coef = new double[] { 250, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "17";
            score = 8;
            user.Name = "Ярослав Пиріг(08i317)";
            coef = new double[] { 400, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));
            user = new User();

            user = new User();
            user.OpenId = "18";
            score = 7;
            user.Name = "Ірина Харів(08i318)";
            coef = new double[] { 400, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "19";
            score = 7;
            user.Name = "Ярослав Мота(08i319)";
            coef = new double[] { 350, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "20";
            score = 7;
            user.Name = "08i320 Федорович(08i320)";
            coef = new double[] { 450, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));
            #endregion

            return result;
        }

        public IEnumerable<KeyValuePair<User, double[]>> GetStudentListForPMI42()
        {
            List<KeyValuePair<User, double[]>> result = new List<KeyValuePair<User, double[]>>();
            Random rnd = new Random(System.Environment.TickCount);

            #region PMI-42
            User user = new User();
            user.OpenId = "21";
            int score = 7;
            user.Name = "Олег Булатовський(08i201)";
            double[] coef = new double[] { 400, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "22";
            score = 8;
            user.Name = "Василь Ванівський(08i202)";
            coef = new double[] { 400, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "23";
            score = 4;
            user.Name = "Boзняк Максим(08i203)";
            coef = new double[] { 300, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "24";
            score = 6;
            user.Name = "Юра Гой(08i204)";
            coef = new double[] { 350, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "25";
            score = 5;
            user.Name = "Остап Демків(08i205)";
            coef = new double[] { 300, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "26";
            score = 7;
            user.Name = "Юра Дерев`янко(08i206)";
            coef = new double[] { 300, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "27";
            score = 4;
            user.Name = "Роман Дроботiй(08i207)";
            coef = new double[] { 200, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "28";
            score = 9;
            user.Name = "Дубик Петро(08i208)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "29";
            score = 7;
            user.Name = "Віталій Засадний(08i209)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "30";
            score = 5;
            user.Name = "08i210 Андрусишин(08i210)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "31";
            score = 5;
            user.Name = "Тарас Кміть(08i211)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "32";
            score = 6;
            user.Name = "Роман Коваль(08i212)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "33";
            score = 8;
            user.Name = "Маріана Кушла(08i213)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "34";
            score = 7;
            user.Name = "Юрій Ладанівський(08i214)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "35";
            score = 6;
            user.Name = "Leskiv Andriy(08i215)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "36";
            score = 4;
            user.Name = "Юра Лучків(08i216)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "37";
            score = 6;
            user.Name = "Mamchur Andriy(08i217)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "38";
            score = 5;
            user.Name = "Христина Мандибур(08i218)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "39";
            score = 7;
            user.Name = "Андрiй Протасов(08i219)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "40";
            score = 7;
            user.Name = "08i220 08i220(08i220)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "41";
            score = 7;
            user.Name = "Стадник Роман(08i221)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "42";
            score = 5;
            user.Name = "Андрій Столбовой(08i222)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "43";
            score = 5;
            user.Name = "Ігор Сторянський(08i223)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "44";
            score = 6;
            user.Name = "Фатич Михайло(08i224)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            #endregion

            return result;
        }

        public IEnumerable<KeyValuePair<User, double[]>> GetStudentListForPMI41()
        {
            List<KeyValuePair<User, double[]>> result = new List<KeyValuePair<User, double[]>>();
            Random rnd = new Random(System.Environment.TickCount);

            #region PMI-41
            User user = new User();
            user.OpenId = "45";
            int score = 8;
            user.Name = "Роман Баїк(08i101)";
            double[] coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "46";
            score = 8;
            user.Name = "Катерина Бугай(08i102)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "47";
            score = 7;
            user.Name = "Олексій Гелей(08i103)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "48";
            score = 9;
            user.Name = "Карпунь Богдан(08i104)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "49";
            score = 9;
            user.Name = "08i105 08i105(08i105)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "50";
            score = 7;
            user.Name = "Олександр Козачук(08i106)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "51";
            score = 5;
            user.Name = "Аня Кітчак(08i107)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "52";
            score = 9;
            user.Name = "Кравець Роман(08i108)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "53";
            score = 8;
            user.Name = "Андрій Крупич(08i109)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "54";
            score = 9;
            user.Name = "Літинський Ростислав(08i110)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "55";
            score = 6;
            user.Name = "Христина Макар(08i111)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "56";
            score = 7;
            user.Name = "Юрко Тимчук(08i112)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "57";
            score = 9;
            user.Name = "Oleg Papirnyk(08i113)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "58";
            score = 8;
            user.Name = "Віталій Нобіс(08i114)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "59";
            score = 7;
            user.Name = "Taras Pelenyo(08i115)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "60";
            score = 5;
            user.Name = "08i116 08i116(08i116)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "61";
            score = 8;
            user.Name = "Стадник Богдан(08i117)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "62";
            score = 5;
            user.Name = "Andriy Pachva(08i118)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "63";
            score = 8;
            user.Name = "Фай Роман(08i119)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "64";
            score = 7;
            user.Name = "Мар'яна Хлєбик(08i120)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "65";
            score = 7;
            user.Name = "08i121 08i121(08i121)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "66";
            score = 7;
            user.Name = "Руслан Івать(08i122)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "67";
            score = 6;
            user.Name = "Андрій Сташко(08i123)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "68";
            score = 7;
            user.Name = "08i124 08i124(08i124)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));

            user = new User();
            user.OpenId = "69";
            score = 8;
            user.Name = "Ігор Михалевич(08i125)";
            coef = new double[] { rnd.Next(80, 600), score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score - rnd.Next(10, 100) / 100.0, score * 10 - rnd.Next(0, 10) };
            result.Add(new KeyValuePair<User, double[]>(user, coef));
            #endregion

            return result;
        }

        #endregion
    }
}
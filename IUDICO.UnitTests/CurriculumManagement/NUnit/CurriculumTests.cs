using System;
using System.Linq;
using IUDICO.Common.Models.Shared;
using IUDICO.CurriculumManagement.Controllers;
using NUnit.Framework;

namespace IUDICO.UnitTests.CurriculumManagement.NUnit
{
    [TestFixture]
    public class CurriculumTests : BaseTestFixture
    {
        #region CurriculumMethods

        [Test]
        public void AddCurriculum()
        {
            this.DataPreparer.CreateCurriculumsSet1();
            var expectedItems = this.DataPreparer.GetCurriculums();
            var controller = GetController<CurriculumController>();

            // add curriculums
            expectedItems.ForEach(item => controller.Create(item.ToCreateModel()));
            // then add "special" curriculum
            controller.Create(
                new Curriculum
                {
                    IsValid = false,
                    IsDeleted = true,
                    DisciplineRef = this.DataPreparer.DisciplineIds[0],
                    UserGroupRef = this.UserService.GetGroup(2).Id }.ToCreateModel());

            expectedItems = this.DataPreparer.GetCurriculums();
            var allItems = this.CurriculumStorage.GetCurriculums()
                .OrderBy(item => item.Id)
                .ToList();
            var actualItems = allItems
                .Take(expectedItems.Count)
                .ToList();
            var actualItem = allItems.Last();

            AdvAssert.AreEqual(expectedItems, actualItems);
            Assert.AreEqual(false, actualItem.IsDeleted);
            Assert.AreEqual(true, actualItem.IsValid);

            // add bad curriculum
            controller = GetController<CurriculumController>();
            controller.Create(
                new Curriculum
                {
                    DisciplineRef = this.DataPreparer.DisciplineIds[1],
                    UserGroupRef = this.UserService.GetGroup(1).Id,
                    StartDate = DateTime.Now.AddDays(1), // bad start date
                    EndDate = DateTime.Now }.ToCreateModel());
            Assert.AreEqual(false, controller.ModelState.IsValid);
            Assert.AreEqual(expectedItems.Count + 1, this.CurriculumStorage.GetCurriculums().Count);

            // add bad curriculum
            controller = GetController<CurriculumController>();
            controller.Create(
                new Curriculum
                {
                    DisciplineRef = this.DataPreparer.DisciplineIds[0], // curriculum with same group and discipline already exist!
                    UserGroupRef = this.UserService.GetGroup(2).Id,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now }.ToCreateModel());
            Assert.AreEqual(false, controller.ModelState.IsValid);
            Assert.AreEqual(expectedItems.Count + 1, this.CurriculumStorage.GetCurriculums().Count);

            // add bad curriculum
            controller = GetController<CurriculumController>();
            controller.Create(
                new Curriculum
                {
                    DisciplineRef = -1, // disciplineId < 0
                    UserGroupRef = this.UserService.GetGroup(2).Id,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now }.ToCreateModel());
            Assert.AreEqual(false, controller.ModelState.IsValid);
            Assert.AreEqual(expectedItems.Count + 1, this.CurriculumStorage.GetCurriculums().Count);

            // add bad curriculum
            controller = GetController<CurriculumController>();
            controller.Create(
                new Curriculum
                {
                    DisciplineRef = this.DataPreparer.DisciplineIds[0],
                    UserGroupRef = -1, // groupId < 0
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now }.ToCreateModel());
            Assert.AreEqual(false, controller.ModelState.IsValid);
            Assert.AreEqual(expectedItems.Count + 1, this.CurriculumStorage.GetCurriculums().Count);

            // add bad curriculum
            // controller = GetController<CurriculumController>();
            // controller.Create(
            // new Curriculum
            // {
            // DisciplineRef = _DataPreparer.DisciplineIds[1],
            // UserGroupRef = _UserService.GetGroup(1).Id,
            // StartDate = DateTime.Now, //choose end date too!
            // EndDate = null
            // }.ToCreateModel()
            // );
            // Assert.AreEqual(false, controller.ModelState.IsValid);
            // Assert.AreEqual(expectedItems.Count + 1, _CurriculumStorage.GetCurriculums().Count);

            // add bad curriculum
            controller = GetController<CurriculumController>();
            controller.Create(
                new Curriculum
                {
                    DisciplineRef = this.DataPreparer.DisciplineIds[1],
                    UserGroupRef = this.UserService.GetGroup(1).Id,
                    StartDate = new DateTime(1100, 1, 1), // too small date
                    EndDate = new DateTime(2400, 1, 1) // too big date
                }.ToCreateModel());
            Assert.AreEqual(false, controller.ModelState.IsValid);
            Assert.AreEqual(expectedItems.Count + 1, this.CurriculumStorage.GetCurriculums().Count);
        }

/*        [Test]
        public void UpdateCurriculum()
        {
            IUserService userService = _Tests.LmsService.FindService<IUserService>();
            Group gr = userService.GetGroup(2);

            var disciplines = CreateDefaultData();
            disciplines.ForEach(item => _Storage.AddDiscipline(item));

            var curriculums =
                disciplines.Select(item => new Curriculum { Discipline = item, UserGroupRef = gr.Id }).ToList();
            var ids = curriculums.Select(item => _Storage.AddCurriculum(item)).ToList();

            curriculums.Select((item, i) => i)
                .ToList()
                .ForEach(i => AdvAssert.AreEqual(curriculums[i], _Storage.GetCurriculum(ids[i])));

            var timelines = curriculums.Select(item => new Timeline
                                                           {
                                                               Curriculum = item,
                                                               StartDate = new DateTime(2011, 12, 1),
                                                               EndDate = new DateTime(2011, 12, 31)
                                                           }).ToList();
            timelines.ForEach(item => _Storage.AddTimeline(item));

            var chapters = disciplines.Select(item => new Chapter { Discipline = item, Name = "Chapter" }).ToList();
            var idsSt = chapters.Select(item => _Storage.AddChapter(item)).ToList();

            List<Timeline> chapterTimeline = new List<Timeline>();
            for (int i = 0; i < disciplines.Count; ++i)
            {
                chapterTimeline.Add(new Timeline
                                        {
                                            Curriculum = curriculums[i],
                                            ChapterRef = idsSt[i],
                                            StartDate = new DateTime(2011, 12, 1 + i * 3),
                                            EndDate = new DateTime(2011, 12, 4 + i * 3)
                                        });
            }
            chapterTimeline.ForEach(item => _Storage.AddTimeline(item));

            var topic =
                chapters.Select(item => new Topic { Name = "Topic", Chapter = item, TopicType = _Storage.GetTopicType(2) })
                    .ToList();
            topic.ForEach(item => _Storage.AddTopic(item));

            List<TopicAssignment> topicAssignments = new List<TopicAssignment>();
            for (int i = 0; i < disciplines.Count; ++i)
            {
                topicAssignments.Add(new TopicAssignment
                                         {
                                             Curriculum = curriculums[i],
                                             Topic = topic[i],
                                             MaxScore = i * 5
                                         });
            }
            topicAssignments.ForEach(item => _Storage.AddTopicAssignment(item));

            curriculums.ForEach(item => _Storage.UpdateCurriculum(item));

            curriculums.Select((item, i) => i)
                .ToList()
                .ForEach(i => AdvAssert.AreEqual(curriculums[i], _Storage.GetCurriculum(ids[i])));

            try
            {
                _Storage.UpdateCurriculum(null);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual(true, true);
            }
            try
            {
                _Storage.UpdateCurriculum(new Curriculum());
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual(true, true);
            }
        }

        [Test]
        public void DeleteCurriculum()
        {
            IUserService userService = _Tests.LmsService.FindService<IUserService>();
            Group gr = userService.GetGroup(2);

            var disciplines = CreateDefaultData();
            disciplines.ForEach(item => _Storage.AddDiscipline(item));

            var curriculums =
                disciplines.Select(item => new Curriculum { Discipline = item, UserGroupRef = gr.Id }).ToList();

            var timelines = curriculums.Select(item => new Timeline
                                                           {
                                                               Curriculum = item,
                                                               StartDate = new DateTime(2011, 5, 1),
                                                               EndDate = new DateTime(2011, 5, 31)
                                                           }).ToList();
            var idsT = timelines.Select(item => _Storage.AddTimeline(item)).ToList();

            var chapters = disciplines.Select(item => new Chapter { Discipline = item, Name = "Chapter" }).ToList();
            var idsSt = chapters.Select(item => _Storage.AddChapter(item)).ToList();

            List<Timeline> chapterTimeline = new List<Timeline>();
            for (int i = 0; i < disciplines.Count; ++i)
            {
                chapterTimeline.Add(new Timeline
                                        {
                                            Curriculum = curriculums[i],
                                            ChapterRef = idsSt[i],
                                            StartDate = new DateTime(2011, 5, 1 + i * 4),
                                            EndDate = new DateTime(2011, 5, 4 + i * 4)
                                        });
            }
            var idsStT = chapterTimeline.Select(item => _Storage.AddTimeline(item)).ToList();

            var topic =
                chapters.Select(item => new Topic { Name = "Topic", Chapter = item, TopicType = _Storage.GetTopicType(2) })
                    .ToList();
            topic.ForEach(item => _Storage.AddTopic(item));

            List<TopicAssignment> topicAssignments = new List<TopicAssignment>();
            for (int i = 0; i < disciplines.Count; ++i)
            {
                topicAssignments.Add(new TopicAssignment
                                         {
                                             Curriculum = curriculums[i],
                                             Topic = topic[i],
                                             MaxScore = i * 5
                                         });
            }
            var idsThA = topicAssignments.Select(item => _Storage.AddTopicAssignment(item)).ToList();


            var ids = curriculums.Select(item => _Storage.AddCurriculum(item)).ToList();

            curriculums.Select((item, i) => i)
                .ToList()
                .ForEach(i => AdvAssert.AreEqual(curriculums[i], _Storage.GetCurriculum(ids[i])));


            curriculums.ForEach(item => _Storage.DeleteCurriculum(item.Id));


            curriculums.Select((item, i) => i)
                .ToList()
                .ForEach(i => Assert.AreEqual(null, _Storage.GetCurriculum(ids[i])));

            Assert.AreEqual(0, _Storage.GetChapterTimelinesByCurriculumId(1).ToList().Count());
            for (int i = 0; i < chapterTimeline.Count; ++i)
            {
                for (int j = 0; j < _Storage.GetChapterTimelinesByCurriculumId(ids[i]).ToList().Count; ++j)
                {
                    Assert.AreEqual(null, _Storage.GetChapterTimelinesByCurriculumId(ids[i]).ToList()[j]);
                }
            }

            //topicAssignments.Select((item, i) => i)
            //    .ToList()
            //    .ForEach(i => Assert.AreEqual(null, _Storage.GetTopicAssignment(idsThA[i])));

            try
            {
                _Storage.DeleteCurriculum(ids.Max() + 1);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual(true, true);
            }
        }

        [Test]
        public void DeleteCurriculums()
        {
            IUserService userService = _Tests.LmsService.FindService<IUserService>();
            Group gr = userService.GetGroup(2);

            var disciplines = CreateDefaultData();
            disciplines.ForEach(item => _Storage.AddDiscipline(item));

            var curriculums =
                disciplines.Select(item => new Curriculum { Discipline = item, UserGroupRef = gr.Id }).ToList();

            var timelines = curriculums.Select(item => new Timeline
                                                           {
                                                               Curriculum = item,
                                                               StartDate = new DateTime(2011, 5, 1),
                                                               EndDate = new DateTime(2011, 5, 31)
                                                           }).ToList();
            var idsT = timelines.Select(item => _Storage.AddTimeline(item)).ToList();

            var chapters = disciplines.Select(item => new Chapter { Discipline = item, Name = "Chapter" }).ToList();
            var idsSt = chapters.Select(item => _Storage.AddChapter(item)).ToList();

            List<Timeline> chapterTimeline = new List<Timeline>();
            for (int i = 0; i < disciplines.Count; ++i)
            {
                chapterTimeline.Add(new Timeline
                                        {
                                            Curriculum = curriculums[i],
                                            ChapterRef = idsSt[i],
                                            StartDate = new DateTime(2011, 5, 1 + i * 4),
                                            EndDate = new DateTime(2011, 5, 4 + i * 4)
                                        });
            }
            var idsStT = chapterTimeline.Select(item => _Storage.AddTimeline(item)).ToList();

            var topic =
                chapters.Select(item => new Topic { Name = "Topic", Chapter = item, TopicType = _Storage.GetTopicType(2) })
                    .ToList();
            topic.ForEach(item => _Storage.AddTopic(item));

            List<TopicAssignment> topicAssignments = new List<TopicAssignment>();
            for (int i = 0; i < disciplines.Count; ++i)
            {
                topicAssignments.Add(new TopicAssignment
                                         {
                                             Curriculum = curriculums[i],
                                             Topic = topic[i],
                                             MaxScore = i * 5
                                         });
            }
            var idsThA = topicAssignments.Select(item => _Storage.AddTopicAssignment(item)).ToList();


            var ids = curriculums.Select(item => _Storage.AddCurriculum(item)).ToList();

            curriculums.Select((item, i) => i)
                .ToList()
                .ForEach(i => AdvAssert.AreEqual(curriculums[i], _Storage.GetCurriculum(ids[i])));


            _Storage.DeleteCurriculums(ids);


            curriculums.Select((item, i) => i)
                .ToList()
                .ForEach(i => Assert.AreEqual(null, _Storage.GetCurriculum(ids[i])));

            for (int i = 0; i < chapterTimeline.Count; ++i)
            {
                for (int j = 0; j < _Storage.GetChapterTimelinesByCurriculumId(ids[i]).ToList().Count; ++j)
                {
                    Assert.AreEqual(null, _Storage.GetChapterTimelinesByCurriculumId(ids[i]).ToList()[j]);
                }
            }
            try
            {
                _Storage.DeleteCurriculums(null);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual(true, true);
            }
        }

        [Test]
        public void MakeCurriculumsInvalid()
        {
            IUserService userService = _Tests.LmsService.FindService<IUserService>();
            Group gr = userService.GetGroup(2);
            int groupId = gr.Id;

            var disciplines = CreateDefaultData();
            disciplines.ForEach(item => _Storage.AddDiscipline(item));

            var curriculums =
                disciplines.Select(item => new Curriculum { Discipline = item, UserGroupRef = gr.Id }).ToList();

            var timelines = curriculums.Select(item => new Timeline
                                                           {
                                                               Curriculum = item,
                                                               StartDate = new DateTime(2011, 5, 1),
                                                               EndDate = new DateTime(2011, 5, 31)
                                                           }).ToList();
            var idsT = timelines.Select(item => _Storage.AddTimeline(item)).ToList();

            var chapters = disciplines.Select(item => new Chapter { Discipline = item, Name = "Chapter" }).ToList();
            var idsSt = chapters.Select(item => _Storage.AddChapter(item)).ToList();

            List<Timeline> chapterTimeline = new List<Timeline>();
            for (int i = 0; i < disciplines.Count; ++i)
            {
                chapterTimeline.Add(new Timeline
                                        {
                                            Curriculum = curriculums[i],
                                            ChapterRef = idsSt[i],
                                            StartDate = new DateTime(2011, 7, 1 + i * 5),
                                            EndDate = new DateTime(2011, 7, 4 + i * 5)
                                        });
            }
            var idsStT = chapterTimeline.Select(item => _Storage.AddTimeline(item)).ToList();

            var topic =
                chapters.Select(item => new Topic { Name = "Topic", Chapter = item, TopicType = _Storage.GetTopicType(2) })
                    .ToList();
            topic.ForEach(item => _Storage.AddTopic(item));

            List<TopicAssignment> topicAssignments = new List<TopicAssignment>();
            for (int i = 0; i < disciplines.Count; ++i)
            {
                topicAssignments.Add(new TopicAssignment
                                         {
                                             Curriculum = curriculums[i],
                                             Topic = topic[i],
                                             MaxScore = i * 5
                                         });
            }
            var idsThA = topicAssignments.Select(item => _Storage.AddTopicAssignment(item)).ToList();


            var ids = curriculums.Select(item => _Storage.AddCurriculum(item)).ToList();

            curriculums.Select((item, i) => i)
                .ToList()
                .ForEach(i => AdvAssert.AreEqual(curriculums[i], _Storage.GetCurriculum(ids[i])));


            _Storage.MakeCurriculumsInvalid(groupId);


            curriculums.Select((item, i) => i)
                .ToList()
                .ForEach(i => Assert.AreEqual(false, _Storage.GetCurriculum(ids[i]).IsValid));

            try
            {
                _Storage.MakeCurriculumsInvalid(0);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual(true, true);
            }
        }

        [Test]
        public void GetCurriculum()
        {
            IUserService userService = _Tests.LmsService.FindService<IUserService>();
            Group gr = userService.GetGroup(2);

            Discipline cur = new Discipline { Name = "Discipline" };
            _Storage.AddDiscipline(cur);

            Curriculum curAss = new Curriculum { Discipline = cur, UserGroupRef = gr.Id };
            int curAssId = _Storage.AddCurriculum(curAss);

            AdvAssert.AreEqual(curAss, _Storage.GetCurriculum(curAssId));

            try
            {
                _Storage.GetCurriculum(0);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual(true, true);
            }
        }

        [Test]
        public void GetCurriculums()
        {
            IUserService userService = _Tests.LmsService.FindService<IUserService>();
            Group gr = userService.GetGroup(2);

            Discipline cur = new Discipline { Name = "Discipline" };
            _Storage.AddDiscipline(cur);

            List<Curriculum> curAss = new List<Curriculum>();
            curAss.Add(new Curriculum { Discipline = cur, UserGroupRef = gr.Id });

            var curAssId = curAss.Select(item => _Storage.AddCurriculum(item)).ToList();

            Assert.AreEqual(curAss, _Storage.GetCurriculums(curAssId));

            try
            {
                _Storage.GetCurriculums(null);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual(true, true);
            }
        }

        [Test]
        public void GetDisciplineAssignmnetsByDisciplineId()
        {
            IUserService userService = _Tests.LmsService.FindService<IUserService>();
            Group gr = userService.GetGroup(2);

            Discipline cur = new Discipline { Name = "Discipline" };
            var curId = _Storage.AddDiscipline(cur);

            List<Curriculum> curAss = new List<Curriculum>();
            curAss.Add(new Curriculum { Discipline = cur, UserGroupRef = gr.Id });
            curAss.ForEach(item => _Storage.AddCurriculum(item));

            Assert.AreEqual(curAss, _Storage.GetDisciplineAssignmnetsByDisciplineId(curId).ToList());

            try
            {
                _Storage.GetDisciplineAssignmnetsByDisciplineId(curId + 1);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual(true, true);
            }
        }

        [Test]
        public void GetCurriculumsByGroupId()
        {
            IUserService userService = _Tests.LmsService.FindService<IUserService>();
            Group gr = userService.GetGroup(2);
            int groupId = gr.Id;

            Discipline cur = new Discipline { Name = "Discipline" };
            _Storage.AddDiscipline(cur);

            List<Curriculum> curAss = new List<Curriculum>();
            curAss.Add(new Curriculum { Discipline = cur, UserGroupRef = gr.Id });
            curAss.ForEach(item => _Storage.AddCurriculum(item));

            Assert.AreEqual(curAss, _Storage.GetCurriculumsByGroupId(groupId).ToList());

            try
            {
                _Storage.GetDisciplineAssignmnetsByDisciplineId(groupId + 1);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual(true, true);
            }
        }

        [Test]
        public void GetDisciplineASsignments()
        {
            IUserService userService = _Tests.LmsService.FindService<IUserService>();
            Group gr = userService.GetGroup(2);
            Group group = userService.GetGroup(1);

            Discipline cur = new Discipline { Name = "Discipline" };
            _Storage.AddDiscipline(cur);

            List<Curriculum> curAss = new List<Curriculum>();
            curAss.Add(new Curriculum { Discipline = cur, UserGroupRef = gr.Id });
            curAss.Add(new Curriculum { Discipline = cur, UserGroupRef = group.Id });
            curAss.Add(new Curriculum { Discipline = cur, UserGroupRef = gr.Id });
            curAss.ForEach(item => _Storage.AddCurriculum(item));

            Assert.AreEqual(curAss, _Storage.GetCurriculums().ToList());
        }*/

        #endregion

        /*#region TimelineMethods

        [Test]
        public void AddTimeline()
        {
            var disciplines = CreateDefaultData();
            var curriculum = disciplines.Select(item => new Curriculum {Discipline = item}).ToList();
            var timelines = curriculum.Select(item => new Timeline {Curriculum = item}).ToList();

            var ids = timelines.Select(item => _Storage.AddTimeline(item)).ToList();

            timelines.Select((item, i) => i)
                .ToList()
                .ForEach(i => AdvAssert.AreEqual(timelines[i], _Storage.GetTimeline(ids[i])));

            try
            {
                _Storage.AddTimeline(null);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }
            try
            {
                _Storage.AddTimeline(new Timeline {});
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }
        }

        [Test]
        public void GetTimeline()
        {
            var disciplines = CreateDefaultData();
            var curriculum = disciplines.Select((item, i) => new Curriculum {Discipline = item}).ToList();
            var timelines = curriculum.Select(item => new Timeline {Curriculum = item}).ToList();

            timelines.ForEach(item => _Storage.AddTimeline(item));
            timelines.Select((item, i) => i)
                .ToList()
                .ForEach(i => AdvAssert.AreEqual(timelines[i], _Storage.GetTimeline(timelines[i].Id)));

            try
            {
                _Storage.GetTimeline(0);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }
        }

        [Test]
        public void GetCurriculumTimelines()
        {
            var disciplines = CreateDefaultData();
            var curriculum = disciplines.Select((item, i) => new Curriculum {Discipline = item, Id = 1}).ToList();
            var timelines = curriculum.Select(item => new Timeline {Curriculum = item}).ToList();

            timelines.ForEach(item => _Storage.AddTimeline(item));
            timelines.Select((item, i) => i)
                .ToList()
                .ForEach(
                    i =>
                    AdvAssert.AreEqual(timelines[i],
                                       _Storage.GetCurriculumTimelines(timelines[i].Curriculum.Id).ToList()[i]));

            try
            {
                _Storage.GetCurriculumTimelines(0);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }
        }

        [Test]
        public void GetChapterTimelinesByCurriculumId()
        {
            var disciplines = CreateDefaultData();
            var curriculum = disciplines.Select((item, i) => new Curriculum {Discipline = item, Id = 1}).ToList();
            var timelines = curriculum.Select(item => new Timeline {Curriculum = item, ChapterRef = 1}).ToList();

            timelines.ForEach(item => _Storage.AddTimeline(item));

            timelines.Select((item, i) => i)
                .ToList()
                .ForEach(
                    i =>
                    AdvAssert.AreEqual(timelines[i],
                                       _Storage.GetChapterTimelinesByCurriculumId(timelines[i].Curriculum.Id).ToList()[i
                                           ]));

            try
            {
                _Storage.GetChapterTimelinesByCurriculumId(0);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }
        }

        [Test]
        public void GetChapterTimelinesByChapterId()
        {
            var disciplines = CreateDefaultData();
            var curriculum = disciplines.Select((item, i) => new Curriculum {Discipline = item}).ToList();
            var timelines = curriculum.Select(item => new Timeline {Curriculum = item, ChapterRef = 1}).ToList();

            timelines.ForEach(item => _Storage.AddTimeline(item));

            timelines.Select((item, i) => i)
                .ToList()
                .ForEach(
                    i =>
                    AdvAssert.AreEqual(timelines[i],
                                       _Storage.GetChapterTimelinesByChapterId(timelines[i].ChapterRef.Value).ToList()[i
                                           ]));

            try
            {
                _Storage.GetChapterTimelinesByChapterId(0);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }
        }

        [Test]
        public void GetChapterTimelines()
        {
            var disciplines = CreateDefaultData();
            var curriculum = disciplines.Select((item, i) => new Curriculum {Discipline = item, Id = 1}).ToList();
            var timelines = curriculum.Select(item => new Timeline {Curriculum = item, ChapterRef = 1}).ToList();

            timelines.ForEach(item => _Storage.AddTimeline(item));

            timelines.Select((item, i) => i)
                .ToList()
                .ForEach(
                    i =>
                    AdvAssert.AreEqual(timelines[i],
                                       _Storage.GetChapterTimelines(timelines[i].ChapterRef.Value,
                                                                    timelines[i].Curriculum.Id).ToList()[i]));

            try
            {
                _Storage.GetChapterTimelines(0, 0);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }
            try
            {
                _Storage.GetChapterTimelines(0, timelines[0].Curriculum.Id);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }
            try
            {
                _Storage.GetChapterTimelines(timelines[0].ChapterRef.Value, 0);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }
        }

        [Test]
        public void GetTimelines()
        {
            var disciplines = CreateDefaultData();
            var curriculum = disciplines.Select((item, i) => new Curriculum {Discipline = item}).ToList();
            var timelines = curriculum.Select(item => new Timeline {Curriculum = item, Id = 1}).ToList();

            var ids = timelines.Select(item => _Storage.AddTimeline(item)).ToList();
            Assert.AreEqual(timelines.Count, ids.Count);
            timelines.Select((item, i) => i)
                .ToList()
                .ForEach(i => AdvAssert.AreEqual(timelines[i], _Storage.GetTimelines(ids).ToList()[i]));

            try
            {
                _Storage.GetTimelines(null);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }
        }

        [Test]
        public void UpdateTimeline()
        {
            var disciplines = CreateDefaultData();
            var curriculum = disciplines.Select((item, i) => new Curriculum {Discipline = item}).ToList();
            var timelines = curriculum.Select(item => new Timeline {Curriculum = item, Id = 1}).ToList();

            var ids = timelines.Select(item => _Storage.AddTimeline(item)).ToList();

            timelines.Select((item, i) => i)
                .ToList()
                .ForEach(i => AdvAssert.AreEqual(timelines[i], _Storage.GetTimeline(ids[i])));

            timelines.ForEach(item => item.EndDate = DateTime.Now);

            timelines.ForEach(item => _Storage.UpdateTimeline(item));

            timelines.Select((item, i) => i)
                .ToList()
                .ForEach(i => AdvAssert.AreEqual(timelines[i], _Storage.GetTimeline(ids[i])));

            try
            {
                _Storage.UpdateTimeline(null);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }
            try
            {
                _Storage.UpdateTimeline(new Timeline());
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }
        }

        [Test]
        public void DeleteTimeline()
        {
            var disciplines = CreateDefaultData();
            var curriculum = disciplines.Select((item, i) => new Curriculum {Discipline = item}).ToList();
            List<Timeline> timelines = new List<Timeline>();
            for (int i = 0; i < 4; ++i)
            {
                timelines.Add(new Timeline {Curriculum = curriculum[i], Id = i});
            }
            var ids = timelines.Select(item => _Storage.AddTimeline(item)).ToList();

            timelines.ForEach(item => _Storage.DeleteTimeline(item.Id));

            timelines.Select((item, i) => i)
                .ToList()
                .ForEach(i => Assert.AreEqual(null, _Storage.GetTimeline(ids[i])));

            try
            {
                _Storage.DeleteTimeline(5);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }
        }

        [Test]
        public void DeleteTimelines()
        {
            var disciplines = CreateDefaultData();
            var curriculum = disciplines.Select((item, i) => new Curriculum {Discipline = item}).ToList();
            List<Timeline> timelines = new List<Timeline>();
            for (int i = 0; i < 4; ++i)
            {
                timelines.Add(new Timeline {Curriculum = curriculum[i], Id = i});
            }
            var ids = timelines.Select(item => _Storage.AddTimeline(item)).ToList();

            _Storage.DeleteTimelines(ids);

            timelines.Select((item, i) => i)
                .ToList()
                .ForEach(i => Assert.AreEqual(null, _Storage.GetTimeline(ids[i])));

            try
            {
                _Storage.DeleteTimelines(null);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }
        }

        #endregion

        #region TopicAssignmentMethods

        [Test]
        public void AddTopicAssignment()
        {
            var disciplines = CreateDefaultData();
            var chapters =
                disciplines.Select(item => new Chapter {Discipline = item, Id = 1, Name = "chapter"}).ToList();
            var topic = chapters.Select(item => new Topic {Chapter = item, Name = "topic"}).ToList();
            var topicassignment = topic.Select(item => new TopicAssignment {Topic = item}).ToList();
            for (int i = 0; i < topicassignment.Count; ++i)
            {
                topicassignment[i].Id = i;
            }

            var ids = topicassignment.Select(item => _Storage.AddTopicAssignment(item)).ToList();

            topicassignment.Select((item, i) => i)
                .ToList()
                .ForEach(i => AdvAssert.AreEqual(topicassignment[i], _Storage.GetTopicAssignment(ids[i])));

            try
            {
                _Storage.AddTopicAssignment(new TopicAssignment());
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }
            try
            {
                _Storage.AddTopicAssignment(null);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }
        }

        [Test]
        public void GetTopicAssignment()
        {
            var disciplines = CreateDefaultData();
            var chapters =
                disciplines.Select(item => new Chapter {Discipline = item, Id = 1, Name = "chapter"}).ToList();
            var topic = chapters.Select(item => new Topic {Chapter = item, Name = "topic"}).ToList();
            var topicassignment = topic.Select(item => new TopicAssignment {Topic = item}).ToList();
            for (int i = 0; i < topicassignment.Count; ++i)
            {
                topicassignment[i].Id = i;
            }

            topicassignment.ForEach(item => _Storage.AddTopicAssignment(item));

            topicassignment.Select((item, i) => i)
                .ToList()
                .ForEach(i => AdvAssert.AreEqual(topicassignment[i], _Storage.GetTopicAssignment(topicassignment[i].Id)));

            try
            {
                _Storage.GetTopicAssignment(5);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }
        }

        [Test]
        public void GetTopicAssignmentsByCurriculumId()
        {
            var disciplines = CreateDefaultData();
            var disciplinesassignment = disciplines.Select(item => new Curriculum {Discipline = item, Id = 1}).ToList();
            var chapters =
                disciplines.Select(item => new Chapter {Discipline = item, Id = 1, Name = "chapter"}).ToList();
            var topic = chapters.Select(item => new Topic {Chapter = item, Name = "topic"}).ToList();
            var topicassignment = topic.Select(item => new TopicAssignment {Topic = item}).ToList();
            for (int i = 0; i < topicassignment.Count; ++i)
            {
                topicassignment[i].Curriculum = disciplinesassignment[i];
            }
            topicassignment.ForEach(item => _Storage.AddTopicAssignment(item));

            topicassignment.Select((item, i) => i)
                .ToList()
                .ForEach(
                    i =>
                    AdvAssert.AreEqual(topicassignment[i],
                                       _Storage.GetTopicAssignmentsByCurriculumId(topicassignment[i].Curriculum.Id).
                                           ToList()[i]));

            try
            {
                _Storage.GetTopicAssignmentsByCurriculumId(0);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }
        }

        [Test]
        public void GetTopicAssignmentsByTopicId()
        {
            var disciplines = CreateDefaultData();
            var chapters = disciplines.Select(item => new Chapter {Discipline = item, Name = "chapter"}).ToList();
            var topic = chapters.Select(item => new Topic {Chapter = item, Name = "topic", Id = 1}).ToList();

            var topicassignment = topic.Select(item => new TopicAssignment {Topic = item}).ToList();

            topicassignment.ForEach(item => _Storage.AddTopicAssignment(item));

            topicassignment.Select((item, i) => i)
                .ToList()
                .ForEach(
                    i =>
                    AdvAssert.AreEqual(topicassignment[i],
                                       _Storage.GetTopicAssignmentsByTopicId(topicassignment[i].Topic.Id).ToList()[i]));

            try
            {
                _Storage.GetTopicAssignmentsByTopicId(0);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }
        }

        [Test]
        public void GetTopicAssignments()
        {
            var disciplines = CreateDefaultData();
            var chapters = disciplines.Select(item => new Chapter {Discipline = item, Name = "chapter"}).ToList();
            var topic = chapters.Select(item => new Topic {Chapter = item, Name = "topic", Id = 1}).ToList();

            var topicassignment = topic.Select(item => new TopicAssignment {Topic = item}).ToList();

            var ids = topicassignment.Select(item => _Storage.AddTopicAssignment(item)).ToList();

            topicassignment.Select((item, i) => i)
                .ToList()
                .ForEach(i => AdvAssert.AreEqual(topicassignment[i], _Storage.GetTopicAssignments(ids).ToList()[i]));

            try
            {
                _Storage.GetTopicAssignments(null);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }
        }

        [Test]
        public void UpdateTopicAssignment()
        {
            var disciplines = CreateDefaultData();
            var chapters = disciplines.Select(item => new Chapter {Discipline = item, Name = "chapter"}).ToList();
            var topic = chapters.Select(item => new Topic {Chapter = item, Name = "topic", Id = 1}).ToList();

            var topicassignment = topic.Select(item => new TopicAssignment {Topic = item}).ToList();
            for (int i = 0; i < topicassignment.Count; ++i)
            {
                topicassignment[i].MaxScore = 20*i;
                topicassignment[i].Id = i;
            }
            topicassignment.ForEach(item => _Storage.AddTopicAssignment(item));

            topicassignment.Select((item, i) => i)
                .ToList()
                .ForEach(i => AdvAssert.AreEqual(topicassignment[i], _Storage.GetTopicAssignment(topicassignment[i].Id)));


            topicassignment.ForEach(item => item.MaxScore = 0);
            topicassignment.ForEach(item => _Storage.UpdateTopicAssignment(item));

            topicassignment.Select((item, i) => i)
                .ToList()
                .ForEach(i => AdvAssert.AreEqual(topicassignment[i], _Storage.GetTopicAssignment(topicassignment[i].Id)));

            try
            {
                _Storage.UpdateTopicAssignment(null);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }
            try
            {
                _Storage.UpdateTopicAssignment(new TopicAssignment());
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.AreEqual(true, true);
            }
        }

        [Test]
        public void DeleteTopicAssignments()
        {
            var disciplines = CreateDefaultData();
            var chapters = disciplines.Select(item => new Chapter {Discipline = item, Name = "chapter"}).ToList();
            var topic = chapters.Select(item => new Topic {Chapter = item, Name = "topic", Id = 1}).ToList();

            var topicassignment = topic.Select(item => new TopicAssignment {Topic = item}).ToList();
            for (int i = 0; i < topicassignment.Count; ++i)
            {
                topicassignment[i].Id = i;
            }
            var ids = topicassignment.Select(item => _Storage.AddTopicAssignment(item)).ToList();

            topicassignment.Select((item, i) => i)
                .ToList()
                .ForEach(i => AdvAssert.AreEqual(topicassignment[i], _Storage.GetTopicAssignment(ids[i])));

            _Storage.DeleteTopicAssignments(ids);

            topicassignment.Select((item, i) => i)
                .ToList()
                .ForEach(i => Assert.AreEqual(null, _Storage.GetTopicAssignment(ids[i])));
        }

        #endregion

        #region ReactionToDeleting

        [Test]
        public void DeletingGroup()
        {
            Group gr = _Tests.UserStorage.GetGroup(1);
            Discipline cur = new Discipline {Name = "Discipline"};
            var id = _Storage.AddDiscipline(cur);

            Curriculum ass = new Curriculum {Discipline = cur, UserGroupRef = gr.Id};
            _Storage.AddCurriculum(ass);

            _Tests.UserStorage.DeleteGroup(gr.Id);
            cur.IsValid = false;
            _Storage.UpdateDiscipline(cur);
            Discipline c = new Discipline();

            Assert.AreEqual(false, _Storage.GetDiscipline(id).IsValid);
        }

        [Test]
        public void DeletingUser()
        {
            Guid g = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
            User us = _Tests.UserStorage.GetUser(item => item.Id == g);
            Discipline cur = new Discipline {Name = "Discipline", Owner = us.Username};
            var id = _Storage.AddDiscipline(cur);

            _Tests.UserStorage.DeleteUser(item => item.Id == us.Id);
            Assert.AreEqual(false, _Storage.GetDiscipline(id).IsValid);
        }

        [Test]
        public void DeleteCourse()
        {
            Course course = _Tests.CourseStorage.GetCourse(1);
            Discipline cur = new Discipline {Name = "Discipline"};
            var currId = _Storage.AddDiscipline(cur);
            Curriculum as1 = new Curriculum {Discipline = cur, UserGroupRef = 1};
            _Storage.AddCurriculum(as1);
            Chapter st = new Chapter {Name = "Chapter", Discipline = cur};
            var chapterId = _Storage.AddChapter(st);
            Topic topic = new Topic
                              {
                                  Name = "Topic",
                                  Chapter = st,
                                  TopicType = _Storage.GetTopicType(1),
                                  CourseRef = course.Id
                              };
            _Storage.AddTopic(topic);
            _Tests.CourseStorage.DeleteCourse(course.Id);
            Assert.AreEqual(false, _Storage.GetDiscipline(currId).IsValid);
        }

        #endregion*/
    }
}

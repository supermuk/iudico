using System;

using IUDICO.Common.Controllers;
using IUDICO.Common.Models.Services;
using IUDICO.CurriculumManagement.Controllers;
using IUDICO.CurriculumManagement.Models.Storage;
using IUDICO.DisciplineManagement.Controllers;
using IUDICO.DisciplineManagement.Models.Storage;

using NUnit.Framework;

namespace IUDICO.UnitTests.CurriculumManagement.NUnit
{
    public class BaseTestFixture
    {
        #region Protected members

        protected DisciplineCurriculumTestEngine tests = DisciplineCurriculumTestEngine.GetInstance();

        protected IDisciplineStorage Storage
        {
            get
            {
                return this.tests.DisciplineStorage;
            }
        }

        protected ICurriculumStorage CurriculumStorage
        {
            get
            {
                return this.tests.CurriculumStorage;
            }
        }

        protected ILmsService LmsService
        {
            get
            {
                return this.tests.LmsService;
            }
        }

        protected ICourseService CourseService
        {
            get
            {
                return this.tests.CourseService;
            }
        }

        protected IUserService UserService
        {
            get
            {
                return this.tests.UserService;
            }
        }

        protected DataPreparer DataPreparer
        {
            get
            {
                return this.tests.DataPreparer;
            }
        }

        protected T GetController<T>() where T : PluginController
        {
            PluginController controller;
            if (typeof(T) == typeof(DisciplineController))
            {
                controller = new DisciplineController(this.Storage);
            }
            else if (typeof(T) == typeof(ChapterController))
            {
                controller = new ChapterController(this.Storage);
            }
            else if (typeof(T) == typeof(TopicController))
            {
                controller = new TopicController(this.Storage);
            }
            else if (typeof(T) == typeof(CurriculumController))
            {
                controller = new CurriculumController(this.CurriculumStorage);
            }
            else if (typeof(T) == typeof(CurriculumChapterController))
            {
                controller = new CurriculumChapterController(this.CurriculumStorage);
            }
            else if (typeof(T) == typeof(CurriculumChapterTopicController))
            {
                controller = new CurriculumChapterTopicController(this.CurriculumStorage);
            }
            else
            {
                throw new NotImplementedException();
            }

            var mocks = new ContextMocks(controller);
            mocks.RouteData.Values["action"] = "Index";
            return (T)controller;
        }

        #endregion

        [SetUp]
        public void InitializeTest()
        {
            this.tests.ClearTables();
        }
    }
}
using System;
using IUDICO.Common.Controllers;
using IUDICO.Common.Models.Services;
using IUDICO.CurriculumManagement.Models.Storage;
using IUDICO.DisciplineManagement.Controllers;
using IUDICO.DisciplineManagement.Models.Storage;
using NUnit.Framework;
using IUDICO.CurriculumManagement.Controllers;

namespace IUDICO.UnitTests.CurriculumManagement.NUnit
{
    public class BaseTestFixture
    {
        #region Protected members

        protected DisciplineCurriculumTestEngine _Tests = DisciplineCurriculumTestEngine.GetInstance();

        protected IDisciplineStorage _Storage
        {
            get { return _Tests.DisciplineStorage; }
        }

        protected ICurriculumStorage _CurriculumStorage
        {
            get { return _Tests.CurriculumStorage; }
        }

        protected ILmsService _LmsService
        {
            get { return _Tests.LmsService; }
        }

        protected ICourseService _CourseService
        {
            get { return _Tests.CourseService; }
        }

        protected IUserService _UserService
        {
            get { return _Tests.UserService; }
        }

        protected DataPreparer _DataPreparer
        {
            get { return _Tests.DataPreparer; }
        }

        protected T GetController<T>() where T : PluginController
        {
            PluginController controller;
            if (typeof(T) == typeof(DisciplineController))
            {
                controller = new DisciplineController(_Storage);
            }
            else if (typeof(T) == typeof(ChapterController))
            {
                controller = new ChapterController(_Storage);
            }
            else if (typeof(T) == typeof(TopicController))
            {
                controller = new TopicController(_Storage);
            }
            else if (typeof(T) == typeof(CurriculumController))
            {
                controller = new CurriculumController(_CurriculumStorage);
            }
            else if (typeof(T) == typeof(CurriculumChapterController))
            {
                controller = new CurriculumChapterController(_CurriculumStorage);
            }
            else if (typeof(T) == typeof(CurriculumChapterTopicController))
            {
                controller = new CurriculumChapterTopicController(_CurriculumStorage);
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
            _Tests.ClearTables();
        }
    }
}

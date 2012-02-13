using System;
using System.Collections.Generic;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared.Statistics;
using IUDICO.Common.Models;
using IUDICO.Common.Controllers;
using System.Web;
using Microsoft.LearningComponents.Storage;
using Microsoft.LearningComponents;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using System.Data;
using IUDICO.Common.Models.Shared;

namespace IUDICO.TestingSystem.Models
{
    public class TestingService : ITestingService
    {
        protected readonly IMlcProxy MlcProxy;

        public TestingService(IMlcProxy proxy)
        {
            this.MlcProxy = proxy;
        }

        #region ITestingService interface implementation

        public IEnumerable<AttemptResult> GetResults(User user, Topic topic)
        {
            IEnumerable<AttemptResult> result = MlcProxy.GetResults(user, topic);
            return result;
        }

        public IEnumerable<AttemptResult> GetResults(User user)
        {
            IEnumerable<AttemptResult> result = MlcProxy.GetResults(user);
            return result;
        }

        public IEnumerable<AttemptResult> GetResults(Topic topic)
        {
            IEnumerable<AttemptResult> result = MlcProxy.GetResults(topic);
            return result;
        }

        public IEnumerable<AttemptResult> GetAllAttempts()
        {
            IEnumerable<AttemptResult> result = MlcProxy.GetAllAttempts();
            return result;
        }

        public IEnumerable<AnswerResult> GetAnswers(AttemptResult attempt)
        {
            IEnumerable<AnswerResult> result = MlcProxy.GetAnswers(attempt);
            return result;
        }

        public ActionLink BuildLink(Topic topic)
        {
            RouteValueDictionary routeValueDictionary = new RouteValueDictionary();
            routeValueDictionary.Add("id", topic.Id);
            ActionLink actionLink = new ActionLink("Play", "Training", routeValueDictionary);
            return actionLink;
        }

        #endregion
    }
}
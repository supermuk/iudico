using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Shared;
using IUDICO.Common.Models.Shared.DisciplineManagement;
using IUDICO.Common.Models.Shared.Statistics;
using IUDICO.TestingSystem.Models.VOs;
using LearningComponentsHelper;
using Microsoft.LearningComponents;
using Microsoft.LearningComponents.Storage;

namespace IUDICO.TestingSystem.Models
{
    /// <summary>
    /// Singleton class representing proxy between Microsoft Learning Components
    /// Learning&Package Store on the one side and IUDICO Testing Service on the other.
    /// Mostly used by Controller of TestingService plugin and by according service.
    /// </summary>
    public class MlcProxy : MlcHelper, IMlcProxy
    {
        #region Constructor
        
        public MlcProxy(ILmsService lmsService)
            : base(lmsService)
        {
        }
        
        #endregion

        #region IMlcProxy implementation Methods

        /// <summary>
        /// Checks if related to topic package has been already uploaded.
        /// In case it was not uploaded - upload package.
        /// Check attempt has been created and get attempt id.
        /// </summary>
        /// <param name="curriculumChapterTopicId">Iudico CurriculumChapterTopic.Id</param>
        /// <param name="courseId">Iudico Course.Id</param>
        /// <param name="topicType"><see cref="TopicTypeEnum"/> enumeration value.</param>
        /// <returns>Long integer value representing attempt id.</returns>
        public long GetAttemptId(int curriculumChapterTopicId, int courseId, TopicTypeEnum topicType)
        {
            GetCurrentUserIdentifier();
            AttemptItemIdentifier attemptId = null;
            ActivityPackageItemIdentifier organizationId;
            var packageId = GetPackageIdentifier(courseId);

            // in case package has not been uploaded yet.
            if (packageId == null)
            {
                string zipPath = CourseService.Export(courseId);
                Package package = new ZipPackage(zipPath);
                package.CourseID = courseId;
                packageId = AddPackage(package);
                organizationId = GetOrganizationIdentifier(packageId);
                attemptId = CreateAttempt(organizationId.GetKey(), curriculumChapterTopicId, topicType);
            }
            // otherwise check if attempt was created
            else
            {
                organizationId = GetOrganizationIdentifier(packageId);

                AttemptItemIdentifier attId = GetAttemptIdentifier(organizationId, curriculumChapterTopicId, topicType);
                attemptId = attId ?? CreateAttempt(organizationId.GetKey(), curriculumChapterTopicId, topicType);
            }

            return attemptId.GetKey();
        }

        public IEnumerable<AttemptResult> GetResults()
        {
            LearningStoreJob job = LStore.CreateJob();
            RequestAllAttempts(job);
            var dataTable = job.Execute<DataTable>();
            foreach (DataRow dataRow in dataTable.AsEnumerable())
            {
                AttemptItemIdentifier attemptItemId;
                LStoreHelper.CastNonNull(dataRow[Schema.AllAttemptsResults.AttemptId], out attemptItemId);
                long attemptId = attemptItemId.GetKey();

                String userKey;
                LStoreHelper.CastNonNull(dataRow[Schema.AllAttemptsResults.UserItemKey], out userKey);
                User user = UserService.GetUsers().Single(curr => curr.Id.ToString() == userKey);
                if (user == null)
                {
                    throw new NoNullAllowedException("Error while getting user with id = " + userKey);
                }

                Int32 curriculumChapterTopicId;
                LStoreHelper.CastNonNull(dataRow[Schema.AllAttemptsResults.CurriculumChapterTopicId], out curriculumChapterTopicId);
                var curriculumChapterTopic = CurriculumService.GetCurriculumChapterTopicById(curriculumChapterTopicId);
                if (curriculumChapterTopic == null)
                {
                    throw new NoNullAllowedException("Error while getting curriculum-chapter-topic with id = " + curriculumChapterTopicId);
                }

                int rawTopicType;
                LStoreHelper.CastNonNull(dataRow[Schema.AllAttemptsResults.TopicType], out rawTopicType);
                var topicType = (TopicTypeEnum) rawTopicType;

                Microsoft.LearningComponents.CompletionStatus completionStatus;
                LStoreHelper.CastNonNull(dataRow[Schema.AllAttemptsResults.CompletionStatus], out completionStatus);
                var iudicoCompletionStatus = (IUDICO.Common.Models.Shared.Statistics.CompletionStatus)completionStatus;

                Microsoft.LearningComponents.AttemptStatus attemptStatus;
                LStoreHelper.CastNonNull(dataRow[Schema.AllAttemptsResults.AttemptStatus], out attemptStatus);
                var iudicoAttemptStatus = (IUDICO.Common.Models.Shared.Statistics.AttemptStatus)attemptStatus;

                Microsoft.LearningComponents.SuccessStatus successStatus;
                LStoreHelper.CastNonNull(dataRow[Schema.AllAttemptsResults.SuccessStatus], out successStatus);
                var iudicoSuccessStatus = (IUDICO.Common.Models.Shared.Statistics.SuccessStatus)successStatus;

                DateTime? startTime;
                LStoreHelper.Cast(dataRow[Schema.AllAttemptsResults.StartedTimestamp], out startTime);

                float? scaledScore;
                LStoreHelper.Cast<float>(dataRow[Schema.AllAttemptsResults.Score], out scaledScore);

                // Create AttemptResult object
                var attemptResult = new AttemptResult(attemptId, user, curriculumChapterTopic, topicType, iudicoCompletionStatus, iudicoAttemptStatus, iudicoSuccessStatus, startTime, scaledScore);
                yield return attemptResult;
            }
        }

        public IEnumerable<AttemptResult> GetResults(User user, CurriculumChapterTopic curriculumChapterTopic)
        {
            LearningStoreJob job = LStore.CreateJob();
            RequestAttemptsByTopicAndUser(job, user.Id.ToString(), curriculumChapterTopic.Id);
            var dataTable = job.Execute<DataTable>();
            
            foreach (DataRow dataRow in dataTable.AsEnumerable())
            {
                AttemptItemIdentifier attemptItemId;
                LStoreHelper.CastNonNull(dataRow[Schema.AttemptsResultsByTopicAndUser.AttemptId], out attemptItemId);
                long attemptId = attemptItemId.GetKey();

                int rawTopicType;
                LStoreHelper.CastNonNull(dataRow[Schema.AttemptsResultsByTopicAndUser.TopicType], out rawTopicType);
                var topicType = (TopicTypeEnum)rawTopicType;

                Microsoft.LearningComponents.CompletionStatus completionStatus;
                LStoreHelper.CastNonNull(dataRow[Schema.AttemptsResultsByTopicAndUser.CompletionStatus], out completionStatus);
                var iudicoCompletionStatus = (IUDICO.Common.Models.Shared.Statistics.CompletionStatus)completionStatus;

                Microsoft.LearningComponents.AttemptStatus attemptStatus;
                LStoreHelper.CastNonNull(dataRow[Schema.AttemptsResultsByTopicAndUser.AttemptStatus], out attemptStatus);
                var iudicoAttemptStatus = (IUDICO.Common.Models.Shared.Statistics.AttemptStatus)attemptStatus;

                Microsoft.LearningComponents.SuccessStatus successStatus;
                LStoreHelper.CastNonNull(dataRow[Schema.AttemptsResultsByTopicAndUser.SuccessStatus], out successStatus);
                var iudicoSuccessStatus = (IUDICO.Common.Models.Shared.Statistics.SuccessStatus)successStatus;

                DateTime? startTime;
                LStoreHelper.Cast(dataRow[Schema.AttemptsResultsByTopicAndUser.StartedTimestamp], out startTime);

                float? score;
                LStoreHelper.Cast<float>(dataRow[Schema.AttemptsResultsByTopicAndUser.Score], out score);
                float? scaledScore = null;
                
                if (score != null)
                {
                    scaledScore = score / 100;
                }

                // Create AttemptResult object
                var attemptResult = new AttemptResult(attemptId, user, curriculumChapterTopic, topicType, iudicoCompletionStatus, iudicoAttemptStatus, iudicoSuccessStatus, startTime, scaledScore);
                yield return attemptResult;
            }
        }

        public IEnumerable<AttemptResult> GetResults(User user)
        {
            LearningStoreJob job = LStore.CreateJob();
            RequestAttemptsByUser(job, user.Id.ToString());
            var dataTable = job.Execute<DataTable>();

            foreach (DataRow dataRow in dataTable.AsEnumerable())
            {
                AttemptItemIdentifier attemptItemId;
                LStoreHelper.CastNonNull(dataRow[Schema.AllAttemptsResults.AttemptId], out attemptItemId);
                long attemptId = attemptItemId.GetKey();

                Int32 curriculumChapterTopicId;
                LStoreHelper.CastNonNull(dataRow[Schema.AllAttemptsResults.CurriculumChapterTopicId], out curriculumChapterTopicId);
                CurriculumChapterTopic curriculumChapterTopic = CurriculumService.GetCurriculumChapterTopicById(curriculumChapterTopicId);
                if (curriculumChapterTopic == null)
                {
                    throw new NoNullAllowedException("Error while getting curriculum chapter topic with id = " + curriculumChapterTopicId);
                }

                int rawTopicType;
                LStoreHelper.CastNonNull(dataRow[Schema.AllAttemptsResults.TopicType], out rawTopicType);
                var topicType = (TopicTypeEnum)rawTopicType;

                Microsoft.LearningComponents.CompletionStatus completionStatus;
                LStoreHelper.CastNonNull(dataRow[Schema.AllAttemptsResults.CompletionStatus], out completionStatus);
                var iudicoCompletionStatus = (IUDICO.Common.Models.Shared.Statistics.CompletionStatus)completionStatus;

                Microsoft.LearningComponents.AttemptStatus attemptStatus;
                LStoreHelper.CastNonNull(dataRow[Schema.AllAttemptsResults.AttemptStatus], out attemptStatus);
                var iudicoAttemptStatus = (IUDICO.Common.Models.Shared.Statistics.AttemptStatus)attemptStatus;

                Microsoft.LearningComponents.SuccessStatus successStatus;
                LStoreHelper.CastNonNull(dataRow[Schema.AllAttemptsResults.SuccessStatus], out successStatus);
                var iudicoSuccessStatus = (IUDICO.Common.Models.Shared.Statistics.SuccessStatus)successStatus;

                DateTime? startTime;
                LStoreHelper.Cast(dataRow[Schema.AllAttemptsResults.StartedTimestamp], out startTime);

                float? score;
                LStoreHelper.Cast<float>(dataRow[Schema.AllAttemptsResults.Score], out score);
                float? scaledScore = null;

                if (score != null)
                {
                    scaledScore = score / 100;
                }

                // Create AttemptResult object
                var attemptResult = new AttemptResult(attemptId, user, curriculumChapterTopic, topicType, iudicoCompletionStatus, iudicoAttemptStatus, iudicoSuccessStatus, startTime, scaledScore);
                yield return attemptResult;
            }
        }

        public IEnumerable<AttemptResult> GetResults(Topic topic)
        {
            LearningStoreJob job = LStore.CreateJob();
            foreach (var curriculumChapterTopic in topic.CurriculumChapterTopics)
            {
                RequestAttemptsByCurriculumChapterTopic(job, curriculumChapterTopic.Id);
            }
            var dataTable = job.Execute<DataTable>();

            foreach (DataRow dataRow in dataTable.AsEnumerable())
            {
                AttemptItemIdentifier attemptItemId;
                LStoreHelper.CastNonNull(dataRow[Schema.AllAttemptsResults.AttemptId], out attemptItemId);
                long attemptId = attemptItemId.GetKey();

                String userKey;
                LStoreHelper.CastNonNull(dataRow[Schema.AllAttemptsResults.UserItemKey], out userKey);
                User user = UserService.GetUsers().Single(curr => curr.Id.ToString() == userKey);
                if (user == null)
                {
                    throw new NoNullAllowedException("Error while getting user with id = " + userKey);
                }

                Int32 curriculumChapterTopicId;
                LStoreHelper.CastNonNull(dataRow[Schema.AllAttemptsResults.CurriculumChapterTopicId], out curriculumChapterTopicId);
                CurriculumChapterTopic curriculumChapterTopic = CurriculumService.GetCurriculumChapterTopicById(curriculumChapterTopicId);
                if (curriculumChapterTopic == null)
                {
                    throw new NoNullAllowedException("Error while getting curriculum chapter topic with id = " + curriculumChapterTopicId);
                }

                int rawTopicType;
                LStoreHelper.CastNonNull(dataRow[Schema.AllAttemptsResults.TopicType], out rawTopicType);
                var topicType = (TopicTypeEnum)rawTopicType;

                Microsoft.LearningComponents.CompletionStatus completionStatus;
                LStoreHelper.CastNonNull(dataRow[Schema.AllAttemptsResults.CompletionStatus], out completionStatus);
                var iudicoCompletionStatus = (IUDICO.Common.Models.Shared.Statistics.CompletionStatus)completionStatus;

                Microsoft.LearningComponents.AttemptStatus attemptStatus;
                LStoreHelper.CastNonNull(dataRow[Schema.AllAttemptsResults.AttemptStatus], out attemptStatus);
                var iudicoAttemptStatus = (IUDICO.Common.Models.Shared.Statistics.AttemptStatus)attemptStatus;

                Microsoft.LearningComponents.SuccessStatus successStatus;
                LStoreHelper.CastNonNull(dataRow[Schema.AllAttemptsResults.SuccessStatus], out successStatus);
                var iudicoSuccessStatus = (IUDICO.Common.Models.Shared.Statistics.SuccessStatus)successStatus;

                DateTime? startTime;
                LStoreHelper.Cast(dataRow[Schema.AllAttemptsResults.StartedTimestamp], out startTime);

                float? score;
                LStoreHelper.Cast<float>(dataRow[Schema.AllAttemptsResults.Score], out score);
                float? scaledScore = null;

                if (score != null)
                {
                    scaledScore = score / 100;
                }

                // Create AttemptResult object
                var attemptResult = new AttemptResult(attemptId, user, curriculumChapterTopic, topicType, iudicoCompletionStatus, iudicoAttemptStatus, iudicoSuccessStatus, startTime, scaledScore);
                yield return attemptResult;
            }
        }

        public IEnumerable<AttemptResult> GetResults(CurriculumChapterTopic curriculumChapterTopic)
        {
            LearningStoreJob job = LStore.CreateJob();
            RequestAttemptsByCurriculumChapterTopic(job, curriculumChapterTopic.Id);
            var dataTable = job.Execute<DataTable>();

            foreach (DataRow dataRow in dataTable.AsEnumerable())
            {
                AttemptItemIdentifier attemptItemId;
                LStoreHelper.CastNonNull(dataRow[Schema.AllAttemptsResults.AttemptId], out attemptItemId);
                long attemptId = attemptItemId.GetKey();

                String userKey;
                LStoreHelper.CastNonNull(dataRow[Schema.AllAttemptsResults.UserItemKey], out userKey);
                User user = UserService.GetUsers().Single(curr => curr.Id.ToString() == userKey);
                if (user == null)
                {
                    throw new NoNullAllowedException("Error while getting user with id = " + userKey);
                }

                int rawTopicType;
                LStoreHelper.CastNonNull(dataRow[Schema.AllAttemptsResults.TopicType], out rawTopicType);
                var topicType = (TopicTypeEnum)rawTopicType;

                Microsoft.LearningComponents.CompletionStatus completionStatus;
                LStoreHelper.CastNonNull(dataRow[Schema.AllAttemptsResults.CompletionStatus], out completionStatus);
                var iudicoCompletionStatus = (IUDICO.Common.Models.Shared.Statistics.CompletionStatus)completionStatus;

                Microsoft.LearningComponents.AttemptStatus attemptStatus;
                LStoreHelper.CastNonNull(dataRow[Schema.AllAttemptsResults.AttemptStatus], out attemptStatus);
                var iudicoAttemptStatus = (IUDICO.Common.Models.Shared.Statistics.AttemptStatus)attemptStatus;

                Microsoft.LearningComponents.SuccessStatus successStatus;
                LStoreHelper.CastNonNull(dataRow[Schema.AllAttemptsResults.SuccessStatus], out successStatus);
                var iudicoSuccessStatus = (IUDICO.Common.Models.Shared.Statistics.SuccessStatus)successStatus;

                DateTime? startTime;
                LStoreHelper.Cast(dataRow[Schema.AllAttemptsResults.StartedTimestamp], out startTime);

                float? score;
                LStoreHelper.Cast<float>(dataRow[Schema.AllAttemptsResults.Score], out score);
                float? scaledScore = null;

                if (score != null)
                {
                    scaledScore = score / 100;
                }

                // Create AttemptResult object
                var attemptResult = new AttemptResult(attemptId, user, curriculumChapterTopic, topicType, iudicoCompletionStatus, iudicoAttemptStatus, iudicoSuccessStatus, startTime, scaledScore);
                yield return attemptResult;
            }
        }

        public IEnumerable<AnswerResult> GetAnswers(AttemptResult attemptResult)
        {
            LearningStoreJob job = LStore.CreateJob();
            var attemptId = new AttemptItemIdentifier(attemptResult.AttemptId);
            RequestInteractionResultsByAttempt(job, attemptId);
            var dataTable = job.Execute<DataTable>();
            foreach (DataRow dataRow in dataTable.AsEnumerable())
            {
                ActivityAttemptItemIdentifier activityAttemptItemId;
                LStoreHelper.CastNonNull(dataRow[Schema.InteractionResultsByAttempt.ActivityAttemptId], out activityAttemptItemId);
                long activityAttemptId = activityAttemptItemId.GetKey();

                ActivityPackageItemIdentifier activityPackageItemId;
                LStoreHelper.CastNonNull(dataRow[Schema.InteractionResultsByAttempt.ActivityPackageId], out activityPackageItemId);
                long activityPackageId = activityPackageItemId.GetKey();

                String activityTitle;
                LStoreHelper.CastNonNull(dataRow[Schema.InteractionResultsByAttempt.ActivityTitle], out activityTitle);

                InteractionItemIdentifier interactionItemId;
                LStoreHelper.Cast(dataRow[Schema.InteractionResultsByAttempt.InteractionId], out interactionItemId);
                long? interactionId = null;
                if (interactionItemId != null)
                {
                    interactionId = interactionItemId.GetKey();
                }

                Microsoft.LearningComponents.CompletionStatus completionStatus;
                LStoreHelper.CastNonNull(dataRow[Schema.InteractionResultsByAttempt.CompletionStatus], out completionStatus);
                var iudicoCompletionStatus = (IUDICO.Common.Models.Shared.Statistics.CompletionStatus)completionStatus;

                Microsoft.LearningComponents.SuccessStatus? successStatus;
                LStoreHelper.Cast(dataRow[Schema.InteractionResultsByAttempt.SuccessStatus], out successStatus);
                var iudicoSuccessStatus = (IUDICO.Common.Models.Shared.Statistics.SuccessStatus?)successStatus;

                bool? learnerResponseBool = null;
                LStoreHelper.Cast(dataRow[Schema.InteractionResultsByAttempt.LearnerResponseBool], out learnerResponseBool);

                string learnerResponseString = null;
                LStoreHelper.Cast(dataRow[Schema.InteractionResultsByAttempt.LearnerResponseString], out learnerResponseString);

                double? learnerResponseNumeric = null;
                LStoreHelper.Cast(dataRow[Schema.InteractionResultsByAttempt.LearnerResponseNumeric], out learnerResponseNumeric);

                object learnerResponse = null;
                if (learnerResponseBool != null)
                {
                    learnerResponse = learnerResponseBool;
                }
                if (learnerResponseString != null)
                {
                    learnerResponse = learnerResponseString;
                }
                if (learnerResponseNumeric != null)
                {
                    learnerResponse = learnerResponseNumeric;
                }

                string correctResponse;
                LStoreHelper.Cast(dataRow[Schema.InteractionResultsByAttempt.CorrectResponse], out correctResponse);

                Microsoft.LearningComponents.InteractionType? interactionType = null;
                LStoreHelper.Cast(dataRow[Schema.InteractionResultsByAttempt.InteractionType], out interactionType);
                IUDICO.Common.Models.Shared.Statistics.InteractionType? learnerResponseType = null;
                if (interactionType != null)
                {
                    learnerResponseType = (IUDICO.Common.Models.Shared.Statistics.InteractionType)interactionType;
                }
                
                float? scaledScore;
                LStoreHelper.Cast<float>(dataRow[Schema.InteractionResultsByAttempt.ScaledScore], out scaledScore);

                // Create AnswerResult object
                var answerResult = new AnswerResult(activityAttemptId, activityPackageId, activityTitle, interactionId, iudicoCompletionStatus, iudicoSuccessStatus, attemptResult, learnerResponse, correctResponse, learnerResponseType, scaledScore);
                yield return answerResult;
            }
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Requests that the list of all attempts all users have performed on all organizaions.
        /// Adds the request to a given <c>LearningStoreJob</c> for later execution.
        /// </summary>
        /// <param name="job">A <c>LearningStoreJob</c> to add the new query to.</param>
        protected void RequestAllAttempts(LearningStoreJob job)
        {
            LearningStoreQuery query = LStore.CreateQuery(Schema.AllAttemptsResults.ViewName);
            
            query.AddColumn(Schema.AllAttemptsResults.AttemptId);
            query.AddColumn(Schema.AllAttemptsResults.UserItemKey);
            query.AddColumn(Schema.AllAttemptsResults.CurriculumChapterTopicId);
            query.AddColumn(Schema.AllAttemptsResults.TopicType);
            query.AddColumn(Schema.AllAttemptsResults.CompletionStatus);
            query.AddColumn(Schema.AllAttemptsResults.AttemptStatus);
            query.AddColumn(Schema.AllAttemptsResults.SuccessStatus);
            query.AddColumn(Schema.AllAttemptsResults.StartedTimestamp);
            query.AddColumn(Schema.AllAttemptsResults.Score);
            
            job.PerformQuery(query);
        }

        /// <summary>
        /// Requests that the list of all attempts specified user performed on specified topic.
        /// Adds the request to a given <c>LearningStoreJob</c> for later execution.
        /// </summary>
        /// <param name="job">A <c>LearningStoreJob</c> to add the new query to.</param>
        /// <param name="userKey">String value represents user key.</param>
        /// <param name="curriculumChapterTopicId">Id of <see cref="CurriculumChapterTopic"/></param>
        /// <param name="topicType"><see cref="TopicTypeEnum"/> value</param>
        protected void RequestAttemptsByTopicAndUser(LearningStoreJob job, String userKey, Int32 curriculumChapterTopicId)
        {
            LearningStoreQuery query = LStore.CreateQuery(Schema.AttemptsResultsByTopicAndUser.ViewName);

            query.AddColumn(Schema.AttemptsResultsByTopicAndUser.AttemptId);
            query.AddColumn(Schema.AttemptsResultsByTopicAndUser.TopicType);
            query.AddColumn(Schema.AttemptsResultsByTopicAndUser.CompletionStatus);
            query.AddColumn(Schema.AttemptsResultsByTopicAndUser.AttemptStatus);
            query.AddColumn(Schema.AttemptsResultsByTopicAndUser.SuccessStatus);
            query.AddColumn(Schema.AttemptsResultsByTopicAndUser.StartedTimestamp);
            query.AddColumn(Schema.AttemptsResultsByTopicAndUser.Score);

            query.SetParameter(Schema.AttemptsResultsByTopicAndUser.CurriculumChapterTopicIdParam, curriculumChapterTopicId);
            query.SetParameter(Schema.AttemptsResultsByTopicAndUser.UserKeyParam, userKey);

            job.PerformQuery(query);
        }

        protected void RequestAttemptsByUser(LearningStoreJob job, String userKey)
        {
            // TODO: use more specific sql request
            LearningStoreQuery query = LStore.CreateQuery(Schema.AllAttemptsResults.ViewName);

            query.AddColumn(Schema.AllAttemptsResults.AttemptId);
            query.AddColumn(Schema.AllAttemptsResults.CurriculumChapterTopicId);
            query.AddColumn(Schema.AllAttemptsResults.TopicType);
            query.AddColumn(Schema.AllAttemptsResults.CompletionStatus);
            query.AddColumn(Schema.AllAttemptsResults.AttemptStatus);
            query.AddColumn(Schema.AllAttemptsResults.SuccessStatus);
            query.AddColumn(Schema.AllAttemptsResults.StartedTimestamp);
            query.AddColumn(Schema.AllAttemptsResults.Score);

            query.AddCondition(Schema.AllAttemptsResults.UserItemKey, LearningStoreConditionOperator.Equal, userKey);

            job.PerformQuery(query);
        }

        protected void RequestAttemptsByCurriculumChapterTopic(LearningStoreJob job, Int32 curriculumChapterTopicId)
        {
            // TODO: use more specific sql request
            LearningStoreQuery query = LStore.CreateQuery(Schema.AllAttemptsResults.ViewName);

            query.AddColumn(Schema.AllAttemptsResults.AttemptId);
            query.AddColumn(Schema.AllAttemptsResults.UserItemKey);
            query.AddColumn(Schema.AllAttemptsResults.TopicType);
            query.AddColumn(Schema.AllAttemptsResults.CompletionStatus);
            query.AddColumn(Schema.AllAttemptsResults.AttemptStatus);
            query.AddColumn(Schema.AllAttemptsResults.SuccessStatus);
            query.AddColumn(Schema.AllAttemptsResults.StartedTimestamp);
            query.AddColumn(Schema.AllAttemptsResults.Score);

            query.AddCondition(Schema.AllAttemptsResults.CurriculumChapterTopicId, LearningStoreConditionOperator.Equal, curriculumChapterTopicId);

            job.PerformQuery(query);
        }

        /// <summary>
        /// Requests that the list of all answers by specified attempt on organization.
        /// Adds the request to a given <c>LearningStoreJob</c> for later execution.
        /// </summary>
        /// <param name="job">A <c>LearningStoreJob</c> to add the new query to.</param>
        /// <param name="attemptId"><c>AttemptItemIdentifier</c> represents id of attempt.</param>
        protected void RequestInteractionResultsByAttempt(LearningStoreJob job, AttemptItemIdentifier attemptId)
        {
            LearningStoreQuery query = LStore.CreateQuery(Schema.InteractionResultsByAttempt.ViewName);

            query.AddColumn(Schema.InteractionResultsByAttempt.ActivityAttemptId);
            query.AddColumn(Schema.InteractionResultsByAttempt.ActivityPackageId);
            query.AddColumn(Schema.InteractionResultsByAttempt.ActivityTitle);
            query.AddColumn(Schema.InteractionResultsByAttempt.InteractionId);
            query.AddColumn(Schema.InteractionResultsByAttempt.CompletionStatus);
            query.AddColumn(Schema.InteractionResultsByAttempt.SuccessStatus);
            query.AddColumn(Schema.InteractionResultsByAttempt.LearnerResponseBool);
            query.AddColumn(Schema.InteractionResultsByAttempt.LearnerResponseString);
            query.AddColumn(Schema.InteractionResultsByAttempt.LearnerResponseNumeric);
            query.AddColumn(Schema.InteractionResultsByAttempt.CorrectResponse);
            query.AddColumn(Schema.InteractionResultsByAttempt.InteractionType);
            query.AddColumn(Schema.InteractionResultsByAttempt.ScaledScore);

            query.SetParameter(Schema.InteractionResultsByAttempt.AttemptIdParam, attemptId);

            job.PerformQuery(query);

        }

        /// <summary>
        /// Requests that the list of training for the current user be retrieved from the LearningStore
        /// database.  Adds the request to a given <c>LearningStoreJob</c> for later execution.
        /// </summary>
        /// <param name="job">A <c>LearningStoreJob</c> to add the new query to.</param>
        /// <param name="packageId">To request information related to a single pass the
        /// 	<c>PackageItemIdentifier</c> of the package as this parameter.  Otherwise, leave this
        /// 	parameter <c>null</c>.</param>
        protected void RequestMyTraining(LearningStoreJob job,
            PackageItemIdentifier packageId)
        {
            LearningStoreQuery query = LStore.CreateQuery(Schema.MyAttempts.ViewName);
            query.AddColumn(Schema.MyAttempts.PackageId);
            query.AddColumn(Schema.MyAttempts.OrganizationId);
            query.AddColumn(Schema.MyAttempts.AttemptId);
            query.AddColumn(Schema.MyAttempts.AttemptStatus);
            query.AddColumn(Schema.MyAttempts.TotalPoints);
            if (packageId != null)
            {
                query.AddCondition(Schema.MyAttempts.PackageId,
                    LearningStoreConditionOperator.Equal, packageId);
            }
            job.PerformQuery(query);
        }

        /// <summary>
        /// Creates attempt on given organization and returns attempt identifier.
        /// </summary>
        /// <param name="orgID">Long integer value represents organization identifier to create attempt on.</param>
        /// <returns>Long integer value, representing attempt identifier of created attempt.</returns>
        protected AttemptItemIdentifier CreateAttempt(long orgID, int curriculumChapterTopicId, TopicTypeEnum topicType)
        {
            var organizationID = new ActivityPackageItemIdentifier(orgID);
            
            StoredLearningSession session = StoredLearningSession.CreateAttempt(this.PStore, this.GetCurrentUserIdentifier(), organizationID, LoggingOptions.LogAll);
            LearningStoreJob job = LStore.CreateJob();
            Dictionary<string, object> dic = new Dictionary<string, object>();
            // TODO: Change IudicoThemeRef => IudicoCurriculumchapterTopicRef
            dic.Add(Schema.AttemptItem.IudicoCurriculumChapterTopicRef, curriculumChapterTopicId);
            dic.Add(Schema.AttemptItem.IudicoTopicType, topicType);
            job.UpdateItem(session.AttemptId, dic);
            job.Execute();

            return session.AttemptId;
        }
        
        /// <summary>
        /// Adds package to the database.
        /// </summary>
        /// <param name="package">Package value represents package object with necessary information.</param>
        protected PackageItemIdentifier AddPackage(Package package)
        {
            PackageItemIdentifier packageId = null;

            using (PackageReader packageReader = package.GetPackageReader())
            {
                AddPackageResult result = PStore.AddPackage(packageReader, new PackageEnforcement(false, false, false));
                packageId = result.PackageId;
            }

            LearningStoreJob job = LStore.CreateJob();
            var dic = new Dictionary<string, object>
                          {
                              {Schema.PackageItem.IudicoCourseRef, package.CourseID}
                          };
            job.UpdateItem(packageId, dic);
            job.Execute();

            return packageId;
        }

        /// <summary>
        /// Retrieves package id by specified IUDICO course.
        /// </summary>
        /// <param name="course">Course object represents iudico course entity.</param>
        /// <returns>PackageItemIdentifier value representing corresponding MLC Package ID.</returns>
        protected PackageItemIdentifier GetPackageIdentifier(int courseId)
        {
            PackageItemIdentifier result = null;
            LearningStoreJob job = LStore.CreateJob();

            LearningStoreQuery query = LStore.CreateQuery(Schema.PackageIdByCourse.ViewName);
            query.AddColumn(Schema.PackageIdByCourse.PackageId);
            query.SetParameter(Schema.PackageIdByCourse.IudicoCourseRef, courseId);

            job.PerformQuery(query);

            ReadOnlyCollection<object> resultList = job.Execute();

            DataTable dataTable = (DataTable)resultList[0];

            if (dataTable.Rows.Count > 0)
            {
                LStoreHelper.Cast(dataTable.Rows[0][Schema.PackageIdByCourse.PackageId], out result);
            }
            return result;
        }

        /// <summary>
        /// Retrieves Organization Id by specified package oidentifier.
        /// </summary>
        /// <param name="packageId"><c>PackageItemIdentifier</c> value representing package id, organization is being searched by.</param>
        /// <returns><c>ActivityPackageItemIdentifier</c> value, which represents organization identifier of specified package.</returns>
        protected ActivityPackageItemIdentifier GetOrganizationIdentifier(PackageItemIdentifier packageId)
        {
            ActivityPackageItemIdentifier result = null;
            LearningStoreJob job = LStore.CreateJob();

            LearningStoreQuery query = LStore.CreateQuery(Schema.RootActivityByPackage.ViewName);
            query.AddColumn(Schema.RootActivityByPackage.RootActivity);
            query.SetParameter(Schema.RootActivityByPackage.PackageId, packageId);

            job.PerformQuery(query);

            var resultList = job.Execute();

            DataTable dataTable = (DataTable)resultList[0];

            if (dataTable.Rows.Count > 0)
                LStoreHelper.Cast(dataTable.Rows[0][Schema.RootActivityByPackage.RootActivity], out result);

            return result;
        }

        /// <summary>
        /// Retrieves attempt identifier for specified organization id and Iudico topic id.
        /// </summary>
        /// <param name="orgId"><c>ActivityPackageItemIdentifier</c> value representing Organization ID.</param>
        /// <param name="topicId">Integer value - IUDICO topic id.</param>
        /// <returns><c>AttemptItemIdentifier</c> value representing Attempt Identifier.</returns>
        protected AttemptItemIdentifier GetAttemptIdentifier(ActivityPackageItemIdentifier orgId, int curriculumChapterTopicId, TopicTypeEnum topicType)
        {
            // TODO: use more specific sql request
            AttemptItemIdentifier result = null;
            LearningStoreJob job = LStore.CreateJob();

            LearningStoreQuery query = LStore.CreateQuery(Schema.MyAttempts.ViewName);
            query.AddColumn(Schema.MyAttempts.AttemptId);
            query.AddCondition(Schema.MyAttempts.OrganizationId, LearningStoreConditionOperator.Equal, orgId);
            query.AddCondition(Schema.MyAttempts.CurriculumChapterTopicId, LearningStoreConditionOperator.Equal, curriculumChapterTopicId);
            query.AddCondition(Schema.MyAttempts.TopicType, LearningStoreConditionOperator.Equal, topicType);

            job.PerformQuery(query);

            ReadOnlyCollection<object> resultList = job.Execute();

            var dataTable = (DataTable)resultList[0];

            if (dataTable.Rows.Count > 0)
            {
                // get last result
                LStoreHelper.Cast(dataTable.Rows[dataTable.Rows.Count-1][Schema.MyAttempts.AttemptId], out result);
            }
            return result;
        }

	    #endregion
    }
}
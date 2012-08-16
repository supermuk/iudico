// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MlcProxy.cs" company="">
//   
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

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
    using AttemptStatus = Microsoft.LearningComponents.AttemptStatus;
    using CompletionStatus = Microsoft.LearningComponents.CompletionStatus;
    using InteractionType = Microsoft.LearningComponents.InteractionType;
    using SuccessStatus = Microsoft.LearningComponents.SuccessStatus;

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
            this.GetCurrentUserIdentifier();
            AttemptItemIdentifier attemptId;
            ActivityPackageItemIdentifier organizationId;
            var packageId = this.GetPackageIdentifier(courseId);

            // in case package has not been uploaded yet.
            if (packageId == null)
            {
                string zipPath = this.CourseService.Export(courseId);
                Package package = new ZipPackage(zipPath);
                package.CourseID = courseId;
                packageId = this.AddPackage(package);
                organizationId = this.GetOrganizationIdentifier(packageId);
                attemptId = this.CreateAttempt(organizationId.GetKey(), curriculumChapterTopicId, topicType);
            }
            else
            {
                // otherwise check if attempt was created
                organizationId = this.GetOrganizationIdentifier(packageId);

                AttemptItemIdentifier attId = this.GetAttemptIdentifier(
                    organizationId, curriculumChapterTopicId, topicType);
                attemptId = attId ?? this.CreateAttempt(organizationId.GetKey(), curriculumChapterTopicId, topicType);
            }

            return attemptId.GetKey();
        }

        public IEnumerable<AttemptResult> GetResults()
        {
            var job = this.LStore.CreateJob();
            var conditions = new List<QueryCondition>();

            this.RequestAttemptResults(job, conditions);

            var dataTable = job.Execute<DataTable>();

            return this.ParseAttemptResults(dataTable.AsEnumerable(), conditions);
        }

        public AttemptResult GetResult(long attemptId)
        {
            var job = this.LStore.CreateJob();
            var conditions = new List<QueryCondition>
                { new QueryCondition(Schema.AllAttemptsResults.AttemptId, new AttemptItemIdentifier(attemptId)) };
            this.RequestAttemptResults(job, conditions);

            var dataTable = job.Execute<DataTable>();

            var results = this.ParseAttemptResults(dataTable.AsEnumerable(), conditions);

            return results.SingleOrDefault();
        }

        public IEnumerable<AttemptResult> GetResults(User user, CurriculumChapterTopic curriculumChapterTopic)
        {
            var job = this.LStore.CreateJob();
            var conditions = new List<QueryCondition>
                {
                    new QueryCondition(Schema.AllAttemptsResults.UserItemKey, user.Id.ToString()),
                    new QueryCondition(Schema.AllAttemptsResults.CurriculumChapterTopicId, curriculumChapterTopic.Id)
                };

            this.RequestAttemptResults(job, conditions);

            var dataTable = job.Execute<DataTable>();

            return this.ParseAttemptResults(dataTable.AsEnumerable(), conditions);
        }

        public IEnumerable<AttemptResult> GetResults(
            User user, CurriculumChapterTopic curriculumChapterTopic, TopicTypeEnum topicType)
        {
            var job = this.LStore.CreateJob();
            var conditions = new List<QueryCondition>
                {
                    new QueryCondition(Schema.AllAttemptsResults.UserItemKey, user.Id.ToString()),
                    new QueryCondition(Schema.AllAttemptsResults.CurriculumChapterTopicId, curriculumChapterTopic.Id),
                    new QueryCondition(Schema.AllAttemptsResults.TopicType, (int)topicType)
                };

            this.RequestAttemptResults(job, conditions);

            var dataTable = job.Execute<DataTable>();

            return this.ParseAttemptResults(dataTable.AsEnumerable(), conditions);
        }

        public IEnumerable<AttemptResult> GetResults(User user)
        {
            var job = this.LStore.CreateJob();
            var conditions = new List<QueryCondition>
                { new QueryCondition(Schema.AllAttemptsResults.UserItemKey, user.Id.ToString()) };

            this.RequestAttemptResults(job, conditions);

            var dataTable = job.Execute<DataTable>();

            return this.ParseAttemptResults(dataTable.AsEnumerable(), conditions);
        }

        public IEnumerable<AttemptResult> GetResults(Topic topic)
        {
            var job = this.LStore.CreateJob();

            var conditions = new List<QueryCondition>();

            // create request
            foreach (var curriculumChapterTopic in topic.CurriculumChapterTopics)
            {
                conditions.Clear();
                conditions.Add(
                    new QueryCondition(Schema.AllAttemptsResults.CurriculumChapterTopicId, curriculumChapterTopic.Id));
                this.RequestAttemptResults(job, conditions);
            }

            // perform request on DB
            var dataTables = job.Execute().AsEnumerable().Cast<DataTable>().ToList();

            var result = new List<AttemptResult>();

            var i = 0;
            foreach (var curriculumChapterTopic in topic.CurriculumChapterTopics)
            {
                var curriculumChapterTopicId = curriculumChapterTopic.Id;
                conditions.Clear();
                conditions.Add(
                    new QueryCondition(Schema.AllAttemptsResults.CurriculumChapterTopicId, curriculumChapterTopicId));
                var filteredRows = dataTables[i].AsEnumerable();
                result.AddRange(this.ParseAttemptResults(filteredRows, conditions));
                i++;
            }
            return result;
        }

        public IEnumerable<AttemptResult> GetResults(CurriculumChapterTopic curriculumChapterTopic)
        {
            var job = this.LStore.CreateJob();
            var conditions = new List<QueryCondition>
                { new QueryCondition(Schema.AllAttemptsResults.CurriculumChapterTopicId, curriculumChapterTopic.Id) };

            this.RequestAttemptResults(job, conditions);

            var dataTable = job.Execute<DataTable>();

            return this.ParseAttemptResults(dataTable.AsEnumerable(), conditions);
        }

        public IEnumerable<AnswerResult> GetAnswers(AttemptResult attemptResult)
        {
            LearningStoreJob job = this.LStore.CreateJob();
            var attemptId = new AttemptItemIdentifier(attemptResult.AttemptId);
            this.RequestInteractionResultsByAttempt(job, attemptId);
            var dataTable = job.Execute<DataTable>();
            foreach (DataRow dataRow in dataTable.AsEnumerable())
            {
                ActivityAttemptItemIdentifier activityAttemptItemId;
                LStoreHelper.CastNonNull(
                    dataRow[Schema.InteractionResultsByAttempt.ActivityAttemptId], out activityAttemptItemId);
                long activityAttemptId = activityAttemptItemId.GetKey();

                ActivityPackageItemIdentifier activityPackageItemId;
                LStoreHelper.CastNonNull(
                    dataRow[Schema.InteractionResultsByAttempt.ActivityPackageId], out activityPackageItemId);
                long activityPackageId = activityPackageItemId.GetKey();

                string activityTitle;
                LStoreHelper.CastNonNull(dataRow[Schema.InteractionResultsByAttempt.ActivityTitle], out activityTitle);

                InteractionItemIdentifier interactionItemId;
                LStoreHelper.Cast(dataRow[Schema.InteractionResultsByAttempt.InteractionId], out interactionItemId);
                long? interactionId = null;
                if (interactionItemId != null)
                {
                    interactionId = interactionItemId.GetKey();
                }

                CompletionStatus completionStatus;
                LStoreHelper.CastNonNull(
                    dataRow[Schema.InteractionResultsByAttempt.CompletionStatus], out completionStatus);
                var iudicoCompletionStatus = (Common.Models.Shared.Statistics.CompletionStatus)completionStatus;

                SuccessStatus? successStatus;
                LStoreHelper.Cast(dataRow[Schema.InteractionResultsByAttempt.SuccessStatus], out successStatus);
                var iudicoSuccessStatus = (Common.Models.Shared.Statistics.SuccessStatus?)successStatus;

                bool? learnerResponseBool;
                LStoreHelper.Cast(
                    dataRow[Schema.InteractionResultsByAttempt.LearnerResponseBool], out learnerResponseBool);

                string learnerResponseString;
                LStoreHelper.Cast(
                    dataRow[Schema.InteractionResultsByAttempt.LearnerResponseString], out learnerResponseString);

                double? learnerResponseNumeric;
                LStoreHelper.Cast(
                    dataRow[Schema.InteractionResultsByAttempt.LearnerResponseNumeric], out learnerResponseNumeric);

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

                InteractionType? interactionType;
                LStoreHelper.Cast(dataRow[Schema.InteractionResultsByAttempt.InteractionType], out interactionType);
                Common.Models.Shared.Statistics.InteractionType? learnerResponseType = null;
                if (interactionType != null)
                {
                    learnerResponseType = (Common.Models.Shared.Statistics.InteractionType)interactionType;
                }

                float? minScore;
                LStoreHelper.Cast(dataRow[Schema.InteractionResultsByAttempt.MinScore], out minScore);

                float? maxScore;
                LStoreHelper.Cast(dataRow[Schema.InteractionResultsByAttempt.MaxScore], out maxScore);

                float? rawScore;
                LStoreHelper.Cast(dataRow[Schema.InteractionResultsByAttempt.RawScore], out rawScore);

                float? scaledScore;
                LStoreHelper.Cast(dataRow[Schema.InteractionResultsByAttempt.ScaledScore], out scaledScore);

                // Create AnswerResult object
                var answerResult = new AnswerResult(
                    activityAttemptId,
                    activityPackageId,
                    activityTitle,
                    interactionId,
                    iudicoCompletionStatus,
                    iudicoSuccessStatus,
                    attemptResult,
                    learnerResponse,
                    correctResponse,
                    learnerResponseType,
                    minScore,
                    maxScore,
                    rawScore,
                    scaledScore);
                yield return answerResult;
            }
        }

        #endregion

        #region Protected Methods

        private static readonly ReadOnlyCollection<string> AllAttemptsColumns =
            new ReadOnlyCollection<string>(
                new List<string>
                    {
                        Schema.AllAttemptsResults.AttemptId,
                        Schema.AllAttemptsResults.AttemptStatus,
                        Schema.AllAttemptsResults.CompletionStatus,
                        Schema.AllAttemptsResults.CurriculumChapterTopicId,
                        Schema.AllAttemptsResults.MinScore,
                        Schema.AllAttemptsResults.MaxScore,
                        Schema.AllAttemptsResults.RawScore,
                        Schema.AllAttemptsResults.Score,
                        Schema.AllAttemptsResults.StartedTimestamp,
                        Schema.AllAttemptsResults.FinishedTimestamp,
                        Schema.AllAttemptsResults.SuccessStatus,
                        Schema.AllAttemptsResults.TopicType,
                        Schema.AllAttemptsResults.UserItemKey
                    });

        /// <summary>
        /// Affects <paramref name="job"/> object by adding query for all columns from <see cref="AllAttemptsColumns"/>
        /// and adding conditions specified in <paramref name="conditions"/>
        /// </summary>
        /// <param name="job"><see cref="LearningStoreJob"/> object to add query to.</param>
        /// <param name="conditions"><see cref="List{T}"/> of <see cref="QueryCondition"/> objects to add conditions to query.</param>
        protected void RequestAttemptResults(LearningStoreJob job, List<QueryCondition> conditions)
        {
            LearningStoreQuery query = this.LStore.CreateQuery(Schema.AllAttemptsResults.ViewName);

            // Select all columns except those mentioned in conditions.
            var queryColumns = AllAttemptsColumns.Except(conditions.Select(cond => cond.ColumnName)).ToList();

            // add columns to query
            foreach (var queryColumn in queryColumns)
            {
                query.AddColumn(queryColumn);
            }

            // add conditions to query
            foreach (var condition in conditions)
            {
                query.AddCondition(condition.ColumnName, condition.ConditionOperator, condition.Value);
            }

            job.PerformQuery(query);
        }

        /// <summary>
        /// Manages parsing of fields got from DataTable to return results of attempts.
        /// </summary>
        /// <param name="dataRows"><see cref="IEnumerable{T}"/> collection of <see cref="DataRow"/> containing results from <see cref="LearningStore"/></param>
        /// <param name="conditions"><see cref="List{T}"/> of <see cref="QueryCondition"/> objects needed to parse results properly.</param>
        /// <returns><see cref="IEnumerable{T}"/> collection of <see cref="AttemptResult"/> objects parsed from given <paramref name="dataRows"/>.</returns>
        protected IEnumerable<AttemptResult> ParseAttemptResults(
            IEnumerable<DataRow> dataRows, List<QueryCondition> conditions)
        {
            var queryColumns = AllAttemptsColumns.Except(conditions.Select(cond => cond.ColumnName)).ToList();

            foreach (var dataRow in dataRows)
            {
                var attemptResult = new AttemptResult();

                try
                {
                    foreach (var condition in conditions)
                    {
                        this.ParseAttemptResultField(condition.Value, condition.ColumnName, ref attemptResult);
                    }
                    foreach (var queryColumn in queryColumns)
                    {
                        this.ParseAttemptResultField(dataRow[queryColumn], queryColumn, ref attemptResult);
                    }
                }
                catch (NoNullAllowedException)
                {
                    // skip not actual attempt results
                    continue;
                }

                yield return attemptResult;
            }
        }

        /// <summary>
        /// Performs mapping and setting appropriate fields of <paramref name="attemptResult"/> with given values.
        /// </summary>
        /// <param name="rawValue"><see cref="object"/> representing raw value retrieved from LearningStore (DB).</param>
        /// <param name="fieldName"><see cref="string"/> value representing name of the field (column).</param>
        /// <param name="attemptResult"><see cref="AttemptResult"/> value passed by reference. Parsed value is being assigned to it's corresponding property.</param>
        protected void ParseAttemptResultField(object rawValue, string fieldName, ref AttemptResult attemptResult)
        {
            switch (fieldName)
            {
                case Schema.AllAttemptsResults.AttemptId:
                    AttemptItemIdentifier attemptItemId;
                    LStoreHelper.CastNonNull(rawValue, out attemptItemId);
                    attemptResult.AttemptId = attemptItemId.GetKey();
                    break;
                case Schema.AllAttemptsResults.AttemptStatus:
                    AttemptStatus attemptStatus;
                    LStoreHelper.CastNonNull(rawValue, out attemptStatus);
                    attemptResult.AttemptStatus = (Common.Models.Shared.Statistics.AttemptStatus)attemptStatus;
                    break;
                case Schema.AllAttemptsResults.CompletionStatus:
                    CompletionStatus completionStatus;
                    LStoreHelper.CastNonNull(rawValue, out completionStatus);
                    attemptResult.CompletionStatus = (Common.Models.Shared.Statistics.CompletionStatus)completionStatus;
                    break;
                case Schema.AllAttemptsResults.CurriculumChapterTopicId:
                    int curriculumChapterTopicId;
                    LStoreHelper.CastNonNull(rawValue, out curriculumChapterTopicId);
                    var curriculumChapterTopic =
                        this.CurriculumService.GetCurriculumChapterTopicById(curriculumChapterTopicId);
                    if (curriculumChapterTopic == null)
                    {
                        throw new NoNullAllowedException(
                            "Error while getting curriculum-chapter-topic with id = " + curriculumChapterTopicId);
                    }
                    attemptResult.CurriculumChapterTopic = curriculumChapterTopic;
                    break;
                case Schema.AllAttemptsResults.MinScore:
                    {
                        float? score;
                        LStoreHelper.Cast(rawValue, out score);
                        attemptResult.Score.MinScore = score;
                    }
                    break;
                case Schema.AllAttemptsResults.MaxScore:
                    {
                        float? score;
                        LStoreHelper.Cast(rawValue, out score);
                        attemptResult.Score.MaxScore = score;
                    }
                    break;
                case Schema.AllAttemptsResults.RawScore:
                    {
                        float? score;
                        LStoreHelper.Cast(rawValue, out score);
                        attemptResult.Score.RawScore = score;
                    }
                    break;
                case Schema.AllAttemptsResults.Score:
                    {
                        float? score;
                        LStoreHelper.Cast(rawValue, out score);
                        float? scaledScore = null;
                        if (score != null)
                        {
                            scaledScore = score / 100;
                        }
                        attemptResult.Score.ScaledScore = scaledScore;
                    }
                    break;
                case Schema.AllAttemptsResults.StartedTimestamp:
                    DateTime? startTime;
                    LStoreHelper.Cast(rawValue, out startTime);
                    attemptResult.StartTime = startTime;
                    break;
                case Schema.AllAttemptsResults.FinishedTimestamp:
                    DateTime? finishTime;
                    LStoreHelper.Cast(rawValue, out finishTime);
                    attemptResult.FinishTime = finishTime;
                    break;
                case Schema.AllAttemptsResults.SuccessStatus:
                    SuccessStatus successStatus;
                    LStoreHelper.CastNonNull(rawValue, out successStatus);
                    attemptResult.SuccessStatus = (Common.Models.Shared.Statistics.SuccessStatus)successStatus;
                    break;
                case Schema.AllAttemptsResults.TopicType:
                    int rawTopicType;
                    LStoreHelper.CastNonNull(rawValue, out rawTopicType);
                    attemptResult.TopicType = (TopicTypeEnum)rawTopicType;
                    break;
                case Schema.AllAttemptsResults.UserItemKey:
                    string userKey;
                    LStoreHelper.CastNonNull(rawValue, out userKey);
                    var user = this.UserService.GetUsers().SingleOrDefault(curr => curr.Id.ToString() == userKey);
                    if (user == null)
                    {
                        throw new NoNullAllowedException("Error while getting user with id = " + userKey);
                    }
                    attemptResult.User = user;
                    break;
            }
        }

        /// <summary>
        /// Extracts curriculum chapter topic id from given <paramref name="dataRow"/>.
        /// </summary>
        /// <param name="dataRow"><see cref="DataRow"/> value containing needed field value.</param>
        /// <returns><see cref="int"/> value representing id of <see cref="CurriculumChapterTopic"/></returns>
        protected int ParseCurriculumChapterTopicId(DataRow dataRow)
        {
            int curriculumChapterTopicId;
            LStoreHelper.CastNonNull(
                dataRow[Schema.AllAttemptsResults.CurriculumChapterTopicId], out curriculumChapterTopicId);
            return curriculumChapterTopicId;
        }

        /// <summary>
        /// Requests that the list of all answers by specified attempt on organization.
        /// Adds the request to a given <c>LearningStoreJob</c> for later execution.
        /// </summary>
        /// <param name="job">A <c>LearningStoreJob</c> to add the new query to.</param>
        /// <param name="attemptId"><c>AttemptItemIdentifier</c> represents id of attempt.</param>
        protected void RequestInteractionResultsByAttempt(LearningStoreJob job, AttemptItemIdentifier attemptId)
        {
            LearningStoreQuery query = this.LStore.CreateQuery(Schema.InteractionResultsByAttempt.ViewName);

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
            query.AddColumn(Schema.InteractionResultsByAttempt.MinScore);
            query.AddColumn(Schema.InteractionResultsByAttempt.MaxScore);
            query.AddColumn(Schema.InteractionResultsByAttempt.RawScore);
            query.AddColumn(Schema.InteractionResultsByAttempt.ScaledScore);

            query.SetParameter(Schema.InteractionResultsByAttempt.AttemptIdParam, attemptId);

            job.PerformQuery(query);
        }

        /// <summary>
        /// Creates attempt on given organization and returns attempt identifier.
        /// </summary>
        /// <param name="orgId">Long integer value represents organization identifier to create attempt on.</param>
        /// <param name="curriculumChapterTopicId">Int32 value representing id of curriculum chapter topic.</param>
        /// <param name="topicType"><see cref="TopicTypeEnum"/> value defines part of topic.</param>
        /// <returns>Long integer value, representing attempt identifier of created attempt.</returns>
        protected AttemptItemIdentifier CreateAttempt(long orgId, int curriculumChapterTopicId, TopicTypeEnum topicType)
        {
            var organizationId = new ActivityPackageItemIdentifier(orgId);

            StoredLearningSession session = StoredLearningSession.CreateAttempt(
                this.PStore, this.GetCurrentUserIdentifier(), organizationId, LoggingOptions.LogAll);
            LearningStoreJob job = this.LStore.CreateJob();
            var dic = new Dictionary<string, object>
                {
                    { Schema.AttemptItem.IudicoCurriculumChapterTopicRef, curriculumChapterTopicId },
                    { Schema.AttemptItem.IudicoTopicType, topicType }
                };
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
            PackageItemIdentifier packageId;

            using (PackageReader packageReader = package.GetPackageReader())
            {
                AddPackageResult result = this.PStore.AddPackage(
                    packageReader, new PackageEnforcement(false, false, false));
                packageId = result.PackageId;
            }

            LearningStoreJob job = this.LStore.CreateJob();
            var dic = new Dictionary<string, object> { { Schema.PackageItem.IudicoCourseRef, package.CourseID } };
            job.UpdateItem(packageId, dic);
            job.Execute();

            return packageId;
        }

        /// <summary>
        /// Retrieves package id by specified IUDICO course.
        /// </summary>
        /// <param name="courseId">Int32 value representing Iudico-course identifier.</param>
        /// <returns>PackageItemIdentifier value representing corresponding MLC Package ID.</returns>
        protected PackageItemIdentifier GetPackageIdentifier(int courseId)
        {
            PackageItemIdentifier result = null;
            LearningStoreJob job = this.LStore.CreateJob();

            LearningStoreQuery query = this.LStore.CreateQuery(Schema.PackageIdByCourse.ViewName);
            query.AddColumn(Schema.PackageIdByCourse.PackageId);
            query.SetParameter(Schema.PackageIdByCourse.IudicoCourseRef, courseId);

            job.PerformQuery(query);

            ReadOnlyCollection<object> resultList = job.Execute();

            var dataTable = (DataTable)resultList[0];

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
            LearningStoreJob job = this.LStore.CreateJob();

            LearningStoreQuery query = this.LStore.CreateQuery(Schema.RootActivityByPackage.ViewName);
            query.AddColumn(Schema.RootActivityByPackage.RootActivity);
            query.SetParameter(Schema.RootActivityByPackage.PackageId, packageId);

            job.PerformQuery(query);

            var resultList = job.Execute();

            var dataTable = (DataTable)resultList[0];

            if (dataTable.Rows.Count > 0)
            {
                LStoreHelper.Cast(dataTable.Rows[0][Schema.RootActivityByPackage.RootActivity], out result);
            }

            return result;
        }

        /// <summary>
        /// Retrieves attempt identifier for specified organization id and Iudico topic id.
        /// </summary>
        /// <param name="orgId"><c>ActivityPackageItemIdentifier</c> value representing Organization ID.</param>
        /// <param name="curriculumChapterTopicId">Integer value - IUDICO curriculum chapter topic id.</param>
        /// <param name="topicType"><see cref="TopicTypeEnum"/> value.</param>
        /// <returns><c>AttemptItemIdentifier</c> value representing Attempt Identifier.</returns>
        protected AttemptItemIdentifier GetAttemptIdentifier(
            ActivityPackageItemIdentifier orgId, int curriculumChapterTopicId, TopicTypeEnum topicType)
        {
            AttemptItemIdentifier result = null;
            LearningStoreJob job = this.LStore.CreateJob();

            LearningStoreQuery query = this.LStore.CreateQuery(Schema.MyAttemptIds.ViewName);
            query.AddColumn(Schema.MyAttemptIds.AttemptId);
            query.SetParameter(Schema.MyAttemptIds.CurriculumChapterTopicId, curriculumChapterTopicId);
            query.SetParameter(Schema.MyAttemptIds.OrganizationId, orgId);
            query.SetParameter(Schema.MyAttemptIds.TopicType, topicType);

            job.PerformQuery(query);

            ReadOnlyCollection<object> resultList = job.Execute();

            var dataTable = (DataTable)resultList[0];

            if (dataTable.Rows.Count > 0)
            {
                // get last result
                LStoreHelper.Cast(dataTable.Rows[dataTable.Rows.Count - 1][Schema.MyAttemptIds.AttemptId], out result);
            }
            return result;
        }

        #endregion
    }
}
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System;
using Microsoft.LearningComponents;
using Microsoft.LearningComponents.Storage;
using IUDICO.TestingSystem.Models.VO;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models;
using LearningComponentsHelper;

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

        #region Public Methods

        /// <summary>
        /// Retrieves collection of trainings for current user.
        /// </summary>
        /// <returns>IEnumerable collection of Training objects.</returns>
        public IEnumerable<Training> GetTrainings()
        {
            LearningStoreJob job = LStore.CreateJob();
            RequestMyTraining(job, null);
            DataTable results = job.Execute<DataTable>();
            List<Training> packages = new List<Training>();
            foreach (DataRow dataRow in results.AsEnumerable())
            {
                // Create Training object
                Training training = new Training(dataRow);
                packages.Add(training);
            }

            return packages;
        }

        /// <summary>
        /// Checks if related to theme package has been already uploaded.
        /// In case it was not uploaded - upload package.
        /// Check attempt has been created and get attempt id.
        /// </summary>
        /// <param name="theme">Theme object represents specified theme.</param>
        /// <returns>Long integer value representing attempt id.</returns>
        public long GetAttemptId(Theme theme)
        {
            AttemptItemIdentifier attemptId = null;
            ActivityPackageItemIdentifier organizationId;
            var packageId = GetPackageIdentifier(theme.CourseRef);

            // in case package has not been uploaded yet.
            if (packageId == null)
            {
                string zipPath = CourseService.Export(theme.CourseRef);
                Package package = new ZipPackage(zipPath);
                packageId = AddPackage(package);
                organizationId = GetOrganizationIdentifier(packageId);
                attemptId = CreateAttempt(organizationId.GetKey());
            }
            // otherwise check if attempt was created
            else
            {
                organizationId = GetOrganizationIdentifier(packageId);

                AttemptItemIdentifier attId = GetAttemptIdentifier(organizationId, theme.Id);
                if (attId != null)
                {
                    attemptId = attId;
                }
                else
                {
                    attemptId = CreateAttempt(organizationId.GetKey());
                }
            }

            return attemptId.GetKey();
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Requests that the list of training for the current user be retrieved from the LearningStore
        /// database.  Adds the request to a given <c>LearningStoreJob</c> for later execution.
        /// </summary>
        /// 
        /// <param name="job">A <c>LearningStoreJob</c> to add the new query to.</param>
        /// 
        /// <param name="packageId">To request information related to a single pass the
        /// 	<c>PackageItemIdentifier</c> of the package as this parameter.  Otherwise, leave this
        /// 	parameter <c>null</c>.</param>
        /// 
        /// <remarks>
        /// After executing this method, and later calling <c>Job.Execute</c>, call
        /// <c>GetMyTrainingResultsToHtml</c> to convert the <c>DataTable</c> returned by
        /// <c>Job.Execute</c> into HTML.
        /// </remarks>
        ///
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
        protected AttemptItemIdentifier CreateAttempt(long orgID)
        {
            ActivityPackageItemIdentifier organizationID = new ActivityPackageItemIdentifier(orgID);
            
            StoredLearningSession session = StoredLearningSession.CreateAttempt(this.PStore, this.GetCurrentUserIdentifier(), organizationID, LoggingOptions.LogAll);

            return session.AttemptId;
        }
        
        /// <summary>
        /// Adds package to the database.
        /// </summary>
        /// <param name="package">Package value represents package object with necessary information.</param>
        protected PackageItemIdentifier AddPackage(Package package)
        {
            PackageItemIdentifier packageId = null;
            ValidationResults importLog;

            using (PackageReader packageReader = package.GetPackageReader())
            {
                AddPackageResult result = PStore.AddPackage(packageReader, new PackageEnforcement(false, false, false));
                packageId = result.PackageId;
                importLog = result.Log;
            }

            return packageId;
        }

        /// <summary>
        /// Deletes pacakge and related attempts from database.
        /// </summary>
        /// <param name="packId">Long integer value represents package identifier.</param>
        protected void DeletePackage(long packId)
        {
            // set <packageId> to the ID of this package
            PackageItemIdentifier packageId = new PackageItemIdentifier(packId);

            // before we delete the package, we need to delete all attempts on the package --
            // the following query looks for those attempts
            LearningStoreJob job = LStore.CreateJob();
            LearningStoreQuery query = LStore.CreateQuery(
                Schema.MyAttempts.ViewName);
            query.AddCondition(Schema.MyAttempts.PackageId,
                LearningStoreConditionOperator.Equal, packageId);
            query.AddCondition(Schema.MyAttempts.AttemptId,
                LearningStoreConditionOperator.NotEqual, null);
            query.AddColumn(Schema.MyAttempts.AttemptId);
            query.AddSort(Schema.MyAttempts.AttemptId,
                LearningStoreSortDirection.Ascending);
            job.PerformQuery(query);
            DataTable dataTable = job.Execute<DataTable>();
            AttemptItemIdentifier previousAttemptId = null;

            // loop once for each attempt on this package
            foreach (DataRow dataRow in dataTable.Rows)
            {
                // set <attemptId> to the ID of this attempt
                AttemptItemIdentifier attemptId;
                LStoreHelper.CastNonNull(dataRow["AttemptId"], out attemptId);

                // if <attemptId> is a duplicate attempt ID, skip it; note that the query
                // results are sorted by attempt ID (see above)
                if ((previousAttemptId != null) &&
                    (previousAttemptId.GetKey() == attemptId.GetKey()))
                    continue;

                // delete this attempt
                StoredLearningSession.DeleteAttempt(LStore, attemptId);

                // continue to the next attempt
                previousAttemptId = attemptId;
            }

            // delete the package
            PStore.DeletePackage(packageId);
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
        /// 
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="themeId"></param>
        /// <returns></returns>
        protected AttemptItemIdentifier GetAttemptIdentifier(ActivityPackageItemIdentifier orgId, int themeId)
        {
            AttemptItemIdentifier result = null;
            LearningStoreJob job = LStore.CreateJob();

            LearningStoreQuery query = LStore.CreateQuery(Schema.MyAttempts.ViewName);
            query.AddColumn(Schema.MyAttempts.AttemptId);
            query.AddCondition(Schema.MyAttempts.OrganizationId, LearningStoreConditionOperator.Equal, orgId);
            query.AddCondition(Schema.MyAttempts.ThemeId, LearningStoreConditionOperator.Equal, themeId);

            job.PerformQuery(query);

            ReadOnlyCollection<object> resultList = job.Execute();

            DataTable dataTable = (DataTable)resultList[0];

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
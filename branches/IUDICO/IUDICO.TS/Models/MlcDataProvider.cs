using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Microsoft.LearningComponents.DataModel;
using Microsoft.LearningComponents;
using Microsoft.LearningComponents.Storage;
using IUDICO.TS.Models.Shared;
using IUDICO.TS.Models.Schema;
using LearningComponentsHelper;


namespace IUDICO.TS.Models
{
    /// <summary>
    /// Singleton
    /// </summary>
    public class MlcDataProvider : MlcHelper
    {
        #region Singleton Implementation
        
        private static MlcDataProvider instance = null;

        private MlcDataProvider()
        {

        }

        public static MlcDataProvider Instance 
        {
            get
            {
                if (instance == null)
                {
                    instance = new MlcDataProvider();
                }
                return instance;
            }
        }

        #endregion
       
        #region Protected Properties
		
 
	    #endregion

        #region Public Methods

        public IEnumerable<Training> GetTrainings(long userID)
        {
            this.UserKey = userID.ToString();
            LearningStoreJob job = LStore.CreateJob();
            RequestMyTraining(job, null);
            DataTable results = job.Execute<DataTable>();
            List<Training> packages = new List<Training>();
            foreach (DataRow dataRow in results.AsEnumerable())
            {
                // extract information from <dataRow> into local variables
                PackageItemIdentifier packageId;
                LStoreHelper.CastNonNull(dataRow[Schema.MyAttemptsAndPackages.PackageId],
                    out packageId);
                long? pID;
                if (packageId == null)
                {
                    pID = null;
                }
                else
                {
                    pID = packageId.GetKey();
                }
                string packageFileName;
                LStoreHelper.CastNonNull(dataRow[Schema.MyAttemptsAndPackages.PackageFileName],
                    out packageFileName);
                ActivityPackageItemIdentifier organizationId;
                LStoreHelper.CastNonNull(dataRow[Schema.MyAttemptsAndPackages.OrganizationId],
                    out organizationId);
                long? orgID;
                if (organizationId == null)
                {
                    orgID = null;
                }
                else
                {
                    orgID = organizationId.GetKey();
                }
                string organizationTitle;
                LStoreHelper.CastNonNull(dataRow[Schema.MyAttemptsAndPackages.OrganizationTitle],
                    out organizationTitle);
                AttemptItemIdentifier attemptId;
                LStoreHelper.Cast(dataRow[Schema.MyAttemptsAndPackages.AttemptId],
                    out attemptId);
                long? attID;
                if (attemptId == null)
                {
                    attID = null;
                }
                else
                {
                    attID = attemptId.GetKey();
                }
                DateTime? uploadDateTime;
                LStoreHelper.Cast(dataRow[Schema.MyAttemptsAndPackages.UploadDateTime],
                    out uploadDateTime);
                AttemptStatus? attemptStatus;
                LStoreHelper.Cast(dataRow[Schema.MyAttemptsAndPackages.AttemptStatus],
                    out attemptStatus);
                float? score;
                LStoreHelper.Cast(dataRow[Schema.MyAttemptsAndPackages.TotalPoints],
                    out score);
                                
                // set <trainingName> to a name to use for this row (i.e. one
                // organization of one package)
                string trainingName;
                if (organizationTitle.Length == 0)
                    trainingName = packageFileName;
                else
                    trainingName = String.Format("{0} - {1}", packageFileName, organizationTitle); 
                
                
                // Create Training object
                Training training = new Training(pID, packageFileName, orgID, organizationTitle, attID, uploadDateTime, attemptStatus, score);
                packages.Add(training);
            }

            return packages;
        }

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
            LearningStoreQuery query = LStore.CreateQuery(Schema.MyAttemptsAndPackages.ViewName);
            query.AddColumn(Schema.MyAttemptsAndPackages.PackageId);
            query.AddColumn(Schema.MyAttemptsAndPackages.PackageFileName);
            query.AddColumn(Schema.MyAttemptsAndPackages.OrganizationId);
            query.AddColumn(Schema.MyAttemptsAndPackages.OrganizationTitle);
            query.AddColumn(Schema.MyAttemptsAndPackages.AttemptId);
            query.AddColumn(Schema.MyAttemptsAndPackages.UploadDateTime);
            query.AddColumn(Schema.MyAttemptsAndPackages.AttemptStatus);
            query.AddColumn(Schema.MyAttemptsAndPackages.TotalPoints);
            query.AddSort(Schema.MyAttemptsAndPackages.UploadDateTime,
                LearningStoreSortDirection.Ascending);
            query.AddSort(Schema.MyAttemptsAndPackages.OrganizationId,
                LearningStoreSortDirection.Ascending);
            if (packageId != null)
            {
                query.AddCondition(Schema.MyAttemptsAndPackages.PackageId,
                    LearningStoreConditionOperator.Equal, packageId);
            }
            job.PerformQuery(query);
        }

        /// <summary>
        /// Creates attempt on given organization and returns attempt identifier.
        /// </summary>
        /// <param name="orgID">Long integer value represents organization identifier to create attempt on.</param>
        /// <returns>Long integer value, representing attempt identifier of created attempt.</returns>
        public long CreateAttempt(long orgID)
        {
            ActivityPackageItemIdentifier organizationID = new ActivityPackageItemIdentifier(orgID);
            
            StoredLearningSession session = StoredLearningSession.CreateAttempt(this.PStore, this.CurrentUserIdentifier, organizationID, LoggingOptions.LogAll);

            long attemptID = session.AttemptId.GetKey();
            return attemptID;
        }

        /// <summary>
        /// Adds package to the database
        /// </summary>
        /// <param name="package">Package value represents package object with necessary information.</param>
        public void AddPackage(Package package)
        {

        }

	    #endregion
    }
}
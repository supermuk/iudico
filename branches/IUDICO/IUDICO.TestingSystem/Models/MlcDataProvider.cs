using System.Collections.Generic;
using System.Data;
using Microsoft.LearningComponents;
using Microsoft.LearningComponents.Storage;
using IUDICO.TestingSystem.Models.Shared;


namespace IUDICO.TestingSystem.Models
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

        /// <summary>
        /// Retrieves collection of trainings for given user.
        /// </summary>
        /// <param name="userID">Long integer value representing user id.</param>
        /// <returns>IEnumerable collection of Training objects.</returns>
        public IEnumerable<Training> GetTrainings(long userID)
        {
            this.UserKey = userID.ToString();
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
        public Training AddPackage(Package package)
        {
            PackageItemIdentifier packageId;
            ValidationResults importLog;
           
            using (PackageReader packageReader = package.GetPackageReader())
            {
                AddPackageResult result = PStore.AddPackage(packageReader, new PackageEnforcement(false, false, false));
                packageId = result.PackageId;
                importLog = result.Log;
            }

            // fill in the application-specific columns of the PackageItem table
            LearningStoreJob job = LStore.CreateJob();
            Dictionary<string, object> properties = new Dictionary<string, object>();
            properties[Schema.PackageItem.Owner] = new UserItemIdentifier(package.Owner);
            properties[Schema.PackageItem.FileName] = package.FileName;
            properties[Schema.PackageItem.UploadDateTime] = package.UploadDateTime;
            job.UpdateItem(packageId, properties);
            job.Execute();

            // retrieve information about the package
            job = LStore.CreateJob();
            RequestMyTraining(job, packageId);

            DataTable dataTableResults = job.Execute<DataTable>();
            Training training = new Training(dataTableResults.Rows[0]);

            return training;
        }

	    #endregion
    }
}
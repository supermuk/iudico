using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Microsoft.LearningComponents.DataModel;
using Microsoft.LearningComponents.Storage;
using IUDICO.TS.Models.Shared;
using IUDICO.TS.Models.Schema;


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

        public IEnumerable<Package> GetPackages(long userID)
        {
            this.UserKey = userID.ToString();
            LearningStoreJob job = LStore.CreateJob();
            RequestMyTraining(job, null);
            DataTable results = job.Execute<DataTable>();
            List<Package> packages = new List<Package>();
            foreach (DataRow row in results.AsEnumerable())
            {
                LearningStoreItemIdentifier pid = row[Schema.MyAttemptsAndPackages.PackageId] as LearningStoreItemIdentifier;
                long id = pid.GetKey();
                string location = row[Schema.MyAttemptsAndPackages.PackageFileName].ToString();
                string title = row[Schema.MyAttemptsAndPackages.OrganizationTitle].ToString();
                string orgID = row[Schema.MyAttemptsAndPackages.OrganizationId].ToString();
                Package package = new Package(id, location, title);
                packages.Add(package);
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

	    #endregion
    }
}
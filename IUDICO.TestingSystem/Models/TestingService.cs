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
using BasicWebPlayer.Schema;
using System.Data;

namespace IUDICO.TestingSystem.Models
{
    public class TestingService : ITestingService
    {
        protected PageHelper PageHelper { get; set; }

        public TestingService()
        {
            PageHelper = new PageHelper();
        }

        public ICourseService CourseService
        {
            get
            {
                return PluginController.LmsService.FindService<ICourseService>();
            }
        }

        #region ITestingService interface implementation

        public IEnumerable<AttemptResult> GetResults(User user, Theme theme)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttemptResult> GetAllAttempts()
        {
            throw new NotImplementedException();
        }


        public IEnumerable<AnswerResult> GetAnswers(AttemptResult attempt)
        {
            throw new NotImplementedException();
        }

        public PackageItemIdentifier UploadPackage(Course course)
        {
            LStoreUserInfo currentUser = PageHelper.GetCurrentUserInfo();

            PackageItemIdentifier packageId;
            
            using (PackageReader packageReader = PackageReader.Create(File.OpenRead(CourseService.Export(course.Id))))
            {
                AddPackageResult result = PageHelper.PStore.AddPackage(packageReader, new PackageEnforcement(false, false, false));
                packageId = result.PackageId;
            }

            LearningStoreJob job = PageHelper.LStore.CreateJob();
            Dictionary<string, object> properties = new Dictionary<string, object>();
            properties[PackageItem.Owner] = currentUser.Id;
            properties[PackageItem.FileName] = course.Name;
            properties[PackageItem.UploadDateTime] = DateTime.Now;
            properties[PackageItem.IudicoCourseRef] = course.Id;
            job.UpdateItem(packageId, properties);
            job.Execute();

            return packageId;
        }

        public PackageItemIdentifier GetPackage(Course course)
        {
            LStoreUserInfo currentUser = PageHelper.GetCurrentUserInfo();

            LearningStoreJob job = PageHelper.LStore.CreateJob();

            LearningStoreQuery query = PageHelper.LStore.CreateQuery(PackageIdByCourse.ViewName);

            query.AddColumn(PackageIdByCourse.PackageId);
            query.SetParameter(PackageIdByCourse.IudicoCourseRef, course.Id);

            job.PerformQuery(query);

            var resultList = job.Execute();

            DataTable result = (DataTable)resultList[0];

            if (result.Rows.Count > 0)
                return new PackageItemIdentifier((LearningStoreItemIdentifier)result.Rows[0][PackageIdByCourse.PackageId]);

            return null;
        }

        public ActivityPackageItemIdentifier GetRootActivity(PackageItemIdentifier packageId)
        {
            LStoreUserInfo currentUser = PageHelper.GetCurrentUserInfo();

            LearningStoreJob job = PageHelper.LStore.CreateJob();

            LearningStoreQuery query = PageHelper.LStore.CreateQuery(RootActivityByPackage.ViewName);

            query.AddColumn(RootActivityByPackage.RootActivity);
            query.SetParameter(RootActivityByPackage.PackageId, packageId);

            job.PerformQuery(query);

            var resultList = job.Execute();

            DataTable result = (DataTable)resultList[0];

            if (result.Rows.Count > 0)
                return new ActivityPackageItemIdentifier((LearningStoreItemIdentifier)result.Rows[0][RootActivityByPackage.RootActivity]);

            return null;
        }

        public AttemptItemIdentifier CreateAttempt(ActivityPackageItemIdentifier rootActivity)
        {
            LStoreUserInfo currentUser = PageHelper.GetCurrentUserInfo();

            StoredLearningSession session = StoredLearningSession.CreateAttempt(PageHelper.PStore, currentUser.Id, rootActivity, LoggingOptions.LogAll);

            return session.AttemptId;
        }

        public long GetAttempt(Course course)
        {
            var packageId = GetPackage(course);

            if (packageId == null)
                packageId = UploadPackage(course);

            var rootActivity = GetRootActivity(packageId);

            var attempt = CreateAttempt(rootActivity);

            return attempt.GetKey();
        }

        #endregion
    }
}
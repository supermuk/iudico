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
            properties[Schema.PackageItem.Owner] = currentUser.Id;
            properties[Schema.PackageItem.FileName] = CourseService.Export(course.Id);
            properties[Schema.PackageItem.UploadDateTime] = DateTime.Now;
            job.UpdateItem(packageId, properties);
            job.Execute();

            return packageId;
        }

        public int GetAttempt(Course course)
        {
            UploadPackage(course);

            return 0;
        }

        #endregion
    }
}
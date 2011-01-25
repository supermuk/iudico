using System;
using System.Web.Configuration;
using Microsoft.LearningComponents;
using Microsoft.LearningComponents.Storage;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models;

namespace IUDICO.TestingSystem.Models
{
    public class MlcHelper
    {
        #region Service Properties

        protected ILmsService LmsSevice { get; set; }

        protected IUserService UserService
        {
            get
            {
                IUserService service = LmsSevice.FindService<IUserService>();
                return service;
            }
        }

        protected ICourseService CourseService
        {
            get
            {
                ICourseService service = LmsSevice.FindService<ICourseService>();
                return service;
            }
        }
       
        protected ICurriculumService CurriculumService
        {
            get
            {
                ICurriculumService service = LmsSevice.FindService<ICurriculumService>();
                return service;
            }
        }
        
        #endregion

        #region Constructor

        public MlcHelper(ILmsService lmsService)
        {
            LmsSevice = lmsService;
        }

        #endregion

        #region Protected Methods
        
        protected string GetConnectionString()
        {
            string result = LmsSevice.GetDbConnection().ConnectionString;
            return result;
        }

        protected User GetCurrentUser()
        {
            var result = UserService.GetCurrentUser();
            return result;
        }

        #endregion

        #region Private fields

        /// <summary>
        /// Holds the value of the <c>CurrentUserKey</c> property.
        /// </summary>
        long _CurrentUserKey;

        /// <summary>
        /// Holds the value of the <c>LStore</c> property.
        /// </summary>
        LearningStore m_lstore;

        /// <summary>
        /// Holds the value of the <c>PStoreDirectoryPath</c> property.
        /// </summary>
        string m_pstoreDirectoryPath;

        /// <summary>
        /// Holds the value of the <c>PStore</c> property.
        /// </summary>
        FileSystemPackageStore m_pstore;

        #endregion
        
        #region Properties

        /// <summary>
        /// The full path to the directory which contains the unzipped package
        /// files stored in PackageStore.
        /// </summary>
        ///
        public string PStoreDirectoryPath
        {
            get
            {
                if (m_pstoreDirectoryPath == null)
                {
                    // set <m_pstoreDirectoryPath> to the full path to the
                    // directory
                    m_pstoreDirectoryPath = "c:\\BasicWebPlayerPackages";
                }
                return m_pstoreDirectoryPath;
            }
            set
            {
                if (value.Trim().Length == 0)
                {
                    throw new ArgumentException("Packages store directory path should be a valid string!");
                }
                this.m_pstoreDirectoryPath = value;
            }
        }

        /// <summary>
        /// Retrieves the Guid of current user in context of which work is done.
        /// </summary>
        public long CurrentUserKey
        {
            get
            {
                if (_CurrentUserKey == null)
                {
                    _CurrentUserKey = 1;//GetCurrentUser().Id;
                }
                // TODO: change to current user key
                return 1;
            }
            set
            {
                _CurrentUserKey = value;
            }
        }

        /// <summary>
        /// Gets the SQL Server connection string that LearningStore will use to
        /// access this application's database.  The string is stored in
        /// "appSettings" section of Web.config.
        /// </summary>
        public string LStoreConnectionString
        {
            get
            {
                string result = "Server=.\\SQLEXPRESS;Database=Training;Integrated Security=true";
                // TODO: GetConnectionString();
                return result;
            }
        }

        /// <summary>
        /// Gets a reference to this application's LearningStore database.
        /// </summary>
        ///
        public LearningStore LStore
        {
            get
            {
                if (m_lstore == null)
                {
                    m_lstore = new LearningStore(
                        LStoreConnectionString, CurrentUserKey.ToString(), ImpersonationBehavior.UseOriginalIdentity);
                }
                return m_lstore;
            }
        }

        /// <summary>
        /// Gets a reference to this application's PackageStore, which consists of
        /// the "PackageFiles" subdirectory (within this application's directory)
        /// containing unzipped package files, plus information about these
        /// packages stored in the LearningStore database.  
        /// </summary>
        public FileSystemPackageStore PStore
        {
            get
            {
                if (m_pstore == null)
                {
                    m_pstore = new FileSystemPackageStore(LStore,
                        PStoreDirectoryPath, ImpersonationBehavior.UseOriginalIdentity);
                }
                return m_pstore;
            }
        }

        #endregion

        #region Protected methods

        protected UserItemIdentifier CurrentUserIdentifier
        {
            get
            {
                UserItemIdentifier id = new UserItemIdentifier(Convert.ToInt64(this.CurrentUserKey));
                return id;
            }
        }

        #endregion
    }
}
using System;
using System.Web.Configuration;
using Microsoft.LearningComponents;
using Microsoft.LearningComponents.Storage;

namespace IUDICO.TestingSystem.Models
{
    public class MlcHelper
    {
        #region Private fields

        /// <summary>
        /// Holds the value of the <c>UserKey</c> property.
        /// </summary>
        string m_userKey;

        /// <summary>
        /// Holds the value of the <c>UserName</c> property.
        /// </summary>
        string m_userName;

        /// <summary>
        /// Holds the value of the <c>LStoreConnectionString</c> property.
        /// </summary>
        string m_lstoreConnectionString;

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
                    m_pstoreDirectoryPath = WebConfigurationManager.AppSettings
                        ["packageStoreDirectoryPath"];
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
        /// Gets the string provided by the operating environment which uniquely
        /// identifies this user.
        /// </summary>
        public String UserKey
        {
            get
            {
                return this.m_userKey;
            }
            set
            {
                if (value.ToString().Length == 0)
                {
                    throw new ArgumentException("User Key should be a valid guid!");
                }
                this.m_userKey = value;
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
                if (m_lstoreConnectionString == null)
                {
                    m_lstoreConnectionString = WebConfigurationManager.AppSettings
                        ["learningComponentsConnnectionString"];
                }
                return m_lstoreConnectionString;
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
                        LStoreConnectionString, UserKey.ToString(), ImpersonationBehavior.UseOriginalIdentity);
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
                UserItemIdentifier id = new UserItemIdentifier(Convert.ToInt64(this.UserKey));
                return id;
            }
        }

        #endregion
    }
}
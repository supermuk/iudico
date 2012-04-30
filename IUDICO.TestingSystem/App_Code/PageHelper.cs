// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="PageHelper.cs">
//   
// </copyright>
// 
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Security.Principal;
using System.Web;
using System.Web.Configuration;

using IUDICO.TestingSystem.Models;

using Microsoft.LearningComponents;
using Microsoft.LearningComponents.Storage;

using Schema = IUDICO.TestingSystem.Schema;

// <summary>
// Helps implement this MLC web-based application.  ASP.NET web pages can be
// based on this class.
// </summary>
public class PageHelper : System.Web.UI.Page
{
    #region Private Fields

    /// <summary>
    /// Holds the value of the <c>UserKey</c> property.
    /// </summary>
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string mUserKey;

    /// <summary>
    /// Holds the value of the <c>UserName</c> property.
    /// </summary>
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string mUserName;

    /// <summary>
    /// Holds the value of the <c>LStoreConnectionString</c> property.
    /// </summary>
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string mLstoreConnectionString;

    /// <summary>
    /// Holds the value of the <c>LStore</c> property.
    /// </summary>
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private LearningStore mLstore;

    /// <summary>
    /// Holds the value of the <c>PStoreDirectoryPath</c> property.
    /// </summary>
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string mPstoreDirectoryPath;

    /// <summary>
    /// Holds the value of the <c>PStore</c> property.
    /// </summary>
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private FileSystemPackageStore mPstore;

    #endregion

    #region Public Properties

    /// <summary>
    /// Gets the string provided by the operating environment which uniquely
    /// identifies this user.  Since this application uses Windows
    /// authentication, we'll use the user's security ID (e.g.
    /// "S-1-5-21-2127521184-...") as the user key.
    /// </summary>
    public virtual string UserKey
    {
        get
        {
            if (this.mUserKey == null)
            {
                this.mUserKey = ServicesProxy.Instance.UserService.GetCurrentUser().Id.ToString();
            }

            return this.mUserKey;
        }
    }

    /// <summary>
    /// Gets the name of the current user.
    /// </summary>
    public virtual string UserName
    {
        get
        {
            if (this.mUserName == null)
            {
                this.mUserName = ServicesProxy.Instance.UserService.GetCurrentUser().Name;
            }

            return this.mUserName;
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
            if (this.mLstoreConnectionString == null)
            {
                this.mLstoreConnectionString =
                    WebConfigurationManager.AppSettings["learningComponentsConnnectionString"];
            }
            return this.mLstoreConnectionString;
        }
    }

    /// <summary>
    /// Gets a reference to this application's LearningStore database.
    /// </summary>
    public LearningStore LStore
    {
        get
        {
            if (this.mLstore == null)
            {
                this.mLstore = new LearningStore(
                    this.LStoreConnectionString, this.UserKey, ImpersonationBehavior.UseOriginalIdentity);
            }
            return this.mLstore;
        }
    }

    /// <summary>
    /// The full path to the directory which contains the unzipped package
    /// files stored in PackageStore.
    /// </summary>
    public string PStoreDirectoryPath
    {
        get
        {
            if (this.mPstoreDirectoryPath == null)
            {
                // set <m_pstoreDirectoryPath> to the full path to the
                // directory
                var path = HttpContext.Current == null
                               ? Path.Combine(Environment.CurrentDirectory, "Site")
                               : HttpContext.Current.Request.PhysicalApplicationPath;
                if (path == null)
                {
                    throw new NullReferenceException("Can't retrieve path to Player Packages folder");
                }
                this.mPstoreDirectoryPath = Path.Combine(path, @"Data\PlayerPackages");
            }
            return this.mPstoreDirectoryPath;
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
            if (this.mPstore == null)
            {
                this.mPstore = new FileSystemPackageStore(
                    this.LStore, this.PStoreDirectoryPath, ImpersonationBehavior.UseOriginalIdentity);
            }
            return this.mPstore;
        }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Requests that information about the current user be retrieved from the
    /// LearningStore database.  Adds the request to a given
    /// <c>LearningStoreJob</c> for later execution.
    /// </summary>
    /// 
    /// <param name="job">A <c>LearningStoreJob</c> to add the new query to.
    ///     </param>
    /// 
    /// <remarks>
    /// After executing this method, and later calling <c>Job.Execute</c>,
    /// call <c>GetCurrentUserInfoResults</c> to convert the <c>DataTable</c>
    /// returned by <c>Job.Execute</c> into an <c>LStoreUserInfo</c> object.
    /// </remarks>
    protected void RequestCurrentUserInfo(LearningStoreJob job)
    {
        // look up the user in the UserItem table in the database using their
        // user key, and set <userId> to the LearningStore numeric identifier
        // of the user (i.e. UserItem.Id -- the "Id" column of the UserItem
        // table) and <userName> to their full name (e.g. "Karen Berg"); if
        // there's no UserItem for the user, add one and set <userId> to its
        // ID
        LearningStoreQuery query = this.LStore.CreateQuery(Schema.Me.ViewName);
        query.AddColumn(Schema.Me.UserId);
        query.AddColumn(Schema.Me.UserName);
        job.PerformQuery(query);
    }

    /// <summary>
    /// Reads a <c>DataTable</c>, returned by <c>Job.Execute</c>, containing
    /// the results requested by a previous call to
    /// <c>RequestCurrentUserInfo</c>.  Returns an <c>LStoreUserInfo</c>
    /// object containing information about the user.  If the user isn't
    /// already listed in LearningStore, a separate call to the database is
    /// made to add them.
    /// </summary>
    ///
    /// <param name="dataTable">A <c>DataTable</c> returned from
    /// <c>Job.Execute</c>.</param>
    protected LStoreUserInfo GetCurrentUserInfoResults(DataTable dataTable)
    {
        DataRowCollection results = dataTable.Rows;
        LearningStoreJob job = this.LStore.CreateJob();
        UserItemIdentifier userId;
        string userName;
        if (results.Count == 0)
        {
            // the user isn't listed in the UserItem table -- add them...

            // set <userName> to the name of the user that SCORM will use
#if false
    // the following code queries Active Directory for the full name
    // of the user (for example, "Karen Berg") -- this code assumes a
    // particular Active Directory configuration which may or may not
    // work in your situation
            string adsiPath = String.Format("WinNT://{0},user", 
                UserIdentity.Name.Replace(@"\", "/"));
            using (DirectoryEntry de = new DirectoryEntry(adsiPath))
                userName = (string)de.Properties["FullName"].Value;
#else
            // the following code uses the "name" portion of the user's
            // "domain\name" network account name as the name of the user
            userName = this.UserName;
            int backslash = userName.IndexOf('\\');
            if (backslash >= 0)
            {
                userName = userName.Substring(backslash + 1);
            }
#endif

            // create the UserItem for this user in LearningStore; we use
            // AddOrUpdateItem() instead of AddItem() in case this learner
            // was added by another application between the check above and
            // the code below
            job = this.LStore.CreateJob();
            Dictionary<string, object> uniqueValues = new Dictionary<string, object>();
            uniqueValues[Schema.UserItem.Key] = this.UserKey;
            Dictionary<string, object> addValues = new Dictionary<string, object>();
            addValues[Schema.UserItem.Name] = userName;
            job.AddOrUpdateItem(Schema.UserItem.ItemTypeName, uniqueValues, addValues, null, true);
            userId = new UserItemIdentifier(job.Execute<LearningStoreItemIdentifier>());
        }
        else
        {
            userId = new UserItemIdentifier((LearningStoreItemIdentifier)results[0][Schema.Me.UserId]);
            userName = (string)results[0][Schema.Me.UserName];
        }

        // return a LStoreUserInfo object
        return new LStoreUserInfo(userId, userName);
    }

    /// <summary>
    /// Retrieves information about the current user from the LearningStore
    /// database.
    /// </summary>
    public LStoreUserInfo GetCurrentUserInfo()
    {
        LearningStoreJob job = this.LStore.CreateJob();
        this.RequestCurrentUserInfo(job);
        return this.GetCurrentUserInfoResults(job.Execute<DataTable>());
    }

    /// <summary>
    /// A delegate with no parameters and no return value.
    /// </summary>
    ///
    public delegate void VoidDelegate();

    /// <summary>
    /// Executes a supplied delegate while impersonating the application pool
    /// account.
    /// </summary>
    /// <param name="del">The delegate to execute.</param>
    public void ImpersonateAppPool(VoidDelegate del)
    {
        try
        {
            WindowsImpersonationContext context = null;
            try
            {
                context = WindowsIdentity.Impersonate(IntPtr.Zero);
                del();
            }
            finally
            {
                if (context != null)
                {
                    context.Dispose();
                }
            }
        }
        catch
        {
            // prevent exception filter exploits
            throw;
        }
    }

    /// <summary>
    /// Formats a message using <c>String.Format</c> and writes to the event
    /// log.
    /// </summary>
    /// <param name="type">The type of the event log entry.</param>
    ///
    /// <param name="format">A string containing zero or more format items;
    ///     for example, "An exception occurred: {0}".</param>
    /// 
    /// <param name="args">Formatting arguments.</param>
    public void LogEvent(EventLogEntryType type, string format, params object[] args)
    {
        this.ImpersonateAppPool(
            delegate() { EventLog.WriteEntry("BasicWebPlayer", string.Format(format, args), type); });
    }

    #endregion
}

// <summary>
// Holds LearningStore information about the current user.  Use
// <c>GetCurrentUserInfo</c> to retrieve this information.
// </summary>
public class LStoreUserInfo
{
    #region Private Fields

    /// <summary>
    /// Holds the value of the <c>Id</c> property.
    /// </summary>
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private UserItemIdentifier mId;

    /// <summary>
    /// Holds the value of the <c>Name</c> property.
    /// </summary>
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string mName;

    #endregion 

    #region Public Properties

    /// <summary>
    /// Gets the UserItem.Id of this user, i.e. LearningStore's numeric
    /// identifier for this user.
    /// </summary>
    public UserItemIdentifier Id
    {
        get
        {
            return this.mId;
        }
    }

    /// <summary>
    /// Gets the full name of the user; for example, "Karen Berg".
    /// </summary>
    public string Name
    {
        get
        {
            return this.mName;
        }
    }

    /// <summary>
    /// Initializes an instance of this class.
    /// </summary>
    /// <param name="id">The value to use for the <c>Id</c> property.</param>
    /// <param name="name">The value to use for the <c>Name</c> property.</param>
    public LStoreUserInfo(UserItemIdentifier id, string name)
    {
        this.mId = id;
        this.mName = name;
    }

    #endregion
}
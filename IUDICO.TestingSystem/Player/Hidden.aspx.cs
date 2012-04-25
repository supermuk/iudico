// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="Hidden.aspx.cs">
//   
// </copyright>
// 
// --------------------------------------------------------------------------------------------------------------------

// using Resources;

using System;
using System.Threading;
using System.Web;

using IUDICO.Common.Models.Notifications;
using IUDICO.Common.Models.Shared.Statistics;
using IUDICO.TestingSystem;
using IUDICO.TestingSystem.Models;

using Microsoft.LearningComponents.Storage;

// This file contains the BWP-specific hidden frame rendering code. Most of the actual work is done in the code shared
// with SLK.

namespace Microsoft.LearningComponents.Frameset
{
    /// <summary>
    /// Parameters to this page:
    ///     AttemptId = current attempt id
    ///     View = requested view
    ///     I = (value is ignored). If present, the page is being display during initialization of the frameset.
    /// </summary>
    public partial class Frameset_Hidden : BwpFramesetPage
    {
        private HiddenHelper mHiddenHelper;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.mHiddenHelper = new HiddenHelper(this.Request, this.Response, this.FramesetPath);
                this.mHiddenHelper.ProcessPageLoad(
                    this.PStore,
                    this.GetSessionTitle,
                    this.ProcessViewParameter,
                    this.ProcessAttemptIdParameter,
                    AppendContentFrameDetails,
                    this.RegisterError,
                    this.GetErrorInfo,
                    this.ProcessSessionEnd,
                    this.ProcessViewRequest,
                    this.GetMessage,
                    this.IsPostBack);
            }
            catch (ThreadAbortException)
            {
                // Do nothing -- thread is leaving.
            }
            catch (Exception ex)
            {
                this.ClearError();
                this.RegisterError(
                    ResHelper.GetMessage(Localization.GetMessage("FRM_UnknownExceptionTitle")),
                    ResHelper.GetMessage(Localization.GetMessage("FRM_UnknownExceptionMsg"), HttpUtility.HtmlEncode(ex.Message.Replace("\r\n", " "))),
                    false);
            }
        }

        /// <summary>
        /// Render all hidden controls on to the page. 
        /// </summary>
        public void WriteHiddenControls()
        {
            this.mHiddenHelper.WriteHiddenControls();
        }

        /// <summary>
        /// Write the script to initialize the frameset manager.
        /// </summary>
        public void WriteFrameMgrInit()
        {
            this.mHiddenHelper.WriteFrameMgrInit();
        }

        /// <summary>
        /// Gets the title to display in the session.
        /// </summary>
        public PlainTextString GetSessionTitle(LearningSession session)
        {
            return new PlainTextString(session.Title);
        }

        /// <summary>
        /// Allows the app to take action when the session is ending.
        /// </summary>
        public void ProcessSessionEnd(LearningSession session, ref string messageTitle, ref string message)
        {
            // Session ending results in message shown to the user. 
            if (session.View == SessionView.Execute)
            {
                var slsSession = session as StoredLearningSession;
                if (slsSession != null)
                {
                    // The rollup and/or sequencing process may have changed the state of the attempt. If so, there are some cases
                    // that cannot continue so show an error message. 
                    switch (slsSession.AttemptStatus)
                    {
                        case AttemptStatus.Abandoned:
                            messageTitle = Localization.GetMessage("HID_SessionAbandonedTitle");
                            message = Localization.GetMessage("FRM_ExecuteViewAbandonedSessionMsg");
                            break;
                        case AttemptStatus.Completed:
                            AttemptResult attemptResult;
                            try
                            {
                                attemptResult =
                                    ServicesProxy.Instance.TestingService.GetResult(slsSession.AttemptId.GetKey());
                            }
                            catch (InvalidOperationException)
                            {
                                attemptResult = null;
                            }
                            ServicesProxy.Instance.LmsService.Inform(TestingNotifications.TestCompleted, attemptResult);
                            messageTitle = Localization.GetMessage("HID_SessionCompletedTitle");
                            message = Localization.GetMessage("FRM_ExecuteViewCompletedSessionMsg");
                            break;
                        case AttemptStatus.Suspended:
                            messageTitle = Localization.GetMessage("HID_SessionSuspendedTitle");
                            message = Localization.GetMessage("FRM_ExecuteViewSuspendedSessionMsg");
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Gets the uri path to the frameset directory.
        /// </summary>
        private Uri mFramesetPath;

        public Uri FramesetPath
        {
            get
            {
                if (this.mFramesetPath == null)
                {
                    // Need to get the Frameset directory, so that include files work. 
                    // We have to do the following path magic in order to account for the fact that the 
                    // current URL is of arbitrary depth.
                    // Request.Url.OriginalString
                    string currentExecutionPath = this.Request.Url.OriginalString; // Request.CurrentExecutionFilePath;
                    // Find the part of the first instance of /Frameset/Hidden.aspx and then get a substring that is the 
                    // current url, up to the end of the /Frameset/.
                    string framesetPath = currentExecutionPath.Substring(
                        0,
                        currentExecutionPath.IndexOf("/Frameset/Hidden.aspx", StringComparison.OrdinalIgnoreCase) + 10);
                    this.mFramesetPath = new Uri(framesetPath, UriKind.Absolute);
                }
                return this.mFramesetPath;
            }
        }
    }
}
// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="HiddenHelper.cs">
//   
// </copyright>
// 
// --------------------------------------------------------------------------------------------------------------------

// using Resources;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Xml;

using IUDICO.TestingSystem;

using Microsoft.LearningComponents;
using Microsoft.LearningComponents.DataModel;
using Microsoft.LearningComponents.Storage;

namespace Microsoft.LearningComponents.Frameset
{
    using IUDICO.Common;

    /// <summary>
    /// Delegate to return previously registered error information.
    /// </summary>
    /// <param name="hasError">If true, an error has been registered. If false, other parameter values 
    /// should be ignored.</param>
    /// <param name="errorTitle">The registered error title.</param>
    /// <param name="errorMsg">The registered error message.</param>
    /// <param name="asInfo">The registered error is information only.</param>
    public delegate void GetErrorInfo(out bool hasError, out string errorTitle, out string errorMsg, out bool asInfo);

    /// <summary>
    /// Delegate to retrieve the title to display for the session. Return value should be plain text.
    /// </summary>
    public delegate PlainTextString GetSessionTitle(LearningSession session);

    /// <summary>
    /// Delegate to allow the caller to process the end of the session. Returned messages (in text format)
    /// are displayed to the user.
    /// </summary>
    public delegate void ProcessSessionEnd(
        LearningSession session, ref string sessionEndMsgTitle, ref string sessionEndMsg);

    /// <summary>
    /// Delegate to allow application to append the details to the content frame URL that provides information
    /// about, for instance, view and attempt id. In the content frame url, this will be inserted between the 
    /// Content.aspx and resource path.
    /// For instance, if this call returns '/0/24' (indicating, in BWP the view and attempt id), then the content 
    /// frame URL, when loading foo.gif in a package will be:
    /// Content.aspx/0/24/foo.gif
    /// </summary>
    /// <remarks>This method is not called if the current activity entry point is an absolute url.</remarks>
    public delegate void AppendContentFrameDetails(LearningSession session, StringBuilder contentFrameUrl);

    /// <summary>
    /// Code used to assist in rendering and processing the hidden frame. This code is shared between SLK and BWP framesets.
    /// </summary>
    public class HiddenHelper : PostableFrameHelper
    {
        private Collection<HiddenControlInfo> mHiddenControlInfos;

        private bool mPageLoadSuccessful; // set to true if doPageLoad completed without exception

        private IsNavValidResponseData mIsNavValidResponse; // information about Is*Valid request

        private bool mLoadContentFrame; // If true, the command is written to load the content frame with a new URL

        // If true, the page is being rendered for the first time, as part of frameset.
        private bool mIsFramesetInitialization;

        private GetSessionTitle mGetSessionTitle;

        private GetSessionTitle GetSessionTitle
        {
            get
            {
                return this.mGetSessionTitle;
            }
        }

        private string mSessionEndedMsgTitle;

        private string mSessionEndedMsg;

        private bool mIsPostedPage;

        private bool mSaveOnly; // if true, the only command being processed is a save command

        /// <summary>Initializes a new instance of <see cref="HiddenHelper"/>.</summary>
        public HiddenHelper(HttpRequest request, HttpResponse response, Uri embeddedUIPath)
            : base(request, response, embeddedUIPath)
        {
        }

        /// <summary>
        /// Do the processing required for page loading. 
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        // it's not worth changing this now
        public void ProcessPageLoad(
            PackageStore packageStore,
            GetSessionTitle getSessionTitle,
            TryGetViewInfo tryGetViewInfo,
            TryGetAttemptInfo tryGetAttemptInfo,
            AppendContentFrameDetails appendContentFrameDetails,
            RegisterError registerError,
            GetErrorInfo getErrorInfo,
            ProcessSessionEnd processSessionEnd,
            ProcessViewRequest processViewRequest,
            GetFramesetMsg getFramesetMsg,
            // messages that appear that are unique to different framesets
            bool isPostBack)
        {
            try
            {
                this.RegisterError = registerError;
                this.GetErrorInfo = getErrorInfo;
                this.AppendContentFrameDetails = appendContentFrameDetails;
                this.GetFramesetMsg = getFramesetMsg;

                this.mGetSessionTitle = getSessionTitle;
                this.mIsPostedPage = isPostBack;

                AttemptItemIdentifier attemptId;
                SessionView view;
                this.LoadContentFrame = true;
                this.ActivityHasChanged = false;

                if (!tryGetViewInfo(false, out view))
                {
                    this.WriteError(
                        ResHelper.GetMessage(Localization.GetMessage("FRM_ViewNotSupportedMsg")));
                    return;
                }

                switch (view)
                {
                    case SessionView.Execute:
                        {
                            if (!tryGetAttemptInfo(false, out attemptId))
                            {
                                return;
                            }

                            this.Session = new StoredLearningSession(view, attemptId, packageStore);
                            if (!processViewRequest(view, this.Session))
                            {
                                return;
                            }

                            // If the session has ended, allow the application to deal with it.
                            processSessionEnd(this.Session, ref this.mSessionEndedMsgTitle, ref this.mSessionEndedMsg);
                        }
                        break;
                    case SessionView.Review:
                        {
                            if (!tryGetAttemptInfo(false, out attemptId))
                            {
                                return;
                            }

                            this.Session = new StoredLearningSession(view, attemptId, packageStore);
                            // Do not set logging options in review view.

                            if (!processViewRequest(view, this.Session))
                            {
                                return;
                            }
                        }
                        break;
                    case SessionView.RandomAccess:
                        {
                            // Note: RandomAccess is not supported in BWP, however that would have been caught before 
                            // displaying this frame.
                            if (!tryGetAttemptInfo(false, out attemptId))
                            {
                                return;
                            }

                            this.Session = new StoredLearningSession(view, attemptId, packageStore);
                            // Do not set logging options in random access view.

                            if (!processViewRequest(view, this.Session))
                            {
                                return;
                            }

                            // Move to the first activity with a resource.
                            MoveToNextActivity(this.Session);
                        }
                        break;
                    default:
                        this.WriteError(
                            ResHelper.GetMessage(
                                Localization.GetMessage("FRM_ViewNotSupportedMsg")));
                        return;
                }

                // If the page is posted, process posted data. Remember that all posted data should be considered hostile! 
                // Note that if the session has already ended, then none of the posted data is saved or processed.
                if (isPostBack && !this.SessionIsEnded)
                {
                    // Process any datamodel changes before doing any navigation. This does not save any data.
                    this.ProcessDataModelValues(
                        this.Request.Form[HiddenFieldNames.DataModel],
                        this.Request.Form[HiddenFieldNames.ObjectiveIdMap]);

                    // Assume we do not have to reload the content frame
                    this.ActivityHasChanged = false;
                    this.LoadContentFrame = false;

                    // if the view requires more data, get it
                    if ((this.Session.View == SessionView.Review) || (this.Session.View == SessionView.RandomAccess))
                    {
                        // Get the current activity from the posted data and set the session to that activity
                        string strActivityId = this.Request.Form[HiddenFieldNames.ActivityId];
                        long activityId;
                        if (string.IsNullOrEmpty(strActivityId) || !long.TryParse(strActivityId, out activityId))
                        {
                            this.WriteError(
                                ResHelper.GetMessage(
                                    Localization.GetMessage("HID_InvalidActivityId"), strActivityId));
                        }
                        else
                        {
                            MoveToActivity(this.Session, activityId);
                        }
                    }

                    // Find out what the commands are and do them.
                    this.mSaveOnly = true;
                    ICollection<CommandInfo> commands = this.GetCommands();
                    foreach (CommandInfo cmdInfo in commands)
                    {
                        switch (cmdInfo.Command)
                        {
                            case Commands.DoNext:
                                {
                                    if (!this.Session.HasCurrentActivity || !ProcessNavigationRequests(this.Session))
                                    {
                                        if (this.Session.IsMoveToNextValid())
                                        {
                                            MoveToNextActivity(this.Session);
                                            this.ActivityHasChanged = true;
                                            this.LoadContentFrame = true;
                                        }
                                        else
                                        {
                                           this.ActivityHasChanged = true;
                                           this.LoadContentFrame = true;
                                           continue;
                                        }
                                    }
                                    else
                                    {
                                        this.ActivityHasChanged = true;
                                        this.LoadContentFrame = true;
                                    }

                                    if (!this.ActivityHasChanged)
                                    {
                                        // Moving to the next activity is not valid. It's possible that when the current
                                        // activity was unloaded, it exited or suspended itself. If that's the case, we 
                                        // try to reactivate it. Note this causes the attempt count on the activity to 
                                        // increment, so on "poorly" written content, the user may not see their data 
                                        // anymore.
                                        this.ActivateCurrentActivity();
                                        this.WriteError(
                                            ResHelper.Format(
                                                this.GetFramesetMsg(FramesetStringId.MoveToNextFailedHtml),
                                                this.ThemeFolderPath),
                                            true);
                                    }
                                    this.mSaveOnly = false;
                                }
                                break;
                            case Commands.DoPrevious:
                                {
                                    if (!this.Session.HasCurrentActivity || !ProcessNavigationRequests(this.Session))
                                    {
                                        if (this.Session.IsMoveToPreviousValid())
                                        {
                                            MoveToPreviousActivity(this.Session);
                                            this.ActivityHasChanged = true;
                                            this.LoadContentFrame = true;
                                        }
                                        else
                                        {
                                           this.ActivityHasChanged = true;
                                           this.LoadContentFrame = true;
                                           continue;
                                        }
                                    }
                                    else
                                    {
                                        this.ActivityHasChanged = true;
                                        this.LoadContentFrame = true;
                                    }

                                    if (!this.ActivityHasChanged)
                                    {
                                        // Moving to the previous activity is not valid. It's possible that when the current
                                        // activity was unloaded, it exited or suspended itself. If that's the case, we 
                                        // try to reactivate it. Note this causes the attempt count on the activity to 
                                        // increment, so on "poorly" written content, the user may not see their data 
                                        // anymore.
                                        this.ActivateCurrentActivity();
                                        this.WriteError(
                                            ResHelper.Format(
                                                this.GetFramesetMsg(FramesetStringId.MoveToPreviousFailedHtml),
                                                this.ThemeFolderPath),
                                            true);
                                    }
                                    this.mSaveOnly = false;
                                }
                                break;
                            case Commands.DoChoice:
                                {
                                    // This command is used to navigate to activities, primarily from either within a link in an
                                    // error page, or the submit page returning to the current activity. This command does not 
                                    // create a new attempt if the requested activity is already the current activity.

                                    // Requesting to move to a specific activity. The cmdData will include a numeric activity id.
                                    string cmdData = cmdInfo.CommandData;
                                    long activityId;
                                    if (long.TryParse(cmdData, out activityId))
                                    {
                                        // If the requested activity is the current activity, then do not do the navigation.
                                        // We skip it because moving to the activity basically exits the current attempt and creates
                                        // a new one and in this case, that is not the desired behavior. 
                                        // That new one also increments the attempt count. If we don't do the move, we 
                                        // pretend it was done. This will force the content frame to be reloaded with the current 
                                        // activity.
                                        if (this.IsCurrentActiveActivity(activityId))
                                        {
                                            this.ActivityHasChanged = true;
                                            this.LoadContentFrame = true;
                                        }
                                        else
                                        {
                                            // If there is no current activity, or if any navigation requests did not 
                                            // result in a move, then continue with the choice.
                                            if (!this.Session.HasCurrentActivity
                                                || !ProcessNavigationRequests(this.Session))
                                            {
                                                if (this.Session.IsMoveToActivityValid(activityId))
                                                {
                                                    MoveToActivity(this.Session, activityId);
                                                    this.ActivityHasChanged = true;
                                                    this.LoadContentFrame = true;
                                                }
                                            }
                                            else
                                            {
                                                this.ActivityHasChanged = true;
                                                this.LoadContentFrame = true;
                                            }
                                        }
                                    }

                                    if (!this.ActivityHasChanged)
                                    {
                                        // Moving to the selected activity is not valid. It's possible that when the current
                                        // activity was unloaded, it exited or suspended itself. If that's the case, we 
                                        // try to reactivate it. Note this causes the attempt count on the activity to 
                                        // increment, so on "poorly" written content, the user may not see their data 
                                        // anymore.

                                        this.ActivateCurrentActivity();
                                        this.WriteError(
                                            ResHelper.Format(
                                                this.GetFramesetMsg(FramesetStringId.MoveToActivityFailedHtml),
                                                this.ThemeFolderPath),
                                            true);
                                    }
                                    this.mSaveOnly = false;
                                }
                                break;
                            case Commands.DoTocChoice:
                                {
                                    // This command is used to navigate to activities in response to a user selecting a node
                                    // in the TOC. In this case, even if the current activity is the activity that is requested,
                                    // then a MoveToActivity() is requested. This will cause the attempt count on the activity
                                    // to be incremented and the RTE to be reinitialized. This may be a surprise to the user, but 
                                    // is a requirement of the SCORM 2004 conformance tests.

                                    // If the selected page is the submit page (either in Execute or RandomAccess views), then
                                    // display the message and don't ask the session to move to a new activity.

                                    string cmdData = cmdInfo.CommandData;
                                    if (string.CompareOrdinal(cmdData, SubmitId) == 0)
                                    {
                                        // Requesting submit page. Do not change the current activity, but mark it as changed so that 
                                        // it appears to the user that it has changed.
                                        this.ActivityHasChanged = true;
                                        this.LoadContentFrame = true;
                                        string title = this.GetFramesetMsg(FramesetStringId.SubmitPageTitleHtml);
                                        string message;
                                        string saveBtn = this.GetFramesetMsg(FramesetStringId.SubmitPageSaveButtonHtml);
                                        if (this.Session.HasCurrentActivity)
                                        {
                                            message = this.GetFramesetMsg(FramesetStringId.SubmitPageMessageHtml);
                                        }
                                        else
                                        {
                                            message =
                                                this.GetFramesetMsg(
                                                    FramesetStringId.SubmitPageMessageNoCurrentActivityHtml);
                                        }
                                        this.WriteSubmitPage(title, message, saveBtn);
                                    }
                                    else
                                    {
                                        // Requesting to move to a specific activity. The cmdData will include a numeric activity id.

                                        long activityId;
                                        if (long.TryParse(cmdData, out activityId))
                                        {
                                            // If there is no current activity, or if any navigation requests did not 
                                            // result in a move, then continue with the choice.
                                            if (!this.Session.HasCurrentActivity
                                                || !ProcessNavigationRequests(this.Session))
                                            {
                                                if (this.Session.IsMoveToActivityValid(activityId))
                                                {
                                                    MoveToActivity(this.Session, activityId);
                                                    this.ActivityHasChanged = true;
                                                    this.LoadContentFrame = true;
                                                }
                                            }
                                            else
                                            {
                                                this.ActivityHasChanged = true;
                                                this.LoadContentFrame = true;
                                            }
                                        }
                                    }

                                    if (!this.ActivityHasChanged)
                                    {
                                        // Moving to the selected activity is not valid. It's possible that when the current
                                        // activity was unloaded, it exited or suspended itself. If that's the case, we 
                                        // try to reactivate it. Note this causes the attempt count on the activity to 
                                        // increment, so on "poorly" written content, the user may not see their data 
                                        // anymore.

                                        this.ActivateCurrentActivity();
                                        this.WriteError(
                                            ResHelper.Format(
                                                this.GetFramesetMsg(FramesetStringId.MoveToActivityFailedHtml),
                                                this.ThemeFolderPath),
                                            true);
                                    }
                                    this.mSaveOnly = false;
                                }
                                break;
                            case Commands.DoIsChoiceValid:
                                {
                                    string activityKey = cmdInfo.CommandData;
                                    bool isValid = false;
                                    if (!string.IsNullOrEmpty(activityKey))
                                    {
                                        isValid = this.Session.IsMoveToActivityValid(activityKey);
                                    }
                                    this.mIsNavValidResponse = new IsNavValidResponseData(
                                        Commands.DoIsChoiceValid, activityKey, isValid);
                                    this.mSaveOnly = false;
                                }
                                break;
                            case Commands.DoIsNavigationValid:
                                {
                                    string navCommand = cmdInfo.CommandData;
                                    if (!string.IsNullOrEmpty(navCommand))
                                    {
                                        bool isValid = false;
                                        if (navCommand == Commands.DoNext)
                                        {
                                            isValid = this.Session.IsMoveToNextValid();
                                        }
                                        else if (navCommand == Commands.DoPrevious)
                                        {
                                            isValid = this.Session.IsMoveToPreviousValid();
                                        }
                                        this.mIsNavValidResponse =
                                            new IsNavValidResponseData(
                                                Commands.DoIsNavigationValid, navCommand, isValid);
                                    }
                                    this.mSaveOnly = false;
                                }
                                break;
                            case Commands.DoSave:
                                {
                                    // Do nothing. The information will be saved since the page was posted.
                                }
                                break;
                            case Commands.DoTerminate:
                                {
                                    // end the current activity
                                    try
                                    {
                                        if (this.Session.View == SessionView.Execute)
                                        {
                                            // Keep track of state before calling ProcessNavigationRequests so that we can 
                                            // detect if the call changes it in a way that requires reloading the content frame.
                                            long activityBeforeNavigation = this.Session.CurrentActivityId;
                                            int activityAttemptCount =
                                                this.Session.CurrentActivityDataModel.ActivityAttemptCount;

                                            LearningSession session = this.Session;

                                            if (session.HasCurrentActivity)
                                            {
                                                session.ProcessNavigationRequests();
                                            }

                                            // The activity has changed if...
                                            // ... the session now does not have a current activity (since it did before we did the ProcessNavRequests call, OR
                                            // ... the session's current activity is not activity anymore (since it was before the call), OR
                                            // ... the session's activity id has changed
                                            // ... the session's activity id has not changed, but the attempt count has

                                            if (!session.HasCurrentActivity)
                                            {
                                                this.ActivityHasChanged = true;
                                                this.LoadContentFrame = true;
                                            }
                                            else if ((session.View == SessionView.Execute)
                                                     && (!this.Session.CurrentActivityIsActive))
                                            {
                                                // In Execute view, it started as active or would have thrown an exception.
                                                this.ActivityHasChanged = true;

                                                // do not load content frame, as that causes messages to flash while switching activities
                                                this.LoadContentFrame = false;
                                            }
                                            else if (activityBeforeNavigation != session.CurrentActivityId)
                                            {
                                                // The current activity has changed.
                                                this.ActivityHasChanged = true;
                                                this.LoadContentFrame = true;
                                            }
                                            else if ((activityBeforeNavigation == session.CurrentActivityId)
                                                     &&
                                                     (activityAttemptCount
                                                      != session.CurrentActivityDataModel.ActivityAttemptCount))
                                            {
                                                // The activity has not changed, but the attempt count has.
                                                this.ActivityHasChanged = true;
                                                this.LoadContentFrame = true;
                                            }
                                            else
                                            {
                                                // In all other cases, it has not changed
                                                this.ActivityHasChanged = false;
                                                this.LoadContentFrame = false;
                                            }
                                        }
                                        else if ((this.Session.View == SessionView.Review)
                                                 || (this.Session.View == SessionView.RandomAccess))
                                        {
                                            // The activity has changed simply by calling this. This allows the client RTE to reinitialize and behave 
                                            // as if navigation has happened.
                                            this.ActivityHasChanged = true;
                                            this.LoadContentFrame = true;
                                        }
                                    }
                                    catch (SequencingException ex)
                                    {
                                        this.WriteError(
                                            ResHelper.GetMessage(
                                                Localization.GetMessage("HID_TerminateFailed"),
                                                HttpUtility.HtmlEncode(ex.Message)));
                                    }
                                    this.mSaveOnly = false;
                                }
                                break;
                            case Commands.DoSubmit:
                                {
                                    // Submit the attempt -- meaning, do an ExitAll
                                    if (this.Session.View == SessionView.Execute)
                                    {
                                        if (this.Session.HasCurrentActivity)
                                        {
                                            ProcessNavigationRequests(this.Session);
                                        }
                                        this.Session.Exit();
                                    }
                                    else if (this.Session.View == SessionView.RandomAccess)
                                    {
                                        // This may also be a request to end grading (for SLK), in which case 
                                        // we just set a flag, get the right strings to display and call it done
                                        this.SessionIsEnded = true;
                                        processSessionEnd(
                                            this.Session, ref this.mSessionEndedMsgTitle, ref this.mSessionEndedMsg);
                                    }

                                    this.ActivityHasChanged = true;
                                    this.LoadContentFrame = true;
                                    this.mSaveOnly = false;
                                }
                                break;
                        }
                    }
                }
                else
                {
                    // Is this the first page load, as part of frameset? (Some hidden values are only rendered in this case.)
                    string param = this.Request.QueryString[FramesetQueryParameter.Init];
                    if (!string.IsNullOrEmpty(param))
                    {
                        this.mIsFramesetInitialization = true;
                    }
                }

                // If this was not simply a save operation and there is no current activity, then display a message
                // asking the user to select one.
                if (!this.mSaveOnly && !this.Session.HasCurrentActivity)
                {
                    this.RegisterError(
                        this.GetFramesetMsg(FramesetStringId.SelectActivityTitleHtml),
                        this.GetFramesetMsg(FramesetStringId.SelectActivityMessageHtml),
                        true);
                }

                // In Execute view, ProcessSessionEnd may write to the database and change state of data related to the attempt.
                // Therefore, session changes must be written in the same transation as the session end changes.
                TransactionOptions transactionOptions = new TransactionOptions();
                transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.Serializable;
                using (LearningStoreTransactionScope scope = new LearningStoreTransactionScope(transactionOptions))
                {
                    if (!this.SessionIsReadOnly)
                    {
                        // Save all changes
                        this.Session.CommitChanges();
                    }

                    if (this.Session.View == SessionView.Execute)
                    {
                        // The rollup and/or sequencing process may have changed the state of the attempt. If so, there are some cases
                        // that cannot continue so show an error message. Allow the application to process this, since the messages are 
                        // different.
                        processSessionEnd(this.Session, ref this.mSessionEndedMsgTitle, ref this.mSessionEndedMsg);
                    }

                    // finish the transaction
                    scope.Complete();
                }

                this.InitHiddenControlInfo();

                this.mPageLoadSuccessful = true;
            }
            catch (ThreadAbortException)
            {
                // Do nothing -- thread is leaving.
                throw;
            }
            catch (Exception)
            {
                this.mPageLoadSuccessful = false;
                throw;
            }
        }

        /// <summary>
        /// Gets the list of hidden controls that will be rendered on the page.
        /// </summary>
        public Collection<HiddenControlInfo> HiddenControls
        {
            get
            {
                if (this.mHiddenControlInfos == null)
                {
                    this.mHiddenControlInfos = new Collection<HiddenControlInfo>(new List<HiddenControlInfo>(50));
                }

                return this.mHiddenControlInfos;
            }
        }

        /// <summary>
        /// Initialize information for the hidden controls. This sets up the information to create hidden fields in the form
        /// and to update the framesetMgr on page load.
        /// </summary>
        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        // it's not worth changing this now
        private void InitHiddenControlInfo()
        {
            HiddenControlInfo hiddenCtrlInfo;
            Collection<HiddenControlInfo> hiddenControlInfos = this.HiddenControls;
            StringBuilder sb;

            // If the session is attempt-based, then write attempt information
            if (this.Session != null)
            {
                hiddenCtrlInfo = new HiddenControlInfo();
                hiddenCtrlInfo.Id = new PlainTextString(HiddenFieldNames.AttemptId);
                hiddenCtrlInfo.Value = new PlainTextString(FramesetUtil.GetString(this.Session.AttemptId));
                hiddenCtrlInfo.FrameManagerInitializationScript =
                    new JScriptString(
                        ResHelper.FormatInvariant(
                            "frameMgr.SetAttemptId(document.getElementById({0}).value);",
                            JScriptString.QuoteString(HiddenFieldNames.AttemptId, false)));

                hiddenControlInfos.Add(hiddenCtrlInfo);
            }

            // If the session has ended (that is, is suspended, completed or abandoned), then just 
            // tell the framesetmgr and return. Nothing else is required on the client.
            if (this.SessionIsEnded)
            {
                hiddenCtrlInfo = new HiddenControlInfo();
                hiddenCtrlInfo.Id = null; // no data to save
                hiddenCtrlInfo.Value = null;
                hiddenCtrlInfo.FrameManagerInitializationScript =
                    new JScriptString(
                        ResHelper.Format(
                            "frameMgr.TrainingComplete({0}, {1});",
                            JScriptString.QuoteString(this.mSessionEndedMsgTitle, false),
                            JScriptString.QuoteString(this.mSessionEndedMsg, false)));
                hiddenControlInfos.Add(hiddenCtrlInfo);

                hiddenCtrlInfo = new HiddenControlInfo();
                hiddenCtrlInfo.Id = null; // no data to save
                hiddenCtrlInfo.Value = null;
                hiddenCtrlInfo.FrameManagerInitializationScript =
                    new JScriptString(ResHelper.Format("frameMgr.ShowStatisticResults();"));
                hiddenControlInfos.Add(hiddenCtrlInfo);

                return;
            }

            // Write view to display. 
            hiddenCtrlInfo = new HiddenControlInfo();
            hiddenCtrlInfo.Id = new PlainTextString(HiddenFieldNames.View);
            hiddenCtrlInfo.Value = new PlainTextString(FramesetUtil.GetString(this.Session.View));
            hiddenCtrlInfo.FrameManagerInitializationScript =
                new JScriptString(
                    ResHelper.FormatInvariant(
                        "frameMgr.SetView(document.getElementById({0}).value);",
                        JScriptString.QuoteString(HiddenFieldNames.View, false)));

            hiddenControlInfos.Add(hiddenCtrlInfo);

            // Write frame and form to post. They depend on whether this is LRM content or SCORM content. If the submit 
            // page is being displayed, it is always this hidden frame that is posted.
            PlainTextString frameName;
            JScriptString postableFormScript;

            hiddenCtrlInfo = new HiddenControlInfo();
            hiddenCtrlInfo.Id = new PlainTextString(HiddenFieldNames.PostFrame);
            // Post the content frame on the next post if LRM content is being displayed.
            if (!this.mSaveOnly && !this.SubmitPageDisplayed && !this.HasError
                && (this.Session.HasCurrentActivity && (this.Session.CurrentActivityResourceType == ResourceType.Lrm)))
            {
                frameName = new PlainTextString("frameContent");
                postableFormScript =
                    new JScriptString("frameMgr.SetPostableForm(GetContentFrame().contentWindow.document.forms[0]);");
            }
            else
            {
                // Post hidden frame if there is no current activity, if the current activity is not LRM content, or 
                // if there is an error being displayed. This may happen if the current 
                // activity was exited or suspended and a new activity was not automagically determined.
                frameName = new PlainTextString("frameHidden");
                postableFormScript = new JScriptString("frameMgr.SetPostableForm(document.forms[0]);");
            }
            hiddenCtrlInfo.Value = frameName;
            hiddenCtrlInfo.FrameManagerInitializationScript =
                new JScriptString(
                    ResHelper.FormatInvariant(
                        "frameMgr.SetPostFrame(document.getElementById({0}).value);",
                        JScriptString.QuoteString(HiddenFieldNames.PostFrame, false)));

            hiddenControlInfos.Add(hiddenCtrlInfo);

            // Set postable form. 
            hiddenCtrlInfo = new HiddenControlInfo();
            hiddenCtrlInfo.FrameManagerInitializationScript = postableFormScript;
            hiddenControlInfos.Add(hiddenCtrlInfo);

            // If a new activity has been identified, then instruct frameMgr to reinitialize the RTE. 
            // BE CAREFUL to do this before setting any other data related to the rte! 
            if (this.ActivityHasChanged && !this.SubmitPageDisplayed)
            {
                hiddenCtrlInfo = new HiddenControlInfo();
                hiddenCtrlInfo.Id = null; // No need to save data. Just write the script command
                hiddenCtrlInfo.Value = null;
                string initNewActivity = "false";
                if (this.Session.HasCurrentActivity)
                {
                    initNewActivity = (this.CurrentActivityRequiresRte ? "true" : "false");
                }
                hiddenCtrlInfo.FrameManagerInitializationScript =
                    new JScriptString(ResHelper.FormatInvariant("frameMgr.InitNewActivity( {0} );", initNewActivity));
                hiddenControlInfos.Add(hiddenCtrlInfo);
            }

            // Write the current activity Id if it has changed. 
            // Write "SUBMIT" if the submit page is being shown. Otherwise, write -1 if there isn't a current activity.
            hiddenCtrlInfo = new HiddenControlInfo();
            hiddenCtrlInfo.Id = new PlainTextString(HiddenFieldNames.ActivityId);
            PlainTextString setValue; // the value to set in the frameMgr for the current activity id.
            if (this.SubmitPageDisplayed)
            {
                setValue = new PlainTextString(SubmitId);
            }
            else
            {
                // Only set the actual activity id if this is the first rendering or if it has changed in this rendering,
                // and if there is a current activity.
                if (this.mIsFramesetInitialization || this.ActivityHasChanged)
                {
                    setValue =
                        new PlainTextString(
                            this.Session.HasCurrentActivity
                                ? FramesetUtil.GetStringInvariant(this.Session.CurrentActivityId)
                                : "-1");
                }
                else
                {
                    setValue = new PlainTextString("-1");
                }
            }
            // The value of the field is always the current activity id. The value the frameMgr gets is the value of the TOC element.
            hiddenCtrlInfo.Value =
                new PlainTextString(
                    this.Session.HasCurrentActivity
                        ? FramesetUtil.GetStringInvariant(this.Session.CurrentActivityId)
                        : "-1");
            hiddenCtrlInfo.FrameManagerInitializationScript =
                new JScriptString(
                    ResHelper.FormatInvariant(
                        "frameMgr.SetActivityId({0});", JScriptString.QuoteString(setValue, false)));
            hiddenControlInfos.Add(hiddenCtrlInfo);

            // Write the navigation control state. Format of the control state is a series of T (to show) or F (to hide)
            // values, separated by semi-colons. The order of controls is: 
            // showNext, showPrevious, showAbandon, showExit, showSave
            hiddenCtrlInfo = new HiddenControlInfo();
            // Issue: What is the hidden field used for? 
            hiddenCtrlInfo.Id = new PlainTextString(HiddenFieldNames.ShowUI);
            sb = new StringBuilder(10);
            sb.Append((this.Session.ShowNext) ? "T" : "F");
            sb.Append(";");
            sb.Append((this.Session.ShowPrevious) ? "T" : "F");
            sb.Append(";");
            sb.Append((this.Session.ShowAbandon) ? "T" : "F");
            sb.Append(";");
            sb.Append((this.Session.ShowExit) ? "T" : "F");
            sb.Append(";");
            sb.Append((this.Session.ShowSave) ? "T" : "F");
            sb.Append(";");
            hiddenCtrlInfo.Value = new PlainTextString(sb.ToString());
            sb = new StringBuilder(1000);
            sb.Append(string.Empty);
            if (this.SubmitPageDisplayed)
            {
                // If the submit page is being displayed, don't show UI elements
                sb.Append(
                    ResHelper.FormatInvariant(
                        "frameMgr.SetNavVisibility( {0}, {1}, {2}, {3}, {4});",
                        ("false"),
                        // showNext
                        ("false"),
                        // showPrevious
                        ("false"),
                        // showAbandon
                        ("false"),
                        // showExit
                        ("false")));
                        // showSave
            }
            else
            {
                sb.Append(
                    ResHelper.FormatInvariant(
                        "frameMgr.SetNavVisibility( {0}, {1}, {2}, {3}, {4});",
                        (this.Session.ShowNext ? "true" : "false"),
                        (this.Session.ShowPrevious ? "true" : "false"),
                        (this.Session.ShowAbandon ? "true" : "false"),
                        (this.Session.ShowExit ? "true" : "false"),
                        (this.Session.ShowSave ? "true" : "false")));
            }
            hiddenCtrlInfo.FrameManagerInitializationScript = new JScriptString(sb.ToString());
            hiddenControlInfos.Add(hiddenCtrlInfo);

            // If there was an error, write it to the client. Note that if the submit page is being rendered, this code 
            // will execute, as it appears in the same form as an error message.
            if (!string.IsNullOrEmpty(this.ErrorMessage))
            {
                hiddenCtrlInfo = new HiddenControlInfo();
                hiddenCtrlInfo.Id = new PlainTextString(HiddenFieldNames.ErrorMessage);
                hiddenCtrlInfo.Value = this.ErrorMessage;
                if (string.IsNullOrEmpty(this.ErrorTitle))
                {
                    hiddenCtrlInfo.FrameManagerInitializationScript =
                        new JScriptString(
                            ResHelper.Format(
                                "frameMgr.SetErrorMessage(document.getElementById({0}).value);",
                                JScriptString.QuoteString(HiddenFieldNames.ErrorMessage, false)));
                }
                else
                {
                    hiddenCtrlInfo.FrameManagerInitializationScript =
                        new JScriptString(
                            ResHelper.Format(
                                "frameMgr.SetErrorMessage(document.getElementById({0}).value, {1}, {2});",
                                JScriptString.QuoteString(HiddenFieldNames.ErrorMessage, false),
                                JScriptString.QuoteString(this.ErrorTitle, false),
                                this.ErrorAsInfo ? "true" : "false"));
                }
                hiddenControlInfos.Add(hiddenCtrlInfo);
            }

            // If this is the first time rendering the frameset, need to write initialization information.
            if (this.mIsFramesetInitialization)
            {
                hiddenCtrlInfo = new HiddenControlInfo();
                hiddenCtrlInfo.Id = new PlainTextString(HiddenFieldNames.Title);
                hiddenCtrlInfo.Value = this.GetSessionTitle(this.Session).ToHtmlString().ToString();
                hiddenCtrlInfo.FrameManagerInitializationScript =
                    new JScriptString(
                        ResHelper.Format(
                            "frameMgr.SetTitle(document.getElementById({0}).value);",
                            JScriptString.QuoteString(HiddenFieldNames.Title, false)));

                hiddenControlInfos.Add(hiddenCtrlInfo);
            }
            else
            {
                // Only update the toc when this is not the first rendering of the frameset. (The first time, the toc page itself
                // will get it correct.
                hiddenCtrlInfo = new HiddenControlInfo();
                hiddenCtrlInfo.Id = new PlainTextString(HiddenFieldNames.TocState);
                hiddenCtrlInfo.Value = new PlainTextString(this.GetTocStates());
                hiddenCtrlInfo.FrameManagerInitializationScript =
                    new JScriptString(
                        ResHelper.FormatInvariant(
                            "frameMgr.SetTocNodes(document.getElementById({0}).value);",
                            JScriptString.QuoteString(HiddenFieldNames.TocState, false)));

                hiddenControlInfos.Add(hiddenCtrlInfo);
            }

            // Write content href value (value to GET into content frame) -- only if it's required
            hiddenCtrlInfo = new HiddenControlInfo();
            hiddenCtrlInfo.Id = new PlainTextString(HiddenFieldNames.ContentHref);
            sb = new StringBuilder(4096);

            // If we need to load the content frame because the current activity changed, or we need to render the content 
            // frame URL because there was an error, then figure out the content frame URL.

            if (this.LoadContentFrame || this.HasError)
            {
                // The activity has changed, so find the new Url for the content frame. 
                if (this.Session.HasCurrentActivity)
                {
                    sb.Append(this.GetContentFrameUrl());
                }
            }
            hiddenCtrlInfo.Value = new PlainTextString(new UrlString(sb.ToString()).ToAscii());
            hiddenCtrlInfo.FrameManagerInitializationScript =
                new JScriptString(
                    ResHelper.Format(
                        "frameMgr.SetContentFrameUrl(document.getElementById({0}).value);",
                        JScriptString.QuoteString(HiddenFieldNames.ContentHref, false)));

            hiddenControlInfos.Add(hiddenCtrlInfo);

            // Write the data model information, only if the current activity requires it.
            if (!this.SubmitPageDisplayed && this.CurrentActivityRequiresRte)
            {
                // There are 3 controls to update: the data model values and the map between n and id for interactions and objectives
                RteDataModelConverter converter = RteDataModelConverter.Create(this.Session);

                DataModelValues dataModelValues = converter.GetDataModelValues(this.FormatDataModelValueForClient);

                hiddenCtrlInfo = new HiddenControlInfo();
                hiddenCtrlInfo.Id = new PlainTextString(HiddenFieldNames.ObjectiveIdMap);
                hiddenCtrlInfo.Value = dataModelValues.ObjectiveIdMap;
                hiddenCtrlInfo.FrameManagerInitializationScript = null;
                hiddenControlInfos.Add(hiddenCtrlInfo);

                hiddenCtrlInfo = new HiddenControlInfo();
                hiddenCtrlInfo.Id = new PlainTextString(HiddenFieldNames.DataModel);
                hiddenCtrlInfo.Value = dataModelValues.Values;
                StringBuilder initCommand = new StringBuilder(1000);
                initCommand.AppendLine(
                    ResHelper.Format(
                        "var hidDM = document.getElementById({0});",
                        JScriptString.QuoteString(HiddenFieldNames.DataModel, false)));
                initCommand.AppendLine(
                    ResHelper.Format(
                        "var hidObjectiveMap = document.getElementById({0});",
                        JScriptString.QuoteString(HiddenFieldNames.ObjectiveIdMap, false)));
                initCommand.Append("frameMgr.InitDataModelValues(hidDM.value, hidObjectiveMap.value);");
                initCommand.AppendFormat("hidDM.value = {0};", JScriptString.QuoteString(string.Empty, false));
                initCommand.AppendFormat("hidObjectiveMap.value = {0};", JScriptString.QuoteString(string.Empty, false));
                hiddenCtrlInfo.FrameManagerInitializationScript = new JScriptString(initCommand.ToString());
                hiddenControlInfos.Add(hiddenCtrlInfo);
            }

            // If there was an IsMove*Valid request, send the response
            if (this.mIsNavValidResponse != null)
            {
                hiddenCtrlInfo = new HiddenControlInfo();
                hiddenCtrlInfo.Id = new PlainTextString(HiddenFieldNames.IsNavigationValidResponse);
                hiddenCtrlInfo.Value = this.mIsNavValidResponse.ClientFieldResponse;
                hiddenCtrlInfo.FrameManagerInitializationScript =
                    new JScriptString(
                        ResHelper.FormatInvariant(
                            "frameMgr.SetIsNavigationValid(document.getElementById({0}).value);",
                            JScriptString.QuoteString(HiddenFieldNames.IsNavigationValidResponse, false)));

                hiddenControlInfos.Add(hiddenCtrlInfo);
            }

            if (this.mIsPostedPage)
            {
                // Set PostIsComplete. THIS MUST BE THE LAST VALUE SET! 
                hiddenCtrlInfo = new HiddenControlInfo();
                hiddenCtrlInfo.FrameManagerInitializationScript = new JScriptString("frameMgr.PostIsComplete();");
                hiddenControlInfos.Add(hiddenCtrlInfo);
            }
        }

        /// <summary>
        /// If true, the content frame needs to be reloaded.
        /// </summary>
        private bool LoadContentFrame
        {
            get
            {
                return this.mLoadContentFrame;
            }
            set
            {
                this.mLoadContentFrame = value;
            }
        }

        /// <summary>
        /// This is a callback function for the RteConverter class. This method is called when a name / value pair 
        /// needs to be added to the buffer <paramref name="sb"/> for being sent to the client.
        /// </summary>
        /// <param name="sb">Buffer to write to.</param>
        /// <param name="name">The name of the data model value. This cannot be null or empty.</param>
        /// <param name="value">The value of the data model eleemnt. This can be null or empty.</param>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")] // They are validated
        public void FormatDataModelValueForClient(StringBuilder sb, string name, string value)
        {
            FramesetUtil.ValidateNonNullParameter("name", name);

            // Pattern to encode is name@Evalue@N (think of @E as 'equals', @N as 'new line').

            if (string.IsNullOrEmpty(value))
            {
                value = string.Empty;
            }
            else
            {
                value = value.Replace("@", "@A").Replace(">", "@G").Replace("<", "@L");
            }

            // Replace any @ characters with @A in name and value (eg, me@mycompany.com becomes me@Amycompany.com)
            // Replace any < with @L, any > with @G, to avoid any form field data issues
            name = name.Replace("@", "@A").Replace(">", "@G").Replace("<", "@L");

            // Concatenate name and value, using @E between them and @N at the end
            sb.Append(name);
            sb.Append("@E");
            sb.Append(value);
            sb.Append("@N");
        }

        // Process the string returned from the client with updated data model values. It's ok if the 
        // dataModelValues have no information -- the method will return without error.
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        // all exceptions are caught and put into log
        private void ProcessDataModelValues(string dataModelValues, string objectivesIdMap)
        {
            // Values are never set in Review view
            if (this.Session.View == SessionView.Review)
            {
                return;
            }

            // If there's no current activity, ignore the data
            if (!this.Session.HasCurrentActivity)
            {
                return;
            }

            // If there is no data, don't do any processing
            if (string.IsNullOrEmpty(dataModelValues))
            {
                return;
            }

            // If the current activity is not a SCO, don't save any data model values.
            if (this.Session.CurrentActivityResourceType != ResourceType.Sco)
            {
                return;
            }

            RteDataModelConverter converter = RteDataModelConverter.Create(this.Session);
            string[] nameValuePairs;
            // If any conversions or SetValue calls fail, keep all errors and display them at once.
            StringBuilder errors = new StringBuilder(100);

            // Read the interactions map and initialize converter with it.
            InitializeIndexerMapping(converter.ObjectiveIndexer, objectivesIdMap, errors);

            // Process data model values and set the values in the session
            dataModelValues = dataModelValues.Replace("@G", ">").Replace("@L", "<").Replace("@A", "@");
            nameValuePairs = dataModelValues.Split(new[] { "@N" }, StringSplitOptions.None);

            // Each nvPair string in the array is of the form <name>@E<value>
            foreach (string nvPair in nameValuePairs)
            {
                if (string.IsNullOrEmpty(nvPair))
                {
                    continue;
                }

                string[] elements = nvPair.Split(new[] { "@E" }, StringSplitOptions.None);
                if (elements.Length != 2)
                {
                    continue;
                }

                try
                {
                    converter.SetValue(new PlainTextString(elements[0]), new PlainTextString(elements[1]));
                }
                catch (Exception e)
                {
                    LogDataModelError(
                        errors,
                        Localization.GetMessage("CONV_SetValueException"),
                        elements[0],
                        TruncateMessage(elements[1]),
                        e.Message);
                }
            }

            Collection<string> finalSetValueErrors;
            try
            {
                // Let the converter add any pending objects to the data model. It may return a list of errors.
                finalSetValueErrors = converter.FinishSetValue();

                foreach (string error in finalSetValueErrors)
                {
                    LogDataModelError(errors, null, error);
                }
            }
            catch (Exception e)
            {
                LogDataModelError(errors, null, e.Message);
            }

            // If there were errors in the process, write them to the response
            if (errors.Length > 0)
            {
                EndLogDataModelErrors(errors);
                this.WriteError(errors.ToString(), true);
            }
        }

        // If the message exceeds 100 characters, truncate it and add ellipsis.
        private static string TruncateMessage(string message)
        {
            if (message.Length > 100)
            {
                return ResHelper.Format("{0}...", message.Substring(0, 100));
            }
            return message;
        }

        /// <summary>
        /// Helper function to put error message into the message log. 
        /// </summary>
        /// <param name="errorLog">The log to write the error to.</param>
        /// <param name="messageResource">This message id to load. If this is null, messageParts[0] is displayed. Text (not html) format.</param>
        /// <param name="messageParts">The message parts to fill in the tokens in the string loaded by messageResource. Text (not html) format.</param>
        private static void LogDataModelError(
            StringBuilder errorLog, string messageResource, params string[] messageParts)
        {
            // If this is the first error, add a header to the message.
            if (errorLog.Length == 0)
            {
                errorLog.AppendLine(
                    ResHelper.GetMessage(Localization.GetMessage("HID_SetValueMappingFailedHtml")));
                errorLog.AppendLine("<br><br><ul> ");
            }
            string messageHtml;

            if (messageResource == null)
            {
                messageHtml = HttpUtility.HtmlEncode(messageParts[0]);
            }
            else
            {
                messageHtml = HttpUtility.HtmlEncode(ResHelper.GetMessage(messageResource, messageParts));
            }
            errorLog.AppendLine(ResHelper.Format("<li style=\"margin-top: 10;\">{0}</li>", messageHtml));
        }

        // Helper function to end the list of errors in the data model
        private static void EndLogDataModelErrors(StringBuilder errorLog)
        {
            errorLog.AppendLine("</ul>");
        }

        // Given a string containing a list of mappings between index ('n') and ids, clear the list then 
        // add them to the list of ids, indexed by 'n'.
        // The format of idMapValues is index@Eid@N, repeated. 
        private static void InitializeIndexerMapping(
            List<string> indexerInfos, string idMapValues, StringBuilder errorLog)
        {
            // if there are no mapping values, just create an empty list
            if (string.IsNullOrEmpty(idMapValues))
            {
                return;
            }

            // Make sure list is empty.
            indexerInfos.Clear();

            string[] indexValuePairs;

            idMapValues = idMapValues.Replace("@A", "@");
            indexValuePairs = idMapValues.Split(new[] { "@N" }, StringSplitOptions.None);

            // Each nvPair string in the array is of the form <name>@E<value>
            foreach (string nvPair in indexValuePairs)
            {
                if (string.IsNullOrEmpty(nvPair))
                {
                    continue;
                }

                string[] elements = nvPair.Split(new[] { "@E" }, StringSplitOptions.None);
                if (elements.Length != 2)
                {
                    LogDataModelError(
                        errorLog, Localization.GetMessage("HID_SetValuePostedDataInvalid"));
                    continue;
                }

                try
                {
                    int index = XmlConvert.ToInt32(elements[0]);
                    indexerInfos.Insert(index, elements[1]);
                }
                catch (FormatException e)
                {
                    // if elements[0] is wrong type
                    LogDataModelError(errorLog, e.Message);
                }
                catch (ArgumentOutOfRangeException e)
                {
                    // if index is not valid
                    LogDataModelError(errorLog, e.Message);
                }
            }
        }

        #region called from aspx

        /// <summary>
        /// Render all hidden controls on to the page. 
        /// </summary>
        public void WriteHiddenControls()
        {
            // If the page did not load successfully, don't do anything
            if (!this.mPageLoadSuccessful)
            {
                return;
            }

            // Error situation. Nothing to write.
            if (this.Session == null)
            {
                return;
            }

            HtmlStringWriter sw = new HtmlStringWriter(this.Response.Output);

            foreach (HiddenControlInfo ctrlInfo in this.mHiddenControlInfos)
            {
                if ((ctrlInfo.Id != null) && (ctrlInfo.Value != null))
                {
                    WriteHiddenControl(sw, ctrlInfo.Id, ctrlInfo.Value);
                    this.Response.Write("\r\n");
                }
            }
            sw.EndRender();
        }

        /// <summary>
        /// Write the script to initialize the frameset manager.
        /// </summary>
        public void WriteFrameMgrInit()
        {
            // If the page did not load successfully, just make sure the error string gets written.
            if (!this.mPageLoadSuccessful)
            {
                // If there is an error, then write it. Basically, there are cases where the page did not 
                // load enough to write out the message earlier. 
                if (!string.IsNullOrEmpty(this.ErrorMessage))
                {
                    JScriptString js =
                        new JScriptString(
                            ResHelper.Format(
                                "frameMgr.SetErrorMessage({0});", JScriptString.QuoteString(this.ErrorMessage, false)));
                    this.Response.Write(js.ToString());
                    this.Response.Write("\r\n");
                }

                if (this.mIsPostedPage)
                {
                    // Clear any state waiting for additional information
                    JScriptString js = new JScriptString("frameMgr.WaitForContentCompleted(0);");
                    this.Response.Write(js.ToString());
                    this.Response.Write("\r\n");
                }

                return;
            }

            foreach (HiddenControlInfo ctrlInfo in this.mHiddenControlInfos)
            {
                // Some cases do not have code to initialize frameMgr
                if (ctrlInfo.FrameManagerInitializationScript != null)
                {
                    this.Response.Write(ctrlInfo.FrameManagerInitializationScript.ToString());
                    this.Response.Write("\r\n");
                }
            }
        }

        /// <summary>
        /// Returns true if the data model is being written as the page is being rendered.
        /// </summary>
        public bool WriteDataModel
        {
            get
            {
                // If the page did not load successfully, don't do anything
                if (!this.mPageLoadSuccessful)
                {
                    return false;
                }

                // Only write data model on a GET request if this is an initialization process 
                // and the current activity is a SCO.
                return ((this.Session.CurrentActivityResourceType == ResourceType.Sco)
                        && (this.mIsFramesetInitialization));
            }
        }

        #endregion  // called from aspx

        /// <summary>
        /// Write a hidden control to the string writer. The controlId shows up in id= and name= attributes. The 
        /// value is the Value of the control.
        /// </summary>
        private static void WriteHiddenControl(HtmlStringWriter sw, PlainTextString controlId, PlainTextString value)
        {
            sw.AddAttribute(HtmlTextWriterAttribute.Type, new HtmlString("hidden"));
            sw.AddAttribute(HtmlTextWriterAttribute.Id, controlId.ToHtmlString());
            sw.AddAttribute(HtmlTextWriterAttribute.Name, controlId.ToHtmlString());
            sw.AddAttribute(HtmlTextWriterAttribute.Value, value.ToHtmlString());
            sw.RenderBeginTag(HtmlTextWriterTag.Input);
            sw.RenderEndTag();
        }

        // Small helper class to save information about the pending response to an IsMove*Valid request.
        internal class IsNavValidResponseData
        {
            private string mCommand;

            private string mCommandData;

            private bool mResponse;

            public IsNavValidResponseData(string command, string commandData, bool response)
            {
                this.mCommand = command;
                this.mCommandData = commandData;
                this.mResponse = response;
            }

            // Gets the string to return to the client in a hidden field to convey the response to the IsMove*Valid request.
            public string ClientFieldResponse
            {
                // The format of field is:
                // command@Evalue@N
                // The value will be "true" or "false".
                // In the case of a choice command, the command will be of the form "C,strActivityId" and strActivityId will 
                // have any @ values replaced by @A.
                get
                {
                    string command;
                    if (this.mCommand == Commands.DoIsChoiceValid)
                    {
                        string commandData = this.mCommandData.Replace("@", "@A");
                        command = ResHelper.FormatInvariant("C,{0}", commandData);
                    }
                    else
                    {
                        // For IsMoveNextValid or IsMovePreviousValid, the command data determines whether it's N or P.
                        command = this.mCommandData;
                    }
                    return ResHelper.FormatInvariant("{0}@E{1}@N", command, this.mResponse ? "true" : "false");
                }
            }
        }
    }

    /// <summary>A hidden control.</summary>
    public class HiddenControlInfo
    {
        private PlainTextString mId;

        private PlainTextString mValue;

        private JScriptString mFrameManagerInitializationScript;

        /// <summary>Initializes a new instance of <see cref="HiddenControlInfo"/>.</summary>
        public HiddenControlInfo()
        {
        }

        /// <summary>The control's Id.</summary>
        public PlainTextString Id
        {
            get
            {
                return this.mId;
            }
            set
            {
                this.mId = value;
            }
        }

        /// <summary>The control's value.</summary>
        /// <value></value>
        public PlainTextString Value
        {
            get
            {
                return this.mValue;
            }
            set
            {
                this.mValue = value;
            }
        }

        /// <summary>
        /// Script to inject into the onload handler to initialize frameManager with this information.
        /// </summary>
        public JScriptString FrameManagerInitializationScript
        {
            get
            {
                return this.mFrameManagerInitializationScript;
            }
            set
            {
                this.mFrameManagerInitializationScript = value;
            }
        }
    }
}
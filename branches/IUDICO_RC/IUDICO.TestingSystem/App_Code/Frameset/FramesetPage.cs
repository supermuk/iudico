// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="FramesetPage.cs">
//   
// </copyright>
// 
// --------------------------------------------------------------------------------------------------------------------

// using Resources;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using IUDICO.TestingSystem;

using Microsoft.LearningComponents;
using Microsoft.LearningComponents.Storage;

namespace Microsoft.LearningComponents.Frameset
{
    using IUDICO.Common;

    /// <summary>
    /// Represents a page within the Basic Web Player frameset. This is the base class of all frameset pages. This class is not shared
    /// between SLK and BWP framesets.
    /// </summary>
    public class BwpFramesetPage : PageHelper
    {
        private FramesetPageHelper mHelper;

        public string GetMessage(FramesetStringId stringId)
        {
            switch (stringId)
            {
                case FramesetStringId.MoveToNextFailedHtml:
                    return Localization.GetMessage("HID_MoveNextFailedHtml");
                case FramesetStringId.MoveToPreviousFailedHtml:
                    return Localization.GetMessage("HID_MovePreviousFailedHtml");
                case FramesetStringId.MoveToActivityFailedHtml:
                    return Localization.GetMessage("HID_MoveToActivityFailedHtml");
                case FramesetStringId.SubmitPageTitleHtml:
                    return Localization.GetMessage("HID_SubmitPageTitleHtml");
                case FramesetStringId.SubmitPageMessageHtml:
                    return Localization.GetMessage("HID_SubmitPageMessageHtml");
                case FramesetStringId.SubmitPageMessageNoCurrentActivityHtml:
                    return Localization.GetMessage("HID_SubmitPageMessageNoCurrentActivityHtml");
                case FramesetStringId.SubmitPageSaveButtonHtml:
                    return Localization.GetMessage("POST_SubmitHtml");
                case FramesetStringId.CannotDisplayContentTitle:
                    return Localization.GetMessage("FRM_CannotDisplayContentTitle");
                case FramesetStringId.SessionIsNotActiveMsg:
                    return Localization.GetMessage("FRM_SessionIsNotActiveMsg");
                case FramesetStringId.ActivityIsNotActiveMsg:
                    return Localization.GetMessage("FRM_ActivityIsNotActiveMsg");
                case FramesetStringId.SelectActivityTitleHtml:
                    return Localization.GetMessage("HID_SelectActivityTitleHtml");
                case FramesetStringId.SelectActivityMessageHtml:
                    return Localization.GetMessage("HID_SelectActivityMsgHtml");
                default:
                    throw new InvalidOperationException(
                        Localization.GetMessage("FRM_ResourceNotFound"));
            }
        }

        public BwpFramesetPage()
            : base()
        {
        }

        private FramesetPageHelper FramesetHelper
        {
            get
            {
                if (this.mHelper == null)
                {
                    this.mHelper = new FramesetPageHelper(this.Request);
                }
                return this.mHelper;
            }
        }

        /// <summary>
        /// Delegate implementation to allow the frameset to take action on a session view request. This allows SLK and 
        /// BWP to have different behavior and messages about which requests are not valid.
        /// </summary>
        public bool ProcessViewRequest(SessionView view, LearningSession session)
        {
            this.Completed = false;
            switch (view)
            {
                case SessionView.Execute:
                    {
                        StoredLearningSession slsSession = session as StoredLearningSession;
                        if (slsSession != null)
                        {
                            if (slsSession.AttemptStatus == AttemptStatus.Completed)
                            {
                                this.RegisterError(
                                    ResHelper.GetMessage(Localization.GetMessage("FRM_InvalidAttemptStatusForViewTitle")),
                                    ResHelper.GetMessage(Localization.GetMessage("FRM_ExecuteViewCompletedSessionMsg")),
                                    false);
                                this.Completed = true;
                                return false;
                            }
                            else if (slsSession.AttemptStatus == AttemptStatus.Abandoned)
                            {
                                this.RegisterError(
                                    ResHelper.GetMessage(Localization.GetMessage("FRM_InvalidAttemptStatusForViewTitle")),
                                    ResHelper.GetMessage(Localization.GetMessage("FRM_ExecuteViewAbandonedSessionMsg")),
                                    false);
                                return false;
                            }
                        }
                    }
                    break;

                case SessionView.Review:
                    // BWP does not provide review view
                    this.RegisterError(
                        ResHelper.GetMessage(Localization.GetMessage("FRM_ViewNotSupportedTitle")),
                        ResHelper.GetMessage(Localization.GetMessage("FRM_ReviewViewNotSupportedMsg")),
                        false);
                    break;

                case SessionView.RandomAccess:
                    this.RegisterError(
                        ResHelper.GetMessage(Localization.GetMessage("FRM_ViewNotSupportedTitle")),
                        ResHelper.GetMessage(Localization.GetMessage("FRM_RAViewNotSupportedMsg")),
                        false);
                    break;
            }
            return true;
        }

        /// <summary>
        /// Register error information to be written to the response object.
        /// Only the first error that is registered is displayed.
        /// </summary>
        /// <param name="shortDescription">A short description (sort of a title) of the problem. Html format.</param>
        /// <param name="message">A longer description of the problem. Html format.</param>
        /// <param name="asInfo">If true, display as information message.</param>
        protected virtual void RegisterError(string shortDescription, string message, bool asInfo)
        {
            this.FramesetHelper.RegisterError(shortDescription, message, asInfo);
        }

        /// <summary>
        /// Clear error information that is waiting to be written.
        /// </summary>
        protected void ClearError()
        {
            this.FramesetHelper.ClearError();
        }

        /// <summary>
        /// Returns true, and the value of the required parameter. If the parameter is not found or has no value,
        /// this method will display the error page.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="value">The value of the parameter. The value should be ignored if false is returned.</param>
        /// <returns>True if the parameter existed.</returns>
        public bool GetRequiredParameter(string name, out string value)
        {
            return this.FramesetHelper.TryGetRequiredParameter(name, out value);
        }

        /// <summary>
        /// Helper function to get the AttemptId query parameter. This method assumes the parameter is required. If 
        /// it does not exist or is not numeric, the error page is shown. This method does not check LearningStore.
        /// </summary>
        /// <param name="isRequired">If true, the error page is shown if the value is not provided.</param>
        /// <param name="attemptId">The attempt id.</param>
        /// <returns>If false, the value did not exist or was not valid. The application should not continue with 
        /// page processing.</returns>
        public bool ProcessAttemptIdParameter(bool showErrorPage, out AttemptItemIdentifier attemptId)
        {
            string attemptParam = null;
            bool isValid = true;

            attemptId = null;

            if (!this.GetRequiredParameter(FramesetQueryParameter.AttemptId, out attemptParam))
            {
                return false;
            }

            // Try converting it to a long value. It must be positive.
            try
            {
                long attemptKey = long.Parse(attemptParam, NumberFormatInfo.InvariantInfo);

                if (attemptKey <= 0)
                {
                    isValid = false;
                }
                else
                {
                    attemptId = new AttemptItemIdentifier(attemptKey);
                }
            }
            catch (FormatException)
            {
                isValid = false;
            }

            if (!isValid && showErrorPage)
            {
                this.RegisterError(
                     ResHelper.GetMessage(Localization.GetMessage("FRM_InvalidParameterTitle"), FramesetQueryParameter.AttemptId),
                     ResHelper.GetMessage(Localization.GetMessage("FRM_InvalidParameterMsg"), FramesetQueryParameter.AttemptId, attemptParam),
                     false);
            }

            return isValid;
        }

        /// <summary>
        /// Helper function to process the View parameter. This method assumes the parameter is required. If it does not 
        /// exist or is not a valid value and showErrorPage=true, the error page is shown and the method returns false. 
        /// If false is returned, the caller should ignore the value of <param name="view"/>.    
        /// </summary>
        public bool ProcessViewParameter(bool showErrorPage, out SessionView view)
        {
            return this.FramesetHelper.TryProcessViewParameter(showErrorPage, out view);
        }

        public static void AppendContentFrameDetails(LearningSession session, StringBuilder sb)
        {
            // The URL for attempt-based content frames is:
            // http://<...basicWebApp>/Content.aspx/<view>/<attemptId>/otherdata/
            // the otherdata depends on the view
            sb.Append(string.Format(CultureInfo.CurrentCulture, "/{0}", Convert.ToInt32(session.View)));

            StoredLearningSession slsSession = session as StoredLearningSession;
            sb.AppendFormat("/{0}", slsSession.AttemptId.GetKey().ToString());
        }

        protected bool HasError
        {
            get
            {
                return this.FramesetHelper.HasError;
            }
        }

        /// <summary>
        /// If true, show the error page instead of the page
        /// </summary>
        public bool ShowError
        {
            get
            {
                return this.FramesetHelper.ShowError;
            }
        }

        /// <summary>
        /// Stores Completion Status
        /// </summary>
        public bool Completed { get; set; }

        /// <summary>
        /// Return the short title of the error.
        /// </summary>
        public string ErrorTitle
        {
            get
            {
                return this.FramesetHelper.ErrorTitle;
            }
        }

        /// <summary>
        /// Return the long(er) error message.
        /// </summary>
        public string ErrorMsg
        {
            get
            {
                return this.FramesetHelper.ErrorMsg;
            }
        }

        public bool ErrorAsInfo
        {
            get
            {
                return this.FramesetHelper.ErrorAsInfo;
            }
        }

        /// <summary>
        /// Return all previously registered error information.
        /// </summary>
        public void GetErrorInfo(out bool hasError, out string errorTitle, out string errorMsg, out bool asInfo)
        {
            hasError = this.HasError;
            errorTitle = this.ErrorTitle;
            errorMsg = this.ErrorMsg;
            asInfo = this.ErrorAsInfo;
        }
    }
}
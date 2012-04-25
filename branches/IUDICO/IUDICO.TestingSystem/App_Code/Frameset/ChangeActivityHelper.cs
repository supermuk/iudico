// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="ChangeActivityHelper.cs">
//   
// </copyright>
// 
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Configuration;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using Microsoft.LearningComponents.Storage;

namespace Microsoft.LearningComponents.Frameset
{
    /// <summary>
    /// Changes the current activity and notifies the frameset.
    /// </summary>
    public class ChangeActivityHelper : PostableFrameHelper
    {
        // technically, this is not postable, but has some of the same requirements
        private SessionView mView;

        private AttemptItemIdentifier mAttemptId;

        private long mActivityId;

        /// <summary>Initializes a new instance of <see cref="ChangeActivityHelper"/>.</summary>
        public ChangeActivityHelper(HttpRequest request, HttpResponse response)
            : base(request, response, null)
        {
        }

        /// <summary>Processes the page.</summary>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly")]
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        public void ProcessPageLoad(
            TryGetViewInfo tryGetViewInfo,
            TryGetAttemptInfo tryGetAttemptInfo,
            TryGetActivityInfo tryGetActivityInfo,
            RegisterError registerError,
            GetErrorInfo getErrorInfo,
            GetFramesetMsg getFramesetMessage)
        {
            this.RegisterError = registerError;
            this.GetErrorInfo = getErrorInfo;
            this.GetFramesetMsg = getFramesetMessage;

            // This page simply causes the frameset to request to move to a different activity. It does not verify that 
            // the user is authorized to do this. If the user is not authorized, the subsequent request will fail.

            if (!tryGetViewInfo(true, out this.mView))
            {
                return;
            }

            if (!tryGetAttemptInfo(true, out this.mAttemptId))
            {
                return;
            }

            if (!tryGetActivityInfo(true, out this.mActivityId))
            {
                return;
            }
        }

        /// <summary>Writes the frame manager initialization javascript.</summary>
        public void WriteFrameMgrInit()
        {
            this.Response.Write(
                ResHelper.Format("frameMgr.SetAttemptId({0});\r\n", FramesetUtil.GetString(this.mAttemptId)));
            this.Response.Write(ResHelper.Format("frameMgr.SetView({0});\r\n", FramesetUtil.GetString(this.mView)));
            this.Response.Write("frameMgr.SetPostFrame(\"frameHidden\");\r\n");
            this.Response.Write("frameMgr.SetPostableForm(GetHiddenFrame().contentWindow.document.forms[0]);\r\n");

            // Tell frameMgr to move to new activity
            this.Response.Write(
                ResHelper.Format(
                    "frameMgr.DoChoice(\"{0}\", true);\r\n", FramesetUtil.GetStringInvariant(this.mActivityId)));
        }

        /// <summary>The html for an error message.</summary>
        public string ErrorMessageHtml
        {
            get
            {
                return this.ErrorMessage;
            }
        }

        /// <summary>The html for an error title.</summary>
        public string ErrorTitleHtml
        {
            get
            {
                return this.ErrorTitle;
            }
        }
    }
}
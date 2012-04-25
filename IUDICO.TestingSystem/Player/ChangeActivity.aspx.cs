// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="ChangeActivity.aspx.cs">
//   
// </copyright>
// 
// --------------------------------------------------------------------------------------------------------------------

// using Resources;

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using IUDICO.TestingSystem;

using Microsoft.LearningComponents.Storage;

namespace Microsoft.LearningComponents.Frameset
{
    // Query parameters on this page:
    // AttemptId = 
    // View = 
    // ActivityId =     // the new activity id
    public partial class Frameset_ChangeActivity : BwpFramesetPage
    {
        private ChangeActivityHelper mHelper;

        private bool mPageLoadSuccessful = true;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.mHelper = new ChangeActivityHelper(this.Request, this.Response);
                this.mHelper.ProcessPageLoad(
                    this.ProcessViewParameter,
                    this.ProcessAttemptIdParameter,
                    this.TryGetActivityId,
                    this.RegisterError,
                    this.GetErrorInfo,
                    this.GetMessage);
                this.mPageLoadSuccessful = (!this.HasError);
            }
            catch (Exception e2)
            {
                this.RegisterError(
                     ResHelper.GetMessage(Localization.GetMessage("FRM_UnknownExceptionTitle")),
                     ResHelper.GetMessage(Localization.GetMessage("FRM_UnknownExceptionMsg"), HttpUtility.HtmlEncode(e2.Message)),
                     false);
                this.mPageLoadSuccessful = false;

                // Clear any existing response information so that the error gets rendered correctly.
                this.Response.Clear();
            }
        }

        // public bool TryGetSessionView(bool showErrorPage, out SessionView view)
        // {
        // string viewParam;

        // // Default value to make compiler happy
        // view = SessionView.Execute;

        // if (!TryGetRequiredParameter(FramesetQueryParameter.View, out viewParam))
        // return false;

        // try
        // {
        // // Get the view enum value
        // view = (SessionView)Enum.Parse(typeof(SessionView), viewParam, true);
        // if ((view < SessionView.Execute) || (view > SessionView.Review))
        // {
        // if (showErrorPage)
        // {
        // RegisterError(ResHelper.GetMessage(FramesetResources.FRM_InvalidParameterTitle, FramesetQueryParameter.View),
        // ResHelper.GetMessage(FramesetResources.FRM_InvalidParameterMsg, FramesetQueryParameter.View, viewParam), false);
        // }
        // return false;
        // }
        // }
        // catch (ArgumentException)
        // {
        // if (showErrorPage)
        // {
        // RegisterError(ResHelper.GetMessage(FramesetResources.FRM_InvalidParameterTitle, FramesetQueryParameter.View),
        // ResHelper.GetMessage(FramesetResources.FRM_InvalidParameterMsg, FramesetQueryParameter.View, viewParam), false);
        // }
        // return false;
        // }
        // return true;
        // }

        public bool TryGetActivityId(bool showErrorPage, out long activityId)
        {
            string activityIdParam = null;
            bool isValid = true;

            activityId = -1;

            if (!this.GetRequiredParameter(FramesetQueryParameter.ActivityId, out activityIdParam))
            {
                return false;
            }

            // Try converting it to a long value. It must be positive.
            try
            {
                long activityIdKey = long.Parse(activityIdParam, NumberFormatInfo.InvariantInfo);

                if (activityIdKey <= 0)
                {
                    isValid = false;
                }
                else
                {
                    activityId = activityIdKey;
                }
            }
            catch (FormatException)
            {
                isValid = false;
            }

            if (!isValid && showErrorPage)
            {
                this.RegisterError(
                     ResHelper.GetMessage(Localization.GetMessage("FRM_InvalidParameterTitle"), FramesetQueryParameter.ActivityId),
                     ResHelper.GetMessage(Localization.GetMessage("FRM_InvalidParameterMsg"), FramesetQueryParameter.ActivityId, activityIdParam),
                     false);
            }

            return isValid;
        }

        // public override bool TryGetAttemptId(bool showErrorPage, out AttemptItemIdentifier attemptId)
        // {
        // string attemptIdParam = null;
        // bool isValid = true;

        // // make compiler happy
        // attemptId = null;

        // if (!TryGetRequiredParameter(FramesetQueryParameter.AttemptId, out attemptIdParam))
        // return false;

        // // Try converting it to a long value. It must be positive.
        // try
        // {
        // long attemptIdKey = long.Parse(attemptIdParam, NumberFormatInfo.InvariantInfo);

        // if (attemptIdKey <= 0)
        // isValid = false;
        // else
        // attemptId = new AttemptItemIdentifier(attemptIdKey);
        // }
        // catch (FormatException)
        // {
        // isValid = false;
        // }

        // if (!isValid && showErrorPage)
        // {
        // RegisterError(ResHelper.GetMessage(FramesetResources.FRM_InvalidParameterTitle, FramesetQueryParameter.AttemptId),
        // ResHelper.GetMessage(FramesetResources.FRM_InvalidParameterMsg, FramesetQueryParameter.AttemptId, attemptIdParam), false);
        // }

        // return isValid;
        // }

        #region called from aspx

        public string ErrorTitleHtml
        {
            get
            {
                return this.mHelper.ErrorTitleHtml;
            }
        }

        public string ErrorMsgHtml
        {
            get
            {
                return this.mHelper.ErrorMessageHtml;
            }
        }

        public static string PleaseWaitHtml
        {
            get
            {
                return ResHelper.GetMessage(IUDICO.TestingSystem.Localization.GetMessage("CON_PleaseWait"));
            }
        }

        public void WriteFrameMgrInit()
        {
            // If the page did not load successfully, then don't write anything
            if (!this.mPageLoadSuccessful)
            {
                return;
            }

            this.mHelper.WriteFrameMgrInit();
        }

        #endregion
    }
}
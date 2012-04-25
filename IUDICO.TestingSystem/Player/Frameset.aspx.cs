// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="Frameset.aspx.cs">
//   
// </copyright>
// 
// --------------------------------------------------------------------------------------------------------------------

// using Resources;

using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace Microsoft.LearningComponents.Frameset
{
    /// <summary>
    /// This is the top-level frameset for display views of a package. 
    /// The URL to this page differs based on the view requested.
    /// Query parameters are:
    /// View: The integer that corresponds to the SessionView value for the session.
    /// AttemptId: The attempt to be displayed. This is required if the view is based on an attempt. It must already
    ///     exist in LearningStore. It must correspond to an attempt in a state that is appropriate for the view. For instance,
    ///     if View=0 (Execute), the attempt must be active.
    /// </summary>
    public partial class Frameset_Frameset : BwpFramesetPage
    {
        private FramesetHelper mFramesetHelper;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.mFramesetHelper = new FramesetHelper();
                this.mFramesetHelper.ProcessPageLoad(
                    this.PStore, this.ProcessViewParameter, this.ProcessAttemptIdParameter, this.ProcessViewRequest);
            }
            catch (System.Threading.ThreadAbortException)
            {
                // Do nothing
            }
            catch (FileNotFoundException)
            {
                this.Response.StatusCode = 404;
                this.Response.StatusDescription = "Not Found";
            }
            catch (System.Web.HttpException)
            {
                // Something wrong with the http connection, so in this case do not set the response
                // headers.
                this.RegisterError(
                    IUDICO.TestingSystem.Localization.GetMessage("FRM_NotAvailableTitleHtml"),
                    IUDICO.TestingSystem.Localization.GetMessage("FRM_NotAvailableHtml"),
                    false);
            }
            catch (Exception)
            {
                // Doesn't matter why.
                this.Response.StatusCode = 500;
                this.Response.StatusDescription = "Internal Server Error";
                this.RegisterError(
                    IUDICO.TestingSystem.Localization.GetMessage("FRM_NotAvailableTitleHtml"),
                    IUDICO.TestingSystem.Localization.GetMessage("FRM_NotAvailableHtml"),
                    false);
            }
        }

        #region Called From Aspx    // the following methods are called from in-place aspx code

        /// <summary>
        /// Gets the URL to the page loaded into the MainFrames frame.
        /// </summary>
        public string MainFramesUrl
        {
            get
            {
                StringBuilder frames = new StringBuilder(4096);
                int view = Convert.ToInt32(this.mFramesetHelper.View);
                frames.Append(
                    string.Format(
                        CultureInfo.CurrentUICulture,
                        "MainFrames.aspx?{0}={1}&",
                        FramesetQueryParameter.View,
                        view.ToString()));
                frames.Append(
                    string.Format(
                        CultureInfo.CurrentUICulture,
                        "{0}={1}",
                        FramesetQueryParameter.AttemptId,
                        this.mFramesetHelper.AttemptId.GetKey()));
                return new UrlString(frames.ToString()).ToAscii();
            }
        }

        /// <summary>
        /// Gets the title for the frameset. The one that goes into the title bar of the browser window.
        /// </summary>
        public string PageTitleHtml
        {
            get
            {
                PlainTextString text =
                    new PlainTextString(ResHelper.GetMessage(IUDICO.TestingSystem.Localization.GetMessage("FRM_Title")));
                HtmlString html = new HtmlString(text);
                return html.ToString();
            }
        }

        /// <summary>
        /// Gets the version of SCORM used in the current attempt.
        /// </summary>
        public string ScormVersionHtml
        {
            get
            {
                return this.mFramesetHelper.ScormVersionHtml;
            }
        }

        /// <summary>
        /// Returns "true" if the Rte is required on the first activity. "false" otherwise. (No quotes in the string.)
        /// </summary>
        public string RteRequired
        {
            get
            {
                return this.mFramesetHelper.RteRequired;
            }
        }

        #endregion  // called from aspx
    }
}
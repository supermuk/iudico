// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="TOC.aspx.cs">
//   
// </copyright>
// 
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Web;

namespace Microsoft.LearningComponents.Frameset
{
    using IUDICO.Common;

    public partial class Frameset_TOC : BwpFramesetPage
    {
        private TocHelper mTocHelper;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.mTocHelper = new TocHelper();
                this.mTocHelper.ProcessPageLoad(
                    this.Response,
                    this.PStore,
                    this.ProcessViewParameter,
                    this.ProcessAttemptIdParameter,
                    this.ProcessViewRequest,
                    this.RegisterError,
                    Localization.GetMessage("TOC_SubmitAttempt"));
            }
            catch (Exception ex)
            {
                this.RegisterError(
                    ResHelper.GetMessage(Localization.GetMessage("FRM_UnexpectedErrorTitle")),
                    ResHelper.GetMessage(Localization.GetMessage("FRM_UnexpectedError"), HttpUtility.HtmlEncode(ex.Message)),
                    false);
            }
        }

        #region called from aspx

        /// <summary>
        /// Write the html for the table of contents to the response.
        /// </summary>
        /// <returns></returns>
        public void WriteToc()
        {
            this.mTocHelper.TocElementsHtml();
        }

        /// <summary>
        /// Return the version of the frameset files. This is used to compare to the version of the js file, to ensure the js
        /// file is not being cached from a previous version.
        /// </summary>
        public static string TocVersion
        {
            get
            {
                return TocHelper.TocVersion;
            }
        }

        /// <summary>
        /// Return the message to display if the js version doesn't match the aspx version.
        /// </summary>
        public static string InvalidVersionHtml
        {
            get
            {
                return TocHelper.InvalidVersionHtml;
            }
        }

        #endregion  // called from aspx
    }
}
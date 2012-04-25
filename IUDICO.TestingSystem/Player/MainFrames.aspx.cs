// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="MainFrames.aspx.cs">
//   
// </copyright>
// 
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Globalization;

using Microsoft.LearningComponents.Storage;

namespace Microsoft.LearningComponents.Frameset
{
    public partial class Frameset_MainFrames : BwpFramesetPage
    {
        private AttemptItemIdentifier mAttemptId;

        private SessionView mView;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.ProcessAttemptIdParameter(false, out this.mAttemptId))
            {
                return;
            }

            if (!this.ProcessViewParameter(false, out this.mView))
            {
                return;
            }
        }

        #region called from aspx

        public string HiddenFrameUrl
        {
            get
            {
                string strUrl = string.Format(
                    CultureInfo.CurrentCulture,
                    "Hidden.aspx?{0}={1}&{2}={3}&{4}=1",
                    FramesetQueryParameter.View,
                    Convert.ToInt32(this.mView),
                    FramesetQueryParameter.AttemptId,
                    this.mAttemptId.GetKey().ToString(),
                    FramesetQueryParameter.Init);
                UrlString hiddenUrl = new UrlString(strUrl);
                return hiddenUrl.ToAscii();
            }
        }

        public string TocFrameUrl
        {
            get
            {
                string strUrl = string.Format(
                    CultureInfo.CurrentCulture,
                    "TOC.aspx?View={0}&AttemptId={1}",
                    this.mView.ToString(),
                    this.mAttemptId.GetKey().ToString());
                UrlString hiddenUrl = new UrlString(strUrl);
                return hiddenUrl.ToAscii();
            }
        }

        #endregion
    }
}
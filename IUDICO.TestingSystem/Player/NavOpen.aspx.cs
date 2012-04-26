using System;

namespace Microsoft.LearningComponents.Frameset
{
    using IUDICO.Common;

    public partial class Frameset_NavOpen : PageHelper
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        #region called from aspx

        public static string NextTitleHtml
        {
            get
            {
                PlainTextString titleTxt =
                    new PlainTextString(
                        ResHelper.GetMessage(Localization.GetMessage("NAV_NextTitle")));
                return new HtmlString(titleTxt).ToString();
            }
        }

        public static string PreviousTitleHtml
        {
            get
            {
                PlainTextString titleTxt =
                    new PlainTextString(
                        ResHelper.GetMessage(Localization.GetMessage("NAV_PrevTitle")));
                return new HtmlString(titleTxt).ToString();
            }
        }

        public static string SaveTitleHtml
        {
            get
            {
                PlainTextString titleTxt =
                    new PlainTextString(
                        ResHelper.GetMessage(Localization.GetMessage("NAV_SaveTitle")));
                return new HtmlString(titleTxt).ToString();
            }
        }

        public static string MinimizeTitleHtml
        {
            get
            {
                PlainTextString titleTxt =
                    new PlainTextString(
                        ResHelper.GetMessage(Localization.GetMessage("NAV_MinimizeTitle")));
                return new HtmlString(titleTxt).ToString();
            }
        }

        #endregion
    }
}
using System;
using IUDICO.Common;

namespace Microsoft.LearningComponents.Frameset
{
    public partial class Frameset_NavClosed : PageHelper
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

        public static string MaximizeTitleHtml
        {
            get
            {
                PlainTextString titleTxt =
                    new PlainTextString(
                        ResHelper.GetMessage(Localization.GetMessage("NAV_MaximizeTitle")));
                return new HtmlString(titleTxt).ToString();
            }
        }

        #endregion
    }
}
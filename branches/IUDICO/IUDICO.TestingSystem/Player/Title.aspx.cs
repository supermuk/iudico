using System;

namespace IUDICO.TestingSystem.Player
{
    using IUDICO.Common;

    using Microsoft.LearningComponents.Frameset;

    using HtmlString = System.Web.HtmlString;

    public partial class Title : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        #region called from aspx

        public static string NextTitleHtml
        {
            get
            {
                var titleTxt =
                    new PlainTextString(
                        ResHelper.GetMessage(Localization.GetMessage("NAV_NextTitle")));
                return new HtmlString(titleTxt).ToString();
            }
        }

        public static string PreviousTitleHtml
        {
            get
            {
                var titleTxt =
                    new PlainTextString(
                        ResHelper.GetMessage(Localization.GetMessage("NAV_PrevTitle")));
                return new HtmlString(titleTxt).ToString();
            }
        }

        public static string SaveTitleHtml
        {
            get
            {
                var titleTxt =
                    new PlainTextString(
                        ResHelper.GetMessage(Localization.GetMessage("NAV_SaveTitle")));
                return new HtmlString(titleTxt).ToString();
            }
        }

        public static string MinimizeTitleHtml
        {
            get
            {
                var titleTxt =
                    new PlainTextString(
                        ResHelper.GetMessage(Localization.GetMessage("NAV_MinimizeTitle")));
                return new HtmlString(titleTxt).ToString();
            }
        }

        #endregion
    }
}
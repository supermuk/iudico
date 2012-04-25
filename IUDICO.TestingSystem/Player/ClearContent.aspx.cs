// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="ClearContent.aspx.cs">
//   
// </copyright>
// 
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Web;

namespace Microsoft.LearningComponents.Frameset
{
    public partial class Frameset_ClearContent : BwpFramesetPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public string PleaseWaitHtml
        {
            get
            {
                return ResHelper.GetMessage(IUDICO.TestingSystem.Localization.GetMessage("CON_PleaseWait"));
            }
        }

        public string UnexpectedErrorTitleHtml
        {
            get
            {
                return
                    HttpUtility.HtmlEncode(
                        ResHelper.GetMessage(IUDICO.TestingSystem.Localization.GetMessage("FRM_UnknownExceptionTitle")));
            }
        }

        public string UnexpectedErrorMsgHtml
        {
            get
            {
                return
                    HttpUtility.HtmlEncode(
                        ResHelper.GetMessage(
                            IUDICO.TestingSystem.Localization.GetMessage("FRM_UnexpectedErrorNoException")));
            }
        }
    }
}
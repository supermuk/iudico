using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using IUDICO.DataModel.DB;

namespace IUDICO.DataModel.Common.StudentUtils
{
    public static class TestControlHelper
    {
        private static string GetControlString(TblPages page)
        {
            string controlString = Encoding.Unicode.GetString(page.PageFile.ToArray());
            return controlString;
        }

        public static Control GetTheoryControl(TblPages page, Panel p)
        {
            p.ScrollBars = ScrollBars.None;

            return p.Page.ParseControl(string.Format(@"<IFRAME ID=""_iFrame""  width=""100%"" height=""100%"" Runat=""Server""  src=""DisplayTheory.itp?PageId={0}""></IFRAME>", page.ID));
        }

        public static Control GetPracticeControl(TblPages page, Panel p)
        {
            p.ScrollBars = ScrollBars.Auto;

            string aspText = GetControlString(page);

            string aspTextWithCorrectImagesUrls = ImageHandlerHelper.ChangeImageUrl(aspText, page);

            var control = p.Page.ParseControl(aspTextWithCorrectImagesUrls);


            var divFromPageControl = control.Controls[0];

            return divFromPageControl;
        }
    }
}

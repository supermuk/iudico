using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IUDICO.DataModel.DB;

namespace IUDICO.DataModel.Common.StudentUtils
{
    public static class TestControlHelper
    {

        private static string GetControlString(TblPages page)
        {
            //string controlString = Encoding.Unicode.GetString(page.PageFile.ToArray());
            //return controlString;
            throw new NotImplementedException();
            return "";
        }

        public static Control GetControl(TblResources resource, Panel p, int courseID)
        {
//            string AssetsPath = Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, "Assets");
//            string CoursePath = Path.Combine(AssetsPath, courseID.ToString());
//            string ResourcePath = Path.Combine(CoursePath, resource.Href);
//            string ResourceFile = File.ReadAllText(ResourcePath);

            Uri BaseUri = new Uri("http://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port + HttpContext.Current.Request.ApplicationPath);
            Uri AssetsUri = new Uri(BaseUri, "Assets/");
            Uri CourseUri = new Uri(AssetsUri, courseID.ToString() + "/");
            Uri ResourceUri = new Uri(CourseUri, resource.Href);
            
            Control ResourceControl = p.Page.ParseControl(string.Format(@"<IFRAME ID=""_iFrame""  width=""100%"" height=""100%"" src=""{0}""></IFRAME>", ResourceUri.ToString()));

            return ResourceControl;
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

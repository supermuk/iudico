using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.UI;
using IUDICO.Common.Controllers;

namespace IUDICO.CourseManagement.Controllers
{
    public class CourseBaseController : PluginController
    {
        /*
        public static string RenderPartialToString(string controlName, object model)
        {
            var vp = new ViewPage { ViewData = new ViewDataDictionary(model) };

            var control = vp.LoadControl(controlName);

            vp.Controls.Add(control);

            var sb = new StringBuilder();

            using (var sw = new StringWriter(sb))
            {
                using (var tw = new HtmlTextWriter(sw))
                {
                    vp.RenderControl(tw);
                }
            }

            return sb.ToString();
        }
         */

        public static string RenderPartialToString(ControllerContext context, string partialViewName, ViewDataDictionary viewData, TempDataDictionary tempData)
        {
            //var result = ViewEngines.Engines.FindPartialView(context, "~/Plugins/IUDICO.CourseManagment.dll/IUDICO.CourseManagment/Views/Shared/" + partialViewName +".ascx");
            var result = ViewEngines.Engines.FindPartialView(context, partialViewName);

            if (result.View != null)
            {
                var sb = new StringBuilder();
                using (var sw = new StringWriter(sb))
                {
                    using (var output = new HtmlTextWriter(sw))
                    {
                        var viewContext = new ViewContext(context, result.View, viewData, tempData, output);
                        result.View.Render(viewContext, output);
                    }
                }

                return sb.ToString();
            }

            return string.Empty;
        }

        protected string PartialViewHtml(string partialViewName, object model, ViewDataDictionary viewData)
        {
            var data = new ViewDataDictionary(viewData) {Model = model};

            var tempData = new TempDataDictionary();

            return RenderPartialToString(this.ControllerContext, partialViewName, data, tempData);
        }
    }
}
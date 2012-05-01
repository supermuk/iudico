using System;
using System.IO;
using System.Text;
using System.Web.Mvc;
using System.Web.UI;
using IUDICO.Common.Models.Services;

namespace IUDICO.Common.Controllers
{
    public class PluginController : BaseController
    {
        public static ILmsService LmsService;

        protected override ViewResult View(string viewName, string masterName, object model)
        {
            viewName = viewName ?? RouteData.GetRequiredString("action");

            if (viewName == null)
            {
                throw new ArgumentNullException("viewName", "You must provide a view name in AddinControllers");
            }

            var assembly = GetType().Assembly;
            var assemblyFileName = Path.GetFileName(assembly.Location);
            var assmblyName = assembly.GetName().Name;
            var controllerName = GetType().Name;

            if (!controllerName.EndsWith("Controller"))
            {
                throw new ApplicationException(
                    "Controllers must have a name ending with Controller, e.g: CustomerController");
            }

            var controllerShortenedName = controllerName.Substring(0, controllerName.Length - 10);
            var viewPath = string.Format("~/Plugins/{0}/{1}/Views/{2}/", assemblyFileName, assmblyName, controllerShortenedName);

            return base.View(viewPath + viewName + ".aspx", masterName, model);
        }

        protected override PartialViewResult PartialView(string viewName, object model)
        {
            viewName = viewName ?? RouteData.GetRequiredString("action");

            if (viewName == null)
            {
                throw new ArgumentNullException("viewName", "You must provide a view name in AddinControllers");
            }

            var assembly = GetType().Assembly;
            var assemblyFileName = Path.GetFileName(assembly.Location);
            var assmblyName = assembly.GetName().Name;
            var controllerName = GetType().Name;

            if (!controllerName.EndsWith("Controller"))
            {
                throw new ApplicationException(
                    "Controllers must have a name ending with Controller, e.g: CustomerController");
            }

            var controllerShortenedName = controllerName.Substring(0, controllerName.Length - 10);
            var viewPath = string.Format("~/Plugins/{0}/{1}/Views/{2}/", assemblyFileName, assmblyName, controllerShortenedName);

            return base.PartialView(viewPath + viewName + ".ascx", model);
        }

        protected string PartialViewAsString(string viewName, object model)
        {
            var res = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);

            if (res.View != null)
            {
                var sb = new StringBuilder();
                using (var sw = new StringWriter(sb))
                {
                    using (var output = new HtmlTextWriter(sw))
                    {
                        var data = new ViewDataDictionary(ViewData) { Model = model };
                        var viewContext = new ViewContext(ControllerContext, res.View, data, TempData, output);
                        res.View.Render(viewContext, output);
                    }
                }

                return sb.ToString();
            }

            return string.Empty;
        }
    }
}

// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="HttpModule.cs">
//   
// </copyright>
// 
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using System.Web;

// Sample HttpModule from msdn: http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpguide/html/cpconCustomHttpModules.asp

namespace Microsoft.LearningComponents.Frameset
{
    /// <summary>The <see cref="IHttpModule"/> for the frameset.</summary>
    public class FramesetModule : IHttpModule
    {
        /// <summary>The name of the module.</summary>
        public static string ModuleName
        {
            get
            {
                return "FramesetContentModule";
            }
        }

        // Register for HttpApplication events by adding handler.
        // Rational for suppression: This is called by IIS. If it's null, there's something SERIOUSLY wrong
        /// <summary>Initializes the module.</summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        public void Init(HttpApplication context)
        {
            context.BeginRequest += (new EventHandler(this.Application_BeginRequest));
        }

        /// <summary>
        /// BeginRequest event handler. This determines if the current request is destined for 
        /// the frameset Content.aspx page. If so, it re-writes the URL to have the path to the 
        /// resource (if it exists) as a query parameter.
        /// If the module applies, the incoming request is of the form:
        /// http://server/application/Frameset/Content.aspx/view/view data/optional_resource_path
        /// After processing, the URL will be of the form:
        /// http://server/application/Frameset/Content.aspx?ResPath=/view/view data/optional_resource_path
        /// </summary>
        private void Application_BeginRequest(object source, EventArgs e)
        {
            // See http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dnaspp/html/urlrewriting.asp 

            HttpApplication application = (HttpApplication)source;
            HttpContext context = application.Context;
            string rawUrl = context.Request.Url.AbsolutePath;
            int beginContext = rawUrl.IndexOf("Frameset/Content.aspx/", StringComparison.OrdinalIgnoreCase);
            if (beginContext > 0)
            {
                int endContent = beginContext + "Frameset/Content.aspx".Length;
                // It's a request with information following the page name.
                string filePath = rawUrl.Substring(0, endContent);
                string postFilePath = rawUrl.Substring(endContent);

                StringBuilder newQuery = new StringBuilder(4096);
                BuildQueryString(newQuery, context.Request.QueryString);
                if (postFilePath.Length > 0)
                {
                    if (newQuery.Length > 0)
                    {
                        newQuery.Append("&");
                    }
                    newQuery.Append(FramesetQueryParameter.ContentFilePath);
                    newQuery.Append("=");
                    newQuery.Append(postFilePath);
                }
                context.RewritePath(filePath, string.Empty, newQuery.ToString());
            }
        }

        /// <summary>
        /// Returns a query string that includes the name/value pairs in queryValues.
        /// </summary>
        private static void BuildQueryString(StringBuilder query, NameValueCollection queryValues)
        {
            bool firstParam = true;
            foreach (string name in queryValues.Keys)
            {
                if (firstParam)
                {
                    firstParam = false;
                }
                else
                {
                    query.Append("&");
                }
                query.Append(name);
                if (queryValues[name].Length > 0)
                {
                    string value = queryValues[name];
                    if (!string.IsNullOrEmpty(value))
                    {
                        query.Append("=");
                        query.Append(value);
                    }
                }
            }
        }

        /// <summary>See <see cref="IDisposable.Dispose"/>.</summary>
        public void Dispose()
        {
        }
    }
}
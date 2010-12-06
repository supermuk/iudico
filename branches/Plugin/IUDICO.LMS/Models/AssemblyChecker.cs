using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Reflection;
using IUDICO.Common.Models.Plugin;

namespace IUDICO.LMS.Models
{
    public class AssemblyChecker : MarshalByRefObject
    {
        public static IEnumerable<string> correctAssemblies;

        public static void GetCorrectAssemblies()
        {
            List<string> assemblies = new List<string>();

            System.Diagnostics.Debugger.Launch();

            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += new ResolveEventHandler(delegate(object sender, ResolveEventArgs args)
            {
                return Assembly.ReflectionOnlyLoad(args.Name);
            });

            // TODO: Move Plugins to config?
            String pluginPath = Path.Combine(HttpContext.Current.Server.MapPath("/"), "Plugins");

            foreach (string filename in Directory.GetFiles(pluginPath, "*.dll"))
            {
                try
                {
                    Assembly assembly = Assembly.ReflectionOnlyLoadFrom(filename);

                    //var pluginAttribute = assembly.GetCustomAttributesData().Where(a => a.Constructor.DeclaringType.GUID == typeof(IudicoPluginAttribute).GUID).SingleOrDefault();

                    //if (pluginAttribute == null)
                    //{
                    //    /* TODO log warning about incompatible assembly */
                    //    continue;
                    //}

                    Type type = assembly.GetTypes().Where(t => !t.IsAbstract && !t.IsInterface && t.GetInterface(typeof(IIudicoPlugin).FullName) != null).SingleOrDefault();

                    if (type == null)
                    {
                        /* TODO log warning about no compatible types */
                        continue;
                    }
                    else
                    {
                        assemblies.Add(assembly.Location);
                    }
                }
                catch (Exception)
                {
                    /* TODO: log inability to load dll */
                    continue;
                }
            }

            AssemblyChecker.correctAssemblies = assemblies;
        }
    }
}
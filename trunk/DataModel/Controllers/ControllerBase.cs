using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Web;
using IUDICO.DataModel.Common;
using LEX.CONTROLS;
using System.Linq;

namespace IUDICO.DataModel.Controllers
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ControllerParameterAttribute : Attribute
    {
    }

    public abstract class ControllerBase
    {
        [ControllerParameter]
        public string BackUrl;

        /// <summary>
        /// Executes when page first time loaded
        /// </summary>
        public virtual void Initialize()
        {
        }

        /// <summary>
        /// Executes when page is loaded
        /// </summary>
        public virtual void Loaded()
        {
        }

        public virtual void ValidateParameters()
        {
        }
    }

    public class DefaultController : ControllerBase
    {
        
    }

    public static class ControllerParametersUtility<TController>
        where TController : ControllerBase
    {
        static ControllerParametersUtility()
        {
            // compiling controller parameters
            foreach (var field in typeof(TController).GetFields(BindingFlags.Instance | BindingFlags.SetField | BindingFlags.Public | BindingFlags.NonPublic))
            {
                if (field.HasAtr<ControllerParameterAttribute>())
                {
                    IMemberAL al = field.ToAbstaction();
                    var mt = al.MemberType;
                    if (mt == typeof(int) || mt == typeof(string) || mt.IsEnum)
                    {
                        __ControllerParameters.Add(field.Name, al);
                    }
                    else
                        throw new DMError("Type {0} cannot be specified for controller parameter", mt.FullName);
                }
            }
        }

        public static string BuildUrlParams(TController c, HttpServerUtility server)
        {
            var b = new StringBuilder();
            foreach (var p in __ControllerParameters.Reverse())
            {
                var v = p.Value[c];
                var mt = p.Value.MemberType;
                while (mt.IsGenericType && mt.GetGenericTypeDefinition() == typeof(Nullable<>))
                    mt = mt.GetGenericArguments()[0];
                if (v != null)
                {
                    if (mt == typeof(int) || mt == typeof(string) || mt.IsEnum)
                    {
                        var vstr = v.ToString().Trim();
                        if (vstr.IsNotNull())
                        {
                            if (b.Length > 0)
                            {
                                b.Append("&");
                            }
                            b.Append(p.Key);
                            b.Append('=');
                            b.Append(server.UrlEncode(vstr));
                        }
                    }
                    else
                        throw new InvalidOperationException();
                }
            }
            return b.ToString();
        }

        public static void LoadParametrs(TController c, NameValueCollection @params, HttpServerUtility server)
        {
            foreach (var pr in __ControllerParameters)
            {
                var v = server.UrlDecode(@params[pr.Key]);
                var mt = pr.Value.MemberType;
                while (mt.IsGenericType && mt.GetGenericTypeDefinition() == typeof(Nullable<>))
                    mt = mt.GetGenericArguments()[0];
                if (mt == typeof(int))
                {
                    pr.Value[c] = int.Parse(v);
                }
                else if (mt == typeof(string))
                {
                    pr.Value[c] =  v;
                }
                else if (mt.IsEnum)
                {
                    pr.Value[c] = mt.GetField(v).GetValue(null);
                }
                else
                    throw new InvalidOperationException();
            }
        }

        private static readonly Dictionary<string, IMemberAL> __ControllerParameters = new Dictionary<string, IMemberAL>();
    }
}

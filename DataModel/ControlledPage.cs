using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Security;
using System.Web.UI;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.Controllers;

namespace IUDICO.DataModel
{
    ///<summary>
    /// Base class for all projects' page
    ///</summary>
    /// 
    public abstract class ControlledPage<ControllerType> : Page
        where ControllerType : ControllerBase, new()
    {
        protected ControlledPage()
        {
            if (
                (User == null  || User.Identity == null || !User.Identity.IsAuthenticated) &&
                typeof(ControllerType) != typeof(LoginController))
            {
                FormsAuthentication.RedirectToLoginPage();
            }
            Controller = new ControllerType();
        }

        protected EventHandler BindToEventHandler(Action a)
        {
            return (o, e) => a();
        }

        protected virtual void BindController(ControllerType c)
        {
        }

        protected override void LoadViewState(object savedState)
        {
            var d = (object[])savedState;
            base.LoadViewState(d[0]);
            PageControllerInfo<ControllerType>.LoadStateFor(Controller, d[1]);
        }

        protected override object SaveViewState()
        {
            return new[]
            {
               base.SaveViewState(),
               PageControllerInfo<ControllerType>.SaveStateFor(Controller)
            };
        }

        protected override void OnInitComplete(EventArgs e)
        {
            base.OnInitComplete(e);
            BindController(Controller);
        }

        /// <summary>
        /// Do nothing. It's required to override in derived class to bind exactly controls realy need it
        /// </summary>
        public override void DataBind()
        {
        }

        protected readonly ControllerType Controller;

        #region PageControllerInfo

        private static class PageControllerInfo<TController>
            where TController : ControllerBase
        {
            private struct ValueInfo
            {
                public ValueInfo(FieldInfo storage)
                {
                    Storage = storage;
                }
                public readonly FieldInfo Storage;
            }

            static PageControllerInfo()
            {
                var t = typeof(TController);
                var fields = t.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
                foreach (var inf in fields)
                {
                    if (inf.HasAtr<ControllerValueAttribute>())
                    {
                        if (inf.IsStatic)
                        {
                            throw new DMError("{0} can be applied to instance field only but applied to static ({1}.{2})", typeof(ControllerValueAttribute).Name, t.Name, inf.Name);
                        }
                        if (!IsTypeSupported(inf.FieldType))
                        {
                            throw new DMError("Type '{0}' is not supported for mark with {1}", inf.FieldType.Name, typeof(ControllerValueAttribute).Name);
                        }

                        Values.Add(new ValueInfo(inf));
                    }
                }
            }

            public static object SaveStateFor(TController controller)
            {
                var result = new object[Values.Count];
                for (var i = Values.Count - 1; i >= 0; --i)
                {
                    result[i] = Values[i].Storage.GetValue(controller);
                }
                return result;
            }

            public static void LoadStateFor(TController controller, object state)
            {
                var st = (object[])state;
                if (st.Length != Values.Count)
                {
                    throw new DMError("Invalid count of controller values expected {0} but {1} found", Values.Count, st.Length);
                }
                for (var i = Values.Count - 1; i >= 0; --i)
                {
                    Values[i].Storage.SetValue(controller, st[i]);
                }
            }

            private static bool IsTypeSupported(Type t)
            {
                return t.IsValueType;
            }

            private static readonly List<ValueInfo> Values = new List<ValueInfo>();
        }

        #endregion
    }
}
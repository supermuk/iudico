using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Plugin;
using System.Collections.Generic;
using Castle.Windsor;
using System;

using Lucene.Net.Store;
using Lucene.Net.Analysis;
using Lucene.Net.Index;
using Lucene.Net.Documents;
using Lucene.Net.Analysis.Standard;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Notifications;
using IUDICO.Common.Models.Shared;
using System.IO;
using System.Timers;
using System.Web.Routing;
using Castle.MicroKernel.SubSystems.Configuration;

using SimpleLucene;
using SimpleLucene.Impl;

using Action = IUDICO.Common.Models.Action;
using Directory = Lucene.Net.Store.Directory;
using Node = IUDICO.Common.Models.Shared.Node;
using System.Threading;
using IUDICO.Search.Models;
using SimpleLucene.IndexManagement;
using System.Reflection;

using Component = Castle.MicroKernel.Registration.Component;

namespace IUDICO.Search
{
    public class SearchPlugin : IWindsorInstaller, IPlugin
    {
        protected IWindsorContainer container;

        private BackgroundWorker bw;

        // protected LuceneThread luceneThread;

        #region IWindsorInstaller Members

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                AllTypes.FromThisAssembly().BasedOn<IController>().Configure(
                    c => c.LifeStyle.Transient.Named(c.Implementation.Name)),
                Component.For<IPlugin>().Instance(this).LifeStyle.Is(Castle.Core.LifestyleType.Singleton),
                Component.For<LuceneThread>().ImplementedBy<LuceneThread>().LifeStyle.Is(Castle.Core.LifestyleType.Singleton));

            this.container = container;
        }

        #endregion

        #region IPlugin Members
        public string GetName()
        {
            return "Search";
        }

        public IEnumerable<Action> BuildActions()
        {
            return new Action[] { };
        }

        public IEnumerable<MenuItem> BuildMenuItems()
        {
            return new MenuItem[] { };
        }

        public void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute(
                "Search",
                "Search/{action}",
                new { controller = "Search" });
        }

        public void Update(string evt, params object[] data)
        {
            try
            {
                var luceneThread = this.container.Resolve<LuceneThread>();

                if (evt == LMSNotifications.ApplicationStart)
                {
                    this.bw = new BackgroundWorker();
                    this.bw.DoWork += (sender, args) => ((LuceneThread)args.Argument).RebuildIndex();
                    this.bw.RunWorkerAsync(luceneThread);
                }
                else if (evt == LMSNotifications.ApplicationStop)
                {

                }

                switch (evt)
                {
                    case UserNotifications.UserCreate:
                    case UserNotifications.UserEdit:
                        luceneThread.UpdateIndex((User)data[0]);
                        break;
                    case UserNotifications.UserDelete:
                        luceneThread.DeleteIndex((User)data[0]);
                        break;
                    case UserNotifications.GroupCreate:
                    case UserNotifications.GroupEdit:
                        luceneThread.UpdateIndex((Group)data[0]);
                        break;
                    case UserNotifications.GroupDelete:
                        luceneThread.DeleteIndex((Group)data[0]);
                        break;
                    case DisciplineNotifications.DisciplineCreated:
                    case DisciplineNotifications.DisciplineEdited:
                        luceneThread.UpdateIndex((Discipline)data[0]);
                        break;
                    case DisciplineNotifications.DisciplineDeleted:
                        luceneThread.DeleteIndex((Discipline)data[0]);
                        break;
                    case CourseNotifications.CourseCreate:
                        luceneThread.UpdateIndex((Course)data[0]);
                        break;
                    case CourseNotifications.CourseEdit:
                        luceneThread.DeleteIndex((Course)data[0]);
                        luceneThread.UpdateIndex((Course)data[1]);
                        break;
                    case CourseNotifications.CourseDelete:
                        luceneThread.DeleteIndex((Course)data[0]);
                        break;
                    case DisciplineNotifications.TopicCreated:
                        luceneThread.UpdateIndex((Topic)data[0]);
                        break;
                    case DisciplineNotifications.TopicEdited:
                        luceneThread.DeleteIndex((Topic)data[0]);
                        luceneThread.UpdateIndex((Topic)data[1]);
                        break;
                    case DisciplineNotifications.TopicDeleted:
                        luceneThread.DeleteIndex((Topic)data[0]);
                        break;
                    case CourseNotifications.NodeCreate:
                        luceneThread.UpdateIndex((Node)data[0]);
                        break;
                    case CourseNotifications.NodeEdit:
                        luceneThread.DeleteIndex((Node)data[0]);
                        luceneThread.UpdateIndex((Node)data[1]);
                        break;
                    case CourseNotifications.NodeContentEdit:
                        luceneThread.UpdateIndex((Node)data[0]);
                        break;
                    case CourseNotifications.NodeDelete:
                        luceneThread.DeleteIndex((Node)data[0]);
                        break;
                }

                luceneThread.ProcessQueue();
            }
            catch (Exception)
            {
                // Maybe log exceptions?
            }
        }

        #endregion
    }
}
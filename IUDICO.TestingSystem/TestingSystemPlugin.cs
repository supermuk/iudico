// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestingSystemPlugin.cs" company="">
//   
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Web.Mvc;

using Castle.MicroKernel.Registration;
using Castle.Windsor;

using IUDICO.Common.Models;
using IUDICO.Common.Models.Notifications;
using IUDICO.Common.Models.Plugin;
using IUDICO.Common.Models.Services;
using IUDICO.TestingSystem.Models;

namespace IUDICO.TestingSystem
{
    using System.Web.Routing;

    using Castle.MicroKernel.SubSystems.Configuration;

    public class TestingSystemPlugin : IWindsorInstaller, IPlugin
    {
        #region IWindsorInstaller Members

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                AllTypes.FromThisAssembly().BasedOn<IController>().Configure(c => c.LifeStyle.Transient.Named(c.Implementation.Name)),
                Component.For<IPlugin>().ImplementedBy<TestingSystemPlugin>().LifeStyle.Is(Castle.Core.LifestyleType.Singleton),
                Component.For<ITestingService>().ImplementedBy<TestingService>().LifeStyle.Is(Castle.Core.LifestyleType.Singleton),
                Component.For<IMlcProxy>().ImplementedBy<MlcProxy>().LifeStyle.Is(Castle.Core.LifestyleType.Singleton));
        }

        #endregion

        #region IPlugin Members

        public string GetName()
        {
            return "Testing System";
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
            routes.MapPageRoute(
                "PlayerFrameset",
                "Player/Frameset/{page}.aspx",
                "~/Plugins/IUDICO.TestingSystem.dll/IUDICO.TestingSystem/Player/{page}.aspx");
            routes.MapPageRoute(
                "PlayerContent",
                "Player/Frameset/Content.aspx/{one}/{two}/{three}",
                "~/Plugins/IUDICO.TestingSystem.dll/IUDICO.TestingSystem/Player/Content.aspx");
            routes.MapPageRoute(
                "PlayerContentExtended",
                "Player/Frameset/Content.aspx/{one}/{two}/{three}/{four}",
                "~/Plugins/IUDICO.TestingSystem.dll/IUDICO.TestingSystem/Player/Content.aspx");
            routes.MapPageRoute(
                "PlayerContentFive",
                "Player/Frameset/Content.aspx/{one}/{two}/{three}/{four}/{five}",
                "~/Plugins/IUDICO.TestingSystem.dll/IUDICO.TestingSystem/Player/Content.aspx");
            routes.MapPageRoute(
                "PlayerContentSix",
                "Player/Frameset/Content.aspx/{one}/{two}/{three}/{four}/{five}/{six}",
                "~/Plugins/IUDICO.TestingSystem.dll/IUDICO.TestingSystem/Player/Content.aspx");
            routes.MapPageRoute(
                "PlayerContentSeven",
                "Player/Frameset/Content.aspx/{one}/{two}/{three}/{four}/{five}/{six}/{seven}",
                "~/Plugins/IUDICO.TestingSystem.dll/IUDICO.TestingSystem/Player/Content.aspx");
            routes.MapPageRoute(
                "PlayerContentEight",
                "Player/Frameset/Content.aspx/{one}/{two}/{three}/{four}/{five}/{six}/{seven}/{eight}",
                "~/Plugins/IUDICO.TestingSystem.dll/IUDICO.TestingSystem/Player/Content.aspx");
            routes.MapPageRoute(
                "PlayerContentNine",
                "Player/Frameset/Content.aspx/{one}/{two}/{three}/{four}/{five}/{six}/{seven}/{eight}/{nine}",
                "~/Plugins/IUDICO.TestingSystem.dll/IUDICO.TestingSystem/Player/Content.aspx");
            routes.MapPageRoute(
                "PlayerContentTen",
                "Player/Frameset/Content.aspx/{one}/{two}/{three}/{four}/{five}/{six}/{seven}/{eight}/{nine}/{ten}",
                "~/Plugins/IUDICO.TestingSystem.dll/IUDICO.TestingSystem/Player/Content.aspx");
            routes.MapPageRoute(
                "PlayerContentEleven",
                "Player/Frameset/Content.aspx/{one}/{two}/{three}/{four}/{five}/{six}/{seven}/{eight}/{nine}/{ten}/{eleven}",
                "~/Plugins/IUDICO.TestingSystem.dll/IUDICO.TestingSystem/Player/Content.aspx");

            routes.MapRoute(
                "Training",
                "Training/Play/{curriculumChapterTopicId}/{courseId}/{topicType}",
                new { controller = "Training", action = "Play" });
        }

        public void Update(string name, params object[] data)
        {
        }

        public void Setup(IWindsorContainer container)
        {
        }

        #endregion
    }
}
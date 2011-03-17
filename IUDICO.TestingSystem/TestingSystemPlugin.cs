using System.Collections.Generic;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Plugin;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Notifications;
using IUDICO.TestingSystem.Models;
using Castle.Windsor;


namespace IUDICO.TestingSystem
{
    public class TestingSystemPlugin : IWindsorInstaller, IPlugin
    {
        #region IWindsorInstaller Members

        public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            container.Register(
                AllTypes
                    .FromThisAssembly()
                    .BasedOn<IController>()
                    .Configure(c => c.LifeStyle.Transient
                                        .Named(c.Implementation.Name)),
                Component.For<IPlugin>().ImplementedBy<TestingSystemPlugin>().LifeStyle.Is(Castle.Core.LifestyleType.Singleton),
                Component.For<ITestingService>().ImplementedBy<TestingService>().LifeStyle.Is(Castle.Core.LifestyleType.Singleton),
                Component.For<IMlcProxy>().ImplementedBy<MlcProxy>().LifeStyle.Is(Castle.Core.LifestyleType.Singleton)
                );
        }

        #endregion

        #region IPlugin Members
        public string GetName()
        {
            return "Testing System";
        }

        public IEnumerable<IUDICO.Common.Models.Action> BuildActions(Role role)
        {
            var actions = new List<Action>();

            // do not add actions for testing service
            //actions.Add(new Action("Import Testings", "Package/Index"));
            //actions.Add(new Action("Available Testings", "Training/Index"));

            return actions;
        }

        public void BuildMenu(Menu menu)
        {
            // do not add menu item for testing service
            //menu.Add(new MenuItem("Testing", "Training", "Index"));
        }

        public void RegisterRoutes(System.Web.Routing.RouteCollection routes)
        {
            /*routes.MapPageRoute(
                "Player",
                "Player/{file}",
                "~/Plugins/IUDICO.TestingSystem.dll/IUDICO.TestingSystem/Player/{file}");
            routes.MapPageRoute(
                "PlayerImages",
                "Player/Images/{file}",
                "~/Plugins/IUDICO.TestingSystem.dll/IUDICO.TestingSystem/Player/Images/{file}");
            routes.MapPageRoute(
                "PlayerInclude",
                "Player/Include/{file}",
                "~/Plugins/IUDICO.TestingSystem.dll/IUDICO.TestingSystem/Player/Include/{file}");
            */
            routes.MapPageRoute(
                "PlayerTheme",
                "Plugins/IUDICO.TestingSystem.dll/IUDICO.TestingSystem/Player/Content.aspx/{View}/{Attemptid}/{filePath}",
                "~/Plugins/IUDICO.TestingSystem.dll/IUDICO.TestingSystem/Player/Content.aspx");
            
            /*routes.MapRoute(
               "Training",
               "Training/{packageId}/{attemptId}",
               new { controller = "Training", action = "Details", attemptId = UrlParameter.Optional },
               new { packageID = @"\d+" });
            routes.MapRoute(
               "Trainings",
               "Training/{action}/{id}",
               new { controller = "Training", action = "Index", id = UrlParameter.Optional }
            );*/
            //routes.IgnoreRoute("Content/TimePicker.css");
            routes.MapRoute(
                "Training",
                "Training/{action}",
                new { controller = "Training" });
        }

        public void Update(string name, params object[] data)
        {
            //switch (name)
            //{
               
            //}
        }

        public void Setup(IWindsorContainer container)
        {
            
        }

        #endregion

        
    }
}
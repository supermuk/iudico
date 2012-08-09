using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IUDICO.Common.Models.Plugin;
using IUDICO.Common.Models.Services;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using IUDICO.DataGenerator.Models.Storage;
using IUDICO.DataGenerator.Models.Generators;
using IUDICO.Common.Models.Notifications;

namespace IUDICO.DataGenerator
{
	public class DataGeneratorPlugin: IWindsorInstaller, IPlugin
	{

		IWindsorContainer container;

		IDemoStorage DemoStorage
		{
			get
			{
				return this.container.Resolve<IDemoStorage>();
			}
		}

		#region IPlugin Members

		public string GetName()
		{
			return "DataGenerator";
		}

		public IEnumerable<Common.Models.Action> BuildActions()
		{
			return new Common.Models.Action[]{};
		}

		public IEnumerable<Common.Models.MenuItem> BuildMenuItems()
		{
			return new Common.Models.MenuItem[]{};
		}

		public void RegisterRoutes( System.Web.Routing.RouteCollection routes )
		{
		}

		public void Update( string evt, params object[] data )
		{
			switch (evt)
			{
				case LMSNotifications.ApplicationStart:

					//bool generate = bool.Parse(System.Configuration.ConfigurationManager.AppSettings["DataGenerate"]) ?? true;
					//if (generate)
					//{
						UserGenerator.Generate(this.container);
						CourseGenerator.Generate(this.container);
					//}

					break;
			}
		}

		#endregion

		#region IWindsorInstaller Members

		public void Install( IWindsorContainer container, IConfigurationStore store )
		{
			container.Register(
					 AllTypes
						  .FromThisAssembly()
						  .BasedOn<IController>()
						  .Configure(c => c.LifeStyle.Transient
													 .Named(c.Implementation.Name)),
					 Component.For<IPlugin>().Instance(this).LifeStyle.Is(Castle.Core.LifestyleType.Singleton),
					 Component.For<IDemoStorage>().ImplementedBy<DemoStorage>().LifeStyle.Is(Castle.Core.LifestyleType.Singleton));

			this.container = container;
		}

		#endregion
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Reflection;
using IUDICO.Common.Models.Plugin;
using IUDICO.Common.Models.Caching;
using IUDICO.Common.Models.Services;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using IUDICO.DataGenerator.Models.Storage;
using IUDICO.DataGenerator.Models.Generators;
using IUDICO.Common.Models.Notifications;
using IUDICO.CourseManagement.Models.Storage;
using IUDICO.DisciplineManagement.Models.Storage;


namespace IUDICO.DataGenerator
{
	public class DataGeneratorPlugin : IWindsorInstaller, IPlugin
	{

		IWindsorContainer container;

		IDemoStorage DemoStorage
		{
			get
			{
				return this.container.Resolve<IDemoStorage>();
			}
		}


		#region IWindsorInstaller Members

		public void Install(IWindsorContainer container, IConfigurationStore store)
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


		#region IPlugin Members

		public string GetName()
		{
			return "DataGenerator";
		}

		public IEnumerable<Common.Models.Action> BuildActions()
		{
			return new Common.Models.Action[] { };
		}

		public IEnumerable<Common.Models.MenuItem> BuildMenuItems()
		{
			return new Common.Models.MenuItem[] { };
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
					var userStorage = new FakeDatabaseUserStorage(container.Resolve<ILmsService>(),"lex");
					var demoStorage = container.Resolve<IDemoStorage>();
					UserGenerator.Generate(userStorage,demoStorage);

					this.GeneratePascal();
					//}

					break;
			}
		}

		#endregion

		private void GeneratePascal()
		{
			var courseStorage = container.Resolve<ICourseStorage>();
			var cacheProvider = container.Resolve<ICacheProvider>();
			var path = (new System.Uri(Assembly.GetExecutingAssembly().CodeBase)).AbsolutePath;
			path = path.Replace("IUDICO.LMS/Plugins/IUDICO.DataGenerator.DLL", "IUDICO.DataGenerator/Content/Courses/Pascal/");
			CourseGenerator.PascalCourse(courseStorage,cacheProvider,path);

			path = (new System.Uri(Assembly.GetExecutingAssembly().CodeBase)).AbsolutePath;
			path = path.Replace("IUDICO.LMS/Plugins/IUDICO.DataGenerator.DLL", "IUDICO.DataGenerator/Content/Courses/Pascal/Pascal.disc");
			var databaseStorage = new FakeDatabaseDisciplineStorage(container.Resolve<ILmsService>(), "OlehVukladachenko");
			var storage = new CachedDisciplineStorage(databaseStorage, cacheProvider);
			DisciplineGenerator.PascalDiscipline(storage, path);

			CurriculumGenerator.PascalCurriculum(this.container);
		}
	}
}
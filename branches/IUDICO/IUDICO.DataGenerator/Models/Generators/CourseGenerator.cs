using System;
using System.Text;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Util;
using System.IO;
using Castle.Windsor;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Shared;
using IUDICO.Common.Models.Caching;
using IUDICO.Common.Models.Services;
using IUDICO.DataGenerator.Models.Storage;
using IUDICO.CourseManagement.Models;
using IUDICO.CourseManagement.Models.Storage;
namespace IUDICO.DataGenerator.Models.Generators
{
	public static class CourseGenerator
	{
		public static void Generate(IWindsorContainer container)
		{
			var courseStorage = container.Resolve<ICourseStorage>();
			var cacheProvider = container.Resolve<ICacheProvider>();

			CourseGenerator.PascalCourse(courseStorage,cacheProvider);

		}

		private static void PascalCourse(ICourseStorage courseStorage, ICacheProvider cacheProvider)
		{
		//   var path = HttpContext.Current == null ? System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath : HttpContext.Current.Request.PhysicalApplicationPath;
		//   path = path.Replace("IUDICO.LMS\\", @"IUDICO.DataGenerator\Content\Courses\Pascal\");

			var path = (new System.Uri(Assembly.GetExecutingAssembly().CodeBase)).AbsolutePath;
			path = path.Replace("IUDICO.LMS/Plugins/IUDICO.DataGenerator.DLL", "IUDICO.DataGenerator/Content/Courses/Pascal/");

			if (Directory.Exists(path))
			{
				var files = Directory.GetFiles(path);

				foreach (var file in files)
				{
					var name = Path.GetFileNameWithoutExtension(file);

					if (courseStorage.GetCourses().Where(c => c.Name == name && c.Owner == "OlehVukladachenko").Count() == 0)
					{
						courseStorage.Import(file, "OlehVukladachenko");
					}

					Course course = courseStorage.GetCourses().SingleOrDefault(c => c.Name == name && c.Owner == "OlehVukladachenko");
					if (course != null && course.Locked.Value )
					{
						courseStorage.Parse(course.Id);
						cacheProvider.Invalidate("course-" + course.Id, "courses");
					}
				}
			}
		}
	}
}
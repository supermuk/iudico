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
		public static void PascalCourse(ICourseStorage courseStorage, ICacheProvider cacheProvider, string path)
		{
			if (Directory.Exists(path))
			{
				var files = Directory.GetFiles(path,"*.zip");

				foreach (var file in files)
				{
					var name = Path.GetFileNameWithoutExtension(file);

					if (!courseStorage.GetCourses().Any(c => c.Name == name && c.Owner == "OlehVukladachenko"))
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
      
      public static void GenerateAllCourses(ICourseStorage courseStorage, ICacheProvider cacheProvider)
      {

         var path = (new System.Uri(Assembly.GetExecutingAssembly().CodeBase)).AbsolutePath;
         path = path.Replace("IUDICO.LMS/Plugins/IUDICO.DataGenerator.DLL", "IUDICO.DataGenerator/Content/Courses/");

         if (Directory.Exists(path))
         {
            var files = Directory.GetFiles(path, "*.zip",SearchOption.TopDirectoryOnly);

            foreach (var file in files)
            {
               var name = Path.GetFileNameWithoutExtension(file);

               if (!courseStorage.GetCourses().Any(c => c.Name == name && c.Owner == "prof3"))
               {
                  courseStorage.Import(file, "prof3");
                  Course course = courseStorage.GetCourses().SingleOrDefault(c => c.Name == name && c.Owner == "prof3");
                  if (course != null && course.Locked.Value)
                  {
                     courseStorage.Parse(course.Id);
                     cacheProvider.Invalidate("course-" + course.Id, "courses");
                  }
               }
            }
         }
      }
	}
}
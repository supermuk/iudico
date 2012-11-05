using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Reflection;
using Castle.Windsor;
using IUDICO.DisciplineManagement.Models.Storage;
using IUDICO.Common.Models.Shared;
using IUDICO.Common.Models.Caching;
using IUDICO.Common.Models.Services;
using IUDICO.DisciplineManagement.Models;
using IUDICO.DataGenerator.Models.Storage;

namespace IUDICO.DataGenerator.Models.Generators
{
	public static class DisciplineGenerator
	{
      public static void PascalDiscipline(IDisciplineStorage storage, string path)
		{
			if (storage.GetDisciplines().Any(d => d.Name == "Pascal" && d.Owner == "OlehVukladachenko"))
			{
				return;
			}
			
			ImportExportDiscipline importer = new ImportExportDiscipline(storage);
         importer.Import(path);

		}

      public static void SeleniumTestingSystemTestDiscipline(IDisciplineStorage storage, string path)
      {
         if (storage.GetDisciplines().Any(d => d.Name == "Testing discipline" && d.Owner == "SeleniumTeacher"))
         {
            return;
         }

         ImportExportDiscipline importer = new ImportExportDiscipline(storage);
         importer.Import(path);

      }

      public static void GenerateAllDisciplines(IDisciplineStorage storage)
      {
         ImportExportDiscipline importer = new ImportExportDiscipline(storage);

         var path = (new System.Uri(Assembly.GetExecutingAssembly().CodeBase)).AbsolutePath;
         path = path.Replace("IUDICO.LMS/Plugins/IUDICO.DataGenerator.DLL", "IUDICO.DataGenerator/Content/Disciplines/");

         if (Directory.Exists(path))
         {
           var files = Directory.GetFiles(path, "*.zip");

            foreach (var file in files)
            {
               var name = Path.GetFileNameWithoutExtension(file);

               if (storage.GetDisciplines().Any(d => d.Name == name && d.Owner == "prof3"))
               {
                  return;
               }

               importer.Import(file);
            }
         }
      }
	}
}
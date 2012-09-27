using System;
using System.Collections.Generic;
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

	}
}
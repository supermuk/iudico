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
			//var cacheProvider = container.Resolve<ICacheProvider>();
			//var databaseStorage = new FakeDatabaseDisciplineStorage(container.Resolve<ILmsService>(), "OlehVukladachenko");
			//var storage = new CachedDisciplineStorage(databaseStorage, cacheProvider);

			if (storage.GetDisciplines().Where(d => d.Name == "Pascal" && d.Owner == "OlehVukladachenko").Count() > 0)
			{
				return;
			}
			
			ImportExportDiscipline importer = new ImportExportDiscipline(storage);
			MethodInfo dynMethod = importer.GetType().GetMethod("Deserialize", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.InvokeMethod);
			dynMethod.Invoke(importer, new object[] { path });

		}

	}
}
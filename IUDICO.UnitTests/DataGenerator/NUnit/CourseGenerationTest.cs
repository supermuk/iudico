using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using NUnit.Framework;
using IUDICO.UnitTests.DataGenerator.Fakes;
using IUDICO.CourseManagement.Models.Storage;
using IUDICO.DataGenerator.Models.Generators;
using IUDICO.Common.Models.Caching;

namespace IUDICO.UnitTests.DataGenerator.NUnit
{
	[TestFixture]
	public class CourseGenerationTest
	{
		protected FakeCourseStorage tests = FakeCourseStorage.GetInstance();
		protected FakeCacheProvider cache = FakeCacheProvider.GetInstance();
		protected string path = Path.Combine(ConfigurationManager.AppSettings.Get("PathToIUDICO.UnitTests"), @"IUDICO.UnitTests\DataGenerator\Data\Courses\Pascal");

		protected ICourseStorage Storage
		{
			get
			{
				return this.tests.Storage;
			}
		}

		protected ICacheProvider CacheProvider
		{
			get
			{
				return this.cache.CacheProvider;
			}
		}

		[SetUp]
		public void Init()
		{
			this.tests.ClearTables();
		}

		[TearDown]
		public void OnTearDown()
		{
			Storage.DeleteCourses(Storage.GetCourses().Select(c => c.Id).ToList());
			this.DeleteFiles();
		}

		private void DeleteFiles()
		{
			var files = Directory.GetFiles(Path.Combine(ConfigurationManager.AppSettings.Get("PathToIUDICO.UnitTests"), @"IUDICO.UnitTests\DataGenerator\Data\Tests"), "*.zip");

			foreach (var file in files)
			{
				File.Delete(file);
			}
		}

		[Test]
		public void Test1()
		{
			var files = Directory.GetFiles(path, "*.zip");

			foreach (var file in files)
			{
				var name = Path.GetFileNameWithoutExtension(file);

				Assert.AreEqual(0, Storage.GetCourses().Where(c => c.Name == name && c.Owner == "OlehVukladachenko").Count());
			}

			CourseGenerator.PascalCourse(Storage, CacheProvider, path);
			foreach (var file in files)
			{
				var name = Path.GetFileNameWithoutExtension(file);

				Assert.AreEqual(1, Storage.GetCourses().Where(c => c.Name == name && c.Owner == "OlehVukladachenko").Count());
			}
			this.DeleteFiles();
			CourseGenerator.PascalCourse(Storage, CacheProvider, path);
			foreach (var file in files)
			{
				var name = Path.GetFileNameWithoutExtension(file);

				Assert.AreEqual(1, Storage.GetCourses().Where(c => c.Name == name && c.Owner == "OlehVukladachenko").Count());
			}
		}

	}
}

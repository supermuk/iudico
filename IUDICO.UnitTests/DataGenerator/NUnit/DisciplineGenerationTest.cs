using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using IUDICO.DisciplineManagement.Models.Storage;
using IUDICO.DataGenerator.Models.Generators;
using IUDICO.DataGenerator.Models.Storage;
using IUDICO.UnitTests.DataGenerator.Fakes;
using NUnit.Framework;

namespace IUDICO.UnitTests.DataGenerator.NUnit
{
   public class DisciplineGenerationTest
   {
      private FakeDisciplineStorage tests = FakeDisciplineStorage.GetInstance();
      protected string path = Path.Combine(ConfigurationManager.AppSettings.Get("PathToIUDICO.UnitTests"), @"IUDICO.UnitTests\DataGenerator\Data\Courses\Pascal\Pascal.disc");

      public IDisciplineStorage Storage
      {
          get
          {
              return this.tests.CachedStorage;
          }
      }

      [SetUp]
      public void Init()
      {
          this.tests.ClearTables();
      }

      [Test]
      [Ignore]
      public void Test1()
      {
         Assert.AreEqual(0, this.Storage.GetDisciplines().Where(d => d.Name == "Pascal" && d.Owner == "OlehVukladachenko").Count());

         DisciplineGenerator.PascalDiscipline(this.Storage, this.path);

         Assert.AreEqual(1, this.Storage.GetDisciplines().Where(d => d.Name == "Pascal" && d.Owner == "OlehVukladachenko").Count());
      }
   }
}

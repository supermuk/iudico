using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using Moq.Protected;
using IUDICO.DisciplineManagement.Models.Storage;
using IUDICO.DisciplineManagement.Models;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Services;
using IUDICO.Common.Models.Caching.Provider;
using IUDICO.Common.Models.Shared;

namespace IUDICO.UnitTests.DataGenerator.Fakes
{
   public class FakeDisciplineStorage
   {
      #region Private members
      private static FakeDisciplineStorage instance;

      private Mock<CachedDisciplineStorage> mockCachedDisciplineStorage;
      private Mock<DatabaseDisciplineStorage> mockDatabaseDisciplineStorage;
      private Mock<IDataContext> mockContext;
      private Mock<ILmsService> mockLmsService;
      private Mock<HttpCache> mockCasheProvider;

      #endregion

      #region Properties
      public IDataContext DataContext
      {
         get
         {
            return this.mockContext.Object;
         }
      }

      public IDisciplineStorage CachedStorage
      {
         get
         {
            return this.mockCachedDisciplineStorage.Object;
         }
      }

      public IDisciplineStorage DatabaseStorage
      {
         get
         {
            Console.Error.WriteLine("wtf?");
            return this.mockDatabaseDisciplineStorage.Object;
         }
      }

      public ILmsService LmsService
      {
         get
         {
            return this.mockLmsService.Object;
         }
      }

      #endregion

      public static FakeDisciplineStorage GetInstance()
      {
         Console.Error.WriteLine("inside get instance");
         return instance ?? (instance = new FakeDisciplineStorage());
      }

      private FakeDisciplineStorage()
      {
         Console.Error.WriteLine("inside constructor");

         this.mockContext = new Mock<IDataContext>();
         this.mockLmsService = new Mock<ILmsService>();
         this.mockDatabaseDisciplineStorage = new Mock<DatabaseDisciplineStorage>(this.mockLmsService.Object);
         this.mockCasheProvider = new Mock<HttpCache>();
         this.mockCachedDisciplineStorage = new Mock<CachedDisciplineStorage>(this.mockDatabaseDisciplineStorage.Object, this.mockCasheProvider.Object);


         this.mockDatabaseDisciplineStorage.Protected().Setup<IDataContext>("GetDbContext").Returns(this.mockContext.Object);
         this.mockDatabaseDisciplineStorage.Setup(d => d.GetCurrentUser()).Returns(new User { Username = "OlehVukladachenko" });
         this.Setup();
      }

      public void Setup()
      {
         Console.Error.WriteLine("inside setup");

         this.ClearTables();
      }

      public void ClearTables()
      {
         Console.Error.WriteLine("inside clear tables");

         var disciplines = new Discipline[] { };
         var chapters = new Chapter[] { };
         var topics = new Topic[] { };

         this.mockContext.SetupGet(c => c.Disciplines).Returns(new MemoryTable<Discipline>(disciplines));
         this.mockContext.SetupGet(c => c.Chapters).Returns(new MemoryTable<Chapter>(chapters));
         this.mockContext.SetupGet(c => c.Topics).Returns(new MemoryTable<Topic>(topics));

      }
   }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using IUDICO.Common.Models.Caching;

namespace IUDICO.UnitTests.DataGenerator.Fakes
{
   public class FakeCacheProvider
   {
      protected static FakeCacheProvider provider;

      protected Mock<ICacheProvider> mockCacheProvider;

      public ICacheProvider CacheProvider
      {
         get;
         protected set;
      }

      public static FakeCacheProvider GetInstance()
      {
         return provider ?? (provider = new FakeCacheProvider());
      }

      protected FakeCacheProvider()
      {
         mockCacheProvider = new Mock<ICacheProvider>();

         this.Setup();

         CacheProvider = mockCacheProvider.Object;
      }

      protected void Setup()
      {
         this.mockCacheProvider.Setup(c => c.Invalidate(It.IsAny<string>(), It.IsAny<string>()));
      }
   }
}

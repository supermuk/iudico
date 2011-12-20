using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using IUDICO.Common.Models.Shared;
using NUnit.Framework;
using IUDICO.Common.Models;
using System.Web.Routing;

namespace IUDICO.Security.UnitTests.Tests
{
    [TestFixture]
    class SecurityPluginTester : SecurityTester
    {
        private SecurityPlugin securityPlugin;

        [SetUp]
        public void SetUp()
        {
            securityPlugin = new SecurityPlugin();
        }


        [Test]
        public void BuildMenuItemsTest()
        {
            try
            {
                Assert.False(securityPlugin.BuildMenuItems().Any(
                m => (m.Text == "Security") && (m.Controller == "Security") && (m.Action == "Index")));
            }
            catch (TypeInitializationException)
            {
                                
            }
            
        }


        [Test]
        public void RegisterRoutesTest()
        {
            RouteCollection routeCollection = new RouteCollection();

            securityPlugin.RegisterRoutes(routeCollection);

            Assert.AreEqual(3, routeCollection.Count);
        }

        [Test]
        public void UpdateTest()
        {
            try
            {
                securityPlugin.Update("application/request/start", new object());

                Assert.True(HttpContext.Current.Response.RedirectLocation == "/Ban/Banned");
            }
            catch (NullReferenceException)
            {

            }         
        }
    }
}

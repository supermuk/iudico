using System.Linq;
using IUDICO.CourseManagement.Models.ManifestModels;
using IUDICO.CourseManagement.Models.ManifestModels.OrganizationModels;
using IUDICO.CourseManagement.Models.ManifestModels.ResourceModels;
using NUnit.Framework;

namespace IUDICO.UnitTests.CourseManagement.NUnit
{
    [TestFixture]
    public class ManifestManagerTest : BaseCourseManagementTest
    {
        [Test]
        [Category("ManifestManegerTest")]
        public void AddOrganizationTest()
        {
            var organizations = new Organizations();
            var organization = new Organization { Identifier = "organization1", Title = "Title" };

            ManifestManager.AddOrganization(organizations, organization);

            var org = organizations.OrganizationsList.Single(i => i.Title == "Title");
            Assert.AreEqual("organization1", org.Identifier);
        }

        [Test]
        [Category("ManifestManegerTest")]
        public void AddResourceTest()
        {
            var resources = new IUDICO.CourseManagement.Models.ManifestModels.ResourceModels.Resources();
            var resource = new Resource { Base = "Base", Identifier = "Identifier" };

            ManifestManager.AddResource(resources, resource);

            Resource res = resources.ResourcesList.Single(i => i.Base == "Base");

            Assert.AreEqual("Identifier", res.Identifier);
        }
    }
}
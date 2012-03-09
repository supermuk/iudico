using System.Linq;
using IUDICO.CourseManagement.Models.ManifestModels;
using IUDICO.CourseManagement.Models.ManifestModels.OrganizationModels;
using IUDICO.CourseManagement.Models.ManifestModels.ResourceModels;
using NUnit.Framework;

namespace IUDICO.UnitTests.CourseManagement.NUnit
{
    [TestFixture]
    public class ManifestManagerTest
    {
        [Test]
        [Category("ManifestManegerTest")]
        public void AddOrganizationTest()
        {
            Organizations organizations = new Organizations();
            Organization organization = new Organization {Identifier = "organization1", Title = "Title"};

            ManifestManager.AddOrganization(organizations, organization);

            Organization org = organizations._Organizations.Single(i => i.Title == "Title");
            Assert.AreEqual("organization1", org.Identifier);
        }

        [Test]
        [Category("ManifestManegerTest")]
        public void AddResourceTest()
        {
            IUDICO.CourseManagement.Models.ManifestModels.ResourceModels.Resources resources =
                new IUDICO.CourseManagement.Models.ManifestModels.ResourceModels.Resources();
            Resource resource = new Resource {Base = "Base", Identifier = "Identifier"};

            ManifestManager.AddResource(resources, resource);

            Resource res = resources._Resources.Single(i => i.Base == "Base");

            Assert.AreEqual("Identifier", res.Identifier);
        }
    }
}
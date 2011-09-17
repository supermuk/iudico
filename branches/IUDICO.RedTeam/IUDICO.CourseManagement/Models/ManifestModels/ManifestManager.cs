using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using IUDICO.CourseManagement.Models.ManifestModels.OrganizationModels;
using IUDICO.CourseManagement.Models.ManifestModels.ResourceModels;
using File = IUDICO.CourseManagement.Models.ManifestModels.ResourceModels.File;

namespace IUDICO.CourseManagement.Models.ManifestModels
{
    public class ManifestManager
    {
        private int _ItemsCount = 0;
        private int _OrganizationsCount = 0;
        private int _ResourcesCount = 0;

        public static void Serialize(Manifest manifest, StreamWriter writer)
        {
            var xs = new XmlSerializer(typeof(Manifest));

            var xsn = new XmlSerializerNamespaces();
            xsn.Add(SCORM.Adlcp, SCORM.AdlcpNamespaceV1P3);
            xsn.Add(SCORM.Imsss, SCORM.ImsssNamespace);
            xsn.Add(SCORM.Adlseq, SCORM.AdlseqNamespace);
            xsn.Add(SCORM.Adlnav, SCORM.AdlnavNamespace);
            xsn.Add(SCORM.Imsss, SCORM.ImsssNamespace);

            xs.Serialize(writer, manifest, xsn);
        }

        public Item CreateItem(string resourceId = null)
        {
            return new Item()
                       {
                           Identifier = ConstantStrings.ItemIdPrefix + _ItemsCount++,
                           IdentifierRef = resourceId,
                       };
        }

        public Organization CreateOrganization()
        {
            return new Organization()
                       {
                           Identifier = ConstantStrings.OrganizationIdPrefix + _OrganizationsCount++
                       };
        }

        public Resource CreateResource(ScormType type, List<File> files = null, IEnumerable<string> dependOnResourcesIds = null)
        {
            var resource = new Resource()
                               {
                                   Identifier = ConstantStrings.ResourceIdPrefix + _ResourcesCount++,
                                   ScormType = type
                               };
            if(files != null)
            {
                resource.Files = files;
            }

            if (dependOnResourcesIds != null)
            {
                foreach (var resId in dependOnResourcesIds)
                {
                    resource.Dependencies.Add(new Dependency(resId));
                }
            }

            return resource;
        }

        public static Item AddItem(Item parent, Item child)
        {
            if (parent.IdentifierRef != null)
            {
                throw new Exception("Can't add child item to leaf item");
            }

            parent.Items.Add(child);

            return parent;
        }

        public static Organizations AddOrganization(Organizations organizations, Organization organization)
        {
            organizations._Organizations.Add(organization);
            return organizations;
        }

        public static Resources AddResource(Resources resources, Resource resource)
        {
            resources._Resources.Add(resource);
            return resources;
        }

    }
}

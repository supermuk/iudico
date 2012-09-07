using System.IO;
using System.Linq;
using System.Xml.Serialization;
using IUDICO.Common.Models;
using IUDICO.Common.Models.Shared;
using IUDICO.CourseManagement.Models.ManifestModels.OrganizationModels;
using IUDICO.CourseManagement.Models.ManifestModels.ResourceModels;
using IUDICO.CourseManagement.Models.ManifestModels.SequencingModels;
using IUDICO.CourseManagement.Models.Storage;
using File = System.IO.File;

namespace IUDICO.CourseManagement.Models.ManifestModels
{
    public class Importer
    {
        protected Manifest manifest;
        protected Course course;
        protected ICourseStorage courseStorage;
        protected string coursePath;
        protected string courseTempPath;

        public Importer(Manifest manifest, Course course, ICourseStorage courseStorage)
        {
            this.manifest = manifest;
            this.course = course;
            this.courseStorage = courseStorage;
            this.coursePath = this.courseStorage.GetCoursePath(this.course.Id);
            this.courseTempPath = this.courseStorage.GetCourseTempPath(this.course.Id);
        }

        public void Import()
        {
            foreach (var item in this.manifest.Organizations[this.manifest.Organizations.Default].Items)
            {
                this.ProcessItem(item, null);
            }
        }

        protected void ProcessItem(Item item, Node parent)
        {
            var xml = new XmlSerializer(typeof(Sequencing));

            var node = new Node
            {
                CourseId = this.course.Id,
                Name = item.Title,
                IsFolder = item.IsParent,
                ParentId = (parent != null ? (int?)parent.Id : null),
                Sequencing = xml.SerializeToXElemet(item.Sequencing)
            };

            this.courseStorage.AddNode(node);

            if (item.IsParent && item.Items.Count > 0)
            {
                foreach (var subitem in item.Items)
                {
                    this.ProcessItem(subitem, node);
                }
            }
            else
            {   
                if (item.IdentifierRef != null)
                {
                    var resource = this.manifest.Resources.ResourcesList.Where(r => r.Identifier == item.IdentifierRef).FirstOrDefault();

                    if (resource != null)
                    {
                        this.ProcessResource(node, resource);

                        return;
                    }
                }
            }
        }
        
        protected void ProcessResource(Node node, Resource resource)
        {
            var nodePath = this.courseStorage.GetNodePath(node.Id);
            var coursePath = this.courseStorage.GetCoursePath(node.CourseId);

            File.Copy(Path.Combine(this.courseTempPath, resource.Href), nodePath + ".html", true);

            foreach (var file in resource.Files)
            {

                if (file.Href != resource.Href)
                {
                   var path = Path.Combine(coursePath, file.Href);
                   if(!Directory.GetParent(path).Exists)
                   {
                      Directory.CreateDirectory(Directory.GetParent(path).ToString());
                   }

                   //var fileHref = file.Href.Replace("/", "\\");

                   File.Copy(Path.Combine(this.courseTempPath, file.Href), Path.Combine(coursePath, file.Href));
                }
            }

            foreach (var dependency in resource.Dependencies)
            {
                var depResource = this.manifest.Resources.ResourcesList.Where(r => r.Identifier == dependency.IdentifierRef).FirstOrDefault();

                if (depResource != null)
                {
                    this.manifest.Resources.ResourcesList.Remove(depResource);

                    this.ProcessDependencyResource(node, depResource);
                }
            }
        }

        protected void ProcessDependencyResource(Node node, Resource resource)
        {
            foreach (var file in resource.Files)
            {
                if (!File.Exists(Path.Combine(this.coursePath, file.Href)))
                {
                   var path = Path.Combine(this.coursePath, file.Href);
                   if (!Directory.GetParent(path).Exists)
                   {
                      Directory.CreateDirectory(Directory.GetParent(path).ToString());
                   }
                   
                   File.Copy(Path.Combine(this.courseTempPath, file.Href), Path.Combine(this.coursePath, file.Href));
                }
            }

            foreach (var dependency in resource.Dependencies)
            {
                var depResource = this.manifest.Resources.ResourcesList.Where(r => r.Identifier == dependency.IdentifierRef).FirstOrDefault();

                if (depResource != null)
                {
                    this.manifest.Resources.ResourcesList.Remove(depResource);

                    this.ProcessDependencyResource(node, depResource);
                }
            }
        }
    }
}
﻿using System.IO;
using System.Linq;
using IUDICO.Common.Models;
using IUDICO.CourseManagement.Models.ManifestModels.OrganizationModels;
using IUDICO.CourseManagement.Models.ManifestModels.ResourceModels;
using IUDICO.CourseManagement.Models.Storage;
using File = System.IO.File;

namespace IUDICO.CourseManagement.Models.ManifestModels
{
    public class Importer
    {
        protected Manifest _Manifest;
        protected Course _Course;
        protected ICourseStorage _CourseStorage;
        protected string _CoursePath;
        protected string _CourseTempPath;

        public Importer(Manifest manifest, Course course, ICourseStorage courseStorage)
        {
            _Manifest = manifest;
            _Course = course;
            _CourseStorage = courseStorage;
            _CoursePath = _CourseStorage.GetCoursePath(_Course.Id);
            _CourseTempPath = _CourseStorage.GetCourseTempPath(_Course.Id);
        }

        public void Import()
        {
            foreach (var item in _Manifest.Organizations[_Manifest.Organizations.Default].Items)
            {
                ProcessItem(item, null);
            }
        }

        protected void ProcessItem(Item item, Node parent)
        {
            var node = new Node
            {
                CourseId = _Course.Id,
                Name = item.Title,
                IsFolder = item.IsParent,
                ParentId = (parent != null ? (int?)parent.Id : null)
            };

            _CourseStorage.AddNode(node);

            if (item.IsParent && item.Items.Count > 0)
            {
                foreach (var subitem in item.Items)
                {
                    ProcessItem(subitem, node);
                }
            }
            else
            {   
                if (item.IdentifierRef != null)
                {
                    var resource = _Manifest.Resources._Resources.Where(r => r.Identifier == item.IdentifierRef).FirstOrDefault();

                    if (resource != null)
                    {
                        ProcessResource(node, resource);

                        return;
                    }
                }

                var path = _CourseStorage.GetNodePath(node.Id) + ".html";

                File.Create(path);
            }
        }
        
        protected void ProcessResource(Node node, Resource resource)
        {
            var nodePath = _CourseStorage.GetNodePath(node.Id);
            var nodeParentPath = _CourseStorage.GetNodePath(node.ParentId.Value);

            File.Copy(Path.Combine(_CourseTempPath, resource.Href), _CourseStorage.GetNodePath(node.Id) + ".html");

            foreach (var file in resource.Files)
            {
                if (file.Href != resource.Href)
                {
                    File.Copy(Path.Combine(_CourseTempPath, file.Href), Path.Combine(nodeParentPath, file.Href));
                }
            }

            foreach (var dependency in resource.Dependencies)
            {
                var depResource = _Manifest.Resources._Resources.Where(r => r.Identifier == dependency.IdentifierRef).FirstOrDefault();

                if (depResource != null)
                {
                    _Manifest.Resources._Resources.Remove(depResource);

                    ProcessDependencyResource(node, depResource);
                }
            }
        }

        protected void ProcessDependencyResource(Node node, Resource resource)
        {
            foreach (var file in resource.Files)
            {
                File.Copy(Path.Combine(_CourseTempPath, file.Href), Path.Combine(_CoursePath, file.Href));
            }

            foreach (var dependency in resource.Dependencies)
            {
                var depResource = _Manifest.Resources._Resources.Where(r => r.Identifier == dependency.IdentifierRef).FirstOrDefault();

                if (depResource != null)
                {
                    _Manifest.Resources._Resources.Remove(depResource);

                    ProcessDependencyResource(node, depResource);
                }
            }
        }
    }
}
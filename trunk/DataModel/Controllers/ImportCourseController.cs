using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI.WebControls;
using System.Xml;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.ImportManagers;

namespace IUDICO.DataModel.Controllers
{
    public class ImportCourseController : ControllerBase
    {
        public TextBox Name { get; set; }

        public TextBox Description { get; set; }

        public FileUpload CourseUpload { get; set; }

        public TreeView CourseTree{get; set;}
        
        [PersistantField]
        private bool isCoursePrepared;

        private static readonly ProjectPaths projectPaths = new ProjectPaths();

        private static readonly DeletedItem deletedItems = new DeletedItem();


        public void importButton_Click(object sender, EventArgs e)
        {
            if (CourseUpload.HasFile || isCoursePrepared)
            {
                if (!isCoursePrepared)
                    PrepareCourseAndBuildTree();
                
                Import();
            }
        }

        public void editButton_Click(object sender, EventArgs e)
        {

        }

        public void openButton_Click(object sender, EventArgs e)
        {
            if (CourseUpload.HasFile)
            {
                PrepareCourseAndBuildTree();
            }
        }

        public void deleteButton_Click(object sender, EventArgs e)
        {
            if (CourseTree.SelectedNode.Value.Contains("Theme"))
            {
                deletedItems.DeletedThemes.Add(CourseTree.SelectedNode.Text);
            }
            else
            {
                deletedItems.DeletedPages.Add(CourseTree.SelectedNode.Value);
            }
            CourseTree.SelectedNode.Parent.ChildNodes.Remove(CourseTree.SelectedNode);
        }

        private void PrepareCourseAndBuildTree()
        {
            PrepareCourse();

            if (Name.Text.Equals(string.Empty))
                Name.Text = Path.GetFileNameWithoutExtension(projectPaths.PathToCourseZipFile);
            
            BuildTree();

        }

        private void Import()
        {
            CourseManager.Import(projectPaths, Name.Text, Description.Text, deletedItems);
        }

        private void PrepareCourse()
        {
            InitializePaths(CourseUpload.FileName);
            CourseUpload.SaveAs(projectPaths.PathToCourseZipFile);
            Zipper.ExtractZipFile(projectPaths.PathToCourseZipFile, projectPaths.PathToTempCourseFolder);
            
            isCoursePrepared = true;
        }

        private static void InitializePaths(string fileName)
        {
            projectPaths.PathToTemp = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());

            Directory.CreateDirectory(projectPaths.PathToTemp);

            projectPaths.PathToCourseZipFile = Path.Combine(projectPaths.PathToTemp, fileName);
            projectPaths.PathToTempCourseFolder = Path.Combine(projectPaths.PathToTemp,
                                                               Path.GetFileNameWithoutExtension(fileName));
        }

        private static XmlDocument GetImsmanifest(string pathToTempCourseFolder)
        {
            var imsmanifest = new XmlDocument();
            imsmanifest.Load(Path.Combine(pathToTempCourseFolder, "imsmanifest.xml"));
            return imsmanifest;
        }

        private void BuildTree()
        {
            CourseTree.Nodes.Add(new TreeNode(Name.Text));


            XmlDocument imsmanifest = GetCourseXml();

            if (imsmanifest.DocumentElement != null)
            {
                XmlNode document = imsmanifest.DocumentElement.FirstChild;

                foreach (XmlNode node in document)
                {
                    AddThemaToTree(node, CourseTree.Nodes[0]);
                }
            }

            CourseTree.ExpandAll();
        }

        private static XmlDocument GetCourseXml()
        {
            Zipper.ExtractZipFile(projectPaths.PathToCourseZipFile, projectPaths.PathToTempCourseFolder);
            File.Delete(projectPaths.PathToCourseZipFile);

            return GetImsmanifest(projectPaths.PathToTempCourseFolder);
        }

        private static void AddThemaToTree(XmlNode courseXml, TreeNode treeNode)
        {
            foreach (XmlNode node in courseXml.ChildNodes)
            {
                if (XmlUtility.isItem(node))
                {
                    var currentTreeNode = new TreeNode {Text = XmlUtility.getIdentifier(node), Value = XmlUtility.getIdentifier(node) + "Theme"};

                    treeNode.ChildNodes.Add(currentTreeNode);
                    AddPageToTree(node, currentTreeNode);
                }
            }
        }

        private static void AddPageToTree(XmlNode themaXml, TreeNode treeNode)
        {
            foreach (XmlNode node in themaXml.ChildNodes)
            {
                if (node != null && XmlUtility.isItem(node))
                {
                    if (XmlUtility.isPage(node))
                    {
                        var currentTreeNode = new TreeNode
                                                  {
                                                      Text = XmlUtility.getIdentifier(node),
                                                      Value = XmlUtility.getIdentifierRef(node)
                                                  };

                        if (XmlUtility.isPractice(node))
                        {
                            treeNode.ChildNodes.Add(currentTreeNode);
                        }
                        if (XmlUtility.isTheory(node))
                        {
                            treeNode.ChildNodes.Add(currentTreeNode);
                        }
                    }
                    else if (XmlUtility.isChapter(node))
                    {
                        AddPageToTree(node, treeNode);
                    }
                }
            }
        }
    }

    public class DeletedItem
    {
        private readonly List<string> deletedPages = new List<string>();
        private readonly List<string> deletedThemes = new List<string>();

        public List<string> DeletedPages
        {
            get { return deletedPages; }
        }

        public List<string> DeletedThemes
        {
            get { return deletedThemes; }
        }
    }
}

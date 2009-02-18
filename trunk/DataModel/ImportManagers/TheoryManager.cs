using System.IO;
using System.Xml;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.DB;

namespace IUDICO.DataModel.ImportManagers
{
    public class TheoryManager
    {
        public static void Import(XmlNode node, int themeId, ProjectPaths projectPaths)
        {
            string fileName  = Path.Combine(projectPaths.PathToTempCourseFolder, XmlUtility.getIdentifierRef(node) + FileExtentions.Html);

            byte[] file = FilesManager.GetByteFile(fileName);
            int id = Store(themeId, XmlUtility.getIdentifier(node), file);
            FilesManager.StoreAllPageFiles(id, fileName);
        }

        private static int Store(int themaRef, string name, byte[] file)
        {
            TblPages p = new TblPages
            {
                ThemeRef = themaRef,
                PageName = name,
                PageFile = file,
                PageTypeRef = ((int)FX_PAGETYPE.Theory)
            };

            ServerModel.DB.Insert(p);

            return p.ID;
        }
    }
}
using System.Data.Linq;
using System.IO;
using System.Xml;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.DB;

namespace IUDICO.DataModel.ImportManagers
{
    public class TheoryManager : PageManager
    {
        public static void Import(XmlNode node, int themeId, ProjectPaths projectPaths)
        {
            string pageName = XmlUtility.getIdentifierRef(node) + FileExtentions.Html;
            string fileName  = Path.Combine(projectPaths.PathToTempCourseFolder, pageName);

            byte[] file = GetByteFile(fileName);
            int id = Store(themeId, pageName, file);
            StoreFiles(id, fileName);
        }

        private static int Store(int themaRef, string name, byte[] file)
        {
            TblPages p = new TblPages
            {
                ThemeRef = themaRef,
                PageName = name,
                PageFile = new Binary(file),
                PageTypeRef = ((int)PageTypeEnum.Theory)
            };

            ServerModel.DB.Insert(p);

            return p.ID;
        }
    }
}
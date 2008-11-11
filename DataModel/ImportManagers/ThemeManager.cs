using System.Xml;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.Dao;
using IUDICO.DataModel.Dao.Entity;

namespace IUDICO.DataModel.ImportManagers
{
    public class ThemeManager
    {
        public static void Import(XmlNode chapter, int courseId, ProjectPaths projectPaths)
        {
            var ce = new ThemeEntity(courseId, XmlUtility.getIdentifier(chapter), XmlUtility.isControlChapter(chapter));
           
            SearchPages(chapter,  Store(ce), projectPaths);
        }

        private static void SearchPages(XmlNode chapter, int themeId, ProjectPaths projectPaths)
        {
            foreach (XmlNode node in chapter.ChildNodes)
            {
                if (node != null && XmlUtility.isItem(node))
                {
                    if (XmlUtility.isPage(node))
                    {
                        if (XmlUtility.isPractice(node))
                        {
                            PracticeManager.Import(node, themeId, projectPaths);
                        }
                        if (XmlUtility.isTheory(node))
                        {
                            TheoryManager.Import(node, themeId, projectPaths);
                        }
                    }
                    else if (XmlUtility.isChapter(node))
                    {
                        SearchPages(node, themeId, projectPaths);
                    }
                }
            }
        }

        private static int Store(ThemeEntity ce)
        {
            return DaoFactory.ThemeDao.Insert(ce);
        }
    }
}
using System.Xml;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.Dao;
using IUDICO.DataModel.Dao.Entity;
using IUDICO.DataModel.ImportManagers;

namespace IUDICO.DataModel.ImportManagers
{
    public class ThemeManager
    {
        public static void Import(XmlNode chapter, int courseId, ProjectPaths projectPaths)
        {
            var ce = new ThemeEntity(courseId, XmlUtility.getIdentifier(chapter), XmlUtility.isControlChapter(chapter));
            Store(ce);

            SearchPages(chapter, ce.Id, projectPaths);
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

        private static void Store(ThemeEntity ce)
        {
            DaoFactory.ChapterDao.Insert(ce);
        }
    }
}
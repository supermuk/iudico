using System.Xml;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.DB;

namespace IUDICO.DataModel.ImportManagers
{
    public class ThemeManager
    {
        public static void Import(XmlNode theme, int courseId, ProjectPaths projectPaths)
        {
            int id = Store(courseId, XmlUtility.getIdentifier(theme), XmlUtility.isControlChapter(theme));

            SearchPages(theme, id, projectPaths);
        }

        private static void SearchPages(XmlNode thema, int themeId, ProjectPaths projectPaths)
        {
            foreach (XmlNode node in thema.ChildNodes)
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

        private static int Store(int courseRef, string name, bool isControl)
        {
            var t = new TblThemes
            {
                CourseRef = courseRef,
                Name = name,
                IsControl = isControl
            };

            ServerModel.DB.Insert(t);

            return t.ID;
        }
    }
}
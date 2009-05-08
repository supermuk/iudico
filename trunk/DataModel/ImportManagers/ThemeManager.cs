using System.Xml;
using IUDICO.DataModel.Common.ImportUtils;
using IUDICO.DataModel.DB;

namespace IUDICO.DataModel.ImportManagers
{
    public class ThemeManager
    {
        public static void Import(XmlNode theme, int courseId, ProjectPaths projectPaths)
        {
            int id = Store(courseId, XmlUtility.GetIdentifier(theme), XmlUtility.IsControlChapter(theme));

            SearchPages(theme, id, projectPaths);
        }

        private static void SearchPages(XmlNode thema, int themeId, ProjectPaths projectPaths)
        {
            foreach (XmlNode node in thema.ChildNodes)
            {
                if (node != null && XmlUtility.IsItem(node))
                {
                    if (XmlUtility.IsPage(node))
                    {
                            if (XmlUtility.IsPractice(node))
                            {
                                PracticeManager.Import(node, themeId, projectPaths);
                            }
                            if (XmlUtility.IsTheory(node))
                            {
                                TheoryManager.Import(node, themeId, projectPaths);
                            }
                    }
                    else if (XmlUtility.IsChapter(node))
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
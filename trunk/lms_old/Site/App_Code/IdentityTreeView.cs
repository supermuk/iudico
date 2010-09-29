using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using IUDICO.DataModel.Common;
using IUDICO.DataModel.DB;

namespace IUDICO.DataModel.Controllers
{
    public class IdentityTreeView : TreeView
    {
        protected override TreeNode CreateNode()
        {
            return new IdendtityNode();
        }

        public override object DataSource
        {
            get
            {
                return base.DataSource;
            }
            set
            {
                try
                {
                    base.DataSource = value;
                }
                catch
                {
                    IList<TblCourses> coursesData = value as IList<TblCourses>;
                    IList<TblCurriculums> curriculumssData = value as IList<TblCurriculums>;

                    Nodes.Clear();
                    if (coursesData != null)
                    {
                        foreach (TblCourses course in coursesData)
                        {
                            IdendtityNode courseNode = new IdendtityNode(course);
                            foreach (TblThemes theme in TeacherHelper.ThemesOfCourse(course))
                            {
                                IdendtityNode themeNode = new IdendtityNode(theme);
                                courseNode.ChildNodes.Add(themeNode);
                            }
                            Nodes.Add(courseNode);
                        }
                    }
                    else
                    {
                        if (curriculumssData != null)
                        {
                            foreach (TblCurriculums curriculum in curriculumssData)
                            {
                                IdendtityNode curriculumNode = new IdendtityNode(curriculum);
                                foreach (TblStages stage in TeacherHelper.StagesOfCurriculum(curriculum))
                                {
                                    IdendtityNode stageNode = new IdendtityNode(stage);
                                    foreach (TblThemes theme in TeacherHelper.ThemesOfStage(stage))
                                    {
                                        IdendtityNode themeNode = new IdendtityNode(theme);
                                        stageNode.ChildNodes.Add(themeNode);
                                    }
                                    curriculumNode.ChildNodes.Add(stageNode);
                                }
                                Nodes.Add(curriculumNode);
                            }

                        }
                        else
                        {
                            throw new Exception("Only course and curriculum lists are supported");
                        }
                    }
                    CollapseAll();
                }
            }

        }
    }
}



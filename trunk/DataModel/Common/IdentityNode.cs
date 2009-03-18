using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.DB.Base;

namespace IUDICO.DataModel.Common
{
    public enum NodeType { Curriculum, Stage, Theme, Course };

    public class IdendtityNode : TreeNode
    {
        int id;
        NodeType type;

        public int ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
        public NodeType Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }

        public IdendtityNode(string text)
            : base(text)
        {
        }

        public IdendtityNode(string text, int id)
            : this(text)
        {
            Value = id.ToString();
            ID = id;
        }

        public IdendtityNode(string text, int id, string description)
            : this(text, id)
        {
            ToolTip = description;
        }

        public IdendtityNode(TblCourses course)
            : this(course.Name, course.ID, course.Description)
        {
            Type = NodeType.Course;
            
        }

        public IdendtityNode(TblCurriculums curriculum)
            : this(curriculum.Name, curriculum.ID, curriculum.Description)
        {
            Type = NodeType.Curriculum;
            
        }

        public IdendtityNode(TblStages stage)
            : this(stage.Name, stage.ID, stage.Description)
        {
            Type = NodeType.Stage;
        }

        public IdendtityNode(TblThemes theme)
            : this(theme.Name, theme.ID)
        {
            Type = NodeType.Theme;
        }

        public IdendtityNode()
            : base() { }

        protected override object SaveViewState()
        {
            object[] newState = { base.SaveViewState(), id, Enum.GetName(typeof(NodeType), type) };
            return newState;
        }

        protected override void LoadViewState(object state)
        {
            object[] newState = state as object[];
            base.LoadViewState(newState[0]);
            id = (int)newState[1];
            type = (NodeType)Enum.Parse(typeof(NodeType), newState[2].ToString());
        }

        public static Type GetTable(NodeType type)
        {
            switch (type)
            {
                case NodeType.Course:
                    {
                        return typeof(TblCourses);
                    }
                case NodeType.Curriculum:
                    {
                        return typeof(TblCurriculums);
                    }
                case NodeType.Stage:
                    {
                        return typeof(TblStages);
                    }
                case NodeType.Theme:
                    {
                        return typeof(TblThemes);
                    }
            }
            return null;
        }

        
    }
}

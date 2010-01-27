using System;
using System.Collections.Generic;
using System.Collections;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.DB.Base;

namespace IUDICO.DataModel.Common.Cmi
{
    class Score: CmiFirstLevelCollectionElement
    {
        #region Cmi Variables

        private static Dictionary<string, CmiElement> elements = new Dictionary<string, CmiElement>
        {
            {"scaled", new CmiElement("scaled", true, true, null, null, "score.scaled")},
            {"row", new CmiElement("raw", true, true, null, null, "score.row")},
            {"max", new CmiElement("max", true, true, null, null, "score.max")},
            {"min", new CmiElement("min", true, true, null, null, "score.min")}
        };

        public override void Initialize(){}

        #endregion

        public Score(int learnerSessionId, int userID)
            : base(learnerSessionId, userID)
        {
        }

        public override string GetValue(string path)
        {

            if (path == "_children")
            {
                return GetChildren();
            }
            else if( elements.ContainsKey(path) )
            {
                return GetVariable(path);
            }
            throw new Exception("Requested variable is not supported");
        }

        public override int SetValue(string path, string value)
        {
            double _value;

            if (path == "_children")
            {
                throw new Exception("Requested variable is read-only");
            }
            else if (elements.ContainsKey(path) &&  double.TryParse(value, out _value) )
            {
                elements[path].Verifier.Validate(value);
                return SetVariable(path, value);
            }

            throw new Exception("Requested variable is not supported");
        }

        protected string GetVariable(string name)
        {
            if (elements[name].Read == false)
            {
                throw new Exception("Requested variable is write-only");
            }

            List<TblVarsScore> list = ServerModel.DB.Query<TblVarsScore>(
                        new AndCondtion(
                            new CompareCondition<int>(
                                DataObject.Schema.LearnerSessionRef,
                                new ValueCondition<int>(LearnerSessionId), COMPARE_KIND.EQUAL),
                            new CompareCondition<string>(
                                DataObject.Schema.Name,
                                new ValueCondition<string>(name), COMPARE_KIND.EQUAL)));

            if (list.Count > 0)
            {
                return list[0].Value.ToString();
            }
            else
            {
                return "";
            }
        }

        protected int SetVariable(string name, string value)
        {
             if (elements[name].Write == false)
            {
                throw new Exception("Requested variable is read-only");
            }

             List<TblVarsScore> list = ServerModel.DB.Query<TblVarsScore>(
                        new AndCondtion(
                            new CompareCondition<int>(
                                DataObject.Schema.LearnerSessionRef,
                                new ValueCondition<int>(LearnerSessionId), COMPARE_KIND.EQUAL),
                            new CompareCondition<string>(
                                DataObject.Schema.Name,
                                new ValueCondition<string>(name), COMPARE_KIND.EQUAL)));

            if (list.Count > 0)
            {
                list[0].Value = value;
                ServerModel.DB.Update<TblVarsScore>(list[0]);

                return list[0].ID;
            }
            else
            {
                TblVarsScore lsv = new TblVarsScore
                {
                    LearnerSessionRef = LearnerSessionId,
                    Name = name,
                    Value = value
                };

                return ServerModel.DB.Insert<TblVarsScore>(lsv);
            }
        }

        protected string GetChildren()
        {
            string[] childArray = new string[elements.Keys.Count];
            elements.Keys.CopyTo(childArray, 0);

            return string.Join(",", childArray);
        }

    }
        
}

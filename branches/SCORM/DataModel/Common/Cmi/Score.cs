using System;
using System.Collections.Generic;
using System.Collections;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.DB.Base;

namespace IUDICO.DataModel.Common.Cmi
{
    class Score: CmiBase
    {
        #region Cmi Variables

        private static Dictionary<string, CmiVariable> _variables = new Dictionary<string, CmiVariable>
        {
            {"scaled", new CmiVariable("scaled", true, true, null, null)},
            {"row", new CmiVariable("raw", true, true, null, null)},
            {"max", new CmiVariable("max", true, true, null, null)},
            {"min", new CmiVariable("min", true, true, null, null)}
        };

        private Dictionary<string, CmiBaseCollection> _collections;

        public override void Initialize()
        {

        }

        #endregion

        public Score(int _LearnerSessionId, int _UserID)
            : base(_LearnerSessionId, _UserID)
        {
        }

        public override string GetValue(string path)
        {

            if (path == "_children")
            {
                return GetChildren();
            }
            else if( _variables.ContainsKey(path) )
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
            else if (_variables.ContainsKey(path) &&  double.TryParse(value, out _value) )
            {
                if (path == "scaled")
                {
                    if (_value >= -1 && _value <= 1)
                    {
                        return SetVariable(path, value);

                    }
                }
                else
                {
                    return SetVariable(path, value);
                }

            }

            throw new Exception("Requested variable is not supported");
        }




        protected string GetVariable(string name)
        {
            if (_variables[name].Read == false)
            {
                throw new Exception("Requested variable is write-only");
            }

            List<TblLearnerSessionsVars> list = ServerModel.DB.Query<TblLearnerSessionsVars>(
                        new AndCondtion(
                            new CompareCondition<int>(
                                DataObject.Schema.LearnerSessionRef,
                                new ValueCondition<int>(LearnerSessionId), COMPARE_KIND.EQUAL),
                            new CompareCondition<string>(
                                DataObject.Schema.Name,
                                new ValueCondition<string>("cmi.score." + name), COMPARE_KIND.EQUAL)));

            if (list.Count > 0)
            {
                return list[0].Value;
            }
            else
            {
                return "";
            }
        }

        protected int SetVariable(string name, string value)
        {
             if (_variables[name].Write == false)
            {
                throw new Exception("Requested variable is read-only");
            }

            List<TblLearnerSessionsVars> list = ServerModel.DB.Query<TblLearnerSessionsVars>(
                        new AndCondtion(
                            new CompareCondition<int>(
                                DataObject.Schema.LearnerSessionRef,
                                new ValueCondition<int>(LearnerSessionId), COMPARE_KIND.EQUAL),
                            new CompareCondition<string>(
                                DataObject.Schema.Name,
                                new ValueCondition<string>("cmi.score." + name), COMPARE_KIND.EQUAL)));

            if (list.Count > 0)
            {
                list[0].Value = value;
                ServerModel.DB.Update<TblLearnerSessionsVars>(list[0]);

                return list[0].ID;
            }
            else
            {
                TblLearnerSessionsVars lsv = new TblLearnerSessionsVars
                {
                    LearnerSessionRef = LearnerSessionId,
                    Name = "cmi.score." + name,
                    Value = value
                };

                return ServerModel.DB.Insert<TblLearnerSessionsVars>(lsv);
            }
        }

        protected string GetChildren()
        {
            string[] childArray = new string[_variables.Keys.Count];
            _variables.Keys.CopyTo(childArray, 0);

            return string.Join(",", childArray);
        }

    }
        
}

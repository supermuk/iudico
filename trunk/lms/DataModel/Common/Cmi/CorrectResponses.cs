using System;
using System.Collections.Generic;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.DB.Base;


namespace IUDICO.DataModel.Common.Cmi
{
    class CorrectResponses: CmiBaseCollection
    {
        #region Cmi Variables

        private static Dictionary<string, CmiVariable> _variables = new Dictionary<string, CmiVariable>
        {
            {"pattern", new CmiVariable("pattern", true, true, null, null)}
        };

        #endregion

        public CorrectResponses(int _LearnerSessionId, int _UserID)
            : base(_LearnerSessionId, _UserID)
        {
        }

        protected string GetCount(string n)
        {
            List<TblLearnerSessionsVars> list = ServerModel.DB.Query<TblLearnerSessionsVars>(
                        new AndCondtion(
                            new CompareCondition<int>(
                                DataObject.Schema.LearnerSessionRef,
                                new ValueCondition<int>(LearnerSessionId), COMPARE_KIND.EQUAL),
                            new CompareCondition<string>(
                                DataObject.Schema.Name,
                                new ValueCondition<string>("cmi.interactions." + n + ".correct_responses.%"), COMPARE_KIND.LIKE)));

            return list.Count.ToString();
        }

        public override string GetValue(string n, string path)
        {
            string[] parts = path.Split('.');
            int id;

            if (parts[0] == path)
            {
                if (parts[0] == "_count")
                {
                    return GetCount(n);
                }
            }
            else if (int.TryParse(parts[0], out id) && _variables.ContainsKey(parts[1]) && parts.Length == 2)
            {
                return GetVariable(n, parts[0], parts[1]);
            }
            
            throw new Exception("Requested variable is not supported");
        }

        public override int SetValue(string n, string path, string value)
        {
            string[] parts = path.Split('.');
            int id;

            if (parts[0] == path)
            {
                if (parts[0] == "_count")
                {
                    throw new Exception("Requested variable is read-only");
                }
            }
            else if (int.TryParse(parts[0], out id) && _variables.ContainsKey(parts[1]) && parts.Length == 2)
            {
                return SetVariable(n, parts[0], parts[1], value);
            }

            throw new Exception("Requested variable is not supported");
        }

        protected string GetVariable(string n, string m, string name)
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
                                new ValueCondition<string>("cmi.interactions." + n + ".correct_responses." + m + "." + name), COMPARE_KIND.EQUAL)));

            return list[0].Value;
        }

        protected int SetVariable(string n, string m, string name, string value)
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
                                new ValueCondition<string>("cmi.interactions." + n + ".correct_responses." + m + "." + name), COMPARE_KIND.EQUAL)));

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
                    Name = "cmi.interactions." + n + ".correct_responses." + m + "." + name,
                    Value = value
                };

                return ServerModel.DB.Insert<TblLearnerSessionsVars>(lsv);
            }
        }
    }
}
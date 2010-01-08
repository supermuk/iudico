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

        public CorrectResponses(TblAttempts attempt, int userID)
            : base(attempt, userID)
        {
        }

        protected string GetCount(string n)
        {
            List<TblAttemptsVars> list = ServerModel.DB.Query<TblAttemptsVars>(
                        new AndCondtion(
                            new CompareCondition<int>(
                                DataObject.Schema.AttemptRef,
                                new ValueCondition<int>(Attempt.ID), COMPARE_KIND.EQUAL),
                            new CompareCondition<int>(
                                DataObject.Schema.UserRef,
                                new ValueCondition<int>(UserID), COMPARE_KIND.EQUAL),
                            new CompareCondition<string>(
                                DataObject.Schema.Name,
                                new ValueCondition<string>("cmi.interactions." + n + ".correct_responses."), COMPARE_KIND.LIKE)));

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

            List<TblAttemptsVars> list = ServerModel.DB.Query<TblAttemptsVars>(
                        new AndCondtion(
                            new CompareCondition<int>(
                                DataObject.Schema.AttemptRef,
                                new ValueCondition<int>(Attempt.ID), COMPARE_KIND.EQUAL),
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

            List<TblAttemptsVars> list = ServerModel.DB.Query<TblAttemptsVars>(
                        new AndCondtion(
                            new CompareCondition<int>(
                                DataObject.Schema.AttemptRef,
                                new ValueCondition<int>(Attempt.ID), COMPARE_KIND.EQUAL),
                            new CompareCondition<string>(
                                DataObject.Schema.Name,
                                new ValueCondition<string>("cmi.interactions." + n + ".correct_responses." + m + "." + name), COMPARE_KIND.EQUAL)));

            if (list.Count > 0)
            {
                list[0].Value = value;
                ServerModel.DB.Update<TblAttemptsVars>(list[0]);

                return list[0].ID;
            }
            else
            {
                TblAttemptsVars av = new TblAttemptsVars
                {
                    AttemptRef = Attempt.ID,
                    Name = "cmi.interactions." + n + ".correct_responses." + m + "." + name,
                    Value = value
                };

                return ServerModel.DB.Insert<TblAttemptsVars>(av);
            }
        }
    }
}

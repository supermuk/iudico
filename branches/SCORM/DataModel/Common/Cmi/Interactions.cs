using System;
using System.Collections.Generic;
using System.Collections;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.DB.Base;

namespace IUDICO.DataModel.Common.Cmi
{
    class Interactions: CmiBase
    {
        #region Cmi Variables

        private static Dictionary<string, CmiVariable> _variables = new Dictionary<string, CmiVariable>
        {
            {"id", new CmiVariable("id", true, true, null, null)},
            {"type", new CmiVariable("type", true, true,
                new string[] { "true-false", "multiple-choice", "fill-in", "long-fill-in", "matching", "performance", "sequencing", "likert", "numeric", "other"}, null)},
            
            {"timestamp", new CmiVariable("timestamp", true, true, null, null)},
            {"weighting", new CmiVariable("weighting", true, true, null, null)},
            {"result", new CmiVariable("result", true, true,
                new string[] { "correct", "incorrect", "unanticipated", "neutral", "_real" }, null )},
            {"latency", new CmiVariable("latency", true, true, null, null)},
            {"description", new CmiVariable("description", true, true, null, null)},

            //{"objectives", new CmiVariable("objectives", true, false, null)),
            //{"correct_responses", new CmiVariable("correct_responses", true, false, null)),
            {"learner_response", new CmiVariable("learner_response", true, true, null, null)},
        };

        private Dictionary<string, CmiBaseCollection> _collections;

        public override void Initialize()
        {
            _collections = new Dictionary<string, CmiBaseCollection>
            {
                {"correct_responses", new Cmi.CorrectResponses(_attempt, _userID)}
            };
        }

        #endregion

        public Interactions(TblAttempts attempt, int userID)
            : base(attempt, userID)
        {
        }

        public override string GetValue(string path)
        {
            string[] parts = path.Split('.');
            int id;

            if (parts[0] == path)
            {
                if (parts[0] == "_children")
                {
                    return GetChildren();
                }
                else if (parts[0] == "_count")
                {
                    return GetCount();
                }
            }
            else if (int.TryParse(parts[0], out id))
            {
                if (_variables.ContainsKey(parts[1]) && parts.Length == 2)
                {
                    return GetVariable(parts[0], parts[1]);
                }
                else if (_collections.ContainsKey(parts[1]))
                {
                    return _collections[parts[1]].GetValue(parts[0], string.Join(".", parts, 2, parts.Length - 2));
                }
            }

            throw new Exception("Requested variable is not supported");
        }

        public override int SetValue(string path, string value)
        {
            string[] parts = path.Split('.');
            int id;

            if (parts[0] == path)
            {
                if (parts[0] == "_children")
                {
                    throw new Exception("Requested variable is read-only");
                }
                else if (parts[0] == "_count")
                {
                    throw new Exception("Requested variable is read-only");
                }
            }
            else if (int.TryParse(parts[0], out id))
            {
                if (_variables.ContainsKey(parts[1]) && parts.Length == 2)
                {
                    return SetVariable(parts[0], parts[1], value);
                }
                else if (_collections.ContainsKey(parts[1]))
                {
                    return _collections[parts[1]].SetValue(parts[0], string.Join(".", parts, 2, parts.Length - 2), value);
                }
            }

            throw new Exception("Requested variable is not supported");
        }

        protected string GetVariable(string n, string name)
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
                            new CompareCondition<int>(
                                DataObject.Schema.UserRef,
                                new ValueCondition<int>(UserID), COMPARE_KIND.EQUAL),
                            new CompareCondition<string>(
                                DataObject.Schema.Name,
                                new ValueCondition<string>("cmi.interactions." + n + "." + name), COMPARE_KIND.EQUAL)));

            if (list.Count > 0)
            {
                return list[0].Value;
            }
            else
            {
                return "";
            }
        }

        protected int SetVariable(string n, string name, string value)
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
                                new ValueCondition<string>("cmi.interactions." + n + "." + name), COMPARE_KIND.EQUAL)));

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
                    Name = "cmi.interactions." + n + "." + name,
                    Value = value
                };

                return ServerModel.DB.Insert<TblAttemptsVars>(av);
            }
        }

        protected string GetChildren()
        {
            string[] childArray = new string[_variables.Keys.Count];
            _variables.Keys.CopyTo(childArray, 0);

            return string.Join(",", childArray);
        }

        protected string GetCount()
        {
            List<TblAttemptsVars> list = ServerModel.DB.Query<TblAttemptsVars>(
                        new AndCondtion(
                            new CompareCondition<int>(
                                DataObject.Schema.AttemptRef,
                                new ValueCondition<int>(Attempt.ID), COMPARE_KIND.EQUAL),
                            new CompareCondition<string>(
                                DataObject.Schema.Name,
                                new ValueCondition<string>("cmi.interactions.%"), COMPARE_KIND.LIKE)));

            return list.Count.ToString();
        }
    }
}

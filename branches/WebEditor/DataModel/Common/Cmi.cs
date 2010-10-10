using System;
using System.Collections.Generic;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.DB.Base;

namespace IUDICO.DataModel.Common
{
    public static class Cmi
    {
        public static int Initialize(int themeId)
        {
            TblAttempts a = new TblAttempts
            {
                ThemeRef = themeId,
                UserRef = ServerModel.User.Current.ID
            };

            return ServerModel.DB.Insert(a);
        }

        public static string GetCount(string name, int attemptId)
        {
            return ServerModel.DB.Query<TblAttemptsVars>(
                new AndCondtion(
                    new CompareCondition<int>(
                        DataObject.Schema.AttemptRef,
                        new ValueCondition<int>(attemptId), COMPARE_KIND.EQUAL),
                    new CompareCondition<int>(
                        DataObject.Schema.UserRef,
                        new ValueCondition<int>(ServerModel.User.Current.ID), COMPARE_KIND.EQUAL),
                    new CompareCondition<string>(
                        DataObject.Schema.Name,
                        new ValueCondition<string>(name), COMPARE_KIND.LIKE))).Count.ToString();
        }

        public static string GetChildren(string name, int attemptId)
        {
            List<TblAttemptsVars> children = ServerModel.DB.Query<TblAttemptsVars>(
                new AndCondtion(
                    new CompareCondition<int>(
                        DataObject.Schema.AttemptRef,
                        new ValueCondition<int>(attemptId), COMPARE_KIND.EQUAL),
                    new CompareCondition<int>(
                        DataObject.Schema.UserRef,
                        new ValueCondition<int>(ServerModel.User.Current.ID), COMPARE_KIND.EQUAL),
                    new CompareCondition<string>(
                        DataObject.Schema.Name,
                        new ValueCondition<string>(name), COMPARE_KIND.LIKE)));

            string[] childArray = new string[children.Count];

            for (int i = 0; i < children.Count; i++)
            {
                childArray[i] = children[i].Name;
            }

            return string.Join(",", childArray);
        }

        public static string GetVariable(string name, int attemptId)
        {
            return ServerModel.DB.Query<TblAttemptsVars>(
                new AndCondtion(
                    new CompareCondition<int>(
                        DataObject.Schema.AttemptRef,
                        new ValueCondition<int>(attemptId), COMPARE_KIND.EQUAL),
                    new CompareCondition<int>(
                        DataObject.Schema.UserRef,
                        new ValueCondition<int>(ServerModel.User.Current.ID), COMPARE_KIND.EQUAL),
                    new CompareCondition<string>(
                        DataObject.Schema.Name,
                        new ValueCondition<string>(name), COMPARE_KIND.EQUAL)))[0].Value;
        }

        public static int SetVariable(string name, string value, int attemptId)
        {
            TblAttemptsVars av = new TblAttemptsVars
            {
                AttemptRef = attemptId,
                Name = name,
                Value = value
            };

            return ServerModel.DB.Insert(av);
        }
    }
}

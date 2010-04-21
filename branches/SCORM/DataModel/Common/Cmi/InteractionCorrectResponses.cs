using System;
using System.Collections.Generic;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.DB.Base;


namespace IUDICO.DataModel.Common.Cmi
{
    class InteractionCorrectResponses: CmiSecondLevelCollectionElement
    {
        #region Cmi Variables

        private static Dictionary<string, CmiElement> elements = new Dictionary<string, CmiElement>
        {
            {"pattern", new CmiElement("pattern", true, true, null, null, "interaction.correct_response.pattern")}
        };

        #endregion

        public InteractionCorrectResponses(int learnerSessionId, int userID)
            : base(learnerSessionId, userID)
        {
        }

        protected string GetCount(string n)
        {
            int number;
            int.TryParse(n, out number);

            List<TblVarsInteractionCorrectResponses> list = ServerModel.DB.Query<TblVarsInteractionCorrectResponses>(
                          new AndCondition(
                            new CompareCondition<int>(
                                DataObject.Schema.LearnerSessionRef,
                                new ValueCondition<int>(LearnerSessionId), COMPARE_KIND.EQUAL),
                            new CompareCondition<int>(
                                DataObject.Schema.InteractionRef,
                                new ValueCondition<int>(number), COMPARE_KIND.EQUAL)
                          )
            );

            int count = 0;
            for (int i = 0; i < list.Count; i++)
            {
              count++;
              for (int j = 0; j < i; j++)
              {
                if (list[i].Number == list[j].Number)
                {
                  count--;
                  break;
                }
              }
            }
            return count.ToString();
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
            else if (int.TryParse(parts[0], out id) && elements.ContainsKey(parts[1]) && parts.Length == 2)
            {
                return GetVariable(n, parts[0], parts[1]);
            }

            throw new NotSupportedException(Translations.InteractionCorrectResponses_GetValue_Requested_variable_is_not_supported);
        }

        public override int SetValue(string n, string path, string value)
        {
            string[] parts = path.Split('.');
            int id;

            if (parts[0] == path)
            {
                if (parts[0] == "_count")
                {
                    throw new CmiReadWriteOnlyException(Translations.InteractionCorrectResponses_SetValue_Requested_variable_is_read_only);
                }
            }
            else if (int.TryParse(parts[0], out id) && elements.ContainsKey(parts[1]) && parts.Length == 2)
            {
                elements[parts[1]].Verifier.Validate(value);
                return SetVariable(n, parts[0], parts[1], value);
            }

            throw new NotSupportedException(Translations.InteractionCorrectResponses_GetValue_Requested_variable_is_not_supported);
        }

        protected string GetVariable(string n, string m, string name)
        {
            if (elements[name].Read == false)
            {
              throw new Exception(Translations.InteractionCorrectResponses_GetVariable_Requested_variable_is_write_only);
            }

            int interactionRef, number;
            int.TryParse(n, out interactionRef);
            int.TryParse(m, out number);

            List<TblVarsInteractionCorrectResponses> list = ServerModel.DB.Query<TblVarsInteractionCorrectResponses>(
                        new AndCondition(
                            new CompareCondition<int>(
                                DataObject.Schema.LearnerSessionRef,
                                new ValueCondition<int>(LearnerSessionId), COMPARE_KIND.EQUAL),
                            new CompareCondition<string>(
                                DataObject.Schema.Name,
                                new ValueCondition<string>(name), COMPARE_KIND.EQUAL),
                            new CompareCondition<int>(
                                DataObject.Schema.InteractionRef,
                                new ValueCondition<int>(interactionRef), COMPARE_KIND.EQUAL),
                            new CompareCondition<int>(
                                DataObject.Schema.Number,
                                new ValueCondition<int>(number), COMPARE_KIND.EQUAL)));

            if (list.Count > 0)
            {
              return list[0].Value;
            }
            else
            {
              return "";
            }
        }

        protected int SetVariable(string n, string m, string name, string value)
        {
            if (elements[name].Write == false)
            {
              throw new CmiReadWriteOnlyException(Translations.InteractionCorrectResponses_SetValue_Requested_variable_is_read_only);
            }

            int interactionRef, number;
            int.TryParse(n, out interactionRef);
            int.TryParse(m, out number);

            List<TblVarsInteractionCorrectResponses> list = ServerModel.DB.Query<TblVarsInteractionCorrectResponses>(
                        new AndCondition(
                            new CompareCondition<int>(
                                DataObject.Schema.LearnerSessionRef,
                                new ValueCondition<int>(LearnerSessionId), COMPARE_KIND.EQUAL),
                            new CompareCondition<string>(
                                DataObject.Schema.Name,
                                new ValueCondition<string>(name), COMPARE_KIND.EQUAL),
                            new CompareCondition<int>(
                                DataObject.Schema.InteractionRef,
                                new ValueCondition<int>(interactionRef), COMPARE_KIND.EQUAL),
                            new CompareCondition<int>(
                                DataObject.Schema.Number,
                                new ValueCondition<int>(number), COMPARE_KIND.EQUAL)));

            if (list.Count > 0)
            {
              list[0].Value = value;
              ServerModel.DB.Update<TblVarsInteractionCorrectResponses>(list[0]);

              return list[0].ID;
            }
            else
            {
              TblVarsInteractionCorrectResponses lsv = new TblVarsInteractionCorrectResponses
              {
                LearnerSessionRef = LearnerSessionId,
                InteractionRef = interactionRef,
                Name = name,
                Value = value,
                Number = number
              };

              return ServerModel.DB.Insert<TblVarsInteractionCorrectResponses>(lsv);
            }
        }
    }
}
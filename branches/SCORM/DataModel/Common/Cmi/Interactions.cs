using System;
using System.Collections.Generic;
using System.Collections;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.DB.Base;

namespace IUDICO.DataModel.Common.Cmi
{
    class Interactions: CmiFirstLevelCollectionElement
    {
        #region Cmi Variables

        private static Dictionary<string, CmiElement> elements = new Dictionary<string, CmiElement>
        {
            {"id", new CmiElement("id", true, true, null, null, "interaction.id")},
            {"type", new CmiElement("type", true, true,
                new string[] { "true-false", "choice", "fill-in", "long-fill-in", "matching", "performance", "sequencing", "likert", "numeric", "other"}, null, "interaction.type")},
            {"timestamp", new CmiElement("timestamp", true, true, null, null, "interaction.timestamp")},
            {"weighting", new CmiElement("weighting", true, true, null, null, "interaction.weighting")},
            {"result", new CmiElement("result", true, true,
                new string[] { "correct", "incorrect", "unanticipated", "neutral", "_real"}, null,"interaction.result" )},
            {"latency", new CmiElement("latency", true, true, null, null, "interaction.latency")},
            {"description", new CmiElement("description", true, true, null, null, "interaction.description")},
            {"learner_response", new CmiElement("learner_response", true, true, null, null, "interaction.learner_response")},
        };

        private Dictionary<string, CmiSecondLevelCollectionElement> collections;

        #endregion

        public Interactions(int learnerSessionId, int userID)
            : base(learnerSessionId, userID)
        {
          Initialize();
        }

        public override void Initialize()
        {
          collections = new Dictionary<string, CmiSecondLevelCollectionElement>
            {
                {"objectives", new InteractionObjectives(LearnerSessionId, UserID)},
                {"correct_responses", new InteractionCorrectResponses(LearnerSessionId, UserID)}
            };
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
                if (elements.ContainsKey(parts[1]) && parts.Length == 2)
                {
                    return GetVariable(parts[0], parts[1]);
                }
                else if (collections.ContainsKey(parts[1]))
                {
                  return collections[parts[1]].GetValue(parts[0], string.Join(".", parts, 2, parts.Length - 2));
                }
            }

            throw new NotSupportedException(Translations.Interactions_GetValue_Requested_variable_is_not_supported);
        }

        public override int SetValue(string path, string value)
        {
            string[] parts = path.Split('.');
            int id;

            if (parts[0] == path)
            {
                if (parts[0] == "_children" || parts[0] == "_count")
                {
                  throw new CmiReadWriteOnlyException(Translations.Interactions_SetValue_Requested_variable_is_read_only);
                }
            }
            else if (int.TryParse(parts[0], out id))
            {
                if (elements.ContainsKey(parts[1]) && parts.Length == 2)
                {
                    elements[parts[1]].Verifier.Validate(value);
                    return SetVariable(parts[0], parts[1], value);
                }
                else if (collections.ContainsKey(parts[1]))
                {
                    return collections[parts[1]].SetValue(parts[0], string.Join(".", parts, 2, parts.Length - 2), value);
                }
            }

            throw new NotSupportedException(Translations.Interactions_GetValue_Requested_variable_is_not_supported);
        }

        protected string GetVariable(string n, string name)
        {
            if (elements[name].Read == false)
            {
              throw new CmiReadWriteOnlyException(Translations.Interactions_GetVariable_Requested_variable_is_write_only);
            }

            int number;
            int.TryParse(n, out number);

            List<TblVarsInteractions> list = ServerModel.DB.Query<TblVarsInteractions>(
                        new AndCondition(
                            new CompareCondition<int>(
                                DataObject.Schema.LearnerSessionRef,
                                new ValueCondition<int>(LearnerSessionId), COMPARE_KIND.EQUAL),
                            new CompareCondition<string>(
                                DataObject.Schema.Name,
                                new ValueCondition<string>(name), COMPARE_KIND.EQUAL),
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

        protected int SetVariable(string n, string name, string value)
        {
            if (elements[name].Write == false)
            {
              throw new CmiReadWriteOnlyException(Translations.Interactions_SetValue_Requested_variable_is_read_only);
            }

            int number;
            int.TryParse(n, out number);

            List<TblVarsInteractions> list = ServerModel.DB.Query<TblVarsInteractions>(
                        new AndCondition(
                            new CompareCondition<int>(
                                DataObject.Schema.LearnerSessionRef,
                                new ValueCondition<int>(LearnerSessionId), COMPARE_KIND.EQUAL),
                            new CompareCondition<string>(
                                DataObject.Schema.Name,
                                new ValueCondition<string>(name), COMPARE_KIND.EQUAL),
                            new CompareCondition<int>(
                                DataObject.Schema.Number,
                                new ValueCondition<int>(number), COMPARE_KIND.EQUAL)));

            if (list.Count > 0)
            {
                list[0].Value = value;
                ServerModel.DB.Update<TblVarsInteractions>(list[0]);

                return list[0].ID;
            }
            else
            {
                TblVarsInteractions lsv = new TblVarsInteractions
                {
                    LearnerSessionRef = LearnerSessionId,
                    Name = name,
                    Value = value,
                    Number = number
                };

                return ServerModel.DB.Insert<TblVarsInteractions>(lsv);
            }
        }

        protected string GetChildren()
        {
            string[] childArray = new string[elements.Keys.Count];
            elements.Keys.CopyTo(childArray, 0);

            return string.Join(",", childArray);
        }

        protected string GetCount()
        {
            List<TblVarsInteractions> list = ServerModel.DB.Query<TblVarsInteractions>(
                            new CompareCondition<int>(
                                DataObject.Schema.LearnerSessionRef,
                                new ValueCondition<int>(LearnerSessionId), COMPARE_KIND.EQUAL
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
    }
}

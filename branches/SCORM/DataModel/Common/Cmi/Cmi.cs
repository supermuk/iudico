using System;
using System.Collections.Generic;
using System.Collections;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.DB.Base;

namespace IUDICO.DataModel.Common
{
    /// <summary>
    /// Base class for collections of elements in cmi request which lie in first level e.g 
    /// cmi.interactions
    /// </summary>
    abstract public class CmiFirstLevelCollectionElement
    {
        protected int learnerSessionId;
        protected int userID;

        #region Properties

        public int LearnerSessionId
        {
            get
            {
                return learnerSessionId;
            }
        }

        public int UserID
        {
            get
            {
                return userID;
            }
        }

        #endregion

        public CmiFirstLevelCollectionElement(int learnerSessionId, int userID)
        {
            this.learnerSessionId = learnerSessionId;
            this.userID = userID;

            Initialize();
        }

        public virtual void Initialize()
        {
        }

        abstract public string GetValue(string path);
        abstract public int SetValue(string path, string value);
    }

    /// <summary>
    /// Base class for collections of elements in cmi request which lie in second level e.g 
    /// cmi.interactions.n.objectives
    /// </summary>
    abstract public class CmiSecondLevelCollectionElement
    {
        protected int _learnerSessionId;
        protected int _userID;

        #region Properties

        public int LearnerSessionId
        {
            get
            {
                return _learnerSessionId;
            }
        }

        public int UserID
        {
            get
            {
                return _userID;
            }
        }

        #endregion

        public CmiSecondLevelCollectionElement(int _LearnerSessionId, int _UserID)
        {
            _learnerSessionId = _LearnerSessionId;
            _userID = _UserID;

            Initialize();
        }

        public virtual void Initialize()
        {
        }

        abstract public string GetValue(string n, string path);
        abstract public int SetValue(string n, string path, string value);
    }

    /// <summary>
    /// Simple Data Model Element e.g cmi.exit or cmi.interactions.id
    /// </summary>
    public class CmiElement
    {
        private string name;
        private bool read;
        private bool write;
        private string[] allowed;
        private string defaultValue;
        private Cmi.DataModelVerifier verifier;

        #region Properties

        public Cmi.DataModelVerifier Verifier
        {
          get { return verifier; }
        }
        public bool Read
        {
            get { return read; }
        }
        public bool Write
        {
            get { return write; }
        }
        public string DefaultValue
        {
            get { return defaultValue; }
        }

        #endregion

        public CmiElement(string name, bool read, bool write, string[] allowed, string defaultValue, string verifierElementName)
        {
            this.name = name;
            this.read = read;
            this.write = write;
            this.allowed = allowed;
            this.defaultValue = defaultValue;
            this.verifier = new Cmi.DataModelVerifier(verifierElementName);
        }
    }

    /// <summary>
    /// DataModel interface which allows to get or set values of DataModelElements.
    /// Use GetValue and SetValue methods
    /// </summary>
    public class CmiDataModel
    {
        #region Cmi Variables

        private static Dictionary<string, CmiElement> elements = new Dictionary<string, CmiElement>
        {
            {"completion_status", new CmiElement("completion_status", true, true, new string[] { "completed", "incomplete", "not_attempted", "unknown"}, "unknown", "completion_status")},
            {"credit", new CmiElement("credit", true, false, new string[] { "credit", "no-credit" }, "credit", "credit")},
            {"entry", new CmiElement("entry", true, false, new string[] { "ab-initio", "resume", "" }, "ab-initio" , "entry")},
            {"exit", new CmiElement("exit", true, true, null, null, "exit")},
            {"launch_data", new CmiElement("launch_data", true, false, null, null, "launch_data")},
            {"learner_id", new CmiElement("learner_id", true, false, null, null, "learner_id")},
            {"learner_name", new CmiElement("learner_name", true, false, null, null, "learner_name")},
            {"location", new CmiElement("location", true, true, null, null, "location")},
            {"max_time_allowed", new CmiElement("max_time_allowed", true, false, null, null, "max_time_allowed")},
            {"mode", new CmiElement("mode", true, false, new string[]{"browse", "normal", "review"}, "normal", "mode")},
            {"progress_measure", new CmiElement("progress_measure", true, true, null, null, "progress_measure")},
            {"scaled_passing_score", new CmiElement("scaled_passing_score", true, false, null, null, "scaled_passing_score")},
            {"success_status", new CmiElement("success_status", true, true, new string[]{"passed", "failed", "unknown"}, "unknown", "success_status")},
            {"suspend_data", new CmiElement("suspend_data", true, true, null, null, "suspend_data")},
            {"time_limit_action", new CmiElement("time_limit_action", true, false, new string[]{"exit,message", "continue,message", "exit,no message", "continue,no message"}, "continue,no message", "time_limit_action")},
            {"session_time", new CmiElement("session_time", false, true, null, null, "session_time")},
            {"total_time", new CmiElement("total_time", true, false, null, null, "total_time")}
        };

        private Dictionary<string, CmiFirstLevelCollectionElement> collections;

        #endregion

        #region Variables

        private int learnerSessionId;
        private int userId;
        private bool isSystem;

        #endregion

        #region Properties

        public int LearnerSessionId
        {
            get { return learnerSessionId; }
        }
        public int UserId
        {
            get { return userId; }
        }

        #endregion

        #region Public Methods

        public CmiDataModel(int learnerSessionId, int userId, bool isSystem)
        {
            this.learnerSessionId = learnerSessionId;
            this.userId = userId;
            this.isSystem = isSystem;

            Initialize();
        }

        /// <summary>
        /// Get collection of Cmi Data Model elements.Examples:
        /// dataModel.GetCollection<TblVars>("*");
        /// dataModel.GetCollection<TblVarsInteractions>("interactions.*.*");
        /// dataModel.GetCollection<TblVarsInteractions>("interactions.i.*");
        /// dataModel.GetCollection<TblVarsInteractionCorrectResponses>("interactions.*.correct_responses.*");
        /// dataModel.GetCollection<TblVarsInteractionCorrectResponses>("interactions.i.correct_responses.*");
        /// Collections are sorted by Number(or InteractionRef)property and, at coincidence, by Name property!
        /// </summary>
        /// <typeparam name="TDataObject"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public List<TDataObject> GetCollection<TDataObject>(string path)
                                 where TDataObject:IDataObject, new()
        {
          string[] parts = path.Split('.');
          int number;
          List<IDBPredicate> predicats=new List<IDBPredicate>();
          predicats.Add(new CompareCondition<int>(DataObject.Schema.LearnerSessionRef, new ValueCondition<int>(LearnerSessionId), COMPARE_KIND.EQUAL));

          //parsing path
          if (path == "*")
          {
          }
          else if (parts[0] == "interactions")
          {
            if(parts.Length==3 && parts[2]=="*")
            {
              if (parts[1] == "*") 
              {
              }
              else if (int.TryParse(parts[1], out number))
              {
                predicats.Add(new CompareCondition<int>(DataObject.Schema.Number, new ValueCondition<int>(number), COMPARE_KIND.EQUAL));
              }
              else
              {
                throw new NotSupportedException(Translations.CmiDataModel_GetCollection_Requested_variable_is_not_supported);
              }
            }
            else if(parts.Length==4 && parts[2]=="correct_responses" && parts[3]=="*")
            {
              if(parts[1] == "*")
              {
              }
              else if(int.TryParse(parts[1], out number))
              {
                predicats.Add(new CompareCondition<int>(DataObject.Schema.InteractionRef, new ValueCondition<int>(number), COMPARE_KIND.EQUAL));
              }
              else
              {
                throw new NotSupportedException(Translations.CmiDataModel_GetCollection_Requested_variable_is_not_supported);
              }
            }
            else
            {
              throw new NotSupportedException(Translations.CmiDataModel_GetCollection_Requested_variable_is_not_supported);
            }
          }
          else
          {
            throw new NotSupportedException(Translations.CmiDataModel_GetCollection_Requested_variable_is_not_supported);
          }

          //execute SQL command
          List<TDataObject> result;
          if(predicats.Count>1)
          {
            result = ServerModel.DB.Query<TDataObject>(new AndCondition(predicats.ToArray()));
          }
          else
          {
            result = ServerModel.DB.Query<TDataObject>(predicats[0]);
          }

          //sort:by Name
          if (typeof(TDataObject) == typeof(TblVars))
          {
            (result as List<TblVars>).Sort((t1, t2) => { return t1.Name.CompareTo(t2.Name); });
          }
          //sort:first by Number, then by Name
          else if (typeof(TDataObject) == typeof(TblVarsInteractions))
          {
            (result as List<TblVarsInteractions>).Sort((t1, t2) => { return t1.Number.CompareTo(t2.Number)==0 ?
                                                                            t1.Name.CompareTo(t2.Name):
                                                                            t1.Number.CompareTo(t2.Number);});
          }
          //sort:first by InteractionRef, then by Name
          else if(typeof(TDataObject) == typeof(TblVarsInteractionCorrectResponses))
          {
            (result as List<TblVarsInteractionCorrectResponses>).Sort((t1, t2) => { return t1.InteractionRef.CompareTo(t2.InteractionRef)==0 ?
                                                                                           t1.Name.CompareTo(t2.Name):
                                                                                           t1.InteractionRef.CompareTo(t2.InteractionRef);
            });
          }
          
          return result;
        }

        public string GetValue(string path)
        {
            string[] parts = path.Split('.');
            string name = parts[0];

            if (name == path)
            {
                if (name == "_version")
                {
                    return "1.0";
                }
                else if (name == "_children")
                {
                    return GetChildren();
                }
                else if (name == "total_time")
                {
                    return GetTotalTime();
                }
                else if (elements.ContainsKey(name))
                {
                  return GetVariable(name);
                }
            }

            if (collections.ContainsKey(name))
            {
                return collections[name].GetValue(string.Join(".", parts, 1, parts.Length - 1));
            }

            throw new NotSupportedException(Translations.CmiDataModel_GetCollection_Requested_variable_is_not_supported);
        }

        public int SetValue(string path, string value)
        {
            string[] parts = path.Split('.');
            string name = parts[0];

            if (name == path)
            {  
                if (name == "_version" || name=="_children" || name=="total_time" || name=="_count")
                {
                  throw new CmiReadWriteOnlyException(Translations.CmiDataModel_SetValue_Requested_variable_is_read_only);
                }
                else if (elements.ContainsKey(name))
                {
                  elements[name].Verifier.Validate(value);
                  return SetVariable(name, value);
                }
            }

            if (collections.ContainsKey(name))
            {
                return collections[name].SetValue(string.Join(".", parts, 1, parts.Length - 1), value);
            }

            throw new NotSupportedException(Translations.CmiDataModel_GetCollection_Requested_variable_is_not_supported);
        }

        #endregion

        #region Private Methods

        private void Initialize()
        {
            collections = new Dictionary<string, CmiFirstLevelCollectionElement>
            {
                {"interactions", new Cmi.Interactions(LearnerSessionId, UserId)},
                {"score", new Cmi.Score(LearnerSessionId, UserId)}
            };
        }

        private int SetVariable(string name, string value)
        {
            if (elements[name].Write == false && ! isSystem)
            {
              throw new CmiReadWriteOnlyException(Translations.CmiDataModel_SetValue_Requested_variable_is_read_only);
            }

            List<TblVars> list = ServerModel.DB.Query<TblVars>(
                        new AndCondition(
                            new CompareCondition<int>(
                                DataObject.Schema.LearnerSessionRef,
                                new ValueCondition<int>(LearnerSessionId), COMPARE_KIND.EQUAL),
                            new CompareCondition<string>(
                                DataObject.Schema.Name,
                                new ValueCondition<string>(name), COMPARE_KIND.EQUAL)));

            if (list.Count > 0)
            {
                list[0].Value = value;
                ServerModel.DB.Update<TblVars>(list[0]);

                return list[0].ID;
            }
            else
            {
                TblVars lsv = new TblVars
                {
                    LearnerSessionRef = LearnerSessionId,
                    Name = name,
                    Value = value
                };

                return ServerModel.DB.Insert<TblVars>(lsv);
            }
        }

        private string GetVariable(string name)
        {
            if (elements[name].Read == false)
            {
              throw new CmiReadWriteOnlyException(Translations.CmiDataModel_GetVariable_Requested_variable_is_write_only);
            }

            switch (name)
            {
                case "learner_id":
                    return ServerModel.User.Current.ID.ToString();
                case "learner_name":
                    return ServerModel.User.Current.UserName;
                default:
                    List<TblVars> list = ServerModel.DB.Query<TblVars>(
                        new AndCondition(
                            new CompareCondition<int>(
                                DataObject.Schema.LearnerSessionRef,
                                new ValueCondition<int>(LearnerSessionId), COMPARE_KIND.EQUAL),
                            new CompareCondition<string>(
                                DataObject.Schema.Name,
                                new ValueCondition<string>(name), COMPARE_KIND.EQUAL)));

                    if (list.Count > 0)
                    {
                        return list[0].Value;
                    }
                    else
                    {
                      return elements[name].DefaultValue == null ? "" : elements[name].DefaultValue;
                    }
            }
        }

        private string GetChildren()
        {
            string[] childArray = new string[elements.Keys.Count];
            elements.Keys.CopyTo(childArray, 0);

            return string.Join(",", childArray);
        }

        private string GetTotalTime()
        {
          throw new NotImplementedException();
        }

        #endregion
    }

    public class CmiReadWriteOnlyException:Exception
    {
      public CmiReadWriteOnlyException(string message):base(message){}
    }
}

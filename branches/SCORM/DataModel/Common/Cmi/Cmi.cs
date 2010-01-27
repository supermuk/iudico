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
        private string init;
        private Cmi.DataModelVerifier verifier;

        #region Properties

        public Cmi.DataModelVerifier Verifier
        {
          get
          {
            return verifier;
          }
        }

        public bool Read
        {
            get
            {
                return read;
            }
        }

        public bool Write
        {
            get
            {
                return write;
            }
        }

        #endregion

        public CmiElement(string name, bool read, bool write, string[] allowed, string init, string verifierElementName)
        {
            this.name = name;
            this.read = read;
            this.write = write;
            this.allowed = allowed;
            this.init = init;
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
            {"mode", new CmiElement("mode", true, false, null, null, "mode")},
            {"progress_measure", new CmiElement("progress_measure", true, true, null, null, "progress_measure")},
            {"scaled_passing_score", new CmiElement("scaled_passing_score", true, false, null, null, "scaled_passing_score")},
            {"success_status", new CmiElement("success_status", true, true, new string[]{"passed", "failed", "unknown"}, "unknown", "success_status")},
            {"suspend_data", new CmiElement("suspend_data", true, true, null, null, "suspend_data")},
            {"time_limit_action", new CmiElement("time_limit_action", true, false, null, null, "time_limit_action")},
            {"session_time", new CmiElement("session_time", false, true, null, null, "session_time")},
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
            get
            {
                return learnerSessionId;
            }
        }

        public int UserId
        {
            get
            {
                return userId;
            }
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

            throw new Exception("Requested variable is not supported");
        }

        public int SetValue(string path, string value)
        {
            string[] parts = path.Split('.');
            string name = parts[0];

            if (name == path)
            {
                elements[name].Verifier.Validate(value);
                if (name == "_version")
                {
                    throw new Exception("Requested variable is read-only");
                }
                else if (name == "_children")
                {
                    throw new Exception("Requested variable is read-only");
                }
                else if (name == "total_time")
                {
                    throw new Exception("Requested variable is read-only");
                }
                else if (elements.ContainsKey(name))
                {
                  return SetVariable(name, value);
                }
            }

            if (collections.ContainsKey(name))
            {
                return collections[name].SetValue(string.Join(".", parts, 1, parts.Length - 1), value);
            }

            throw new Exception("Requested variable is not supported");
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
                throw new Exception("Requested variable is read-only");
            }

            List<TblVars> list = ServerModel.DB.Query<TblVars>(
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
                throw new Exception("Requested variable is write-only");
            }

            switch (name)
            {
                case "learner_id":
                    return ServerModel.User.Current.ID.ToString();
                case "learner_name":
                    return ServerModel.User.Current.UserName;
                default:
                    List<TblVars> list = ServerModel.DB.Query<TblVars>(
                        new AndCondtion(
                            new CompareCondition<int>(
                                DataObject.Schema.LearnerSessionRef,
                                new ValueCondition<int>(LearnerSessionId), COMPARE_KIND.EQUAL),
                            new CompareCondition<string>(
                                DataObject.Schema.Name,
                                new ValueCondition<string>("cmi." + name), COMPARE_KIND.EQUAL)));

                    if (list.Count > 0)
                    {
                        return list[0].Value;
                    }
                    else
                    {
                        return "";
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
          //Переробити!!!
          int result = 0;
          /*List<TblVars> list = ServerModel.DB.Query<TblVars>(
                  new CompareCondition<string>(
                      DataObject.Schema.Name,
                      new ValueCondition<string>("session_time"), COMPARE_KIND.EQUAL));
          
          for (int i = 0; i < list.Count; i++)
          {
            result+=TimeSpan.Parse(list[i].Value);
          }*/

          return result.ToString();
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Collections;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.DB.Base;

namespace IUDICO.DataModel.Common
{
    abstract public class CmiBase
    {
        protected TblAttempts _attempt;
        protected int _userID;

        #region Properties

        public TblAttempts Attempt
        {
            get
            {
                return _attempt;
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

        public CmiBase(TblAttempts attempt, int userID)
        {
            _attempt = attempt;
            _userID = userID;

            Initialize();
        }

        public virtual void Initialize()
        {
        }

        abstract public string GetValue(string path);
        abstract public int SetValue(string path, string value);
    }

    abstract public class CmiBaseCollection
    {
        protected TblAttempts _attempt;
        protected int _userID;

        #region Properties

        public TblAttempts Attempt
        {
            get
            {
                return _attempt;
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

        public CmiBaseCollection(TblAttempts attempt, int userID)
        {
            _attempt = attempt;
            _userID = userID;

            Initialize();
        }

        public virtual void Initialize()
        {
        }

        abstract public string GetValue(string n, string path);
        abstract public int SetValue(string n, string path, string value);
    }

    public class CmiVariable
    {
        private string name;
        private bool read;
        private bool write;
        private string[] allowed;
        private string init;

        #region Properties

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

        public CmiVariable(string _name, bool _read, bool _write, string[] _allowed, string _init)
        {
            name = _name;
            read = _read;
            write = _write;
            allowed = _allowed;
            init = _init;
        }
    }

    public class CmiDataModel
    {
        #region Cmi Variables

        private static Dictionary<string, CmiVariable> _variables = new Dictionary<string, CmiVariable>
        {
            {"completion_status", new CmiVariable("completion_status", true, true, new string[] { "completed", "incomplete", "not_attempted", "unknown" }, "unknown")},
            {"credit", new CmiVariable("credit", true, false, new string[] { "credit", "no-credit" }, "credit")},
            {"entry", new CmiVariable("entry", true, false, new string[] { "ab-initio", "resume", "" }, "ab-initio" )},
            
            {"exit", new CmiVariable("exit", true, true, null, null)},
            {"launch_data", new CmiVariable("launch_data", true, false, null, null)},
            {"learner_id", new CmiVariable("learner_id", true, false, null, null)},
            {"learner_name", new CmiVariable("learner_name", true, false, null, null)},
            {"location", new CmiVariable("location", true, true, null, null)},
            {"max_time_allowed", new CmiVariable("max_time_allowed", true, false, null, null)},
            {"mode", new CmiVariable("mode", true, false, null, null)},
            {"progress_measure", new CmiVariable("progress_measure", true, true, null, null)},
            {"scaled_passing_score", new CmiVariable("scaled_passing_score", true, false, null, null)},
            {"success_status", new CmiVariable("success_status", true, true, null, null)},
            {"suspend_data", new CmiVariable("suspend_data", true, true, null, null)},
            {"time_limit_action", new CmiVariable("time_limit_action", true, false, null, null)},
            {"session_time", new CmiVariable("session_time", false, true, null, null)},
        };

        private Dictionary<string, CmiBase> _collections;

        #endregion

        #region Variables

        private int _themeID;
        private int _userID;
        private TblAttempts _attempt;

        #endregion

        #region Properties

        public TblAttempts Attempt
        {
            get
            {
                return _attempt;
            }
        }
        public int ThemeID
        {
            get
            {
                return _themeID;
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

        #region Public Methods

        public CmiDataModel(int themeID, int userID)
        {
            _themeID = themeID;
            _userID = userID;

            Initialize();
            SetCollections();
        }

        public CmiDataModel(int AttemptId)
        {
            _attempt = ServerModel.DB.Load<TblAttempts>(AttemptId);
            
            _themeID = _attempt.ThemeRef;
            _userID = _attempt.UserRef;

            SetCollections();
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
                else if (_variables.ContainsKey(name))
                {
                    return GetVariable(name);
                }
            }

            if (_collections.ContainsKey(name))
            {
                return _collections[name].GetValue(string.Join(".", parts, 1, parts.Length - 1));
            }

            throw new Exception("Requested variable is not supported");
        }

        public int SetValue(string path, string value)
        {
            string[] parts = path.Split('.');
            string name = parts[0];

            if (name == path)
            {
                if (name == "_version")
                {
                    throw new Exception("Requested variable is read-only");
                }
                else if (name == "_children")
                {
                    throw new Exception("Requested variable is read-only");
                }
                else if (_variables.ContainsKey(name))
                {
                    return SetVariable(name, value);
                }
            }

            if (_collections.ContainsKey(name))
            {
                return _collections[name].SetValue(string.Join(".", parts, 1, parts.Length - 1), value);
            }

            throw new Exception("Requested variable is not supported");
        }

        #endregion

        #region Private Methods

        private void Initialize()
        {
            // Check cmi.exit to resume or not?
            // Initialize variables (cmi.entry) and etc
            // Load Attempt

            TblAttempts a = new TblAttempts
            {
                ThemeRef = _themeID,
                UserRef = _userID
            };

            int AttemptId = ServerModel.DB.Insert(a);

            _attempt = ServerModel.DB.Load<TblAttempts>(AttemptId);
        }

        private void SetCollections()
        {
            _collections = new Dictionary<string, CmiBase>
            {
                {"interactions", new Cmi.Interactions(_attempt, _userID)}
            };
        }

        private int SetVariable(string name, string value)
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
                                new ValueCondition<string>("cmi." + name), COMPARE_KIND.EQUAL)));

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
                    Name = "cmi." + name,
                    Value = value
                };

                return ServerModel.DB.Insert<TblAttemptsVars>(av);
            }
        }

        private string GetVariable(string name)
        {
            if (_variables[name].Read == false)
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
                    List<TblAttemptsVars> list = ServerModel.DB.Query<TblAttemptsVars>(
                        new AndCondtion(
                            new CompareCondition<int>(
                                DataObject.Schema.AttemptRef,
                                new ValueCondition<int>(Attempt.ID), COMPARE_KIND.EQUAL),
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
            string[] childArray = new string[_variables.Keys.Count];
            _variables.Keys.CopyTo(childArray, 0);

            return string.Join(",", childArray);
        }

        #endregion
    }
}

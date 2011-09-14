using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IUDICO.DataModel.DB;

namespace IUDICO.DataModel.Common
{
    public class LMSSettings
    {
        Dictionary<string, TblSettings> values = new Dictionary<string, TblSettings>();

        public LMSSettings()
        {
            var data = ServerModel.DB.Query<TblSettings>(null);

            foreach (TblSettings setting in data)
            {
                values.Add(setting.Name, setting);
            }
        }

        public string GetValue(string Key)
        {
            return (values.ContainsKey(Key) ? values[Key].Value : null);
        }

        public void SetValue(string Key, string Value)
        {
            if (values.ContainsKey(Key))
            {
                TblSettings setting = values[Key];
                setting.Value = Value;

                ServerModel.DB.Update(setting);
            }
            else
            {
                var setting = new TblSettings
                {
                    Name = Key,
                    Value = Value
                };

                ServerModel.DB.Insert(setting);
            }

            values[Key].Value = Value;
        }

        public List<TblSettings> GetAll()
        {
            return values.Values.ToList();
        }

        public List<TblSettings> Query(string Pattern)
        {
            return values.Where(d => d.Value.Name.Contains(Pattern) || d.Value.Value.Contains(Pattern)).Select(d => d.Value).ToList();
        }

        public TblSettings GetSettings(int ID)
        {
            return values.FirstOrDefault(d => d.Value.ID == ID).Value;
        }
    }
}

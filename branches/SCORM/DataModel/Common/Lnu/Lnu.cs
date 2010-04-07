using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.DB.Base;

namespace IUDICO.DataModel.Common
{
    public class LnuDataModel
    {
        public string GetValue(string path)
        {
            string[] parts = path.Split('.');
            string type = parts[0];

            if (type == "settings")
            {
                string name = parts[1];

                return ServerModel.DB.Query<TblSettings>(new CompareCondition<string> (
                    DataObject.Schema.Name,
                    new ValueCondition<string>(name), COMPARE_KIND.EQUAL)).FirstOrDefault().Value;
            }
            else
            {
                throw new Exception("Requested variable is not supported");
            }
        }

        public int SetValue(string path, string value)
        {
            string[] parts = path.Split('.');
            string type = parts[0];

            if (type == "settings")
            {
                string name = parts[1];

                TblSettings setting = ServerModel.DB.Query<TblSettings>(new CompareCondition<string>(
                    DataObject.Schema.Name,
                    new ValueCondition<string>(name), COMPARE_KIND.EQUAL)).FirstOrDefault();

                if (setting == null)
                {
                    throw new Exception("Requested variable is not supported");
                }

                setting.Value = value;

                ServerModel.DB.Update(setting);

                return 1;
            }
            else
            {
                throw new Exception("Requested variable is not supported");
            }
        }
    }
}

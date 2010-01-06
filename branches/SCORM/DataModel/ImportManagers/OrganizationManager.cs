using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using IUDICO.DataModel.Common.ImportUtils;
using IUDICO.DataModel.DB;

namespace IUDICO.DataModel.ImportManagers
{
    public class OrganizationManager
    {
        public static int Import(XmlNode organization, int courseID)
        {
            // store organization in db
            int organizationID = Store(courseID, XmlUtility.GetNode(organization, "ns:title").InnerText);

            // import list of <item>
            XmlNodeList items = XmlUtility.GetNodes(organization, "ns:item");
            foreach (XmlNode node in items)
            {
                ItemManager.Import(node, organizationID, null);
            }

            return organizationID;
        }

        private static int Store(int courseID, string title)
        {
            TblOrganizations t = new TblOrganizations
            {
                CourseRef = courseID,
                Title = title
            };

            ServerModel.DB.Insert(t);

            return t.ID;
        }
    }
}

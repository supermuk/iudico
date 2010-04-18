using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using IUDICO.DataModel.Common.ImportUtils;
using IUDICO.DataModel.DB;
using IUDICO.DataModel.DB.Base;

namespace IUDICO.DataModel.ImportManagers
{
    public class OrganizationManager
    {
        /// <summary>
        /// Import organization from XmlNodes
        /// </summary>
        /// <param name="imsmanifestOrganization">Organization in imsmanifest.xml</param>
        /// <param name="answersOrganization">Same organization(rank and questions) in answers.xml</param>
        /// <param name="courseID"></param>
        /// <returns></returns>
        public static int Import(XmlNode manifestXmlOrganization, XmlNode answersXmlOrganization, int courseID)
        {
            // store organization in db
            int organizationID = Store(courseID, XmlUtility.GetNode(manifestXmlOrganization, "ns:title").InnerText);

            // import list of <item>
            XmlNodeList manifestItems = XmlUtility.GetNodes(manifestXmlOrganization, "ns:item");
            XmlNodeList answerItems = XmlUtility.GetNodes(answersXmlOrganization, "item");

            foreach (XmlNode node in manifestItems)
            {
                ItemManager.Import(node, answerItems, organizationID, null);
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

        private static int GetRank(int organizationID)
        {
            int result=0;
            List<TblItems> items = ServerModel.DB.Query<TblItems>(new CompareCondition<int>(DataObject.Schema.OrganizationRef, new ValueCondition<int>(organizationID), COMPARE_KIND.EQUAL));
            foreach(TblItems item in items)
            {
              if(item.Rank!=null)
              {
                result += (int) item.Rank;
              }
            }
            return result;
        }
    }
}

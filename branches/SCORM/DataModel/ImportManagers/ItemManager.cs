using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using IUDICO.DataModel.Common.ImportUtils;
using IUDICO.DataModel.DB;

namespace IUDICO.DataModel.ImportManagers
{
    public class ItemManager
    {
        public static void Import(XmlNode item, int organizationID, int? parentID)
        {
            XmlNodeList childItems = XmlUtility.GetNodes(item, "ns:item");
            
            if (childItems.Count > 0)
            {
                int itemID = Store(parentID, organizationID, XmlUtility.GetNode(item, "ns:title").InnerText, null);

                foreach (XmlNode node in childItems)
                {
                    Import(node, organizationID, itemID);
                }
            }
            else
            {
                string resourceIdentifier = item.Attributes["identifierref"].Value;

                if (ResourceManager.Resources.ContainsKey(resourceIdentifier))
                {
                    Store(parentID, organizationID, XmlUtility.GetNode(item, "ns:title").InnerText, ResourceManager.Resources[resourceIdentifier]);
                }
            }
        }

        /// <summary>
        /// Store non-leaves
        /// </summary>
        /// <param name="pid">Parent ID of Item</param>
        /// <param name="organizationID">Organization ID to which this item belongs</param>
        /// <param name="title">Title of this item received from "title" tag</param>
        /// <param name="resourceID">Resource to which this item is linked</param>
        /// <returns>ID of the stored item</returns>
        private static int Store(int? pid, int organizationID, string title, int? resourceID)
        {
            TblItems t = new TblItems
            {
                PID = pid,
                OrganizationRef = organizationID,
                Title = title,
                IsLeaf = resourceID != null,
                ResourceRef = resourceID
            };

            ServerModel.DB.Insert(t);

            return t.ID;
        }
    }
}

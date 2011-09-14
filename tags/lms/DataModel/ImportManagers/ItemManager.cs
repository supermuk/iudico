using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using IUDICO.DataModel.Common.ImportUtils;
using IUDICO.DataModel.DB;

namespace IUDICO.DataModel.ImportManagers
{
    /// <summary>
    /// Class to work with items
    /// </summary>
    public class ItemManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="manifestItem"> Item tag from manifest file for current item </param>
        /// <param name="answerItems">Organization tag with current item tag from answers file</param>
        /// <param name="organizationID"></param>
        /// <param name="parentID"></param>
        public static void Import(XmlNode manifestItem, XmlNodeList answerItems, int organizationID, int? parentID)
        {
            XmlNodeList childItems = XmlUtility.GetNodes(manifestItem, "ns:item");

            if (childItems.Count > 0)
            {
                int itemID = Store(parentID, organizationID, XmlUtility.GetNode(manifestItem, "ns:title").InnerText, null, null);

                foreach (XmlNode node in childItems)
                {
                  Import(node, answerItems, organizationID, itemID);
                }
            }
            else
            {
                //string resourceIdentifier = manifestItem.Attributes["identifierref"].Value;
                string resourceIdentifier = XmlUtility.GetIdentifierRef(manifestItem);

                if (ResourceManager.Resources.ContainsKey(resourceIdentifier))
                {
                    Store(parentID, organizationID, XmlUtility.GetNode(manifestItem, "ns:title").InnerText,
                          ResourceManager.Resources[resourceIdentifier], XmlUtility.GetNodeById(answerItems, XmlUtility.GetIdentifier(manifestItem)));
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
        /// <param name="item">XmlNode represents *item* tag in answers file with rank tag</param>
        /// <returns>ID of the stored item</returns>
        private static int Store(int? pid, int organizationID, string title, int? resourceID, XmlNode item)
        {
            int? totalRank = null;
            if(item!=null)
            {
                XmlNodeList questions = XmlUtility.GetNodes(item, "question");
                totalRank = 0;
                foreach (XmlNode question in questions)
                {
                  int rank = 0;
                  if (int.TryParse(XmlUtility.GetNode(question, "rank").InnerText, out rank))
                  {
                    totalRank += rank;
                  }
                  else
                  {
                    throw new Exception(Translations.ItemManager_Store_Cannot_get_rank_for_item);
                  }
                }
            }

            TblItems t = new TblItems
            {
                PID = pid,
                OrganizationRef = organizationID,
                Title = title,
                IsLeaf = resourceID != null,
                ResourceRef = resourceID,
                Rank = totalRank
            };

            ServerModel.DB.Insert(t);

            return t.ID;
        }
    }
}

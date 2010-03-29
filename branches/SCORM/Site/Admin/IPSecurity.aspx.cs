using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Web.Administration;
using System.Web.Configuration;

public partial class Admin_IPSecurity : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack) 
        {
            using (ServerManager serverManager = new ServerManager())
            {
                Configuration config = serverManager.GetWebConfiguration("Iudico");
                ConfigurationSection section = config.GetSection("system.webServer/security/ipSecurity");
                ConfigurationElementCollection coll = section.GetCollection();
                CheckBoxList1.Items.Clear();
                for (int i = 0; i < coll.Count; i++)
                {
                    CheckBoxList1.Items.Add(coll[i].Attributes["ipAddress"].Value.ToString());
                }
            }
        }
        
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        using (ServerManager serverManager = new ServerManager())
        {
            Configuration config = serverManager.GetWebConfiguration("Iudico");
            ConfigurationSection section = config.GetSection("system.webServer/security/ipSecurity");
            ConfigurationElementCollection coll = section.GetCollection();
            ConfigurationElement element = coll.CreateElement("add");
            element.SetAttributeValue("ipAddress", TextBox1.Text);
            element["allowed"] = false;
            coll.Add(element);
            CheckBoxList1.Items.Add(TextBox1.Text);
            serverManager.CommitChanges();
            TextBox1.Text = "";
        }
    }
    protected void removeButton_Click(object sender, EventArgs e)
    {
        using (ServerManager serverManager = new ServerManager())
        {
            Configuration config = serverManager.GetWebConfiguration("Iudico");
            ConfigurationSection section = config.GetSection("system.webServer/security/ipSecurity");
            ConfigurationElementCollection coll = section.GetCollection();
            for (int i = 0; i < CheckBoxList1.Items.Count; i++)
            {
                if (CheckBoxList1.Items[i].Selected)
                {
                    ConfigurationElement removeElem = coll.Where(el => el.Attributes["ipAddress"].Value.ToString() == CheckBoxList1.Items[i].Text).FirstOrDefault();

                    if (removeElem != null)
                    {
                        coll.Remove(removeElem);
                    }
                }
            }

            serverManager.CommitChanges();

            CheckBoxList1.Items.Clear();
            for (int i = 0; i < coll.Count; i++)
            {
                CheckBoxList1.Items.Add(coll[i].Attributes["ipAddress"].Value.ToString());
            }
        }
    }
}

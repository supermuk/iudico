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
                Configuration config = serverManager.GetWebConfiguration("Hudson IUDICO");
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

    static string[] GetRange(string ips)
    {
        List<string> range = new List<string>();
        string ip = ips.Substring(0, ips.IndexOf("-"));
        string ip2 = ips.Substring(ips.IndexOf("-") + 1);
        string[] ip1sections = ip.Split('.');
        string[] ip2sections = ip2.Split('.');
        if (ip1sections[0] != ip2sections[0] || ip1sections[1] != ip2sections[1]
            || Convert.ToInt32(ip1sections[2]) > Convert.ToInt32(ip2sections[2])
            || (Convert.ToInt32(ip1sections[2]) <= Convert.ToInt32(ip2sections[2])
                && Convert.ToInt32(ip1sections[3]) > Convert.ToInt32(ip2sections[3])))
        {
            throw new Exception("Wrong range bounds");
        }
        else
        {
            range.Add(ip);
            while (!ip.Equals(ip2))
            {
                ip = Iterate(ip);
                range.Add(ip);
            }
        }

        return range.ToArray();
    }

    static string Iterate(string ip)
    {
        string[] string_sections = ip.Split('.');
        int[] sections = new int[4];
        sections[0] = Convert.ToInt32(string_sections[0]);
        sections[1] = Convert.ToInt32(string_sections[1]);
        sections[2] = Convert.ToInt32(string_sections[2]);
        sections[3] = Convert.ToInt32(string_sections[3]);

        if (sections[3] < 255)
        {
            sections[3]++;
        }
        else if (sections[2] < 255)
        {
            sections[2]++;
            sections[3] = 0;
        }
        else throw new Exception("Can't iterate ip");

        return sections[0] + "." + sections[1] + "." + sections[2] + "." + sections[3];
    }

    static bool IsValid(string ip)
    {
        string[] sections = ip.Split('.');
        if (sections.Count() != 4)
        {
            return false;
        }

        foreach (var item in sections)
        {
            int num = Convert.ToInt32(item);
            if (num > 255 || num < 0)
            {
                return false;
            }
        }

        return true;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        using (ServerManager serverManager = new ServerManager())
        {
            Configuration config = serverManager.GetWebConfiguration("Iudico");
            ConfigurationSection section = config.GetSection("system.webServer/security/ipSecurity");
            ConfigurationElementCollection coll = section.GetCollection();
            string ip = TextBox1.Text.Replace(" ", "");
            if (ip.Contains("-"))
            {
                string left = ip.Substring(0, ip.IndexOf("-"));
                string right = ip.Substring(ip.IndexOf("-") + 1);
                if (IsValid(left) && IsValid(right))
                {
                    GetRange(ip);
                    foreach (string item in GetRange(ip))
                    {
                        ConfigurationElement element = coll.CreateElement("add");
                        element.SetAttributeValue("ipAddress", item);
                        element["allowed"] = false;
                        coll.Add(element);
                        CheckBoxList1.Items.Add(item);
                    }
                }

            }
            
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

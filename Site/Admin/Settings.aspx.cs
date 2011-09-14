using IUDICO.DataModel;
using IUDICO.DataModel.Controllers;
using IUDICO.DataModel.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Web.Administration;
using System.Web.Configuration;

public partial class Admin_Settings : ControlledPage<Admin_SettingsController>
{
    string siteName;

    protected void Page_Load(object sender, EventArgs e)
    {
        siteName = System.Configuration.ConfigurationSettings.AppSettings["siteName"];

        /*if (!Page.IsPostBack)
        {
            using (ServerManager serverManager = new ServerManager())
            {
                Configuration config = serverManager.GetWebConfiguration(siteName);
                ConfigurationSection section = config.GetSection("system.webServer/security/ipSecurity");
                if (Convert.ToBoolean(section.Attributes["allowUnlisted"].Value))
                {
                    ComboBox1.SelectedValue = "Allow";
                    ConfigurationElementCollection coll = section.GetCollection();
                    CheckBoxList1.Items.Clear();
                    for (int i = 0; i < coll.Count; i++)
                    {
                        if (!Convert.ToBoolean(coll[i].Attributes["allowed"].Value))
                        {
                            CheckBoxList1.Items.Add(coll[i].Attributes["ipAddress"].Value.ToString());
                        }
                        
                    }
                }
                else
                {
                    ComboBox1.SelectedValue = "Deny";
                    ConfigurationElementCollection coll = section.GetCollection();
                    CheckBoxList1.Items.Clear();
                    for (int i = 0; i < coll.Count; i++)
                    {
                        if (Convert.ToBoolean(coll[i].Attributes["allowed"].Value))
                        {
                            CheckBoxList1.Items.Add(coll[i].Attributes["ipAddress"].Value.ToString());
                        }

                    }
                }
                
            }
        }*/
    }

    protected override void BindController(Admin_SettingsController c)
    {
        base.BindController(c);
        Bind2Ways(tbSearchPattern, c.SearchPattern);
        Bind(btnSearch, DataBind);
    }

    public override void DataBind()
    {
        base.DataBind();
        SettingList.DataSource = Controller.GetSettings();
        SettingList.DataBind();
        btnCreateSetting.PostBackUrl = ServerModel.Forms.BuildRedirectUrl(new Admin_CreateSettingController { BackUrl = Request.Url.AbsolutePath });
    }
    private string[] ConvertIP(string line)
    {
        string[] arr = line.Split(',');
        List<string> ips = new List<string>();
        foreach (string item in arr)
        {
            if (item.Contains("-"))
            {
                foreach (string ip in GetRange(item))
                {
                    if (!ips.Contains(ip))
                    {
                        ips.Add(ip);
                    }
                }
            }
            else
            {
                if (IsValidIP(item) && !ips.Contains(item))
                {
                    ips.Add(item);
                }
            }
        }

        return ips.ToArray();
    }

    private string[] GetRange(string ips)
    {
        List<string> range = new List<string>();
        string ip = ips.Substring(0, ips.IndexOf("-"));
        string ip2 = ips.Substring(ips.IndexOf("-") + 1);
        if (!IsValidIP(ip) || !IsValidIP(ip2))
        {
            throw new Exception("Invalid ip");
        }
        string[] ip1sections = ip.Split('.');
        string[] ip2sections = ip2.Split('.');
        if (ip1sections[0] != ip2sections[0] || ip1sections[1] != ip2sections[1]
            || Convert.ToInt32(ip1sections[2]) > Convert.ToInt32(ip2sections[2])
            || (Convert.ToInt32(ip1sections[2]) <= Convert.ToInt32(ip2sections[2])
                && Convert.ToInt32(ip1sections[3]) > Convert.ToInt32(ip2sections[3])))
        {
            throw new Exception("Wrong sequence");
        }
        else
        {
            while (!ip.Equals(ip2))
            {
                ip = Iterate(ip);
                range.Add(ip);
                Console.WriteLine(ip);
            }
        }

        return range.ToArray();
    }

    private string Iterate(string ip)
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

    private bool IsValidIP(string ip)
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
            Configuration config = serverManager.GetWebConfiguration(siteName);
            ConfigurationSection section = config.GetSection("system.webServer/security/ipSecurity");
            ConfigurationElementCollection coll = section.GetCollection();
            string ips = TextBox1.Text.Replace(" ", "");
            foreach (string ip in ConvertIP(ips))
            {
                ConfigurationElement element = coll.CreateElement("add");
                element.SetAttributeValue("ipAddress", ip);
                if (ComboBox1.SelectedValue == "Allow")
                {
                    element["allowed"] = false;
                }
                else
                {
                    element["allowed"] = true;
                }
                
                coll.Add(element);
                CheckBoxList1.Items.Add(ip);
            }
            serverManager.CommitChanges();
            TextBox1.Text = "";
        }
    }
    protected void removeButton_Click(object sender, EventArgs e)
    {
        using (ServerManager serverManager = new ServerManager())
        {
            Configuration config = serverManager.GetWebConfiguration(siteName);
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
    protected void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Page.IsPostBack)
        {
            using (ServerManager serverManager = new ServerManager())
            {
                Configuration config = serverManager.GetWebConfiguration(siteName);
                ConfigurationSection section = config.GetSection("system.webServer/security/ipSecurity");
                if (ComboBox1.SelectedValue == "Allow")
                {

                    section.Attributes["allowUnlisted"].Value = true;
                    ConfigurationElementCollection coll = section.GetCollection();
                    CheckBoxList1.Items.Clear();
                    for (int i = 0; i < coll.Count; i++)
                    {
                        if (!Convert.ToBoolean(coll[i].Attributes["allowed"].Value))
                        {
                            CheckBoxList1.Items.Add(coll[i].Attributes["ipAddress"].Value.ToString());
                        }

                    }
                }
                else
                {
                    section.Attributes["allowUnlisted"].Value = false;
                    ConfigurationElementCollection coll = section.GetCollection();
                    CheckBoxList1.Items.Clear();
                    for (int i = 0; i < coll.Count; i++)
                    {
                        if (Convert.ToBoolean(coll[i].Attributes["allowed"].Value))
                        {
                            CheckBoxList1.Items.Add(coll[i].Attributes["ipAddress"].Value.ToString());
                        }
                    }
                }
                serverManager.CommitChanges();
            }
        }
    }
}

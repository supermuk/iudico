using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using DBUpdater.Properties;
using LEX.CONTROLS;

namespace DBUpdater
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            RememberTextBoxController.BindSettings(Settings.Default, Settings.Default);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new frmSelectDB());

            Settings.Default.Save();
        }
    }
}

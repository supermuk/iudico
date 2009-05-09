using System;
using System.Windows.Forms;
using DBUpdater.Properties;

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

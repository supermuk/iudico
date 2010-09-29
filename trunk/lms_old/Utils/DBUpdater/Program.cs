using System;
using System.Windows.Forms;
using LEX.CONTROLS.DBUpdater.Properties;

namespace LEX.CONTROLS.DBUpdater
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

            var sb = SelectDatabaseForm.ShowPrompt(null);
            if (sb != null)
            {
                Application.Run(new DBStatusForm(sb));
                Settings.Default.Save();
            }
        }
    }
}

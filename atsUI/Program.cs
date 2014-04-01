using System;
using System.Windows.Forms;

namespace atsUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            using (ATSUI atsui = new ATSUI())
            {
                atsui.init();
                Application.Run(atsui);
            }
        }
    }
}

using EventManager.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EventManager
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
            if (Application.UserAppDataRegistry.GetValue("remeberMe") != null)
            {
                Dashboard dashboard = new Dashboard();
                dashboard.Show();
            }
            else
            {
                Login login = new Login();
                login.Show();
            }
            Application.Run();
        }
    }
}

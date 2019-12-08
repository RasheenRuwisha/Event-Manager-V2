using EventManager.DatabaseHelper;
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
                if (Application.UserAppDataRegistry.GetValue("remeberMe").ToString().Equals("True"))
                {
                    Dashboard dashboard = new Dashboard();
                    dashboard.Show();
                }
                else
                {
                    Login login = new Login();
                    login.Show();
                }
            
            }
            else
            {
                Login login = new Login();
                login.Show();
            }


            DatabaseConnectivity databaseConnectivity = new DatabaseConnectivity();
            databaseConnectivity.CreateLocalXmlFile();
            databaseConnectivity.ConnectionValidator();
            Application.Run();
        }
    }
}

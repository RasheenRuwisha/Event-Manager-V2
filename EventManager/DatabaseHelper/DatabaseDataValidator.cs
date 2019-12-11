using EventManager.Model;
using EventManager.UIComponents;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EventManager.DatabaseHelper
{
    public class DatabaseDataValidator
    {
        readonly string workingDir = Directory.GetCurrentDirectory();
        string userId = Application.UserAppDataRegistry.GetValue("userID").ToString();

        public async Task DataValidator()
        {
            string t = await Task.Run(() => this.DoesMatch());
        }

        /// <summary>
        /// This method syncs the data that was done while the user was offline.
        /// It checks the avaliability of the files which were created for syncing with the database and once synced the file is then delted.
        /// </summary>
        /// <returns></returns>
        public string DoesMatch()
        {
            if(Application.UserAppDataRegistry.GetValue("dbMatch") != null)
            {
                if (Application.UserAppDataRegistry.GetValue("dbMatch").ToString().Equals("False"))
                {
                    if (Application.UserAppDataRegistry.GetValue("dbConnection").ToString().Equals("True"))
                    {

                        if (Application.OpenForms.OfType<DataSync>().Count() != 1)
                        {
                            DataSync dataSync = new DataSync();
                            dataSync.ShowDialog();
                        }

                    }
                }
            }
            return "success";
        }

    }
}

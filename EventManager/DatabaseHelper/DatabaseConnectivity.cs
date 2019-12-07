using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace EventManager.DatabaseHelper
{
    class DatabaseConnectivity
    {
        readonly DatabaseDataValidator databaseDataValidator = new DatabaseDataValidator();
        int successNotificationCount = 0;
        int failNotificationCount = 0;

        public async Task connectionValidator()
        {
            String t = "";
            while (true)
            {
                t = await Task.Run(() => this.checkConnection());
                await Task.Delay(20000);
            }
        }

        public String checkConnection()
        {
            try
            {
                DatabaseModel db = new DatabaseModel();
                db.Database.Connection.Open();
                db.Database.Connection.Close();
                Application.UserAppDataRegistry.SetValue("dbConnection", true);

                if (Application.UserAppDataRegistry.GetValue("dbMatch") != null)
                {
                    if (Application.UserAppDataRegistry.GetValue("dbMatch").Equals("False"))
                    {
                        NotifyIcon notifyIcon = new NotifyIcon
                        {
                            Icon = new Icon(SystemIcons.Application, 40, 40),
                            Visible = true,
                            Text = "Event Manager",
                            BalloonTipText = "The database connection was restablished.Syncing Data to Database.",
                            BalloonTipIcon = ToolTipIcon.Info,
                            BalloonTipTitle = "Database Connection"
                        };
                        notifyIcon.ShowBalloonTip(5000);
                    }
                }
                if(Application.UserAppDataRegistry.GetValue("userId") != null)
                {
                    databaseDataValidator.dataValidator();
                }
                successNotificationCount++;
                failNotificationCount =0;
                return "success";
            }
            catch (Exception ex)
            {
                Application.UserAppDataRegistry.SetValue("dbConnection", false);
                if (failNotificationCount == 0)
                {
                    NotifyIcon notifyIcon = new NotifyIcon
                    {
                        Icon = new Icon(SystemIcons.Application, 40, 40),
                        Visible = true,
                        Text = "Event Manager",
                        BalloonTipText = "The database connection could not be established.Data will be saved offline",
                        BalloonTipIcon = ToolTipIcon.Error,
                        BalloonTipTitle = "Database Connection"
                    };
                    notifyIcon.ShowBalloonTip(10000);
                }
                failNotificationCount++;
                successNotificationCount = 0;
                return "false";
            }
        }


        public void CreateLocalXmlFile()
        {
            if(Application.UserAppDataRegistry.GetValue("userID") != null)
            {
                string userId = Application.UserAppDataRegistry.GetValue("userID").ToString();
                string workingDir = Directory.GetCurrentDirectory();

                if (!File.Exists(workingDir + $@"\{userId}.xml"))
                {
                    XDocument xmlDoc = new XDocument();
                    xmlDoc.Add(new XElement("LocalStore"));
                    xmlDoc.Save(workingDir + $@"\{userId}.xml");
                }
            }
        }
    }
}

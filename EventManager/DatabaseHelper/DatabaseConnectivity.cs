﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EventManager.DatabaseHelper
{
    class DatabaseConnectivity
    {
        private bool wasOffline = false;
        int successNotificationCount = 0;
        int failNotificationCount = 0;

        public async Task connectionValidator()
        {
            String t = "";
            while (true)
            {
                t = await Task.Run(() => this.checkConnection());
                await Task.Delay(10000);
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
                if (successNotificationCount == 0)
                {
                    NotifyIcon notifyIcon = new NotifyIcon();
                    notifyIcon.Icon = new Icon(SystemIcons.Application, 40, 40);
                    notifyIcon.Visible = true;
                    notifyIcon.Text = "Event Manager";
                    notifyIcon.BalloonTipText = "The database connection was restablished.Please Refresh to sync data";
                    notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
                    notifyIcon.BalloonTipTitle = "Database Connection";
                    notifyIcon.ShowBalloonTip(10000);
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
                    NotifyIcon notifyIcon = new NotifyIcon();
                    notifyIcon.Icon = new Icon(SystemIcons.Application, 40, 40);
                    notifyIcon.Visible = true;
                    notifyIcon.Text = "Event Manager";
                    notifyIcon.BalloonTipText = "The database connection could not be established.Data will be saved offline";
                    notifyIcon.BalloonTipIcon = ToolTipIcon.Error;
                    notifyIcon.BalloonTipTitle = "Database Connection";
                    notifyIcon.ShowBalloonTip(10000);
                }
                failNotificationCount++;
                successNotificationCount = 0;
                return "false";
            }
        }

    }
}

using EventManager.Model;
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
        readonly String workingDir = Directory.GetCurrentDirectory();
        public async Task dataValidator()
        {
            String t = "";
            t = await Task.Run(() => this.doesMatch());
        }

        public String doesMatch()
        {
            EventHelper eventHelper = new EventHelper();

            string zzz = Application.UserAppDataRegistry.GetValue("dbMatch").ToString();
            if (Application.UserAppDataRegistry.GetValue("dbMatch").ToString().Equals("False"))
            {
                if (Application.UserAppDataRegistry.GetValue("dbConnection").ToString().Equals("True"))
                {
                    try
                    {
                        if (File.Exists(workingDir + @"\event_add.xml"))
                        {
                            List<UserEvent> userEvents = eventHelper.GettAllUpdateEvent(workingDir + @"\event_add.xml");
                            foreach (UserEvent userEvent in userEvents)
                            {
                                eventHelper.AddEvent(userEvent);
                            }
                            File.Delete(workingDir + @"\event_add.xml");
                        }


                        if (File.Exists(workingDir + @"\event_update.xml"))
                        {
                            List<UserEvent> userEventUpdate = eventHelper.GettAllUpdateEvent(workingDir + @"\event_update.xml");
                            foreach (UserEvent userEvent in userEventUpdate)
                            {
                                eventHelper.UpdateEvent(userEvent);
                            }
                            File.Delete(workingDir + @"\event_update.xml");
                        }


                        if (File.Exists(workingDir + @"\event_remove.xml"))
                        {
                            List<UserEvent> userEventRemove = eventHelper.GettAllUpdateEvent(workingDir + @"\event_remove.xml");
                            foreach (UserEvent userEvent in userEventRemove)
                            {
                                eventHelper.RemoveEvent(userEvent.EventId);
                            }
                            File.Delete(workingDir + @"\event_remove.xml");
                        }

                        Application.UserAppDataRegistry.SetValue("dbMatch", true);
                        NotifyIcon notifyIcon = new NotifyIcon();
                        notifyIcon.Icon = new Icon(SystemIcons.Application, 40, 40);
                        notifyIcon.Visible = true;
                        notifyIcon.Text = "Event Manager";
                        notifyIcon.BalloonTipText = "Data has been synced successfully. Please refresh the view to retreive synced data!";
                        notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
                        notifyIcon.BalloonTipTitle = "Database Connection";
                        notifyIcon.ShowBalloonTip(10000);
                        return "success";
                    }
                    catch (Exception ex)
                    {
                        Application.UserAppDataRegistry.SetValue("dbMatch", false);
                        return "false";
                    }
                }
            }
            return "success";
        }

    }
}

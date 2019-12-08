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
        readonly string workingDir = Directory.GetCurrentDirectory();
        public async Task DataValidator()
        {
            string t = await Task.Run(() => this.DoesMatch());
        }

        public string DoesMatch()
        {
            if(Application.UserAppDataRegistry.GetValue("dbMatch") != null)
            {
                if (Application.UserAppDataRegistry.GetValue("dbMatch").ToString().Equals("False"))
                {
                    if (Application.UserAppDataRegistry.GetValue("dbConnection").ToString().Equals("True"))
                    {
                        try
                        {
                            if (File.Exists(workingDir + @"\event_add.xml"))
                            {
                                List<UserEvent> userEvents = EventHelper.GettAllUpdateEvent(workingDir + @"\event_add.xml");
                                foreach (UserEvent userEvent in userEvents)
                                {
                                    EventHelper.AddEvent(userEvent);
                                }
                                File.Delete(workingDir + @"\event_add.xml");
                            }


                            if (File.Exists(workingDir + @"\event_update.xml"))
                            {
                                List<UserEvent> userEventUpdate = EventHelper.GettAllUpdateEvent(workingDir + @"\event_update.xml");
                                foreach (UserEvent userEvent in userEventUpdate)
                                {
                                    EventHelper.UpdateEvent(userEvent);
                                }
                                File.Delete(workingDir + @"\event_update.xml");
                            }


                            if (File.Exists(workingDir + @"\event_remove.xml"))
                            {
                                List<UserEvent> userEventRemove = EventHelper.GettAllUpdateEvent(workingDir + @"\event_remove.xml");
                                foreach (UserEvent userEvent in userEventRemove)
                                {
                                    EventHelper.RemoveEvent(userEvent.EventId);
                                }
                                File.Delete(workingDir + @"\event_remove.xml");
                            }

                            if (File.Exists(workingDir + @"\contact_add.xml"))
                            {
                                List<Contact> contacts = ContactHelper.GettAllUpdateContact(workingDir + @"\contact_add.xml");
                                foreach (Contact contact in contacts)
                                {
                                    ContactHelper.AddContact(contact);
                                }
                                File.Delete(workingDir + @"\contact_add.xml");
                            }


                            if (File.Exists(workingDir + @"\contact_update.xml"))
                            {
                                List<Contact> contactUpdates = ContactHelper.GettAllUpdateContact(workingDir + @"\contact_update.xml");
                                foreach (Contact contact in contactUpdates)
                                {
                                    ContactHelper.UpdateContacts(contact);
                                }
                                File.Delete(workingDir + @"\contact_update.xml");
                            }


                            if (File.Exists(workingDir + @"\contact_remove.xml"))
                            {
                                List<Contact> contactRemove = ContactHelper.GettAllUpdateContact(workingDir + @"\contact_remove.xml");
                                foreach (Contact contact in contactRemove)
                                {
                                    ContactHelper.RemoveContact(contact.ContactId);
                                }
                                File.Delete(workingDir + @"\contact_remove.xml");
                            }

                            Application.UserAppDataRegistry.SetValue("dbMatch", true);
                            NotifyIcon notifyIcon = new NotifyIcon
                            {
                                Icon = new Icon(SystemIcons.Application, 40, 40),
                                Visible = true,
                                Text = "Event Manager",
                                BalloonTipText = "Data has been synced successfully.",
                                BalloonTipIcon = ToolTipIcon.Info,
                                BalloonTipTitle = "Database Connection"
                            };
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
            }
            return "success";
        }

    }
}

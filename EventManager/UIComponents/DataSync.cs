using EventManager.DatabaseHelper;
using EventManager.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EventManager.UIComponents
{
    public partial class DataSync : Form
    {

        readonly string workingDir = Directory.GetCurrentDirectory();
        string userId = Application.UserAppDataRegistry.GetValue("userID").ToString();

        public DataSync()
        {
            InitializeComponent();
            var worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler(BackgroundWorkerDoWork);
            worker.RunWorkerCompleted += (sender, e) =>
                this.Close();
            worker.RunWorkerAsync();
        }


        private void BackgroundWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            SyncData();
        }

        public void SyncData()
        {
            try
            {
                if (File.Exists(workingDir + $@"\{userId}contact_add.xml"))
                {
                    List<Contact> contacts = ContactHelper.GettAllUpdateContact(workingDir + $@"\{userId}contact_add.xml");
                    foreach (Contact contact in contacts)
                    {
                        ContactHelper.AddContact(contact);
                    }
                    File.Delete(workingDir + $@"\{userId}contact_add.xml");
                }


                if (File.Exists(workingDir + $@"\{userId}contact_update.xml"))
                {
                    List<Contact> contactUpdates = ContactHelper.GettAllUpdateContact(workingDir + $@"\{userId}contact_update.xml");
                    foreach (Contact contact in contactUpdates)
                    {
                        ContactHelper.UpdateContacts(contact);
                    }
                    File.Delete(workingDir + $@"\{userId}contact_update.xml");
                }


                if (File.Exists(workingDir + $@"\{userId}contact_remove.xml"))
                {
                    List<Contact> contactRemove = ContactHelper.GettAllUpdateContact(workingDir + $@"\{userId}contact_remove.xml");
                    foreach (Contact contact in contactRemove)
                    {
                        ContactHelper.RemoveContact(contact.ContactId);
                    }
                    File.Delete(workingDir + $@"\{userId}contact_remove.xml");
                }

                if (File.Exists(workingDir + $@"\{userId}event_add.xml"))
                {
                    List<UserEvent> userEvents = EventHelper.GetAllUpdateEvent(workingDir + $@"\{userId}event_add.xml");
                    foreach (UserEvent userEvent in userEvents)
                    {
                        EventHelper.AddEvent(userEvent);
                    }
                    File.Delete(workingDir + $@"\{userId}event_add.xml");
                }


                if (File.Exists(workingDir + $@"\{userId}event_update.xml"))
                {
                    List<UserEvent> userEventUpdate = EventHelper.GetAllUpdateEvent(workingDir + $@"\{userId}event_update.xml");
                    foreach (UserEvent userEvent in userEventUpdate)
                    {
                        EventHelper.UpdateEvent(userEvent);
                    }
                    File.Delete(workingDir + $@"\{userId}event_update.xml");
                }


                if (File.Exists(workingDir + $@"\{userId}event_remove.xml"))
                {
                    List<UserEvent> userEventRemove = EventHelper.GetAllUpdateEvent(workingDir + $@"\{userId}event_remove.xml");
                    foreach (UserEvent userEvent in userEventRemove)
                    {
                        EventHelper.RemoveEvent(userEvent.EventId);
                    }
                    File.Delete(workingDir + $@"\{userId}event_remove.xml");
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
            }
            catch (Exception ex)
            {
                Application.UserAppDataRegistry.SetValue("dbMatch", false);
            }

        }

        
    }
}

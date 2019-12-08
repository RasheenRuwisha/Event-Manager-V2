using EventManager.DatabaseHelper;
using EventManager.Model;
using EventManager.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EventManager.UIComponents
{
    public partial class RepeatEventConfirmation : Form
    {

        UserEvent eventDetails = new UserEvent();
        CommonUtil commonUtil = new CommonUtil();
        Form form;
        public RepeatEventConfirmation()
        {
            InitializeComponent();
        }

        public RepeatEventConfirmation(UserEvent userEvent, Form form)
        {
            InitializeComponent();
            this.eventDetails = userEvent;
            this.form = form;
        }

        public RepeatEventConfirmation(UserEvent userEvent)
        {
            InitializeComponent();
            this.eventDetails = userEvent;
        }

        private async void btn_alleventsinseries_Click(object sender, EventArgs e)
        {
            DisableButtons();
            if (form == null)
            {
                if (eventDetails.ParentId != null)
                {
                    List<UserEvent> userEvents = EventHelper.GetChildEvents(eventDetails.ParentId);
                    foreach (UserEvent userEvent in userEvents)
                    {
                        await Task.Run(() => EventHelper.RemoveEvent(userEvent.EventId));
                    }
                    await Task.Run(() => EventHelper.RemoveEvent(eventDetails.ParentId));

                }
                else
                {
                    List<UserEvent> userEvents = EventHelper.GetChildEvents(eventDetails.EventId);
                    foreach (UserEvent userEvent in userEvents)
                    {
                        await Task.Run(() => EventHelper.RemoveEvent(userEvent.EventId));
                    }
                    await Task.Run(() => EventHelper.RemoveEvent(eventDetails.EventId));
                }



                Notification notification = new Notification("Event Deleted Successfully");
                Timer timer = new Timer();
                notification.Show();

                timer.Tick += (o, ea) =>
                {
                    notification.Close();
                    this.Close();
                };

                timer.Interval = 1000;
                timer.Start();

            }
            else
            {
                UserEvent existingEvent = EventHelper.GetUserEvent(eventDetails.EventId);
                eventDetails.StartDate = existingEvent.StartDate;
                eventDetails.EndDate = existingEvent.EndDate;
                eventDetails.RepeatTill = existingEvent.RepeatTill;
                await Task.Run(() => EventHelper.RemoveEvent(eventDetails.EventId));
                await Task.Run(() => EventHelper.AddEvent(eventDetails));

                Notification notification = new Notification("Event Updated Successfully");
                Timer timer = new Timer();
                notification.Show();

                timer.Tick += (o, ea) =>
                {
                    notification.Close();
                    this.Close();
                };

                timer.Interval = 1000;
                timer.Start();
                form.Close();
            }

        }

        private async void btn_futureevents_Click(object sender, EventArgs e)
        {
            DisableButtons();
            if (form == null)
            {
                UserEvent existingEvent = EventHelper.GetUserEvent(eventDetails.EventId);
                if (existingEvent.StartDate == eventDetails.StartDate)
                {
                    await Task.Run(() => EventHelper.RemoveEvent(eventDetails.EventId));
                }
                else
                {
                    existingEvent.RepeatTill = eventDetails.StartDate.Date;
                    await Task.Run(() => EventHelper.UpdateEvent(existingEvent)); 
                }
               

                Notification notification = new Notification("Event Deleted Successfully");
                Timer timer = new Timer();
                notification.Show();

                timer.Tick += (o, ea) =>
                {
                    notification.Close();
                    this.Close();
                };

                timer.Interval = 1000;
                timer.Start();
            }
            else
            {
                UserEvent existingEvent = EventHelper.GetUserEvent(eventDetails.EventId);
                if (existingEvent.StartDate == eventDetails.StartDate)
                {
                    await Task.Run(() => EventHelper.RemoveEvent(eventDetails.EventId));
                }
                else
                {
                    existingEvent.RepeatTill = eventDetails.StartDate.Date;
                    eventDetails.EventId = commonUtil.GenerateUserId("event");
                    await Task.Run(() => EventHelper.RemoveEvent(existingEvent.EventId));
                    await Task.Run(() => EventHelper.AddEvent(existingEvent));
                    await Task.Run(() => EventHelper.AddEvent(eventDetails));
                }

                Notification notification = new Notification("Event Updated Successfully");
                Timer timer = new Timer();
                notification.Show();

                timer.Tick += (o, ea) =>
                {
                    notification.Close();
                    this.Close();
                };

                timer.Interval = 1000;
                timer.Start();
                form.Close();
            }

        }

        private async void btn_thisonly_Click(object sender, EventArgs e)
        {
            DisableButtons();
            if (form == null)
            {
                UserEvent existingEvent = EventHelper.GetUserEvent(eventDetails.EventId);
                //if (existingEvent.StartDate == eventDetails.StartDate && existingEvent.RepeatTill == eventDetails.RepeatTill)
                //{
                //    await Task.Run(() => EventHelper.RemoveEvent(existingEvent.EventId));
                //}

                if (existingEvent.StartDate == eventDetails.StartDate && existingEvent.EndDate == eventDetails.EndDate)
                {
                    if (existingEvent.RepeatType.Equals("Daily"))
                    {
                        existingEvent.StartDate = existingEvent.StartDate.AddDays(1);
                        existingEvent.EndDate = existingEvent.EndDate.AddDays(1);
                    }
                    else if (existingEvent.RepeatType.Equals("Weekly"))
                    {
                        existingEvent.StartDate = existingEvent.StartDate.AddDays(7);
                        existingEvent.EndDate = existingEvent.EndDate.AddDays(7);
                    }
                    else if (existingEvent.RepeatType.Equals("Monthly"))
                    {
                        existingEvent.StartDate = existingEvent.StartDate.AddDays(30);
                        existingEvent.EndDate = existingEvent.EndDate.AddDays(30);
                    }
                }
                else
                {

                    existingEvent.RepeatTill = eventDetails.StartDate.Date;
                    eventDetails.EventId = commonUtil.GenerateUserId("event");
                    if (existingEvent.ParentId == null)
                    {
                        eventDetails.ParentId = existingEvent.EventId;
                    }
                    else if (existingEvent.ParentId.Equals(""))
                    {
                        eventDetails.ParentId = existingEvent.EventId;
                    }
                    else
                    {
                        eventDetails.ParentId = existingEvent.ParentId;
                    }
                    if (eventDetails.RepeatType.Equals("Daily"))
                    {
                        eventDetails.StartDate = eventDetails.StartDate.AddDays(1);
                        eventDetails.EndDate = eventDetails.EndDate.AddDays(1);
                    }
                    else if (eventDetails.RepeatType.Equals("Weekly"))
                    {
                        eventDetails.StartDate = eventDetails.StartDate.AddDays(7);
                        eventDetails.EndDate = eventDetails.EndDate.AddDays(7);
                    }
                    else if (eventDetails.RepeatType.Equals("Monthly"))
                    {
                        eventDetails.StartDate = eventDetails.StartDate.AddDays(30);
                        eventDetails.EndDate = eventDetails.EndDate.AddDays(30);
                    }
                    await Task.Run(() => EventHelper.AddEvent(eventDetails));
                }
                await Task.Run(() => EventHelper.UpdateEvent(existingEvent));

                Notification notification = new Notification("Event Deleted Successfully");
                Timer timer = new Timer();
                notification.Show();

                timer.Tick += (o, ea) =>
                {
                    notification.Close();
                    this.Close();
                };

                timer.Interval = 1000;
                timer.Start();
            }
            else
            {
                DateTime repeatTill = eventDetails.RepeatTill;
                UserEvent existingEvent = EventHelper.GetUserEvent(eventDetails.EventId);
                existingEvent.RepeatTill = eventDetails.StartDate.Date;
                await Task.Run(() => EventHelper.UpdateEvent(existingEvent));


                eventDetails.EventId = commonUtil.GenerateUserId("event");
                eventDetails.ParentId = existingEvent.EventId;
                eventDetails.RepeatTill = eventDetails.EndDate.Date;
                await Task.Run(() => EventHelper.AddEvent(eventDetails));

                existingEvent.RepeatTill = repeatTill;
                if (existingEvent.RepeatType.Equals("Daily"))
                {
                    existingEvent.StartDate = eventDetails.StartDate.AddDays(1);
                    existingEvent.EndDate = eventDetails.EndDate.AddDays(1);
                }
                else if (existingEvent.RepeatType.Equals("Weekly"))
                {
                    existingEvent.StartDate = eventDetails.StartDate.AddDays(7);
                    existingEvent.EndDate = eventDetails.EndDate.AddDays(1);
                }
                else if (existingEvent.RepeatType.Equals("Monthly"))
                {
                    existingEvent.StartDate = eventDetails.StartDate.AddDays(30);
                    existingEvent.EndDate = eventDetails.EndDate.AddDays(30);
                }
                existingEvent.ParentId = eventDetails.ParentId;
                existingEvent.EventId = commonUtil.GenerateUserId("event");
                await Task.Run(() => EventHelper.AddEvent(existingEvent));

                Notification notification = new Notification("Event Updated Successfully");
                Timer timer = new Timer();
                notification.Show();

                timer.Tick += (o, ea) =>
                {
                    notification.Close();
                    this.Close();
                };

                timer.Interval = 1000;
                timer.Start();
                form.Close();
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();

            if (form != null)
            {
                form.Close();
            }
        }


        private void DisableButtons()
        {
            btn_alleventsinseries.Enabled = false;
            btn_futureevents.Enabled = false;
            btn_thisonly.Enabled = false;
            btn_cancel.Enabled = false;
        }

        private void EnableButtons()
        {
            btn_alleventsinseries.Enabled = true;
            btn_futureevents.Enabled = true;
            btn_thisonly.Enabled = true;
            btn_cancel.Enabled = true;
        }
    }
}

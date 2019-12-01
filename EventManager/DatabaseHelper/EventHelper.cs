using EventManager.Model;
using EventManager.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace EventManager.DatabaseHelper
{
    public class EventHelper
    {
        readonly String userId = Application.UserAppDataRegistry.GetValue("userID").ToString();
        DatabaseModel databaseModel = new DatabaseModel();
        EventGenerator eventGenerator = new EventGenerator();


        public bool AddEvent(UserEvent userEvent)
        {

            try
            {
                using (var dbContext = new DatabaseModel())
                {
                    dbContext.Events.Add(userEvent);
                    dbContext.SaveChanges();
                    this.AddEventToXML(userEvent);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public List<UserEvent> GetUserEvents()
        {
            WeekStartEnd weekStartEnd = new WeekStartEnd();
            weekStartEnd.WeekStart = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek).Date;
            weekStartEnd.WeekEnd = weekStartEnd.WeekStart.Date.AddDays(7).AddSeconds(-1);

            List<UserEvent> repetitive = new List<UserEvent>();
            List<UserEvent> contacts = new List<UserEvent>();

            databaseModel = new DatabaseModel();

            repetitive = databaseModel.Events.Where(events => events.RepeatTill >= weekStartEnd.WeekStart).ToList();




            return eventGenerator.GenerateEvents(repetitive, weekStartEnd.WeekStart, weekStartEnd.WeekEnd);
        }

        public UserEvent GetUserEvent(string eventid)
        {
            UserEvent contacts = new UserEvent();

            databaseModel = new DatabaseModel();
            contacts = databaseModel.Events.Where(events => events.eventid.Equals(eventid)).FirstOrDefault();
            return contacts;
        }

        public List<UserEvent> SearchUserEvent(DateTime startDate, DateTime endDate)
        {

            List<UserEvent> repetitive = new List<UserEvent>();
            List<UserEvent> contacts = new List<UserEvent>();

            databaseModel = new DatabaseModel();

            repetitive = databaseModel.Events.Where(events => events.RepeatTill >= startDate.Date).ToList();




            return eventGenerator.GenerateEvents(repetitive, startDate.Date, endDate.Date);
        }


        public bool RemoevEvent(string eventid)
        {
            UserEvent contacts = new UserEvent();

            try
            {


                using (var dbContext = new DatabaseModel())
                {
                    contacts = dbContext.Events.Where(events => events.eventid.Equals(eventid)).FirstOrDefault();
                    dbContext.Events.Remove(contacts);
                    dbContext.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateEvent(UserEvent appointment)
        {
            try
            {
                UserEvent contactDetails = new UserEvent();
                using (var dbContext = new DatabaseModel())
                {
                    contactDetails = dbContext.Events.Find(appointment.eventid);
                    contactDetails.AddressLine1 = appointment.AddressLine1;
                    contactDetails.AddressLine2 = appointment.AddressLine2;
                    contactDetails.State = appointment.State;
                    contactDetails.City = appointment.City;
                    contactDetails.Zipcode = appointment.Zipcode;
                    contactDetails.eventid = appointment.eventid;
                    contactDetails.userid = appointment.userid;
                    contactDetails.title = appointment.title;
                    contactDetails.description = appointment.description;
                    contactDetails.type = appointment.type;
                    contactDetails.RepeatType = appointment.RepeatType;
                    contactDetails.EventContacts = appointment.EventContacts;
                    contactDetails.RepeatDuration = appointment.RepeatDuration;
                    contactDetails.RepeatCount = appointment.RepeatCount;
                    contactDetails.RepeatTill = appointment.RepeatTill;
                    contactDetails.StartDate = appointment.StartDate;
                    contactDetails.EndDate = appointment.EndDate;
                    dbContext.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }




        private bool AddEventToXML(UserEvent userEvent)
        {
            XDocument xmlDoc = new XDocument();
            try
            {
                xmlDoc = XDocument.Load($"{userId}.xml");
            }
            catch (Exception ex)
            {
            }

            try
            {


                XElement xEvent = new XElement("Event",
                    new XElement("eventid", userEvent.eventid),
                    new XElement("title", userEvent.title),
                    new XElement("description", userEvent.description),
                    new XElement("type", userEvent.type),
                    new XElement("startdate", userEvent.StartDate),
                    new XElement("enddate", userEvent.EndDate),
                    new XElement("repeatType", userEvent.RepeatType),
                    new XElement("repeatDuration", userEvent.RepeatDuration),
                    new XElement("repeatCount", userEvent.RepeatCount),
                    new XElement("repeatTill", userEvent.RepeatTill),
                    new XElement("addressline1", userEvent.AddressLine1),
                    new XElement("addressline1", userEvent.AddressLine2),
                    new XElement("city", userEvent.City),
                    new XElement("state", userEvent.State),
                    new XElement("zip", userEvent.Zipcode),
                    new XElement("contacts", userEvent.EventContacts.Select(x => new XElement("contact",
                                                                 new XElement("contactId", x.ContactId),
                                                                 new XElement("userId", x.UserId),
                                                                 new XElement("eventId", x.EventId)))));


                xmlDoc.Element("LocalStore").Add(xEvent);
                xmlDoc.Save($"{userId}.xml");
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

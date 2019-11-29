using EventManager.Model;
using EventManager.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EventManager.DatabaseHelper
{
    public class EventHelper
    {
        readonly String userId = Application.UserAppDataRegistry.GetValue("userID").ToString();
        DatabaseModel databaseModel = new DatabaseModel();
        EventGenerator eventGenerator = new EventGenerator();
        public bool AddAppointment(Appointment userEvent)
        {

            try
            {
                using (var dbContext = new DatabaseModel())
                {
                    dbContext.Events.Add(userEvent);
                    dbContext.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool AddEvent(Tasks userEvent)
        {

            try
            {
                using (var dbContext = new DatabaseModel())
                {
                    dbContext.Events.Add(userEvent);
                    dbContext.SaveChanges();
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
            weekStartEnd.WeekStart = DateTime.Now.Date.AddDays(60);
            weekStartEnd.WeekEnd = weekStartEnd.WeekStart.Date.AddDays(7).AddSeconds(-1);

            List<UserEvent> contacts = new List<UserEvent>();
                using (var dbContext = new DatabaseModel())
                {
                    contacts = databaseModel.Events.Where(events => events.RepeatTill >= weekStartEnd.WeekStart).ToList();
                }
            
            return eventGenerator.GenerateEvents(contacts,weekStartEnd.WeekStart,weekStartEnd.WeekEnd);
        }

        public UserEvent GetUserEvent(string eventid)
        {
            UserEvent contacts = new UserEvent();
            contacts = databaseModel.Events.Where(events => events.eventid.Equals(eventid)).FirstOrDefault();
            return contacts;
        }

        public List<UserEvent> SearchUserEvent(string name)
        {
            List<UserEvent> contacts = new List<UserEvent>();
            contacts = databaseModel.Events.Where(events => events.title.Equals(name)).ToList();
            return contacts;
        }
    }
}

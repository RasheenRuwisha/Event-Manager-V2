using EventManager.Model;
using EventManager.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Data.Entity;
using System.Xml.Serialization;

namespace EventManager.DatabaseHelper
{
    public class EventHelper
    {

        public static bool AddEvent(UserEvent userEvent)
        {

            try
            {
                if (Application.UserAppDataRegistry.GetValue("dbConnection").ToString().Equals("True"))
                {
                    using (var dbContext = new DatabaseModel())
                    {
                        dbContext.Events.Add(userEvent);
                        dbContext.SaveChanges();
                        AddEventXML(userEvent);
                    }
                }
                else
                {
                    AddEventXML(userEvent);
                }

                return true;
            }
            catch (System.Data.Entity.Core.EntityException ex)
            {
                Logger.LogException(ex,false);
                AddEventXML(userEvent);
                return true;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                Logger.LogException(ex, false);
                AddEventXML(userEvent);
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, true);
                return false;
            }

        }
    

        public static UserEvent GetUserEvent(string eventid)
        {
            string userId = Application.UserAppDataRegistry.GetValue("userID").ToString();

            UserEvent userEvents = new UserEvent();
            try
            {
                if (Application.UserAppDataRegistry.GetValue("dbConnection").ToString().Equals("True"))
                {
                    using (var dbContext = new DatabaseModel())
                    {
                        userEvents = dbContext.Events.Where(events => events.UserId.Equals(userId)).Where(events => events.EventId.Equals(eventid)).Include(e => e.EventContacts).FirstOrDefault();
                    }
                }
                else
                {
                    userEvents = SearchEventXML(eventid);
                }
            }
            catch (System.Data.Entity.Core.EntityException ex)
            {
                Logger.LogException(ex,false);
                userEvents = SearchEventXML(eventid);
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                Logger.LogException(ex, false);
                userEvents = SearchEventXML(eventid);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, true);
            }
            return userEvents;
        }


        public static List<UserEvent> SearchUserEvent(DateTime startDate, DateTime endDate)
        {
            string userId = Application.UserAppDataRegistry.GetValue("userID").ToString();

            List<UserEvent> userEvents = new List<UserEvent>();
            try
            {
                if (Application.UserAppDataRegistry.GetValue("dbConnection").ToString().Equals("True"))
                {
                    using (var dbContext = new DatabaseModel())
                    {
                        userEvents = dbContext.Events.Where(events => events.UserId.Equals(userId)).Where(events => events.RepeatTill >= startDate.Date).Include(e => e.EventContacts).ToList();
                    }
                }
                else
                {
                    userEvents = FilterEventsXML(startDate);
                }
            }
            catch (System.Data.Entity.Core.EntityException ex)
            {
                Logger.LogException(ex, false);
                userEvents = FilterEventsXML(startDate);
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                Logger.LogException(ex, false);
                userEvents = FilterEventsXML(startDate);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, true);
            }
           
               
            return EventGenerator.GenerateEvents(userEvents, startDate.Date, endDate.Date);
        }


        public static bool RemoveEvent(string eventId)
        {
            string userId = Application.UserAppDataRegistry.GetValue("userID").ToString();

            UserEvent userEvent = new UserEvent();

            try
            {
                if (Application.UserAppDataRegistry.GetValue("dbConnection").ToString().Equals("True"))
                {
                    using (var dbContext = new DatabaseModel())
                    {
                        userEvent = dbContext.Events.Where(events => events.EventId.Equals(eventId)).FirstOrDefault();
                        List<EventContact> eventDates = new List<EventContact>();
                        foreach (EventContact contact in userEvent.EventContacts)
                        {
                            eventDates.Add(contact);
                        }
                        userEvent.EventContacts = eventDates;
                        dbContext.Events.Remove(userEvent);

                        dbContext.SaveChanges();
                        RemoveEventXML(eventId);
                    }
                }
                else
                {
                    RemoveEventXML(eventId);
                }

                return true;
            }
            catch (System.Data.Entity.Core.EntityException ex)
            {
                Logger.LogException(ex, false);
                RemoveEventXML(eventId);
                return true;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                Logger.LogException(ex, false);
                RemoveEventXML(eventId);
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, true);
                return false;
            }
        }

        public static bool UpdateEvent(UserEvent @event)
        {
            try
            {
                if (Application.UserAppDataRegistry.GetValue("dbConnection").ToString().Equals("True"))
                {
                    UserEvent userEvent = new UserEvent();
                    using (var dbContext = new DatabaseModel())
                    {
                        userEvent = dbContext.Events.Find(@event.EventId);

                        userEvent.EventContacts.Clear();

                        userEvent.AddressLine1 = @event.AddressLine1;
                        userEvent.AddressLine2 = @event.AddressLine2;
                        userEvent.State = @event.State;
                        userEvent.City = @event.City;
                        userEvent.Zipcode = @event.Zipcode;
                        userEvent.EventId = @event.EventId;
                        userEvent.UserId = @event.UserId;
                        userEvent.Title = @event.Title;
                        userEvent.Description = @event.Description;
                        userEvent.Type = @event.Type;
                        userEvent.RepeatType = @event.RepeatType;
                        userEvent.EventContacts = @event.EventContacts;
                        userEvent.RepeatDuration = @event.RepeatDuration;
                        userEvent.RepeatCount = @event.RepeatCount;
                        userEvent.RepeatTill = @event.RepeatTill;
                        userEvent.StartDate = @event.StartDate;
                        userEvent.EndDate = @event.EndDate;
                        dbContext.SaveChanges();
                        UpdateEventXML(@event);

                    }
                }
                else
                {
                    UpdateEventXML(@event);
                }

                return true;
            }
            catch (System.Data.Entity.Core.EntityException ex)
            {
                Logger.LogException(ex, false);
                UpdateEventXML(@event);
                return true;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                Logger.LogException(ex, false);
                UpdateEventXML(@event);
                return true;

            }
            catch (Exception ex)
            {
                Logger.LogException(ex, true);
                return false;
            }
        }






        private static bool AddEventXML(UserEvent userEvent)
        {
            string userId = Application.UserAppDataRegistry.GetValue("userID").ToString();

            XDocument xmlDoc = new XDocument();
            try
            {
                xmlDoc = XDocument.Load($"{userId}.xml");
                var updateQuery = (from item in xmlDoc.Descendants("UserEvent")
                                   where item.Element("EventId").Value == userEvent.EventId
                                   select item).FirstOrDefault();
                XElement xEvent = null;
                if (updateQuery == null)
                {
                    xEvent = new XElement("UserEvent",
                   new XElement("EventId", userEvent.EventId),
                   new XElement("UserId", userEvent.UserId),
                   new XElement("Title", userEvent.Title),
                   new XElement("Description", userEvent.Description),
                   new XElement("Type", userEvent.Type),
                   new XElement("StartDate", userEvent.StartDate),
                   new XElement("EndDate", userEvent.EndDate),
                   new XElement("RepeatType", userEvent.RepeatType),
                   new XElement("RepeatDuration", userEvent.RepeatDuration),
                   new XElement("RepeatCount", userEvent.RepeatCount),
                   new XElement("RepeatTill", userEvent.RepeatTill),
                   new XElement("AddressLine1", userEvent.AddressLine1),
                   new XElement("AddressLine2", userEvent.AddressLine2),
                   new XElement("City", userEvent.City),
                   new XElement("State", userEvent.State),
                   new XElement("Zipcode", userEvent.Zipcode),
                   new XElement("EventContacts", userEvent.EventContacts.Select(x => new XElement("EventContact",
                                                                new XElement("Id", x.Id),
                                                                new XElement("ContactId", x.ContactId),
                                                                new XElement("UserId", x.UserId),
                                                                new XElement("EventId", x.EventId)))));


                }

                xmlDoc.Element("LocalStore").Add(xEvent);
                xmlDoc.Save($"{userId}.xml");
                if (Application.UserAppDataRegistry.GetValue("dbConnection").ToString().Equals("False"))
                {
                    InitLocalEventFileAddEvent(xEvent);

                }
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, true);
                return false;
            }
        }




        private static bool UpdateEventXML(UserEvent userEvent)
        {
            string userId = Application.UserAppDataRegistry.GetValue("userID").ToString();

            XDocument xmlDoc = new XDocument();
            try
            {
                xmlDoc = XDocument.Load($"{userId}.xml");
                var updateQuery = (from item in xmlDoc.Descendants("UserEvent")
                                   where item.Element("EventId").Value == userEvent.EventId
                                   select item).FirstOrDefault();


                updateQuery.Element("Title").SetValue(userEvent.Title);
                updateQuery.Element("Description").SetValue(userEvent.Description);
                updateQuery.Element("Type").SetValue(userEvent.Type);
                updateQuery.Element("StartDate").SetValue(userEvent.StartDate);
                updateQuery.Element("EndDate").SetValue(userEvent.EndDate);
                updateQuery.Element("RepeatType").SetValue(userEvent.RepeatType);
                updateQuery.Element("RepeatDuration").SetValue(userEvent.RepeatDuration);
                updateQuery.Element("RepeatCount").SetValue(userEvent.RepeatCount);
                updateQuery.Element("RepeatTill").SetValue(userEvent.RepeatTill);
                updateQuery.Element("AddressLine1").SetValue(userEvent.AddressLine1);
                updateQuery.Element("AddressLine2").SetValue(userEvent.AddressLine2);
                updateQuery.Element("City").SetValue(userEvent.City);
                updateQuery.Element("State").SetValue(userEvent.State);
                updateQuery.Element("Zipcode").SetValue(userEvent.Zipcode);
                updateQuery.Element("EventContacts").Remove();
                if (userEvent.EventContacts.Count == 0)
                {
                    updateQuery.Add(new XElement("EventContacts"));
                }
                else
                {
                    foreach (EventContact eventContact in userEvent.EventContacts)
                    {
                        updateQuery.Add(new XElement("EventContacts", userEvent.EventContacts.Select(x => new XElement("EventContact",
                                                    new XElement("Id", eventContact.Id),
                                                    new XElement("ContactName", eventContact.ContactName),
                                                    new XElement("ContactId", eventContact.ContactId),
                                                    new XElement("UserId", eventContact.UserId),
                                                    new XElement("EventId", eventContact.EventId)))));
                    }
                }
              

                xmlDoc.Save($"{userId}.xml");
                if (Application.UserAppDataRegistry.GetValue("dbConnection").ToString().Equals("False"))
                {
                    InitLocalEventFileUpdateEvent(updateQuery);

                }
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex,true);
                return false;
            }
        }

        private static bool RemoveEventXML(string userEvent)
        {
            string userId = Application.UserAppDataRegistry.GetValue("userID").ToString();

            XDocument xmlDoc = new XDocument();
            try
            {
                xmlDoc = XDocument.Load($"{userId}.xml");
                var removeQuery = (from item in xmlDoc.Descendants("UserEvent")
                                   where item.Element("EventId").Value == userEvent
                                   select item).FirstOrDefault();

                if (Application.UserAppDataRegistry.GetValue("dbConnection").ToString().Equals("False"))
                {
                    InitLocalEventFileRemovevent(removeQuery);
                }

                removeQuery.Remove();
                xmlDoc.Save($"{userId}.xml");
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, true);
                return false;
            }
        }


        private static UserEvent SearchEventXML(string userEvent)
        {
            string userId = Application.UserAppDataRegistry.GetValue("userID").ToString();

            UserEvent e = new UserEvent();
            XDocument xmlDoc = new XDocument();
            try
            {
                xmlDoc = XDocument.Load($"{userId}.xml");
                var searchQuery = (from item in xmlDoc.Descendants("UserEvent")
                                   where item.Element("EventId").Value == userEvent
                                   select new UserEvent
                                   {
                                       EventId = item.Element("EventId").Value,
                                       Title = item.Element("Title").Value,
                                       Description = item.Element("Description").Value,
                                       Type = item.Element("Type").Value,
                                       RepeatType = item.Element("RepeatType").Value,
                                       RepeatDuration = item.Element("RepeatDuration").Value,
                                       RepeatCount = Convert.ToInt32(item.Element("RepeatCount").Value),
                                       RepeatTill = DateTime.Parse(item.Element("RepeatTill").Value),
                                       StartDate = DateTime.Parse(item.Element("StartDate").Value),
                                       EndDate = DateTime.Parse(item.Element("EndDate").Value),
                                       AddressLine1 = item.Element("AddressLine1").Value,
                                       AddressLine2 = item.Element("AddressLine2").Value,
                                       City = item.Element("City").Value,
                                       State = item.Element("State").Value,
                                       Zipcode = item.Element("Zipcode").Value,
                                       EventContacts = item.Element("EventContacts").Elements("EventContact").Select(c => new EventContact
                                       {
                                           Id = Convert.ToInt32(c.Element("Id").Value),
                                           ContactName = c.Element("ContactName").Value,
                                           UserId = c.Element("UserId").Value,
                                           EventId = c.Element("EventId").Value,
                                           ContactId = c.Element("ContactId").Value,
                                       }).ToList()
                                   }).FirstOrDefault();

                return searchQuery;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, true);
                return e;
            }
        }


        private static List<UserEvent> FilterEventsXML(DateTime startDate)
        {

            string userId = Application.UserAppDataRegistry.GetValue("userID").ToString();

            List<UserEvent> e = new List<UserEvent>();
            XDocument xmlDoc = new XDocument();
            try
            {
                xmlDoc = XDocument.Load($"{userId}.xml");
                var filterQuery = (from item in xmlDoc.Descendants("UserEvent")
                                   where DateTime.Parse(item.Element("RepeatTill").Value) >= startDate
                                   select new UserEvent
                                   {
                                       EventId = item.Element("EventId").Value,
                                       Title = item.Element("Title").Value,
                                       Description = item.Element("Description").Value,
                                       Type = item.Element("Type").Value,
                                       RepeatType = item.Element("RepeatType").Value,
                                       RepeatDuration = item.Element("RepeatDuration").Value,
                                       RepeatCount = Convert.ToInt32(item.Element("RepeatCount").Value),
                                       RepeatTill = DateTime.Parse(item.Element("RepeatTill").Value),
                                       StartDate = DateTime.Parse(item.Element("StartDate").Value),
                                       EndDate = DateTime.Parse(item.Element("EndDate").Value),
                                       AddressLine1 = item.Element("AddressLine1").Value,
                                       AddressLine2 = item.Element("AddressLine2").Value,
                                       City = item.Element("City").Value,
                                       State = item.Element("State").Value,
                                       Zipcode = item.Element("Zipcode").Value,
                                       EventContacts = item.Element("EventContacts").Elements("EventContact").Select(c => new EventContact
                                       {
                                           Id = Convert.ToInt32(c.Element("Id").Value),
                                           ContactName = c.Element("ContactName").Value,
                                           UserId = c.Element("UserId").Value,
                                           EventId = c.Element("EventId").Value,
                                           ContactId = c.Element("ContactId").Value,
                                       }).ToList()
                                   }).ToList();

                return filterQuery;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, true);
                return e;
            }
        }



        public static List<UserEvent> GettAllUpdateEvent(String fileepath)
        {
            string userId = Application.UserAppDataRegistry.GetValue("userID").ToString();


            List<UserEvent> filterQuery = new List<UserEvent>();
            XDocument xmlDoc = new XDocument();
            try
            {
                using (var reader = new StreamReader($"{userId}.xml"))
            {
                XmlSerializer deserializer = new XmlSerializer(typeof(List<UserEvent>),
                    new XmlRootAttribute("LocalStore"));
                    filterQuery = (List<UserEvent>)deserializer.Deserialize(reader);
            }


                return filterQuery;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, true);
                return filterQuery;
            }
        }



        public static void InitLocalEventFileAddEvent(XElement xElement)
        {
            string userId = Application.UserAppDataRegistry.GetValue("userID").ToString();

            Application.UserAppDataRegistry.SetValue("dbMatch", false);

            String workingDir = Directory.GetCurrentDirectory();

            if (File.Exists(workingDir + @"\event_add.xml"))
            {
                XDocument xmlDoc = XDocument.Load(workingDir + @"\event_add.xml");
                xmlDoc.Element("LocalStore").Add(xElement);
                xmlDoc.Save(workingDir + @"\event_add.xml");
            }
            else
            {
                XDocument xmlDoc = new XDocument();
                xmlDoc.Add(new XElement("LocalStore"));
                xmlDoc.Element("LocalStore").Add(xElement);
                xmlDoc.Save(workingDir + @"\event_add.xml");
            }
        }

        public static void InitLocalEventFileUpdateEvent(XElement xElement)
        {
            Application.UserAppDataRegistry.SetValue("dbMatch", false);

            String workingDir = Directory.GetCurrentDirectory();

            if (File.Exists(workingDir + @"\event_update.xml"))
            {
                XDocument xmlDoc = XDocument.Load(workingDir + @"\event_update.xml");
                xmlDoc.Element("LocalStore").Add(xElement);
                xmlDoc.Save(workingDir + @"\event_update.xml");
            }
            else
            {
                XDocument xmlDoc = new XDocument();
                xmlDoc.Add(new XElement("LocalStore"));
                xmlDoc.Element("LocalStore").Add(xElement);
                xmlDoc.Save(workingDir + @"\event_update.xml");
            }
        }

        public static void InitLocalEventFileRemovevent(XElement xElement)
        {
            Application.UserAppDataRegistry.SetValue("dbMatch", false);

            String workingDir = Directory.GetCurrentDirectory();

            if (File.Exists(workingDir + @"\event_remove.xml"))
            {
                XDocument xmlDoc = XDocument.Load(workingDir + @"\event_remove.xml");
                xmlDoc.Element("LocalStore").Add(xElement);
                xmlDoc.Save(workingDir + @"\event_remove.xml");
            }
            else
            {
                XDocument xmlDoc = new XDocument();
                xmlDoc.Add(new XElement("LocalStore"));
                xmlDoc.Element("LocalStore").Add(xElement);
                xmlDoc.Save(workingDir + @"\event_remove.xml");
            }
        }
    }
}

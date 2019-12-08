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

        /// <summary>
        /// Adds the event to the database, and write an xml file for local storage, if offline an additional xml file is created and the event is added to that xml file which will be then used to sync the data into the database
        /// </summary>
        /// <param name="userEvent"></param>
        /// <returns></returns>
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
                Logger.LogException(ex, false);
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

        /// <summary>
        /// Get the specific event of the user from the database, if the user is offline then the event is retreived fromt the local xml file
        /// </summary>
        /// <param name="eventid"></param>
        /// <returns></returns>
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
                Logger.LogException(ex, false);
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


        /// <summary>
        /// Get all the child events of a parent event. If offline the events are retreived from the local xml file
        /// </summary>
        /// <param name="eventid"></param>
        /// <returns></returns>
        public static List<UserEvent> GetChildEvents(string eventid)
        {
            string userId = Application.UserAppDataRegistry.GetValue("userID").ToString();

            List<UserEvent> userEvents = new List<UserEvent>();
            try
            {
                if (Application.UserAppDataRegistry.GetValue("dbConnection").ToString().Equals("True"))
                {
                    using (var dbContext = new DatabaseModel())
                    {
                        userEvents = dbContext.Events.Where(events => events.UserId.Equals(userId)).Where(events => events.ParentId.Equals(eventid)).Include(e => e.EventContacts).ToList();
                    }
                }
                else
                {
                    userEvents = GetAllChildEvents(eventid);
                }
            }
            catch (System.Data.Entity.Core.EntityException ex)
            {
                userEvents = GetAllChildEvents(eventid);
                Logger.LogException(ex, false);
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                userEvents = GetAllChildEvents(eventid);
                Logger.LogException(ex, false);
            }
            catch (Exception ex)
            {
                userEvents = GetAllChildEvents(eventid);
                Logger.LogException(ex, true);
            }
            return userEvents;
        }


        /// <summary>
        /// The methods searches for the user events for a specific time period from the database, and if there is no internet connection the data is retreived from the local xml.
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
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


            return EventGenerator.GenerateEvents(userEvents, startDate.Date, endDate.Date.AddHours(24).AddSeconds(-1));
        }

        /// <summary>
        /// This method removes the specific event using the event id from the database and the xml file, 
        /// if there is no internet connection the event is deleted from the local xml file and a spereate file is created with the event which will be then used to remove the event from the database once there is an internet connection
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
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
                        List<EventContact> eventContact = dbContext.EventContacts.Where(x => x.EventId == userEvent.EventId).ToList();
                        foreach (var item in eventContact)
                        {
                            dbContext.EventContacts.Remove(item);
                            dbContext.SaveChanges();
                        }

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

        /// <summary>
        /// This method updates the events from the database and the local xml file, if there is no internet connecction a seperate file is created with the event that needs to be updated which will then be used to update the database
        /// once there is an active internet conenction.
        /// </summary>
        /// <param name="eventDetails"></param>
        /// <returns></returns>
        public static bool UpdateEvent(UserEvent eventDetails)
        {
            try
            {
                if (Application.UserAppDataRegistry.GetValue("dbConnection").ToString().Equals("True"))
                {
                    UserEvent userEvent = new UserEvent();
                    List<EventContact> eventContacts = new List<EventContact>();
                    using (var dbContext = new DatabaseModel())
                    {
                        userEvent = dbContext.Events.Find(eventDetails.EventId);
                        List<EventContact> eventContact = dbContext.EventContacts.Where(x => x.EventId == eventDetails.EventId).ToList();
                        foreach (var item in eventContact)
                        {
                            dbContext.EventContacts.Remove(item);
                            dbContext.SaveChanges();
                        }


                        userEvent.EventContacts.Clear();
                        userEvent.AddressLine1 = eventDetails.AddressLine1;
                        userEvent.AddressLine2 = eventDetails.AddressLine2;
                        userEvent.State = eventDetails.State;
                        userEvent.City = eventDetails.City;
                        userEvent.Zipcode = eventDetails.Zipcode;
                        userEvent.EventId = eventDetails.EventId;
                        userEvent.UserId = eventDetails.UserId;
                        userEvent.Title = eventDetails.Title;
                        userEvent.Description = eventDetails.Description;
                        userEvent.Type = eventDetails.Type;
                        userEvent.RepeatType = eventDetails.RepeatType;
                        userEvent.EventContacts = eventDetails.EventContacts;
                        userEvent.RepeatDuration = eventDetails.RepeatDuration;
                        userEvent.RepeatCount = eventDetails.RepeatCount;
                        userEvent.RepeatTill = eventDetails.RepeatTill;
                        userEvent.ParentId = eventDetails.ParentId;
                        userEvent.StartDate = eventDetails.StartDate;
                        userEvent.EndDate = eventDetails.EndDate;
                        dbContext.SaveChanges();
                        UpdateEventXML(eventDetails);

                    }
                }
                else
                {
                    UpdateEventXML(eventDetails);
                }

                return true;
            }
            catch (System.Data.Entity.Core.EntityException ex)
            {
                Logger.LogException(ex, false);
                UpdateEventXML(eventDetails);
                return true;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                Logger.LogException(ex, false);
                UpdateEventXML(eventDetails);
                return true;

            }
            catch (Exception ex)
            {
                Logger.LogException(ex, true);
                return false;
            }
        }



        //XML WRITING METHOD STARTS


        /// <summary>
        /// This method add the event to the local XML file
        /// </summary>
        /// <param name="userEvent"></param>
        /// <returns></returns>
        private static bool AddEventXML(UserEvent userEvent)
        {
            string userId = Application.UserAppDataRegistry.GetValue("userID").ToString();
            string workingDir = Directory.GetCurrentDirectory();
            XDocument xmlDoc = new XDocument();
            try
            {
                xmlDoc = XDocument.Load(workingDir + $@"\{userId}.xml");
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
                   new XElement("ParentId", userEvent.ParentId == null ? "" : userEvent.ParentId),
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



        /// <summary>
        /// This method updates the event in the local xml file
        /// </summary>
        /// <param name="userEvent"></param>
        /// <returns></returns>
        private static bool UpdateEventXML(UserEvent userEvent)
        {
            string userId = Application.UserAppDataRegistry.GetValue("userID").ToString();
            string workingDir = Directory.GetCurrentDirectory();
            XDocument xmlDoc = new XDocument();
            try
            {
                xmlDoc = XDocument.Load(workingDir + $@"\{userId}.xml");
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
                updateQuery.Element("ParentId").SetValue(userEvent.ParentId == null ? "" : userEvent.ParentId);
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
                Logger.LogException(ex, true);
                return false;
            }
        }


        /// <summary>
        /// This method removes the events in the local xml file
        /// </summary>
        /// <param name="userEvent"></param>
        /// <returns></returns>
        private static bool RemoveEventXML(string userEvent)
        {
            string userId = Application.UserAppDataRegistry.GetValue("userID").ToString();
            string workingDir = Directory.GetCurrentDirectory();
            XDocument xmlDoc = new XDocument();
            try
            {
                xmlDoc = XDocument.Load(workingDir + $@"\{userId}.xml");
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

        /// <summary>
        /// This method searches for the events in the local xml file using the event id
        /// </summary>
        /// <param name="userEvent"></param>
        /// <returns></returns>
        private static UserEvent SearchEventXML(string userEvent)
        {
            string userId = Application.UserAppDataRegistry.GetValue("userID").ToString();
            string workingDir = Directory.GetCurrentDirectory();
            UserEvent e = new UserEvent();
            XDocument xmlDoc = new XDocument();
            try
            {
                xmlDoc = XDocument.Load(workingDir + $@"\{userId}.xml");
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
                                       ParentId = item.Element("ParentId").Value,
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

        /// <summary>
        /// This method gets the event data in the local xml file using the start date
        /// </summary>
        /// <param name="startDate"></param>
        /// <returns></returns>
        private static List<UserEvent> FilterEventsXML(DateTime startDate)
        {

            string userId = Application.UserAppDataRegistry.GetValue("userID").ToString();
            string workingDir = Directory.GetCurrentDirectory();
            List<UserEvent> e = new List<UserEvent>();
            XDocument xmlDoc = new XDocument();
            try
            {
                xmlDoc = XDocument.Load(workingDir + $@"\{userId}.xml");
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
                                       ParentId = item.Element("ParentId").Value,
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


        /// <summary>
        /// This method gets all the child events using the parent event id
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        private static List<UserEvent> GetAllChildEvents(string eventId)
        {

            string userId = Application.UserAppDataRegistry.GetValue("userID").ToString();
            string workingDir = Directory.GetCurrentDirectory();
            List<UserEvent> e = new List<UserEvent>();
            XDocument xmlDoc = new XDocument();
            try
            {
                xmlDoc = XDocument.Load(workingDir + $@"\{userId}.xml");
                var filterQuery = (from item in xmlDoc.Descendants("UserEvent")
                                   where item.Element("ParentId").Value.Equals(eventId)
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
                                       ParentId = item.Element("ParentId").Value,
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

        /// <summary>
        /// This method gets all the local changes that were done and has to be synced to the online database
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static List<UserEvent> GetAllUpdateEvent(String filepath)
        {
            string userId = Application.UserAppDataRegistry.GetValue("userID").ToString();


            List<UserEvent> filterQuery = new List<UserEvent>();
            XDocument xmlDoc = new XDocument();
            try
            {
                using (var reader = new StreamReader(filepath))
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


        /// <summary>
        /// This method adds the data to the local xml file if there is no internet connection.
        /// This method check wether the file exists if the file does not exist a new file will be created.
        /// </summary>
        /// <param name="xElement"></param>
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

        /// <summary>
        /// This method updates the data to the local xml file if there is no internet connection.
        /// This method check wether the file exists if the file does not exist a new file will be created.
        /// </summary>
        /// <param name="xElement"></param>
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


        /// <summary>
        /// This method removes the data to the local xml file if there is no internet connection.
        /// This method check wether the file exists if the file does not exist a new file will be created.
        /// </summary>
        /// <param name="xElement"></param>
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

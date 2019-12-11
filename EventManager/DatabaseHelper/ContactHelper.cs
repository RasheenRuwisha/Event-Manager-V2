using EventManager.Model;
using EventManager.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace EventManager.DatabaseHelper
{
    public class ContactHelper
    {
        /// <summary>
        /// This method checks wetheher the contact name already exists in the users cotnact list.
        /// If there is no internet connection the check is done using the local XML file.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool DoesNameExist(string name)
        {
            string userId = Application.UserAppDataRegistry.GetValue("userID").ToString();

            try
            {
                if (Application.UserAppDataRegistry.GetValue("dbConnection").ToString().Equals("True"))
                {
                    using (var dbContext = new DatabaseModel())
                    {
                        var userDetails = dbContext.Contacts.Where(contact => contact.Name.Equals(name)).Where(contact => contact.UserId.Equals(userId)).FirstOrDefault();
                        if (userDetails != null)
                        {
                            return false;
                        }
                    }
                }
                else
                {

                    var userDetails = SearchContactXML(name, "Name");
                    if (userDetails != null)
                    {
                        return false;
                    }
                }
            }
            catch (System.Data.Entity.Core.EntityException ex)
            {
                Logger.LogException(ex, false);
                var userDetails = SearchContactXML(name, "Name");
                if (userDetails != null)
                {
                    return false;
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                Logger.LogException(ex, false);
                var userDetails = SearchContactXML(name, "Name");
                if (userDetails != null)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, true);
                return false;
            }
            return true;
        }


        /// <summary>
        /// This method checks wether the email exists in the users contact list.
        /// If there is no internet connection the check is done using the local XML file.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool DoesEmailExist(string email)
        {
            string userId = Application.UserAppDataRegistry.GetValue("userID").ToString();

            try
            {
                if (Application.UserAppDataRegistry.GetValue("dbConnection").ToString().Equals("True"))
                {
                    using (var dbContext = new DatabaseModel())
                    {
                        var userDetails = dbContext.Contacts.Where(contact => contact.Email.Equals(email)).Where(contact => contact.UserId.Equals(userId)).FirstOrDefault();
                        if (userDetails != null)
                        {
                            return false;
                        }
                    }
                }
                else
                {

                    var userDetails = SearchContactXML(email, "Email");
                    if (userDetails != null)
                    {
                        return false;
                    }
                }
            }
            catch (System.Data.Entity.Core.EntityException ex)
            {
                Logger.LogException(ex, false);
                var userDetails = SearchContactXML(email, "Email");
                if (userDetails != null)
                {
                    return false;
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                Logger.LogException(ex, false);
                var userDetails = SearchContactXML(email, "Email");
                if (userDetails != null)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, true);
                return false;
            }

            return true;
        }


        /// <summary>
        /// This methods adds the contact to the database. And the xml file gets updated.
        /// If there is no internet connection a sperate fill will be crated, which will then be used to sync the changes to the database once there is an internet conenction.
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        public static bool AddContact(Contact contact)
        {
            string userId = Application.UserAppDataRegistry.GetValue("userID").ToString();

            try
            {
                if (Application.UserAppDataRegistry.GetValue("dbConnection").ToString().Equals("True"))
                {
                    using (var dbContext = new DatabaseModel())
                    {
                        dbContext.Contacts.Add(contact);
                        dbContext.SaveChanges();
                        AddContactXML(contact);
                    }
                }
                else
                {
                    AddContactXML(contact);
                }

                return true;
            }
            catch (System.Data.Entity.Core.EntityException ex)
            {
                Logger.LogException(ex, false);
                AddContactXML(contact);
                return true;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                Logger.LogException(ex, false);
                AddContactXML(contact);
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, true);
                return false;
            }
        }

        /// <summary>
        /// This  method removes the contact from the database and the local xml file.
        /// If there is no internet connection a sperate fill will be crated, which will then be used to sync the changes to the database once there is an internet conenction.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Boolean</returns>
        public static bool RemoveContact(string id)
        {
            try
            {
                if (Application.UserAppDataRegistry.GetValue("dbConnection").ToString().Equals("True"))
                {
                    using (var dbContext = new DatabaseModel())
                    {
                        Contact contact = dbContext.Contacts.Find(id);
                        List<EventContact> eventContact = dbContext.EventContacts.Where(x => x.ContactId == id).ToList();
                        foreach (var item in eventContact)
                        {
                            dbContext.EventContacts.Remove(item);
                            dbContext.SaveChanges();
                        }
                        dbContext.Contacts.Remove(contact);
                        dbContext.SaveChanges();
                        RemoveContactXML(id);
                    }
                }
                else
                {
                    RemoveContactXML(id);
                }

                return true;
            }
            catch (System.Data.Entity.Core.EntityException ex)
            {
                Logger.LogException(ex, false);
                RemoveContactXML(id);
                return true;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                Logger.LogException(ex, false);
                RemoveContactXML(id);
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, true);
                return false;
            }
        }

        /// <summary>
        /// This methods gets all the user contacts from the database. If there is no internet connection the data will be retreived from the local XML.
        /// </summary>
        /// <returns>The users contact list</returns>
        public static List<Contact> GetUserContacts()
        {
            string userId = Application.UserAppDataRegistry.GetValue("userID").ToString();

            List<Contact> contacts = new List<Contact>();

            try
            {
                if (Application.UserAppDataRegistry.GetValue("dbConnection").ToString().Equals("True"))
                {
                    using (var dbContext = new DatabaseModel())
                    {
                        contacts = dbContext.Contacts.Where(contact => contact.UserId.Equals(userId)).OrderBy(contact => contact.Name).ToList();
                    }
                }
                else
                {
                    contacts = GetAllContactsXML();
                }

            }
            catch (System.Data.Entity.Core.EntityException ex)
            {
                Logger.LogException(ex, false);
                contacts = GetAllContactsXML();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                Logger.LogException(ex, false);
                contacts = GetAllContactsXML();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, true);
            }

            return contacts;
        }

        /// <summary>
        /// This method updates the contact in the database and the local XMl file.
        /// If there is no internet connection a sperate fill will be crated, which will then be used to sync the changes to the database once there is an internet conenction.
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        public static bool UpdateContacts(Contact contact)
        {
            try
            {
                if (Application.UserAppDataRegistry.GetValue("dbConnection").ToString().Equals("True"))
                {
                    Contact contactDetails = new Contact();
                    using (var dbContext = new DatabaseModel())
                    {
                        contactDetails = dbContext.Contacts.Find(contact.ContactId);
                        contactDetails.Name = contact.Name;
                        contactDetails.Phone = contact.Phone;
                        contactDetails.Email = contact.Email;
                        contactDetails.Image = contact.Image;
                        contactDetails.AddressLine1 = contact.AddressLine1;
                        contactDetails.AddressLine2 = contact.AddressLine2;
                        contactDetails.State = contact.State;
                        contactDetails.City = contact.City;
                        contactDetails.Zipcode = contact.Zipcode;
                        dbContext.SaveChanges();
                    }
                    UpdateContactXML(contact);
                }
                else
                {
                    UpdateContactXML(contact);
                }

                return true;
            }
            catch (System.Data.Entity.Core.EntityException ex)
            {
                Logger.LogException(ex, false);
                UpdateContactXML(contact);
                return true;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                Logger.LogException(ex, false);
                UpdateContactXML(contact);
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, true);
                return false;
            }
        }


        /// <summary>
        /// This method returns the contact of the user using the name.If there is not internet connection the data is retreived using the local XML
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static List<Contact> GetUserContactsByName(string name)
        {
            string userId = Application.UserAppDataRegistry.GetValue("userID").ToString();

            List<Contact> contacts = new List<Contact>();
            try
            {
                if (Application.UserAppDataRegistry.GetValue("dbConnection").ToString().Equals("True"))
                {
                    using (var dbContext = new DatabaseModel())
                    {
                        contacts = dbContext.Contacts.Where(contact => contact.UserId.Equals(userId)).Where(contact => contact.Name.Contains(name)).OrderBy(contact => contact.Name).ToList();
                    }
                }
                else
                {
                    contacts = SearchContactsByNameXML(name);
                }
            }
            catch (System.Data.Entity.Core.EntityException ex)
            {
                Logger.LogException(ex, false);
                contacts = SearchContactsByNameXML(name);
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                Logger.LogException(ex, false);
                contacts = SearchContactsByNameXML(name);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, true);
            }

            return contacts;
        }




        //XML METHODS START




        /// <summary>
        /// This method adds the data to the local XML.
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        private static bool AddContactXML(Contact contact)
        {
            string userId = Application.UserAppDataRegistry.GetValue("userID").ToString();
            string workingDir = Directory.GetCurrentDirectory();
            try
            {
                XElement xEvent = null;
                XDocument xmlDoc = XDocument.Load(workingDir + $@"\{userId}.xml");

                var updateQuery = (from item in xmlDoc.Descendants("Contact")
                                   where item.Element("ContactId").Value == contact.ContactId
                                   select item).FirstOrDefault();
                if(updateQuery == null)
                {
                    xEvent = new XElement("Contact",
                    new XElement("ContactId", contact.ContactId),
                    new XElement("Email", contact.Email),
                    new XElement("Phone", contact.Phone),
                    new XElement("Image", contact.Image),
                    new XElement("Name", contact.Name),
                    new XElement("AddressLine1", contact.AddressLine1),
                    new XElement("AddressLine2", contact.AddressLine2),
                    new XElement("City", contact.City),
                    new XElement("State", contact.State),
                    new XElement("Zipcode", contact.Zipcode),
                    new XElement("UserId", contact.UserId)
                );
                    xmlDoc.Element("LocalStore").Add(xEvent);
                    xmlDoc.Save($"{userId}.xml");
                }

                if (Application.UserAppDataRegistry.GetValue("dbConnection").ToString().Equals("False"))
                {
                    InitLocalEventFileAddContact(xEvent);

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
        /// This method updates the data in the local XML 
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        private static bool UpdateContactXML(Contact contact)
        {
            string userId = Application.UserAppDataRegistry.GetValue("userID").ToString();
            string workingDir = Directory.GetCurrentDirectory();
            XDocument xmlDoc = new XDocument();
            try
            {
                xmlDoc = XDocument.Load(workingDir + $@"\{userId}.xml");
                var updateQuery = (from item in xmlDoc.Descendants("Contact")
                                   where item.Element("ContactId").Value == contact.ContactId
                                   select item).FirstOrDefault();


                updateQuery.Element("ContactId").SetValue(contact.ContactId);
                updateQuery.Element("UserId").SetValue(contact.UserId);
                updateQuery.Element("Email").SetValue(contact.Email);
                updateQuery.Element("Phone").SetValue(contact.Phone);
                updateQuery.Element("Image").SetValue(contact.Image);
                updateQuery.Element("Name").SetValue(contact.Name);
                updateQuery.Element("AddressLine1").SetValue(contact.AddressLine1);
                updateQuery.Element("AddressLine2").SetValue(contact.AddressLine2);
                updateQuery.Element("City").SetValue(contact.City);
                updateQuery.Element("State").SetValue(contact.State);
                updateQuery.Element("Zipcode").SetValue(contact.Zipcode);

                xmlDoc.Save($"{userId}.xml");

                if (Application.UserAppDataRegistry.GetValue("dbConnection").ToString().Equals("False"))
                {
                    InitLocalEventFileUpdateContact(updateQuery);
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
        /// This method removes the contact from the local XML.
        /// </summary>
        /// <param name="contactId"></param>
        /// <returns></returns>
        private static bool RemoveContactXML(string contactId)
        {
            string userId = Application.UserAppDataRegistry.GetValue("userID").ToString();
            string workingDir = Directory.GetCurrentDirectory();
            XDocument xmlDoc = new XDocument();
            try
            {
                xmlDoc = XDocument.Load(workingDir + $@"\{userId}.xml");
                var removeQuery = (from item in xmlDoc.Descendants("Contact")
                                   where item.Element("ContactId").Value == contactId
                                   select item).FirstOrDefault();

                var removeEventContact = xmlDoc.Descendants("EventContact").Where(e => e.Element("ContactId").Value.Equals(contactId)).ToList();

                if (Application.UserAppDataRegistry.GetValue("dbConnection").ToString().Equals("False"))
                {
                    InitLocalEventFileRemoveContact(removeQuery);
                }
                removeEventContact.Remove();
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
        /// This method retreives all the contacts from the local XML.
        /// </summary>
        /// <returns></returns>
        private static List<Contact> GetAllContactsXML()
        {
            string userId = Application.UserAppDataRegistry.GetValue("userID").ToString();
            string workingDir = Directory.GetCurrentDirectory();
            List<Contact> contacts = null;
            try
            {
                using (var reader = new StreamReader(workingDir + $@"\{userId}.xml"))
                {
                    XmlSerializer deserializer = new XmlSerializer(typeof(List<Contact>), new XmlRootAttribute("LocalStore"));
                    contacts = (List<Contact>)deserializer.Deserialize(reader);
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, true);
            }

            return contacts;
        }

        /// <summary>
        /// This methods searches for contacts in the local XML.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="element">The elemnt that should be queried</param>
        /// <returns></returns>
        private static Contact SearchContactXML(string value, string element)
        {
            string userId = Application.UserAppDataRegistry.GetValue("userID").ToString();
            string workingDir = Directory.GetCurrentDirectory();
            Contact contact = new Contact();
            XDocument xmlDoc = new XDocument();
            try
            {
                xmlDoc = XDocument.Load(workingDir + $@"\{userId}.xml");
                var searchQuery = (from item in xmlDoc.Descendants("Contact")
                                   where item.Element(element).Value == value
                                   select new Contact
                                   {
                                       ContactId = item.Element("ContactId").Value,
                                       UserId = item.Element("UserId").Value,
                                       Email = item.Element("Email").Value,
                                       Phone = item.Element("Phone").Value,
                                       Image = item.Element("Image").Value,
                                       Name = item.Element("Name").Value,
                                       AddressLine1 = item.Element("AddressLine1").Value,
                                       AddressLine2 = item.Element("AddressLine2").Value,
                                       City = item.Element("City").Value,
                                       State = item.Element("State").Value,
                                       Zipcode = item.Element("Zipcode").Value
                                   }).FirstOrDefault();

                return searchQuery;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, true);
                return contact;
            }
        }

        /// <summary>
        /// This method searches the contacts in the XML using the name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private static List<Contact> SearchContactsByNameXML(string name)
        {
            string userId = Application.UserAppDataRegistry.GetValue("userID").ToString();
            string workingDir = Directory.GetCurrentDirectory();
            List<Contact> contact = new List<Contact>();
            XDocument xmlDoc = new XDocument();
            try
            {
                xmlDoc = XDocument.Load(workingDir + $@"\{userId}.xml");
                var searchQuery = (from item in xmlDoc.Descendants("Contact")
                                   where item.Element("Name").Value == name
                                   select new Contact
                                   {
                                       ContactId = item.Element("ContactId").Value,
                                       UserId = item.Element("UserId").Value,
                                       Email = item.Element("Email").Value,
                                       Phone = item.Element("Phone").Value,
                                       Image = item.Element("Image").Value,
                                       Name = item.Element("Name").Value,
                                       AddressLine1 = item.Element("AddressLine1").Value,
                                       AddressLine2 = item.Element("AddressLine2").Value,
                                       City = item.Element("City").Value,
                                       State = item.Element("State").Value,
                                       Zipcode = item.Element("Zipcode").Value
                                   }).ToList();

                return searchQuery;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, true);
                return contact;
            }
        }



        /// <summary>
        /// This method retreives all the contacts that requires to be synced with the database.
        /// </summary>
        /// <param name="fileepath"></param>
        /// <returns></returns>
        public static List<Contact> GettAllUpdateContact(string fileepath)
        {
            List<Contact> contacts = null;
            try
            {
                using (var reader = new StreamReader(fileepath))
                {
                    XmlSerializer deserializer = new XmlSerializer(typeof(List<Contact>), new XmlRootAttribute("LocalStore"));
                    contacts = (List<Contact>)deserializer.Deserialize(reader);
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, true);
            }

            return contacts;
        }


        /// <summary>
        /// This method adds the data to the local xml file if there is no internet connection.
        /// This method check wether the file exists if the file does not exist a new file will be created.
        /// </summary>
        /// <param name="xElement"></param>
        public static void InitLocalEventFileAddContact(XElement xElement)
        {
            Application.UserAppDataRegistry.SetValue("dbMatch", false);
            string userId = Application.UserAppDataRegistry.GetValue("userID").ToString();
            string workingDir = Directory.GetCurrentDirectory();
            string directory = workingDir + $@"\{userId}contact_add.xml";

            if (File.Exists(directory))
            {
                XDocument xmlDoc = XDocument.Load(directory);
                xmlDoc.Element("LocalStore").Add(xElement);
                xmlDoc.Save(directory);
            }
            else
            {
                XDocument xmlDoc = new XDocument();
                xmlDoc.Add(new XElement("LocalStore"));
                xmlDoc.Element("LocalStore").Add(xElement);
                xmlDoc.Save(directory);
            }
        }


        /// <summary>
        /// This method update the data to the local xml file if there is no internet connection.
        /// This method check wether the file exists if the file does not exist a new file will be created.
        /// </summary>
        /// <param name="xElement"></param>
        public static void InitLocalEventFileUpdateContact(XElement xElement)
        {
            Application.UserAppDataRegistry.SetValue("dbMatch", false);
            string userId = Application.UserAppDataRegistry.GetValue("userID").ToString();
            string workingDir = Directory.GetCurrentDirectory();
            string directory = workingDir + $@"\{userId}contact_update.xml";

            if (File.Exists(directory))
            {
                XDocument xmlDoc = XDocument.Load(directory);
                xmlDoc.Element("LocalStore").Add(xElement);
                xmlDoc.Save(directory);
            }
            else
            {
                XDocument xmlDoc = new XDocument();
                xmlDoc.Add(new XElement("LocalStore"));
                xmlDoc.Element("LocalStore").Add(xElement);
                xmlDoc.Save(directory);
            }
        }


        /// <summary>
        /// This method remove the data to the local xml file if there is no internet connection.
        /// This method check wether the file exists if the file does not exist a new file will be created.
        /// </summary>
        /// <param name="xElement"></param>
        public static void InitLocalEventFileRemoveContact(XElement xElement)
        {
            Application.UserAppDataRegistry.SetValue("dbMatch", false);
            string userId = Application.UserAppDataRegistry.GetValue("userID").ToString();
            string workingDir = Directory.GetCurrentDirectory();
            string directory = workingDir + $@"\{userId}contact_remove.xml";

            if (File.Exists(directory))
            {
                XDocument xmlDoc = XDocument.Load(directory);
                xmlDoc.Element("LocalStore").Add(xElement);
                xmlDoc.Save(directory);
            }
            else
            {
                XDocument xmlDoc = new XDocument();
                xmlDoc.Add(new XElement("LocalStore"));
                xmlDoc.Element("LocalStore").Add(xElement);
                xmlDoc.Save(directory);
            }
        }
    }
}

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
        public static bool DoesNameExist(string name)
        {
            string userId = Application.UserAppDataRegistry.GetValue("userID").ToString();

            try
            {
                if (Application.UserAppDataRegistry.GetValue("dbConnection").ToString().Equals("True"))
                {
                    using (var dbContext = new DatabaseModel())
                    {
                        var userDetails = dbContext.Contacts.Where(contact => contact.Email.Equals(name)).Where(contact => contact.UserId.Equals(userId)).FirstOrDefault();
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

        private EventContact genEvCon(string id,string conid)
        {
            string userId = Application.UserAppDataRegistry.GetValue("userID").ToString();

            EventContact ewv = new EventContact()
            {
                EventId = id,
                UserId = userId,
                ContactId = conid
            };

            return ewv;
        }

        public static bool RemoveContact(string id)
        {
            try
            {
                if (Application.UserAppDataRegistry.GetValue("dbConnection").ToString().Equals("True"))
                {
                    using (var dbContext = new DatabaseModel())
                    {
                        Contact contact = dbContext.Contacts.Find(id);
                        List<EventContact> eventContact = dbContext.EventContacts.Where(x => x.ContactId ==  id).ToList();
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

        public static Contact GetContactDetails(string contactId)
        {
            Contact contactDetails = new Contact();

            try
            {
                if (Application.UserAppDataRegistry.GetValue("dbConnection").ToString().Equals("True"))
                {
                    using (var dbContext = new DatabaseModel())
                    {
                        contactDetails = dbContext.Contacts.Find(contactId);
                    }
                }
                else
                {
                    contactDetails = SearchContactXML(contactId, "ContactId");
                }
            }
            catch (System.Data.Entity.Core.EntityException ex)
            {
                contactDetails = SearchContactXML(contactId, "ContactId");
                Logger.LogException(ex, false);
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                contactDetails = SearchContactXML(contactId, "ContactId");
                Logger.LogException(ex, false);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, true);
            }
            return contactDetails;
        }

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
                    contacts = SearchContactsXML(name);
                }
            }
            catch (System.Data.Entity.Core.EntityException ex)
            {
                Logger.LogException(ex, false);
                contacts = SearchContactsXML(name);
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                Logger.LogException(ex, false);
                contacts = SearchContactsXML(name);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, true);
            }
        
            return contacts;
        }










        private static bool AddContactXML(Contact contact)
        {
            string userId = Application.UserAppDataRegistry.GetValue("userID").ToString();

            try
            {
                XDocument xmlDoc = XDocument.Load($"{userId}.xml");

                XElement xEvent = new XElement("Contact",
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




        private static bool UpdateContactXML(Contact contact)
        {
            string userId = Application.UserAppDataRegistry.GetValue("userID").ToString();

            XDocument xmlDoc = new XDocument();
            try
            {
                xmlDoc = XDocument.Load($"{userId}.xml");
                var updateQuery = (from item in xmlDoc.Descendants("Contact")
                                   where item.Element("ContactId").Value == contact.ContactId
                                   select item).FirstOrDefault();


                updateQuery.Element("ContactId").SetValue(contact.ContactId);
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

        private static bool RemoveContactXML(string contactId)
        {
            string userId = Application.UserAppDataRegistry.GetValue("userID").ToString();

            XDocument xmlDoc = new XDocument();
            try
            {
                xmlDoc = XDocument.Load($"{userId}.xml");
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


        private static List<Contact> GetAllContactsXML()
        {
            string userId = Application.UserAppDataRegistry.GetValue("userID").ToString();

            List<Contact> contacts = null;
            try
            {
                using (var reader = new StreamReader($"{userId}.xml"))
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

        private static List<Contact> SearchContactsXML(string name)
        {
            string userId = Application.UserAppDataRegistry.GetValue("userID").ToString();

            List<Contact> contact = new List<Contact>();
            XDocument xmlDoc = new XDocument();
            try
            {
                xmlDoc = XDocument.Load($"{userId}.xml");
                var searchQuery = (from item in xmlDoc.Descendants("Contact")
                                   where item.Element("ContactId").Value == name
                                   select new Contact
                                   {
                                       ContactId = item.Element("ContactId").Value,
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

        private static Contact SearchContactXML(string name, string element)
        {
            string userId = Application.UserAppDataRegistry.GetValue("userID").ToString();

            Contact contact = new Contact();
            XDocument xmlDoc = new XDocument();
            try
            {
                xmlDoc = XDocument.Load($"{userId}.xml");
                var searchQuery = (from item in xmlDoc.Descendants("Contact")
                                   where item.Element(element).Value == name
                                   select new Contact
                                   {
                                       ContactId = item.Element("ContactId").Value,
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



        public static void InitLocalEventFileAddContact(XElement xElement)
        {
            Application.UserAppDataRegistry.SetValue("dbMatch", false);

            string workingDir = Directory.GetCurrentDirectory();

            if (File.Exists(workingDir + @"\contact_add.xml"))
            {
                XDocument xmlDoc = XDocument.Load(workingDir + @"\contact_add.xml");
                xmlDoc.Element("LocalStore").Add(xElement);
                xmlDoc.Save(workingDir + @"\contact_add.xml");
            }
            else
            {
                XDocument xmlDoc = new XDocument();
                xmlDoc.Add(new XElement("LocalStore"));
                xmlDoc.Element("LocalStore").Add(xElement);
                xmlDoc.Save(workingDir + @"\contact_add.xml");
            }
        }

        public static void InitLocalEventFileUpdateContact(XElement xElement)
        {
            Application.UserAppDataRegistry.SetValue("dbMatch", false);

            String workingDir = Directory.GetCurrentDirectory();

            if (File.Exists(workingDir + @"\contact_update.xml"))
            {
                XDocument xmlDoc = XDocument.Load(workingDir + @"\contact_update.xml");
                xmlDoc.Element("LocalStore").Add(xElement);
                xmlDoc.Save(workingDir + @"\contact_update.xml");
            }
            else
            {
                XDocument xmlDoc = new XDocument();
                xmlDoc.Add(new XElement("LocalStore"));
                xmlDoc.Element("LocalStore").Add(xElement);
                xmlDoc.Save(workingDir + @"\contact_update.xml");
            }
        }

        public static void InitLocalEventFileRemoveContact(XElement xElement)
        {
            Application.UserAppDataRegistry.SetValue("dbMatch", false);

            string workingDir = Directory.GetCurrentDirectory();

            if (File.Exists(workingDir + @"\contact_remove.xml"))
            {
                XDocument xmlDoc = XDocument.Load(workingDir + @"\contact_remove.xml");
                xmlDoc.Element("LocalStore").Add(xElement);
                xmlDoc.Save(workingDir + @"\contact_remove.xml");
            }
            else
            {
                XDocument xmlDoc = new XDocument();
                xmlDoc.Add(new XElement("LocalStore"));
                xmlDoc.Element("LocalStore").Add(xElement);
                xmlDoc.Save(workingDir + @"\contact_remove.xml");
            }
        }
    }
}

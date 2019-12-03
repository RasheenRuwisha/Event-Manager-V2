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
        readonly String userId = Application.UserAppDataRegistry.GetValue("userID").ToString();
        Logger logger = new Logger();
        public bool DoesNameExist(String name)
        {

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

                    var userDetails = this.SearchContactXML(name, "Name");
                    if (userDetails != null)
                    {
                        return false;
                    }
                }
            }
            catch (System.Data.Entity.Core.EntityException ex)
            {
                logger.LogException(ex);
                var userDetails = this.SearchContactXML(name, "Name");
                if (userDetails != null)
                {
                    return false;
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                logger.LogException(ex);
                var userDetails = this.SearchContactXML(name, "Name");
                if (userDetails != null)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                logger.LogException(ex);
                return false;
            }
            return true;
        }

        public bool DoesEmailExist(String email)
        {
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

                    var userDetails = this.SearchContactXML(email, "Email");
                    if (userDetails != null)
                    {
                        return false;
                    }
                }
            }
            catch (System.Data.Entity.Core.EntityException ex)
            {
                logger.LogException(ex);
                var userDetails = this.SearchContactXML(email, "Email");
                if (userDetails != null)
                {
                    return false;
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                logger.LogException(ex);
                var userDetails = this.SearchContactXML(email, "Email");
                if (userDetails != null)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                logger.LogException(ex);
                return false;
            }
           
            return true;
        }


        public bool AddContact(Contact contact)
        {
            try
            {
                if (Application.UserAppDataRegistry.GetValue("dbConnection").ToString().Equals("True"))
                {
                    using (var dbContext = new DatabaseModel())
                    {
                        dbContext.Contacts.Add(contact);
                        dbContext.SaveChanges();
                        this.AddContactXML(contact);
                    }
                }
                else
                {
                    this.AddContactXML(contact);
                }

                return true;
            }
            catch (System.Data.Entity.Core.EntityException ex)
            {
                logger.LogException(ex);
                this.AddContactXML(contact);
                return true;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                logger.LogException(ex);
                this.AddContactXML(contact);
                return true;
            }
            catch (Exception ex)
            {
                logger.LogException(ex);
                return false;
            }
        }

        private EventContact genEvCon(string id,string conid)
        {
            EventContact ewv = new EventContact()
            {
                EventId = id,
                UserId = userId,
                ContactId = conid
            };

            return ewv;
        }

        public bool RemoveContact(String id)
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
                        this.RemoveContactXML(id);
                    }
                }
                else
                {
                    this.RemoveContactXML(id);
                }

                return true;
            }
            catch (System.Data.Entity.Core.EntityException ex)
            {
                logger.LogException(ex);
                this.RemoveContactXML(id);
                return true;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                logger.LogException(ex);
                this.RemoveContactXML(id);
                return true;
            }
            catch (Exception ex)
            {
                logger.LogException(ex);
                return false;
            }
        }


        public List<Contact> GetUserContacts()
        {
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
                    contacts = this.GetAllContactsXML();
                }

            }
            catch (System.Data.Entity.Core.EntityException ex)
            {
                logger.LogException(ex);
                contacts = this.GetAllContactsXML();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                logger.LogException(ex);
                contacts = this.GetAllContactsXML();
            }
            catch (Exception ex)
            {
                logger.LogException(ex);
            }
          
            return contacts;
        }

        public Contact GetContactDetails(string contactId)
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
                    contactDetails = this.SearchContactXML(contactId, "ContactId");
                }
            }
            catch (System.Data.Entity.Core.EntityException ex)
            {
                contactDetails = this.SearchContactXML(contactId, "ContactId");
                logger.LogException(ex);
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                contactDetails = this.SearchContactXML(contactId, "ContactId");
                logger.LogException(ex);
            }
            catch (Exception ex)
            {
                logger.LogException(ex);
            }
            return contactDetails;
        }

        public bool UpdateContacts(Contact contact)
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
                    this.UpdateContactXML(contact);
                }
                else
                {
                    this.UpdateContactXML(contact);
                }

                return true;
            }
            catch (System.Data.Entity.Core.EntityException ex)
            {
                logger.LogException(ex);
                this.UpdateContactXML(contact);
                return true;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                logger.LogException(ex);
                this.UpdateContactXML(contact);
                return true;
            }
            catch (Exception ex)
            {
                logger.LogException(ex);
                return false;
            }
        }

        public List<Contact> GetUserContactsByName(String name)
        {
            List<Contact> contacts = new List<Contact>();
            try
            {
                if (Application.UserAppDataRegistry.GetValue("dbConnection").ToString().Equals("True"))
                {
                    using (var dbContext = new DatabaseModel())
                    {
                        contacts = dbContext.Contacts.Where(contact => contact.UserId.Equals(userId)).Where(contact => contact.Name.Contains(name)).OrderBy(contact => contact.Name).ToList();
                    }
                    contacts =this.SearchContactsXML(name);
                }
                else
                {
                    contacts = this.SearchContactsXML(name);
                }
            }
            catch (System.Data.Entity.Core.EntityException ex)
            {
                logger.LogException(ex);
                contacts = this.SearchContactsXML(name);
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                logger.LogException(ex);
                contacts = this.SearchContactsXML(name);
            }
            catch (Exception ex)
            {
                logger.LogException(ex);
            }
        
            return contacts;
        }










        private bool AddContactXML(Contact contact)
        {
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
                return true;
            }
            catch (Exception ex)
            {
                logger.LogException(ex);
                return false;
            }
        }




        private bool UpdateContactXML(Contact contact)
        {
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
                updateQuery.Element("Addressline1").SetValue(contact.AddressLine1);
                updateQuery.Element("Addressline2").SetValue(contact.AddressLine2);
                updateQuery.Element("City").SetValue(contact.City);
                updateQuery.Element("State").SetValue(contact.State);
                updateQuery.Element("Zipcode").SetValue(contact.Zipcode);

                xmlDoc.Save($"{userId}.xml");
                return true;
            }
            catch (Exception ex)
            {
                logger.LogException(ex);
                return false;
            }
        }

        private bool RemoveContactXML(string contactId)
        {
            XDocument xmlDoc = new XDocument();
            try
            {
                xmlDoc = XDocument.Load($"{userId}.xml");
                var removeQuery = (from item in xmlDoc.Descendants("Contact")
                                   where item.Element("ContactId").Value == contactId
                                   select item).FirstOrDefault();

                var removeEventContact = xmlDoc.Descendants("EventContact").Where(e => e.Element("ContactId").Value.Equals(contactId)).ToList();
                removeEventContact.Remove();
                removeQuery.Remove();
                xmlDoc.Save($"{userId}.xml");
                return true;
            }
            catch (Exception ex)
            {
                logger.LogException(ex);
                return false;
            }
        }


        private List<Contact> GetAllContactsXML()
        {
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
                logger.LogException(ex);
            }

            return contacts;
        }

        private List<Contact> SearchContactsXML(string name)
        {
            List<Contact> contact = new List<Contact>();
            XDocument xmlDoc = new XDocument();
            try
            {
                xmlDoc = XDocument.Load($"{userId}.xml");
                var searchQuery = (from item in xmlDoc.Descendants("Contact")
                                   where item.Element("Name").Value == name
                                   select new Contact
                                   {
                                       Name = item.Element("EventId").Value,
                                       ContactId = item.Element("Title").Value,
                                       Image = item.Element("Description").Value,
                                       UserId = item.Element("Type").Value,
                                       Phone = item.Element("RepeatType").Value,
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
                logger.LogException(ex);
                return contact;
            }
        }

        private Contact SearchContactXML(string name, string element)
        {
            Contact contact = new Contact();
            XDocument xmlDoc = new XDocument();
            try
            {
                xmlDoc = XDocument.Load($"{userId}.xml");
                var searchQuery = (from item in xmlDoc.Descendants("Contact")
                                   where item.Element(element).Value == name
                                   select new Contact
                                   {
                                       Name = item.Element("EventId").Value,
                                       ContactId = item.Element("Title").Value,
                                       Image = item.Element("Description").Value,
                                       UserId = item.Element("Type").Value,
                                       Phone = item.Element("RepeatType").Value,
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
                logger.LogException(ex);
                return contact;
            }
        }


    }
}

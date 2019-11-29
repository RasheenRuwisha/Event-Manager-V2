using EventManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EventManager.DatabaseHelper
{
    public class ContactHelper
    {
        readonly String userId = Application.UserAppDataRegistry.GetValue("userID").ToString();
        public bool DoesNameExist(String name)
        {

            using (var dbContext = new DatabaseModel())
            {
                var userDetails = dbContext.Contacts.Where(contact => contact.Name.Equals(name)).Where(contact => contact.Userid.Equals(userId)).FirstOrDefault();
                if (userDetails != null)
                {
                    return false;
                }
            }
            return true;
        }

        public bool DoesEmailExist(String email)
        {

            using (var dbContext = new DatabaseModel())
            {
                var userDetails = dbContext.Contacts.Where(contact => contact.Email.Equals(email)).Where(contact => contact.Userid.Equals(userId)).FirstOrDefault();
                if (userDetails != null)
                {
                    return false;
                }
            }
            return true;
        }


        public bool AddContact(Contact contact)
        {

            try
            {
                using (var dbContext = new DatabaseModel())
                {
                    dbContext.Contacts.Add(contact);
                    dbContext.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool RemoveContact(String id)
        {
            try
            {
                using (var dbContext = new DatabaseModel())
                {
                    Contact contact = dbContext.Contacts.Find(id);
                    dbContext.Contacts.Remove(contact);
                    dbContext.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public List<Contact> GetUserContacts()
        {
            List<Contact> contacts = new List<Contact>();
            using(var dbContext = new DatabaseModel())
            {
                contacts = dbContext.Contacts.Where(contact => contact.Userid.Equals(userId)).OrderBy(contact => contact.Name).ToList();
            }
            return contacts;
        }

        public Contact GetContactDetails(string contactId)
        {
            Contact contactDetails = new Contact();
            using (var dbContext = new DatabaseModel())
            {
                contactDetails = dbContext.Contacts.Find(contactId);
            }
            return contactDetails;
        }

        public bool UpdateContacts(Contact contact)
        {
            try
            {
                Contact contactDetails = new Contact();
                using (var dbContext = new DatabaseModel())
                {
                    contactDetails = dbContext.Contacts.Find(contact.Contactid);
                    contactDetails.Name = contact.Name;
                    contactDetails.Email = contact.Email;
                    contactDetails.Image = contact.Image;
                    contactDetails.AddressLine1 = contact.AddressLine1;
                    contactDetails.AddressLine2 = contact.AddressLine2;
                    contactDetails.State = contact.State;
                    contactDetails.City = contact.City;
                    contactDetails.Zipcode = contact.Zipcode;
                    dbContext.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        
        }

        public List<Contact> GetUserContactsByName(String name)
        {
            List<Contact> contacts = new List<Contact>();
            using (var dbContext = new DatabaseModel())
            {
                contacts = dbContext.Contacts.Where(contact => contact.Userid.Equals(userId)).Where(contact => contact.Name.Contains(name)).OrderBy(contact => contact.Name).ToList();
            }
            return contacts;
        }

    }
}

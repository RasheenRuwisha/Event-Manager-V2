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

namespace EventManager.DatabaseHelper
{
    public class UserHelper
    {

      
        /// <summary>
        /// This methods creates a new user in the database.
        /// Then it creates the user credentials in the database
        /// </summary>
        /// <param name="user"></param>
        /// <param name="userCredential"></param>
        /// <returns></returns>
        public static bool AddUser(User user, UserCredential userCredential)
        {
            try
            {
                using (var dbContext = new DatabaseModel())
                {
                    dbContext.Users.Add(user);
                    dbContext.Userscredentials.Add(userCredential);
                    dbContext.SaveChanges();
                }
                return true;
            }catch(Exception ex)
            {
                Logger.LogException(ex,true);
                return false;
            }
        }

        /// <summary>
        /// This check wether the user 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool UserExists(String email)
        {
            using (var dbContext = new DatabaseModel())
            {
                var userDetails = dbContext.Users.Where(user => user.Email.Equals(email)).FirstOrDefault();
                if (userDetails != null)
                {
                    return true;
                }
            }
            return false;
        }


        /// <summary>
        /// This method validates the user for login with the provided credentails
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="rememberme"></param>
        /// <returns></returns>
        public static bool ValidateUser(String email, String password, bool rememberme)
        {
            CommonUtil commonUtil = new CommonUtil();

            using (var dbContext = new DatabaseModel())
            {
                var userDetails = dbContext.Userscredentials.Where(user => user.Email.Equals(email)).FirstOrDefault();
                if (userDetails != null)
                {
                    if (PasswordHasher.Validate(password, userDetails.Password)){
                        commonUtil.AddUserDetailsToLocalApp(userDetails,rememberme);
                        String workingDir = Directory.GetCurrentDirectory();

                        if (!File.Exists(workingDir + $@"\{userDetails.UserId}.xml"))
                        {
                            XDocument xmlDoc = new XDocument();
                            xmlDoc.Add(new XElement("LocalStore"));
                            xmlDoc.Save(workingDir + $@"\{userDetails.UserId}.xml");
                        }
                        var user = dbContext.Users.Where(userD => userD.Email.Equals(email)).FirstOrDefault();
                        Application.UserAppDataRegistry.SetValue("image", user.Image);
                        return true;
                    }
                    return false;
                }
            }
            return false;
        }


        /// <summary>
        /// This method retreives the users information from the database.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static User GetUser(String userId)
        {
            User user = new User();
            using (var dbContext = new DatabaseModel())
            {
                user = dbContext.Users.Where(users => users.UserId.Equals(userId)).FirstOrDefault();
            }
            return user;
        }

        /// <summary>
        /// This method updates the user information.
        /// </summary>
        /// <param name="userUpdate"></param>
        /// <returns></returns>
        public static bool UpdateUser(User userUpdate)
        {

            try
            {
                User user = new User();
                using (var dbContext = new DatabaseModel())
                {
                    user = dbContext.Users.Where(users => users.UserId.Equals(userUpdate.UserId)).FirstOrDefault();
                    user.Email = userUpdate.Email;
                    user.Phone = userUpdate.Phone;
                    user.Name = userUpdate.Name;
                    user.Email = userUpdate.Email;
                    user.Username = userUpdate.Username;
                    dbContext.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, true);
                return false;
            }


        }
    }
}

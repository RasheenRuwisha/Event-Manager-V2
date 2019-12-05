using EventManager.Model;
using EventManager.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EventManager.DatabaseHelper
{
    public class UserHelper
    {
        CommonUtil commonUtil = new CommonUtil();
        Logger logger = new Logger();
        public bool IsNewUser(String email)
        {
            using (var dbContext = new DatabaseModel())
            {
                var userDetails = dbContext.Users.Where(user => user.Email.Equals(email)).FirstOrDefault();
                if(userDetails != null)
                {
                    return false;
                }
            }
            return true;
        }

        public bool AddUser(User user, UserCredential userCredential)
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
                logger.LogException(ex,true);
                return false;
            }
        }


        public bool UserExists(String email)
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

        public bool ValidateUser(String email, String password)
        {
            using (var dbContext = new DatabaseModel())
            {
                var userDetails = dbContext.Userscredentials.Where(user => user.Email.Equals(email)).FirstOrDefault();
                if (userDetails != null)
                {
                    if (PasswordHasher.Validate(password, userDetails.Password)){
                        commonUtil.AddUserDetailsToLocalApp(userDetails);
                        String workingDir = Directory.GetCurrentDirectory();

                        if (!File.Exists(workingDir + $@"\{userDetails.UserId}.xml"))
                        {
                            XDocument xmlDoc = new XDocument();
                            xmlDoc.Add(new XElement("LocalStore"));
                            xmlDoc.Save(workingDir + $@"\{userDetails.UserId}.xml");
                        }
                        return true;
                    }
                    return false;
                }
            }
            return false;
        }

        public User GetUser(String userId)
        {
            User user = new User();
            using (var dbContext = new DatabaseModel())
            {
                user = dbContext.Users.Where(users => users.UserId.Equals(userId)).FirstOrDefault();
            }
            return user;
        }
    }
}

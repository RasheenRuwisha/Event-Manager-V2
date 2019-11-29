using EventManager.Model;
using EventManager.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.DatabaseHelper
{
    public class UserHelper
    {
        CommonUtil commonUtil = new CommonUtil();
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
                    if (userDetails.Password.Equals(password)){
                        commonUtil.AddUserDetailsToLocalApp(userDetails);
                        return true;
                    }
                    return false;
                }
            }
            return false;
        }
    }
}

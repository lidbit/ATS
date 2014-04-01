using System;
using System.Data.Common;
using Ats.DAL;
using Ats.Model;
using System.Collections.Generic;

namespace core
{
    public class UserManager
    {
        
        private List<User> users;

        public UserManager()
        {
            users = new List<User>();
            initUsers();
        }

        public void initUsers() 
        {
            users = UserDAL.GetUsers();
        }

        public User getUser( String name ) 
        {
		    User user = null;
		    foreach (User usr in users) 
            {
			    if (usr.Name.Equals(name)) 
                {
				    user = usr;
                    break;
			    }
		    }
		    return user;
        }

        public List<User> getUsers()
        {
            return users;
        }
    }
}

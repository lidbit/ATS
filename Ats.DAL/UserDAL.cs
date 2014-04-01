using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ats.Model;
using foundation;
using System.Data.Common;

namespace Ats.DAL
{
    public class UserDAL
    {
        private static IDatabase<DbDataReader, DbConnection> dbmanager;

        public static List<User> GetUsers()
        {
            dbmanager = dbManager.Instance();
            dbmanager.executeQuery("select * from users;");
            List<User> users = new List<User>();

            while (dbmanager.Reader.Read())
            {
                int userid = dbmanager.Reader.GetInt32(0);
                string name = dbmanager.Reader.GetString(1);
                users.Add(new User() 
                { 
                    Name = name, 
                    Id = userid 
                });
            }
            return users;
        }
    }
}

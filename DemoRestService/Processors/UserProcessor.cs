using DemoRestService.DbConnections;
using DemoRestService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoRestService.Processors
{
    public class UserProcessor
    {
        public static bool ProcessUser(User user)
        {
            //maybe later encrypt credentials here
            return DbConnector.AddUserToDB(user);
        }
    }
}
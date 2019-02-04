using DemoRestService.DbConnections;
using DemoRestService.Models;
using DemoRestService.Processors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace DemoRestService.Controllers
{
    public class UserController : ApiController
    {
        [HttpGet]
        [Route("api/GetUser")]
        public User GetUser(string username)
        {
            DataTable resultdt = DbConnector.GetUserFromDB(username);
            if (resultdt == null)
            {
                return null;
            }
            DataRow row = resultdt.Rows[0];
            var user = new User
                {
                    username = row["username"].ToString(),
                    password = row["password"].ToString()
                };
            return user;
            
            
        }
        //POST NEW User
        [HttpPost]
        [Route("api/SaveUser")]
        public bool SaveTower(User user)
        {
            if (user == null)
            {
                return false;
            }
            return UserProcessor.ProcessUser(user);

        }
    }
}
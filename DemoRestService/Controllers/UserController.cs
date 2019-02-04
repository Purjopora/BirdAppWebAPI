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
        [Route("api/VerifyUser")]
        public bool VerifyUser(string username, string password)
        {
            DataTable resultdt = DbConnector.GetUserFromDB(username);
            if (resultdt == null)
            {
                return false;
            }
            DataRow row = resultdt.Rows[0];
            var parts = row["passwordhash"].ToString();
            return PasswordHash.VerifyPassword(password, parts);       
        }


        //POST NEW User
        [HttpPost]
        [Route("api/SaveUser")]
        public bool SaveUser(User user)
        {
            if (user == null)
            {
                return false;
            }
            return UserProcessor.ProcessUser(user);

        }
    }
}
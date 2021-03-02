using DemoEampleApllication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;

namespace DemoEampleApllication.Controllers
{

    public class UserController : ApiController
    {
        // GET: api/UserMaster
        UserManagement usrmgt = new UserManagement();
        [Route("api/demo")]
        public List<UserMaster> GetUsers()
        {
            DataTable dt = new DataTable();
            dt = null;
            List<UserMaster> uMtr = new List<UserMaster>();

            uMtr = usrmgt.GetUserDetails();
            return uMtr;

        }

        // GET: api/User/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/User
        [Route("api/UserEntry")]
        public string Post([FromBody]Dictionary<string, List<Models.UserMaster>> UserEntry)
        {
            string Status = "";
            bool b = false;
            try
            {
                if (UserEntry != null)
                {
                    foreach (KeyValuePair<string, List<Models.UserMaster>> uEn in UserEntry)
                    {
                        List<Models.UserMaster> a = uEn.Value;
                        foreach (Models.UserMaster at in a)
                        {
                            b = usrmgt.insertUserDetails(at.UserId,at.UserName,at.UserPassword,at.Location);
                            if (b)
                                Status = "successfully";
                            else
                                Status = "Failure" + UserEntry.Count; ;
                        }
                    }
                }
                else
                    Status = "NO DATA FOUND " + b;
            }
            catch
            {
            }
            return Status;
        }


        public void Post([FromBody]string value)
        {
        }

        // PUT: api/User/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/User/5
        public void Delete(int id)
        {
        }
    }
}

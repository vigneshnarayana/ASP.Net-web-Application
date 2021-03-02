using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace DemoEampleApllication.Models
{
    public class UserManagement
    {
        public bool insertUserDetails(string UID, string UName, string UPass, string ULocation)
        {
            string S = "";
            bool c = false;
            try
            {
                string sqlQry = "Insert into UserTable (UserId, UserName, UserPassword, Location) VAlues ('" + UID + "' ,'" + UName + "','" + UPass + "','" + ULocation + "')";
                bool b = Demoexample.Data.Instance.ExecuteNonQuery(sqlQry);
                if (b)
                { c = true; }
                else
                { c = false; }

            }
            catch
            {
            }
            return c;
        }
        public List<UserMaster> GetUserDetails()
        {
            DataTable dt = new DataTable();
            int count = 0;
            UserMaster u = null;
            List<UserMaster> user = new List<UserMaster>();
            try
            {
                string strqry = " select UserId,UserName , UserPassword,  Location from UserTable";
                dt = Demoexample.Data.Instance.GetDataTable(strqry);
                count = dt.Rows.Count;
                if (count > 0)

                {
                    for (int i = 0; i < count; i++)
                    {
                        u = new UserMaster();
                        u.UserId = dt.Rows[i]["UserId"].ToString();
                        u.UserName = dt.Rows[i]["UserName"].ToString();
                        u.UserPassword = dt.Rows[i]["UserPassword"].ToString();
                        u.Location = dt.Rows[i]["Location"].ToString();
                        user.Add(u);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                return user = null;
            }
            return user;
        }
    }
}
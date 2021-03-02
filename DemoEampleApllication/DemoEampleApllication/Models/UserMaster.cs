using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoEampleApllication.Models
{
    public class UserMaster

    {
        private String m_UserId =null;
        private String m_UserName = null;
        private String m_UserPassword =null;
        private String m_Location =null;

        public String UserId {
            get { return m_UserId; }
            set { m_UserId = value ;}
        }
        public String UserName
        {
            get { return m_UserName; }
            set { m_UserName = value; }
        }
        public String UserPassword
        {
            get { return m_UserPassword; }
            set { m_UserPassword = value; }
        }
        public String Location
        {
            get { return m_Location; }
            set { m_Location = value; }
        }



    }
}
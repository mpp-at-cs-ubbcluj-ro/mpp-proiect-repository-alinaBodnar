using System;
using System.Collections.Generic;
using System.Text;

namespace teledonCS.model
{
    [Serializable]
    public class Volunteer
    {
        public Volunteer(int id, string username, string password)
        {
            this.id = id;
            this.username = username;
            this.password = password;
        }
        public Volunteer(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }

        public override string ToString()
        {
            return username + " " + password;
        }
    }
}

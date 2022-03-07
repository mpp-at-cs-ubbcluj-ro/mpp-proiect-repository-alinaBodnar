using System;
using System.Collections.Generic;
using System.Text;

namespace teledonCS.model
{
    class Volunteer
    {
        public Volunteer(int id, string firstName, string lastName)
        {
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
        }

        private int id { get; set; }
        private string firstName { get; set; }
        private string lastName { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}

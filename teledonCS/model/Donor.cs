using System;
using System.Collections.Generic;
using System.Text;

namespace teledonCS.model
{
    class Donor
    {
        public Donor(int id, string firstName, string lastName, string address, string phoneNumber)
        {
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.address = address;
            this.phoneNumber = phoneNumber;
        }

        private int id { get; set; }
        private string firstName { get; set; }
        private string lastName { get; set; }
        private string address { get; set; }
        private string phoneNumber { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}

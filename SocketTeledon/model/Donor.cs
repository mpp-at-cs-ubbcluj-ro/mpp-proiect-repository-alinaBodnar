using System;
using System.Collections.Generic;
using System.Text;

namespace teledonCS.model
{
    [Serializable]
    public class Donor
    {
        // public Donor(int id, string firstName, string lastName, string address, string phoneNumber)
        // {
        //     this.id = id;
        //     this.firstName = firstName;
        //     this.lastName = lastName;
        //     this.address = address;
        //     this.phoneNumber = phoneNumber;
        // }

        public Donor(string firstName, string lastName, string address, string phoneNr)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.address = address;
            this.phoneNumber = phoneNr;
        }

        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string address { get; set; }
        public string phoneNumber { get; set; }

        public override string ToString()
        {
            return firstName + " " + lastName+" " + address + " " + phoneNumber;
        }
    }
}

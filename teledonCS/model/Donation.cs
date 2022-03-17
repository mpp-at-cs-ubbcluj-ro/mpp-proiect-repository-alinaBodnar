using System;
using System.Collections.Generic;
using System.Text;

namespace teledonCS.model
{
    class Donation
    {
        public Donation(int id, Donor donor, CharitableCase charitableCase, double amountDonated)
        {
            this.id = id;
            this.donor = donor;
            this.charitableCase = charitableCase;
            this.amountDonated = amountDonated;
        }

        public int id { get; set; }
        public Donor donor { get; set; }
        public CharitableCase charitableCase { get; set; }
        public double amountDonated { get; set; }

        public override string ToString()
        {
            return donor.ToString() + " " + charitableCase.ToString() + " " + amountDonated.ToString();
        }
    }
}

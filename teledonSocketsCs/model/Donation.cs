using System;
using System.Collections.Generic;
using System.Text;

namespace teledonCS.model
{
    [Serializable]
    public class Donation
    {
        public Donation(int id, Donor donor, CharitableCase charitableCase, int amountDonated)
        {
            this.id = id;
            this.donor = donor;
            this.charitableCase = charitableCase;
            this.amountDonated = amountDonated;
        }

        public Donation(Donor donor1, CharitableCase charitableCase1, int amount)
        {
            this.donor = donor1;
            this.charitableCase = charitableCase1;
            this.amountDonated = amount;
        }

        public int id { get; set; }
        public Donor donor { get; set; }
        public CharitableCase charitableCase { get; set; }
        public int amountDonated { get; set; }

        public override string ToString()
        {
            return donor.ToString() + " " + charitableCase.ToString() + " " + amountDonated.ToString();
        }
    }
}

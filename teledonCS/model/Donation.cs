using System;
using System.Collections.Generic;
using System.Text;

namespace teledonCS.model
{
    class Donation
    {
        public Donation(int id, int donorId, int charitableCaseId, double amountDonated)
        {
            this.id = id;
            this.donorId = donorId;
            this.charitableCaseId = charitableCaseId;
            this.amountDonated = amountDonated;
        }

        private int id { get; set; }
        private int donorId { get; set; }
        private int charitableCaseId { get; set; }
        private double amountDonated { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}

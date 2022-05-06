using System;
using System.Collections.Generic;
using System.Text;

namespace teledonCS.model
{
    class CharitableCase
    {
        public int id { get; set; }
        public string name { get; set; }
        public double amountRaised { get; set; }

        public CharitableCase(int id,string name,double amountRaised)
        {
            this.id = id;
            this.name = name;
            this.amountRaised = amountRaised;
        }

        public override string ToString()
        {
            return name + " " + amountRaised.ToString();
        }
    }
}

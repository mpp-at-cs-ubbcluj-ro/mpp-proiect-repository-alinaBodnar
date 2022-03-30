using System;
using System.Collections.Generic;
using System.Text;

namespace teledonCS.model
{
    public class CharitableCase
    {
        public int id { get; set; }
        public string name { get; set; }
        public int amountRaised { get; set; }

        public CharitableCase(int id,string name,double amountRaised)
        {
            this.id = id;
            this.name = name;
            this.amountRaised = (int) amountRaised;
        }

        public CharitableCase(string nameCc, int amountCc)
        {
            this.name = nameCc;
            this.amountRaised = amountCc;
        }

        public override string ToString()
        {
            return name + " " + amountRaised.ToString();
        }
    }
}

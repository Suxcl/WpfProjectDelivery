using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfProjectDelivery.Model
{
    public class Address
    {
        public string state { get; set; }
        public string city {  get; set; } 
        public string address_1 { get; set; }
        public string address_2 { get; set; }
        public string post_code { get; set; }

        public Address(string state, string city, string address_1, string address_2, string post_code)
        {
            this.state = state;
            this.city = city;
            this.address_1 = address_1;
            this.address_2 = address_2;
            this.post_code = post_code;
        }
        public override string ToString()
        {
            String address = state + ", " + city + ", " + address_1 + ", " + address_2 + ", " + post_code;
            return address;
        }
    }
}

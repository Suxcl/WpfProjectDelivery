using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfProjectDelivery.Model
{
    public class Client
    {
        public Guid ClientId { get; }
        public string ClientName { get; set; }
        public string ClientEmail { get; set; }
        public int Number { get; set; }
        public Address ClientAddress { get; set; }

        public Client(string Name, string Email, int number, Address address) {
            ClientId = Guid.NewGuid();
            ClientName = Name;
            ClientEmail = Email;
            Number = number;
            ClientAddress = address;
        }

        public Client() {
            ClientId = Guid.NewGuid();
            ClientName = "";
            ClientEmail = "";
            Number = 0;
            ClientAddress = new Address("", "", "", "", "");
        }

        public override string ToString()
        {
            String client = ClientName + ", " + ClientEmail + ", " + Number + ", " + ClientAddress.ToString();
            return client;
        }
    }

    
}

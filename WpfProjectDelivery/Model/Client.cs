using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfProjectDelivery.Model
{
    public class Client(string Name, string Email, int Number, Address address)
    {
        private Guid ClientId = Guid.NewGuid();
        private string ClientName = Name;
        private string ClientEmail = Email;
        private int Number = Number;
        private Address ClientAddress = address;
    }

    
}

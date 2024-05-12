using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace WpfProjectDelivery.Model
{
    public class Parcel(Client client, Address address_from, Address address_to)
    {
        public Guid ParcelId = Guid.NewGuid();
        public Client client = client;
        public Address address_from = address_from;
        public Address address_to = address_to;
        public ParcelState state = ParcelState.Pending;

    }
}

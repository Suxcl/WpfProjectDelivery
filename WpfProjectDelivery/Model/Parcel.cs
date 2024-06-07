using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace WpfProjectDelivery.Model
{
    

    public class Parcel
    {
        public Guid ParcelId { get; }
        public Client client { get; set; }
        public Address address_from { get; set; }
        public Address address_to { get; set; }
        public ParcelState state {  get; set; }
        
        public Parcel(Client client, Address address_from, Address address_to)
        {
            this.ParcelId = Guid.NewGuid();
            this.client = client;
            this.address_from = address_from;
            this.address_to = address_to;
            this.state = ParcelState.Pending;

        }

        public Parcel()
        {
            ParcelId = Guid.NewGuid();
            client = new Client();
            address_from = new Address("", "", "", "", "");
            address_to = new Address("", "", "", "", "");
        }

        public override string ToString()
        {
            String parcel = client.ToString() + "\n sender: " + address_from.ToString() + "\n reciever: " + address_to.ToString();
            return parcel;
        }

    }
}

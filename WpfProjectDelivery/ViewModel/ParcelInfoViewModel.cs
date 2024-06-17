using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfProjectDelivery.Model;

namespace WpfProjectDelivery.ViewModel
{
    class ParcelInfoViewModel
    {
        public string Id { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string State { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public Parcel ParcelToView { get; set; } = new Parcel();

        public ParcelsList parcelsList;


        public ParcelInfoViewModel()
        {
            Id = "1";
            Sender = "Zwiecrzyniecka 100 Białystok";
            Receiver = "Sienkiewicza 43/23 Białystok";
            State = "Delivered";

            Name = "Jhon Compoany Incorporated";
            Email = "k9GZb@example.com";
            Phone = "312315332";
            Address = "Bialystok, Poland";


        }

        public void SetParcel(Parcel parcel)
        {
            ParcelToView = parcel;

            Id = parcel.ParcelId.ToString();

            State = parcel.state.ToString();

            string SenderState = parcel.address_from.state;
            string SenderCity = parcel.address_from.city;
            string SenderAddress_1 = parcel.address_from.address_1;
            string SenderAddress_2 = parcel.address_from.address_2;
            string SenderPostCode = parcel.address_from.post_code;

            Sender = SenderState + " " + SenderCity + " " + SenderAddress_1 + " " + SenderAddress_2 + " " + SenderPostCode;

            Name = parcel.client.ClientName;
            Email = parcel.client.ClientEmail;
            Phone = parcel.client.Number.ToString();
            Address = parcel.client.ClientAddress.ToString();

            string ReceiverState = parcel.address_to.state;
            string ReceiverCity = parcel.address_to.city;
            string ReceiverAddress_1 = parcel.address_to.address_1;
            string ReceiverAddress_2 = parcel.address_to.address_2;
            string ReceiverPostCode = parcel.address_to.post_code;

            Receiver = ReceiverState + " " + ReceiverCity + " " + ReceiverAddress_1 + " " + ReceiverAddress_2 + " " + ReceiverPostCode;
        }

    }
}

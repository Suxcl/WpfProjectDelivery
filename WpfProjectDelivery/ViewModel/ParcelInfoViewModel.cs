using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }
}

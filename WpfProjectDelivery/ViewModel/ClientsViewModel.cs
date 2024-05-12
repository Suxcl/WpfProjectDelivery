using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfProjectDelivery.Model;
using WpfProjectDelivery.View;

namespace WpfProjectDelivery.ViewModel
{
    public class ClientsViewModel
    {
        public ObservableCollection<Client> Clients { get; set; }
        public ClientsViewModel() { 
            ClientsList clientsList = new ClientsList();
            Clients = clientsList.Clients;
            clientsList.AddClient(new Client("Alex","alex@alex.alex",1234,new Address("A","B","C","D","AA-BBB")));
            clientsList.AddClient(new Client("Barnuch", "Barnuch@Barnuch.Barnuch", 1234,new Address("A","B","C","D","AA-BBB")));
        }

    }
}

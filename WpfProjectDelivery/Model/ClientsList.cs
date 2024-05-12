using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfProjectDelivery.Model
{
    public class ClientsList
    {
        private ObservableCollection<Client> clients = new ObservableCollection<Client>();

        public ObservableCollection<Client> Clients
        {
            get { return clients; }
            set { clients = value; }
        }

        public void AddClient(Client client)
        {
            clients.Add(client);
        }

        public void RemoveClient(Client client)
        {
            clients.Remove(client);
        }
        


    }



}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfProjectDelivery.Model
{
    public sealed class ClientsList
    {
        private ClientsList() {
            /*
            for (int i = 0; i < 1; i++)
            {
                this.AddClient(new Client("Alex", "alex@alex.alex", 1234, new Address("A", "B", "C", "D", "AA-BBB")));
                this.AddClient(new Client("Barnuch", "Barnuch@Barnuch.Barnuch", 1234, new Address("A", "B", "C", "D", "AA-BBB")));
            }
            */
        }
        private static ClientsList? _instance;
        public static ClientsList GetInstance()
        {
            if(_instance == null)
            {
                _instance = new ClientsList();
            } 
            return _instance;
        }

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

        public void setClients(ObservableCollection<Client> clients)
        {
            this.clients = new ObservableCollection<Client>(clients);
        }

        public void RemoveClient(Client client)
        {
            clients.Remove(client);
        }

        internal void EditClient(Client client, Client newClient)
        {
            var item = clients.FirstOrDefault(c => c.ClientId == client.ClientId);
            if (item != null)
            {
                Debug.WriteLine(item.ToString());
                item.ClientName = newClient.ClientName;
                item.ClientEmail = newClient.ClientEmail;
                item.Number = newClient.Number;
                item.ClientAddress = newClient.ClientAddress;
                Debug.WriteLine(item.ToString());
            }
        }
    }



}

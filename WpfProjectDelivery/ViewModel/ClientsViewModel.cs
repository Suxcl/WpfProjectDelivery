using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfProjectDelivery.Model;
using WpfProjectDelivery.View;

namespace WpfProjectDelivery.ViewModel
{
    public class ClientsViewModel
    {
        public ObservableCollection<Client> Clients { get; set; }
        public ICommand AddClient_click { get; }
        public ICommand RemoveClient_click { get; }
        public ICommand EditClient_click { get; }
        public ClientsViewModel() { 
            ClientsList clientsList = ClientsList.GetInstance();
            Clients = clientsList.Clients;
            for (int i = 0; i<50 ; i++)
            {
                clientsList.AddClient(new Client("Alex", "alex@alex.alex", 1234, new Address("A", "B", "C", "D", "AA-BBB")));
                clientsList.AddClient(new Client("Barnuch", "Barnuch@Barnuch.Barnuch", 1234, new Address("A", "B", "C", "D", "AA-BBB")));
            }
            AddClient_click = new RelayCommand(AddClient);
            RemoveClient_click = new RelayCommand(RemoveClient);
            EditClient_click = new RelayCommand(EditClient);
        }

        private void EditClient(object obj)
        {
            Window window = new ClientEditDialog();
            window.ShowDialog();
        }

        private void RemoveClient(object obj)
        {
            throw new NotImplementedException();
        }

        private void AddClient(object obj)
        {
            Window window = new ClientAddDialog();
            window.ShowDialog();
        }
    }
}

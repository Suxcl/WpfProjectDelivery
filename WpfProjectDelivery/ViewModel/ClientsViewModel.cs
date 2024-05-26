using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using WpfProjectDelivery.Model;
using WpfProjectDelivery.View;

namespace WpfProjectDelivery.ViewModel
{
    public class ClientsViewModel
    {
        public CollectionViewSource ViewSource { get; set; }
        public ObservableCollection<Client> Clients { get; set; }
        public ICommand AddClient_click { get; }
        public ICommand RemoveClient_click { get; }
        public ICommand EditClient_click { get; }
        public Client _selectedClient;
        public Client SelectedClient
        {
            get
            {
                return _selectedClient;
            }
            set
            {
                Debug.WriteLine(value);
                _selectedClient = value;
            }
        }

        public ClientsViewModel() { 
            ClientsList clientsList = ClientsList.GetInstance();
            Clients = clientsList.Clients;

            AddClient_click = new RelayCommand(AddClient);
            RemoveClient_click = new RelayCommand(RemoveClient);
            EditClient_click = new RelayCommand(EditClient);

            this.ViewSource = new CollectionViewSource();
            ViewSource.Source = this.Clients;
        }



        private void EditClient(object obj)
        {
            if (SelectedClient != null) {
                //ClientEditDialogViewModel ClientEditDialogViewModel = new ClientEditDialogViewModel(SelectedClient);
                //Window window = new ClientEditDialog() { DataContext = ClientEditDialogViewModel };
                Window window = new ClientEditDialog();
                ClientEditDialogViewModel clientEditDialogViewModel = new ClientEditDialogViewModel();
                clientEditDialogViewModel.SetClient(SelectedClient);
                window.DataContext = clientEditDialogViewModel;

                window.ShowDialog();
                ViewSource.View.Refresh();
            }
        }


        private void RemoveClient(object obj)
        {
            if (SelectedClient != null)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show(
                    messageBoxText: "Are you sure you want to delete\n" + SelectedClient.ToString(), "Deletion Confirmation",
                    System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    ClientsList clientsList = ClientsList.GetInstance();
                    clientsList.RemoveClient(SelectedClient);
                    SelectedClient = null;
                }
            }
        }

        private void AddClient(object obj)
        {
            Window window = new ClientAddDialog();
            window.ShowDialog();
        }
    }
}

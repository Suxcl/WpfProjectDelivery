using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using WpfProjectDelivery.Model;
using WpfProjectDelivery.View;

namespace WpfProjectDelivery.ViewModel
{
    public class ClientsViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public CollectionViewSource ViewSource { get; set; }
        public ObservableCollection<Client> Clients { get; set; }
        public ICommand AddClient_click { get; }
        public ICommand RemoveClient_click { get; }
        public ICommand EditClient_click { get; }
        public ICommand Search_click { get; }
        public ICommand Cancel_click { get; }

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
        public string _searchText { get; set; }

        public string SearchText
        {
            get
            {
                return _searchText;
            }
            set
            {
                Trace.WriteLine("siema2");
                _searchText = value;
                OnPropertyChanged("SearchText");
            }
        }

        public ClientsViewModel() { 
            ClientsList clientsList = ClientsList.GetInstance();
            Clients = clientsList.Clients;

            AddClient_click = new RelayCommand(AddClient);
            RemoveClient_click = new RelayCommand(RemoveClient);
            EditClient_click = new RelayCommand(EditClient);
            Search_click = new RelayCommand(Search);
            Cancel_click = new RelayCommand(Cancel);

            this.ViewSource = new CollectionViewSource();
            ViewSource.Source = this.Clients;
        }

        public ObservableCollection<Client> Filter()
        {
            List<Client> clientList = new List<Client>(this.Clients.ToList());
            
            if (clientList.Count > 0)
            {
                foreach (Client client in this.Clients.ToList())
                {
                    if (SearchText != "")
                    {
                        if (!client.ToString().ToLower().Contains(SearchText.ToLower()))
                        {
                            clientList.Remove(client);
                        }
                    }
                }
            }


            return new ObservableCollection<Client>(clientList);
        }

        private void Cancel(object obj)
        {
            SearchText = "";
            ViewSource.Source = this.Clients;
            ViewSource.View.Refresh();
        }

        private void Search(object obj)
        {
            Trace.WriteLine("szukanie test");
            ViewSource.Source = Filter();
            ViewSource.View.Refresh();
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

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using WpfProjectDelivery.Model;
using WpfProjectDelivery.View;

namespace WpfProjectDelivery.ViewModel
{
    public class ParcelAddDialogViewModel : INotifyPropertyChanged
    {

        public ObservableCollection<Client> Clients { get; set; }
        public Client SelectedClient { get; set; }

        public ICommand Cancel_click { get; set; }
        public ICommand AddParcel_click { get; set; }

        //Bindings
        public String SenderState { get; set; } = "";
        public String SenderCity { get; set; } = "";
        public String SenderAddress_1 { get; set; } = "";
        public String SenderAddress_2 { get; set; } = "";
        public String SenderPostCode { get; set; } = "";
        public String ReceiverState { get; set; } = "";
        public String ReceiverCity { get; set; } = "";
        public String ReceiverAddress_1 { get; set; } = "";
        public String ReceiverAddress_2 { get; set; } = "";
        public String ReceiverPostCode { get; set; } = "";


        public ParcelAddDialogViewModel()
        {
            ClientsList clientsList = ClientsList.GetInstance();
            Clients = new ObservableCollection<Client>(clientsList.Clients);
            


            Cancel_click = new RelayCommand(Cancel);
            AddParcel_click = new RelayCommand(AddParcel);
        }


        public string SearchTextText
        {
            get => SearchTextText;
            set
            {
                if (SearchTextText == value) return;
                SearchTextText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SearchTextText)));
            }
        }

        private void Cancel(object obj)
        {
            CloseWindow(obj);
        }

        private void CloseWindow(object obj)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window is ParcelAddDialog)
                {
                    window.Close();
                    break;
                }
            }
        }


        private void AddParcel(object obj)
        {
            if (string.IsNullOrEmpty(SenderState) || string.IsNullOrEmpty(SenderCity) || string.IsNullOrEmpty(SenderAddress_1) || string.IsNullOrEmpty(SenderAddress_2) || string.IsNullOrEmpty(SenderPostCode) || string.IsNullOrEmpty(ReceiverState) || string.IsNullOrEmpty(ReceiverCity) || string.IsNullOrEmpty(ReceiverAddress_1) || string.IsNullOrEmpty(ReceiverAddress_2) || string.IsNullOrEmpty(ReceiverPostCode))
            {
                MessageBox.Show("Uzupełnij wszystkie pola");
                return;
            }
            if (SenderState.Any(char.IsDigit) || ReceiverState.Any(char.IsDigit))
            {
                MessageBox.Show("Niepoprawny stan");
                return;
            }
            if (SenderCity.Any(char.IsDigit) || ReceiverCity.Any(char.IsDigit))
            {
                MessageBox.Show("Niepoprawne miasto");
                return;
            }

            //Sprawdzanie regexu dla kodu pocztowego jest niemożliwe
            //jest zbyt dużo norm
            ParcelsList parcelslist = ParcelsList.GetInstance();
            Client client = SelectedClient;
            Parcel parcel = new Parcel(
                client,
                new Address(SenderState,SenderCity,SenderAddress_1,SenderAddress_2,SenderPostCode),
                new Address(ReceiverState,ReceiverCity,ReceiverAddress_1,ReceiverAddress_2,ReceiverPostCode));
            parcelslist.AddParcel(parcel);
            CloseWindow(obj);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}

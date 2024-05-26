using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfProjectDelivery.Model;
using WpfProjectDelivery.View;

namespace WpfProjectDelivery.ViewModel
{
    public class ClientAddDialogViewModel
    {
        public ICommand AddClient_click { get; private set; }
        public ICommand Cancel_click { get; private set; }

        public ClientsList clientsList;

        // rozpisanie bindingów
        public String ClientName { get; set; } = "";
        public String ClientEmail { get; set; } = "";
        public String ClientNumber { get; set; } = "";
        public String ClientState { get; set; } = "";
        public String ClientCity { get; set; } = "";
        public String ClientAdress1 { get; set; } = "";
        public String ClientAdress2 { get; set; } = "";
        public String ClientPostcode { get; set; } = "";

        // validacja 


        public ClientAddDialogViewModel()
        {
            clientsList = ClientsList.GetInstance();
            

            AddClient_click = new RelayCommand(AddClient);
            Cancel_click = new RelayCommand(Cancel);
        }

        private void Cancel(object obj)
        {   
            CloseWindow(obj);
        }

        private void CloseWindow(object obj)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window is ClientAddDialog)
                {
                    window.Close();
                    break;
                }
            }
        }


        private void AddClient(object obj)
        {
            

            if (ClientName == "" || ClientEmail == "" || ClientNumber == "" || ClientState == "" || ClientCity == "" || ClientAdress1 == "" || ClientPostcode == "")
            {
                MessageBox.Show("Uzupełnij wszystkie pola");
                return;
            }
            if(Regex.IsMatch(ClientEmail, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase) == false)
            {
                MessageBox.Show("Niepoprawny email");
                return;
            }
            if(Regex.IsMatch(ClientNumber, @"^\d+$") == false)
            {
                MessageBox.Show("Niepoprawny numer");
                return;
            }
            //Sprawdzanie regexu dla kodu pocztowego jest niemożliwe
            //jest zbyt dużo norm
            ClientsList clientsList = ClientsList.GetInstance();
            Client NewClient = new(
                    ClientName, ClientEmail, int.Parse(ClientNumber),
                    new Address(ClientState, ClientCity, ClientAdress1, ClientAdress2, ClientPostcode));
            clientsList.AddClient(NewClient);
            CloseWindow(obj);
        }

        internal void SetClient(Client selectedClient)
        {
            throw new NotImplementedException();
        }
    }
}

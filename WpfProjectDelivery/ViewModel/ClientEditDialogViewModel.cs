using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using WpfProjectDelivery.Model;
using WpfProjectDelivery.View;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace WpfProjectDelivery.ViewModel
{

    public class ClientEditDialogViewModel
    {
        public ICommand EditClient_click { get; private set; }
        public ICommand Cancel_click { get; private set; }

        public Client ClientToEdit { get; set; } = new Client();

        // rozpisanie bindingów
        public String ClientName { get; set; } = "";
        public String ClientEmail { get; set; } = "";
        public String ClientNumber { get; set; } = "";
        public String ClientState { get; set; } = "";
        public String ClientCity { get; set; } = "";
        public String ClientAdress1 { get; set; } = "";
        public String ClientAdress2 { get; set; } = "";
        public String ClientPostcode { get; set; } = "";

        public ClientsList clientsList;

        public ClientEditDialogViewModel()
        {
            clientsList = ClientsList.GetInstance();


            EditClient_click = new RelayCommand(EditClient);
            Cancel_click = new RelayCommand(Cancel);
        }

        public void SetClient(Client client)
        {
            ClientToEdit = client;

            ClientName = client.ClientName;
            ClientEmail = client.ClientEmail;
            ClientNumber = client.Number.ToString();
            ClientState = client.ClientAddress.state;
            ClientCity = client.ClientAddress.city;
            ClientAdress1 = client.ClientAddress.address_1;
            ClientAdress2 = client.ClientAddress.address_2;
            ClientPostcode = client.ClientAddress.post_code;
        }

        private void CloseWindow(object obj)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window is ClientEditDialog)
                {
                    window.Close();
                    break;
                }
            }
        }

        private void Cancel(object obj)
        {
            CloseWindow(obj);
        }

        private void EditClient(object obj)
        {

            if (ClientName == "" || ClientEmail == "" || ClientNumber == "" || ClientState == "" || ClientCity == "" || ClientAdress1 == "" || ClientPostcode == "")
            {
                MessageBox.Show("Uzupełnij wszystkie pola");
                return;
            }
            if (Regex.IsMatch(ClientEmail, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase) == false)
            {
                MessageBox.Show("Niepoprawny email");
                return;
            }
            if (Regex.IsMatch(ClientNumber, @"^\d+$") == false)
            {
                MessageBox.Show("Niepoprawny numer");
                return;
            }
            //Sprawdzanie regexu dla kodu pocztowego jest niemożliwe
            //jest zbyt dużo norm
            ClientsList clientsList = ClientsList.GetInstance();
            Client NewClient = new(
                    ClientName, ClientEmail, int.Parse(ClientNumber),
                    new Address(ClientState, ClientCity, ClientAdress1, ClientAdress2, ClientPostcode));;
            clientsList.EditClient(ClientToEdit, NewClient);
            CloseWindow(obj);
        }
    }


}

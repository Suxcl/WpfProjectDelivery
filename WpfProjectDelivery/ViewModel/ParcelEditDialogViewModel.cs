using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using WpfProjectDelivery.Model;
using WpfProjectDelivery.View;
using System.Collections.ObjectModel;

namespace WpfProjectDelivery.ViewModel
{
    public class ParcelEditDialogViewModel
    {
        public ICommand EditParcel_click { get; private set; }
        public ICommand Cancel_click { get; private set; }

        public Parcel ParcelToEdit { get; set; } = new Parcel();
        public ObservableCollection<string> Clients { get; set; }

        // bindingi
        public String SenderState { get; set; } = "";
        public String SenderCity { get; set; } = "";
        public String SenderAddress_1 { get; set; } = "";
        public String SenderAddress_2 { get; set; } = "";
        public String SenderPost_Code { get; set; } = "";
        public String ReceiverState { get; set; } = "";
        public String ReceiverCity { get; set; } = "";
        public String ReceiverAddress_1 { get; set; } = "";
        public String ReceiverAddress_2 { get; set; } = "";
        public String ReceiverPost_Code { get; set; } = "";

        public ParcelsList parcelsList;

        public ParcelEditDialogViewModel()
        {
            parcelsList = ParcelsList.GetInstance();
            ClientsList clientsList = ClientsList.GetInstance();
            Clients = new ObservableCollection<string>();

            EditParcel_click = new RelayCommand(EditParcel);
            Cancel_click = new RelayCommand(Cancel);
        }

        public void SetParcel(Parcel parcel)
        {
            ParcelToEdit = parcel;

            SenderState = parcel.address_from.state;
            SenderCity = parcel.address_from.city;
            SenderAddress_1 = parcel.address_from.address_1;
            SenderAddress_2 = parcel.address_from.address_2;
            SenderPost_Code = parcel.address_from.post_code;

            ReceiverState = parcel.address_to.state;
            ReceiverCity = parcel.address_to.city;
            ReceiverAddress_1 = parcel.address_to.address_1;
            ReceiverAddress_2 = parcel.address_to.address_2;
            ReceiverPost_Code = parcel.address_to.post_code;
        }

        private void CloseWindow(object obj)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window is ParcelEditDialog) 
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

        private void EditParcel(object obj)
        {
            if(string.IsNullOrEmpty(SenderState)|| string.IsNullOrEmpty(SenderCity)|| string.IsNullOrEmpty(SenderAddress_1)|| string.IsNullOrEmpty(SenderAddress_2)|| string.IsNullOrEmpty(SenderPost_Code)|| string.IsNullOrEmpty(ReceiverState)|| string.IsNullOrEmpty(ReceiverCity)|| string.IsNullOrEmpty(ReceiverAddress_1)|| string.IsNullOrEmpty(ReceiverAddress_2)|| string.IsNullOrEmpty(ReceiverPost_Code))
            {
                MessageBox.Show("Uzupełnij wszystkie pola");
                return;
            }
            if(SenderState.Any(char.IsDigit)||ReceiverState.Any(char.IsDigit))
                {
                    MessageBox.Show("Niepoprawny stan");
                    return; 
                }
            if(SenderCity.Any(char.IsDigit)||ReceiverCity.Any(char.IsDigit))
            {
                MessageBox.Show("Niepoprawne miasto");
                return ;
            }

            ParcelsList parcelsList = ParcelsList.GetInstance();
            Client client = new();
            Parcel NewParcel = new(client,new Address(SenderState,SenderCity,SenderAddress_1,SenderAddress_2,SenderPost_Code),new Address(ReceiverState,ReceiverCity,ReceiverAddress_1,ReceiverAddress_2,ReceiverPost_Code));
            parcelsList.EditParcel(ParcelToEdit, NewParcel);
            CloseWindow(obj);
        }
    }
}

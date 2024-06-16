using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using WpfProjectDelivery.Model;
using static System.Net.Mime.MediaTypeNames;

namespace WpfProjectDelivery.ViewModel
{
    class ParcelStatusViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public CollectionViewSource ViewSource { get; set; }
        public ObservableCollection<Parcel> Parcels { get; set; }
        public ObservableCollection<ParcelState> ParcelStates { get; set; }
        public ICommand set_Pending_click { get; set; }
        public ICommand set_Accepted_click { get; set; }
        public ICommand set_InTransit_click { get; set; }
        public ICommand set_Delivered_click { get; set; }
        public ICommand set_Lost_click { get; set; }
        public ICommand set_Canceled_click { get; set; }

        public Parcel _selectedParcel;
        public Parcel SelectedParcel
        {
            get
            {
                return _selectedParcel;
            }
            set
            {
                Debug.WriteLine(value);
                _selectedParcel = value;
            }
        }

        public ParcelStatusViewModel()
        {
            ParcelsList parcelsList = ParcelsList.GetInstance();
            Parcels = parcelsList.Parcels;

            set_Pending_click = new RelayCommand(set_Pending);
            set_Accepted_click = new RelayCommand(set_Accepted);
            set_InTransit_click = new RelayCommand(set_InTransit);
            set_Delivered_click = new RelayCommand(set_Delivered);
            set_Lost_click = new RelayCommand(set_Lost);
            set_Canceled_click = new RelayCommand(set_Canceled);

            this.ViewSource = new CollectionViewSource();
            ViewSource.Source = this.Parcels;
        }

        protected void update_view()
        {
            Parcels = ParcelsList.GetInstance().Parcels;
            ViewSource.Source = null;
            ViewSource.Source = this.Parcels;
        }

        private void set_Canceled(object obj)
        {
            Parcel parcel = SelectedParcel;
            if (parcel != null)
            {
                Parcel newParcel = new(parcel.client, parcel.address_from, parcel.address_to, ParcelState.Canceled);
                ParcelsList.GetInstance().EditParcel(parcel, newParcel);
                Debug.WriteLine("CANCELED");
                update_view();
                OnPropertyChanged(nameof(Parcels));
            }
        }

        private void set_Lost(object obj)
        {
            Parcel parcel = SelectedParcel;
            if (parcel != null)
            {
                Parcel newParcel = new(parcel.client, parcel.address_from, parcel.address_to, ParcelState.Lost);
                ParcelsList.GetInstance().EditParcel(parcel, newParcel);
                Debug.WriteLine("LOST");
                update_view();
            }
        }

        private void set_Delivered(object obj)
        {
            Parcel parcel = SelectedParcel;
            if (parcel != null)
            {
                Parcel newParcel = new(parcel.client, parcel.address_from, parcel.address_to, ParcelState.Delivered);
                ParcelsList.GetInstance().EditParcel(parcel, newParcel);
                Debug.WriteLine("DELIVERED");
                update_view();
            }
        }

        private void set_InTransit(object obj)
        {
            Parcel parcel = SelectedParcel;
            if (parcel != null)
            {
                Parcel newParcel = new(parcel.client, parcel.address_from, parcel.address_to, ParcelState.InDelivery);
                ParcelsList.GetInstance().EditParcel(parcel, newParcel);
                Debug.WriteLine("IN TRANSIT");
                update_view();
            }
        }

        private void set_Accepted(object obj)
        {
            Parcel parcel = SelectedParcel;
            if (parcel != null)
            {
                Parcel newParcel = new(parcel.client, parcel.address_from, parcel.address_to, ParcelState.Accepted);
                ParcelsList.GetInstance().EditParcel(parcel, newParcel);
                Debug.WriteLine("ACCEPTED");
                update_view();
            }
        }

        private void set_Pending(object obj)
        {
            Parcel parcel = SelectedParcel;
            if (parcel != null)
            {
                Parcel newParcel = new(parcel.client, parcel.address_from, parcel.address_to, ParcelState.Pending);
                ParcelsList.GetInstance().EditParcel(parcel, newParcel);
                Debug.WriteLine("PENDING");
                update_view();
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

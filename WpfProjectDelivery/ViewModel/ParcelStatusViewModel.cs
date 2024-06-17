using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows.Controls;
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
        public ICommand Search_click { get; }
        public ICommand Cancel_click { get; }


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
        public ComboBox StateComboBox { get; set; }
        public string _selectedState { get; set; }
        public string SelectedState
        {
            get
            {
                return _selectedState;
            }
            set
            {
                //SetAndResetView(Filter());
                _selectedState = value;
                OnPropertyChanged("SelectedState");
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
                //SetAndResetView(Filter());
                _searchText = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Parcel> Filter()
        {
            List<Parcel> parcelsList = new List<Parcel>(this.Parcels.ToList());
            var trimmedSelVal = SelectedState.Substring(SelectedState.IndexOf(":") + 1).Trim();
            if (parcelsList.Count > 0)
            {

                {
                    foreach (Parcel parcel in this.Parcels.ToList())
                    {

                        if (trimmedSelVal == "All" || trimmedSelVal == "Wszystkie") { }
                        else
                        {
                            if (parcel.state.ToString() != trimmedSelVal)
                            {
                                parcelsList.Remove(parcel);
                            }
                        };
                        if (SearchText != "")
                        {
                            var search = SearchText.ToLower();

                            if (!parcel.ToStringForSearch().ToLower().Contains(search))

                            {
                                parcelsList.Remove(parcel);
                            }
                        }
                    }
                }
            }


            return new ObservableCollection<Parcel>(parcelsList);
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
            Search_click = new RelayCommand(Search);
            Cancel_click = new RelayCommand(Cancel);

            this.ViewSource = new CollectionViewSource();
            ViewSource.Source = this.Parcels;
        }

        private void Search(object obj)
        {
            SetAndResetView(Filter());
        }

        public void SetAndResetView(ObservableCollection<Parcel> parcels)
        {
            this.ViewSource.Source = parcels;
            ViewSource.View.Refresh();
        }

        private void Cancel(object obj)
        {
            SearchText = "";
            var trimmedSelVal = SelectedState.Substring(SelectedState.IndexOf(":") + 1).Trim();
            if (trimmedSelVal == "All") SelectedState = "All";
            else SelectedState = "Wszystkie";

            var cb = (ComboBox)StateComboBox;
            if (cb != null) cb.SelectedIndex = -1;

            SetAndResetView(this.Parcels);
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

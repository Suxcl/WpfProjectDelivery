using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using WpfProjectDelivery;
using WpfProjectDelivery.Model;
using WpfProjectDelivery.View;

namespace WpfProjectDelivery.ViewModel
{
    internal class ParcelsViewModel : INotifyPropertyChanged
    {
        public CollectionViewSource ViewSource { get; set; }
        public ObservableCollection<Parcel> Parcels { get; set; }
        public ObservableCollection<ParcelState> ParcelStates { get; set; }
        public string _selectedState { get; set; }
        public ComboBox StateComboBox { get; set; }
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
        public Parcel _selectedParcel;
        private ObservableCollection<Parcel> flteredParcel;

        public Parcel SelectedParcel
        {
            get
            {
                return _selectedParcel;
            }
            set
            {
                _selectedParcel = value;
            }
        }


        public void FilterParcels()
        {
            List<Parcel> parcelsList = new List<Parcel>(this.Parcels.ToList());
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
 
                        if(trimmedSelVal == "All" || trimmedSelVal == "Wszystkie"){}
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

        // buttons events
        public ICommand AddParcel_click { get; }
        public ICommand RemoveParcel_click { get; }
        public ICommand EditParcel_click { get; }
        public ICommand ShowInfo_click { get; }
        public ICommand Search_click { get; }
        public ICommand Cancel_click { get; }

        public ParcelsViewModel()
        {
            ParcelsList parcelsList = ParcelsList.GetInstance();
            Parcels = parcelsList.Parcels;

            ParcelStates = new ObservableCollection<ParcelState>
            {
                ParcelState.Pending,
                ParcelState.Accepted,
                ParcelState.InDelivery,
                ParcelState.Delivered,
                ParcelState.Lost,
                ParcelState.Canceled,
            };

            SearchText = "";
            

            

            AddParcel_click = new RelayCommand(AddParcel);
            RemoveParcel_click = new RelayCommand(RemoveParcel);
            EditParcel_click = new RelayCommand(EditParcel);
            ShowInfo_click = new RelayCommand(ShowInfo);
            Search_click = new RelayCommand(Search);
            Cancel_click = new RelayCommand(Cancel);

            this.ViewSource = new CollectionViewSource();
            ViewSource.Source = this.Parcels;
        }

        private void EditParcel(object obj)
        {
            if (SelectedParcel != null)
            {
                Window window = new ParcelEditDialog();
                ParcelEditDialogViewModel parcelEditDialogViewModel = new ParcelEditDialogViewModel();
                parcelEditDialogViewModel.SetParcel(SelectedParcel);
                window.DataContext = parcelEditDialogViewModel;

                window.ShowDialog();
                ViewSource.View.Refresh();
            }
            
        }
        private void Search(object obj)
        {
            Trace.WriteLine("szukanie test");
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
            if(trimmedSelVal == "All") SelectedState = "All";
            else SelectedState = "Wszystkie";
            
            var cb = (ComboBox)StateComboBox;
            if(cb != null) cb.SelectedIndex = -1;

            SetAndResetView(this.Parcels);   
        }
        private void RemoveParcel(object obj)
        {
            if (SelectedParcel != null)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show(
                    messageBoxText: "Are you sure you want to delete\n" + SelectedParcel.ToString(), "Deletion Confirmation",
                    System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    ParcelsList parcelsList = ParcelsList.GetInstance();
                    parcelsList.RemoveParcel(SelectedParcel);
                    SelectedParcel = null;
                }
            }
        }

        private void AddParcel(object obj)
        {
            Console.WriteLine("teścik");
            Trace.WriteLine("teścik");
            Window window = new ParcelAddDialog();
            window.ShowDialog();
        }

        private void ShowInfo(object obj)
        {
            Window window = new ParcelInfo();
            ParcelInfoViewModel parcelInfoViewModel = new ParcelInfoViewModel();
            parcelInfoViewModel.SetParcel(SelectedParcel);
            window.DataContext = parcelInfoViewModel;

            window.ShowDialog();
            ViewSource.View.Refresh();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            Trace.WriteLine("teścik");
            //ViewSource.Source = Filter(this.Parcels);
            //ViewSource.View.Refresh();
            OnPropertyChanged(propertyName);
            return false;
        }
    }
}


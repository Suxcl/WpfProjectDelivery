﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using WpfProjectDelivery.Model;
using WpfProjectDelivery.View;

namespace WpfProjectDelivery.ViewModel
{
    internal class ParcelsViewModel
    {
        public CollectionViewSource ViewSource { get; set; }
        public ObservableCollection<Parcel> Parcels { get; set; }
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
        public ICommand AddParcel_click { get; }
        public ICommand RemoveParcel_click { get; }
        public ICommand EditParcel_click { get; }
        public ParcelsViewModel()
        {
            ParcelsList parcelsList = ParcelsList.GetInstance();
            Parcels = parcelsList.Parcels;

            AddParcel_click = new RelayCommand(AddParcel);
            RemoveParcel_click = new RelayCommand(RemoveParcel);
            EditParcel_click = new RelayCommand(EditParcel);

            this.ViewSource = new CollectionViewSource();
            ViewSource.Source = this.Parcels;
        }

        private void EditParcel(object obj)
        {
            throw new NotImplementedException();
            
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
            Window window = new ParcelAddDialog();
            window.ShowDialog();
        }
    }
}

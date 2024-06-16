using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Windows;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using WpfProjectDelivery.Model;

namespace WpfProjectDelivery.ViewModel
{
    class DashboardViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Parcel> Parcels { get; set; }
        public ObservableCollection<ParcelState> ParcelStates { get; set; }
        public SeriesCollection SeriesCollection { get; set; }
        protected ObservableValue Parcel_Pending { get; set; }
        protected ObservableValue Parcel_Accepted { get; set; }
        protected ObservableValue Parcel_InDelivery { get; set; }
        protected ObservableValue Parcel_Delivered { get; set; }
        protected ObservableValue Parcel_Lost { get; set; }
        protected ObservableValue Parcel_Cancelled { get; set; }


        public CollectionViewSource ViewSource { get; set; }

        public DashboardViewModel()
        {

            ParcelsList parcelsList = ParcelsList.GetInstance();
            Parcels = parcelsList.Parcels;
            
            Parcel_Pending = new ObservableValue(parcelsList.getParcelCount_onStatus(ParcelState.Pending));
            Parcel_Accepted = new ObservableValue(parcelsList.getParcelCount_onStatus(ParcelState.Accepted));
            Parcel_InDelivery = new ObservableValue(parcelsList.getParcelCount_onStatus(ParcelState.InDelivery));
            Parcel_Delivered = new ObservableValue(parcelsList.getParcelCount_onStatus(ParcelState.Delivered));
            Parcel_Lost = new ObservableValue(parcelsList.getParcelCount_onStatus(ParcelState.Lost));
            Parcel_Cancelled = new ObservableValue(parcelsList.getParcelCount_onStatus(ParcelState.Canceled));

            
            SeriesCollection = new SeriesCollection
            {
                new PieSeries
                {
                    Title = ParcelState.Pending.ToString(),
                    Values = new ChartValues<ObservableValue> { Parcel_Pending },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = ParcelState.Accepted.ToString(),
                    Values = new ChartValues<ObservableValue> { Parcel_Accepted },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = ParcelState.InDelivery.ToString(),
                    Values = new ChartValues<ObservableValue> { Parcel_InDelivery },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = ParcelState.Delivered.ToString(),
                    Values = new ChartValues<ObservableValue> { Parcel_Delivered },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = ParcelState.Lost.ToString(),
                    Values = new ChartValues<ObservableValue> { Parcel_Lost },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = ParcelState.Canceled.ToString(),
                    Values = new ChartValues<ObservableValue> { Parcel_Cancelled },
                    DataLabels = true
                },
            };

            this.ViewSource = new CollectionViewSource();
            ViewSource.Source = this.Parcels;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

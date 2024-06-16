using System;
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


        public CollectionViewSource ViewSource { get; set; }

        public DashboardViewModel()
        {
            ParcelsList parcelsList = ParcelsList.GetInstance();
            Parcels = parcelsList.Parcels;


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

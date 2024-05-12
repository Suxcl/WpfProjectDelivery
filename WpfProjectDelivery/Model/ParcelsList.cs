using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfProjectDelivery.Model
{
    public class ParcelsList
    {
        private ObservableCollection<Parcel> parcels = new ObservableCollection<Parcel>();
        public ObservableCollection<Parcel> Parcels
        { 
            get { return parcels; } 
            set { parcels = value; }
        }

        public void AddParcel(Parcel parcel)
        {
            parcels.Add(parcel);
        }

        public void RemoveParcel(Parcel parcel)
        {
            parcels.Remove(parcel);
        }
    }
}

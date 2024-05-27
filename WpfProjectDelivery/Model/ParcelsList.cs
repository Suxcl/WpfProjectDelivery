using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfProjectDelivery.Model
{
    public sealed class ParcelsList
    {
        private ParcelsList() {
            ClientsList clientsList = ClientsList.GetInstance();
            Client client = new Client();
            clientsList.AddClient(client);
            Parcel parcel = new Parcel(client,new Address("a","a","a","a","aa-aaa"), new Address("b", "b", "b", "b", "bb-bbb"));
            this.AddParcel(parcel);
        }
        private static ParcelsList? _instance;
        public static ParcelsList GetInstance()
        {
            _instance ??= new ParcelsList();
            return _instance;

        }
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

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
            //Parcel parcel = new Parcel(client,new Address("a","a","a","a","aa-aaa"), new Address("b", "b", "b", "b", "bb-bbb"));
            
            //this.AddParcel(parcel);
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

        public void setParcels(ObservableCollection<Parcel> parcels)
        {
            this.parcels = new ObservableCollection<Parcel>(parcels);
        }

        public int getParcelCount_onStatus(ParcelState state)
        {
            int count = 0;
            foreach (var parcel in parcels)
            {
                if (parcel.state == state)
                {
                    count++;
                }
            }
            return count;
        }

        public void AddParcel(Parcel parcel)
        {
            parcels.Add(parcel);
        }

        public void RemoveParcel(Parcel parcel)
        {
            parcels.Remove(parcel);
        }

        internal void EditParcel(Parcel parcel,Parcel newParcel) 
        {
            var item = parcels.FirstOrDefault(p=>p.ParcelId==parcel.ParcelId);
            if(item!=null)
            {
                Debug.WriteLine(item.ToString());
                item.client = newParcel.client;
                item.address_from = newParcel.address_from;
                item.address_to = newParcel.address_to;
                item.state = newParcel.state;
                Debug.WriteLine(item.ToString());
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfProjectDelivery.ViewModel
{
    public class ParcelAddDialogViewModel : INotifyPropertyChanged
    {

        public ObservableCollection<string> Clients { get; set; }


        public ParcelAddDialogViewModel()
        {
            Clients = new ObservableCollection<string>();
            for (var i = 0; i < 1000; i++)
            {
                Clients.Add($"Client {i}");
            }


            
        }

        

        public string SearchTextText
        {
            get => SearchTextText;
            set
            {
                if (SearchTextText == value) return;
                SearchTextText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SearchTextText)));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfProjectDelivery.View;

namespace WpfProjectDelivery.ViewModel
{

    public class MainViewModel : INotifyPropertyChanged
    {
        
        // buttons commands
        public ICommand ParcelView_Click { get; }
        public ICommand ClientsView_Click { get; }

        // navigation uri converter for Frame source
        public string FrameSource = "ParcelsView.xaml";
        private Uri _currentPage;
        public Uri CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            ParcelView_Click = new RelayCommand(ChangeViewToParcels);
            ClientsView_Click = new RelayCommand(ChangeViewToClients);


            CurrentPage = new Uri(FrameSource, UriKind.Relative);

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ChangeViewToClients(object obj)
        {
            string next_source = "ClientsView.xaml";
            if (FrameSource == next_source) { return; }
            FrameSource = next_source;
            CurrentPage = new Uri(FrameSource, UriKind.Relative);
        }

        private void ChangeViewToParcels(object obj)
        {
            string next_source = "ParcelsView.xaml";
            if (FrameSource == next_source) { return; }
            FrameSource = next_source;
            CurrentPage = new Uri(FrameSource, UriKind.Relative);
        }

    }


}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WpfProjectDelivery.View;

namespace WpfProjectDelivery.ViewModel
{

    public class MainViewModel : INotifyPropertyChanged
    {
        
        // buttons commands
        public ICommand ParcelView_Click { get; }
        public ICommand ClientsView_Click { get; }
        public ICommand ParcelStatusView_Click { get; }

        public string ImgSrc { get; set; }
        
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
            ParcelStatusView_Click = new RelayCommand(ChangeViewToParcelStatus);


            CurrentPage = new Uri(FrameSource, UriKind.Relative);
            //string path = Path.Combine(path1: Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), path2: @"Assets\wpd.png");
            //ImgSrc = path;

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

        private void ChangeViewToParcelStatus(object obj)
        {
            string next_source = "ParcelStatusView.xaml";
            if (FrameSource == next_source) { return; }
            FrameSource = next_source;
            CurrentPage = new Uri(FrameSource, UriKind.Relative);
        }

        
        

    }


}

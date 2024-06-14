using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Globalization;
using System.Threading;
using System.IO.IsolatedStorage;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.ObjectModel;
using WpfProjectDelivery.Model;
using System.IO;
using System.ComponentModel;
using System.Text.Json;
using System.Diagnostics;
using System.Xml.Linq;





namespace WpfProjectDelivery.View


{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public string parcelPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"parcels.json");
        public string clientPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"clients.json");


        public MainWindow()
        {
            InitializeComponent();
            string parcelPathv2 = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "parcels.dat");
            string clientPathv2 = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "clients.dat");




            ParcelsList parcels = ParcelsList.GetInstance();
            ClientsList clients = ClientsList.GetInstance();


            if (File.Exists(parcelPath))
            {
                string data = File.ReadAllText(parcelPath);
                try
                {
                    var deserialized = JsonSerializer.Deserialize<ObservableCollection<Parcel>>(data);
                    parcels.setParcels(deserialized);
                }
                catch
                {
                    Trace.WriteLine("znowu error parcel");
                }
                
                
            }
            else Trace.WriteLine("brak pliku parcel");
            if (File.Exists(clientPath))
            {

                string data = File.ReadAllText(clientPath);
                try
                {
                    var deserialized = JsonSerializer.Deserialize<ObservableCollection<Client>>(data);
                    //var deserialized = JsonDocument.Parse(data).Deserialize<string>();

                    clients.setClients(deserialized);
                }
                catch
                {
                    Trace.WriteLine("znowu error klient");
                }
            }
            else Trace.WriteLine("brak pliku client");
        }

        private void LangButton_Clikc(object sender, RoutedEventArgs e)
        {
            SetLang(((Button)sender).Tag.ToString());
        }

        private void SetLang(string lang)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(lang);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);

            Application.Current.Resources.MergedDictionaries.Clear();
            ResourceDictionary resourceDictionary = new ResourceDictionary()
            {
                Source = new Uri($"/Dictionary-{lang}.xaml",UriKind.Relative)
            };
            Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);

            LangPL.IsEnabled = true;
            LangEN.IsEnabled = true;

            switch (lang)
            {
                case "en-US":
                    LangEN.IsEnabled = false;
                    break;
                case "pl-PL":
                    LangPL.IsEnabled = false;
                    break ;
                default:
                    break;
            }
        }

        //public ObservableCollection<Parcel> parcels { get; set; }

        
        
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosed(e);
            ParcelsList parcels = ParcelsList.GetInstance();
            ClientsList clients = ClientsList.GetInstance();

            string jsonParcels = System.Text.Json.JsonSerializer.Serialize(parcels.Parcels);
            string jsonClients = System.Text.Json.JsonSerializer.Serialize(clients.Clients);
            File.WriteAllText(parcelPath, jsonParcels);
            File.WriteAllText(clientPath, jsonClients);
                
                
            
            App.Current.Shutdown();
        }

        

    }
}

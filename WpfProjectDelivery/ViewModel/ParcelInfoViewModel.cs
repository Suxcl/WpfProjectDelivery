using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WpfProjectDelivery.Model;

namespace WpfProjectDelivery.ViewModel
{
    class ParcelInfoViewModel
    {
        public Parcel Parcel { get; set; }
        public string Id { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string State { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public ICommand Print_click { get; }

        public Parcel ParcelToView { get; set; } = new Parcel();

        public ParcelsList parcelsList;


        public ParcelInfoViewModel()
        {
            Print_click = new RelayCommand(PrintClick);



            Id = "1";
            Sender = "Zwiecrzyniecka 100 Białystok";
            Receiver = "Sienkiewicza 43/23 Białystok";
            State = "Delivered";

            Name = "Jhon Compoany Incorporated";
            Email = "k9GZb@example.com";
            Phone = "312315332";
            Address = "Bialystok, Poland";


        }

        private void PrintClick(object obj)
        {

            FlowDocument doc = new FlowDocument();
            doc.PageWidth = 793.7; 
            doc.PageHeight = 1122.52;
            doc.ColumnWidth = 793.7; 
            /*
            Image image = new Image();
            image.Source = new BitmapImage(new Uri("pack://application:,,,/WpfProjectDelivery;component/wpd.png"));
            image.Width = 200; 
            image.Height = 100; 
            image.Stretch = Stretch.Uniform;
            BlockUIContainer blockUIContainer = new BlockUIContainer(image);
            doc.Blocks.Add(blockUIContainer);
            */
            doc.Blocks.Add(new Paragraph(new Run("Parcel Specifications: ")));
            doc.Blocks.Add(new Paragraph(new Run("ParcelID: "+Parcel.ParcelId.ToString())));
            doc.Blocks.Add(new Paragraph(new Run("Client Info:")));
            doc.Blocks.Add(new Paragraph(new Run("  Name: "+ Parcel.client.ClientName)));
            doc.Blocks.Add(new Paragraph(new Run("  Email: " + Parcel.client.ClientEmail)));
            doc.Blocks.Add(new Paragraph(new Run("  Number: " + Parcel.client.Number.ToString())));
            doc.Blocks.Add(new Paragraph(new Run("  Address: " + Parcel.client.ClientAddress.ToString())));
            doc.Blocks.Add(new Paragraph(new Run("Parcel details: ")));
            doc.Blocks.Add(new Paragraph(new Run("  State: " + Parcel.state.ToString())));
            doc.Blocks.Add(new Paragraph(new Run("  Sender address: " + Parcel.address_from.ToString())));
            doc.Blocks.Add(new Paragraph(new Run("  Receiver address: " + Parcel.address_to.ToString())));
            doc.Blocks.Add(new Paragraph(new Run("")));


            IDocumentPaginatorSource idpSource = doc;
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintDocument(idpSource.DocumentPaginator, "My Document");
            }
            
        }

        public void SetParcel(Parcel parcel)
        {
            ParcelToView = parcel;
            this.Parcel = parcel;

            Id = parcel.ParcelId.ToString();

            State = parcel.state.ToString();

            string SenderState = parcel.address_from.state;
            string SenderCity = parcel.address_from.city;
            string SenderAddress_1 = parcel.address_from.address_1;
            string SenderAddress_2 = parcel.address_from.address_2;
            string SenderPostCode = parcel.address_from.post_code;

            Sender = SenderState + " " + SenderCity + " " + SenderAddress_1 + " " + SenderAddress_2 + " " + SenderPostCode;

            Name = parcel.client.ClientName;
            Email = parcel.client.ClientEmail;
            Phone = parcel.client.Number.ToString();
            Address = parcel.client.ClientAddress.ToString();

            string ReceiverState = parcel.address_to.state;
            string ReceiverCity = parcel.address_to.city;
            string ReceiverAddress_1 = parcel.address_to.address_1;
            string ReceiverAddress_2 = parcel.address_to.address_2;
            string ReceiverPostCode = parcel.address_to.post_code;

            Receiver = ReceiverState + " " + ReceiverCity + " " + ReceiverAddress_1 + " " + ReceiverAddress_2 + " " + ReceiverPostCode;
        }

    }
}

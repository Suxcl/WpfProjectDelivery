using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Documents.Serialization;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;

namespace WpfProjectDelivery.View
{
    /// <summary>
    /// Logika interakcji dla klasy ParcelInfo.xaml
    /// </summary>
    public partial class ParcelInfo : Window
    {
        public ParcelInfo()
        {
            InitializeComponent();
        }

        private void PrintBtn(object sender, RoutedEventArgs e)
        {
            try
            {
                this.IsEnabled = false;

                PrintDialog printDialog = new PrintDialog();
                if(printDialog.ShowDialog()== true)
                {
                    printDialog.PrintVisual(Info, "ParcelInfo");
                }
            }
            finally
            {

                this.IsEnabled = true;
            }
        }

        public void Print_Preview(FrameworkElement element)
        {
            if (File.Exists("print_preview.xps") == true) File.Delete("print_preview.xps");

            XpsDocument doc = new XpsDocument("print_preview.xps", FileAccess.ReadWrite);
            XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(doc);
            SerializerWriterCollator output_Document = writer.CreateVisualsCollator();
            output_Document.BeginBatchWrite();
            output_Document.Write(element);
            output_Document.EndBatchWrite();

            FixedDocumentSequence preview = doc.GetFixedDocumentSequence();
            doc.Close();

            var window = new Window();
            window.Content = new DocumentViewer { Document = preview };
            window.ShowDialog();

            writer = null;
            output_Document = null;
            doc = null;
        }
    }
}

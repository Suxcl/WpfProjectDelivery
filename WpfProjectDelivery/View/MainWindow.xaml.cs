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

namespace WpfProjectDelivery.View
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

 


        public MainWindow()
        {
            InitializeComponent();
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
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfProjectDelivery.Model;

namespace WpfProjectDelivery.ViewModel
{
    public class ClientAddDialogViewModel
    {
        public ICommand AddClient_click { get; set; }
        public ICommand Cancel_click { get; set; }

        public ClientsList clientsList;

        // rozpisanie bindingów
        // validacja 


        public ClientAddDialogViewModel()
        {
            clientsList = ClientsList.GetInstance();
            

            AddClient_click = new RelayCommand(AddClient);
            Cancel_click = new RelayCommand(Cancel);
        }

        private void Cancel(object obj)
        {
            Window.GetWindow((FrameworkElement)obj).Close();
        }

        private void AddClient(object obj)
        {
            throw new NotImplementedException();
        }
    }
}

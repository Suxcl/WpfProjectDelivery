using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using WpfProjectDelivery.Model;

namespace WpfProjectDelivery.ViewModel
{

    public class ClientEditDialogViewModel
    {
        public ICommand EditClient_click { get; set; }
        public ICommand Cancel_click { get; set; }

        public ClientsList clientsList;

        public ClientEditDialogViewModel()
        {
            clientsList = ClientsList.GetInstance();


            EditClient_click = new RelayCommand(EditClient);
            Cancel_click = new RelayCommand(Cancel);
        }

        private void Cancel(object obj)
        {
            Window.GetWindow((FrameworkElement)obj).Close();
        }

        private void EditClient(object obj)
        {
            throw new NotImplementedException();
        }
    }


}

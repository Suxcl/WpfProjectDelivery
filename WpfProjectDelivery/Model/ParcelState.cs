using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfProjectDelivery.Model
{
    public enum ParcelState
    {
        Pending,
        Accepted,
        InDelivery,
        Delivered,
        Lost,
        Canceled,
    }
}

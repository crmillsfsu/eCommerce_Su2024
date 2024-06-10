using Amazon.Library.Models;
using Amazon.Library.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.MAUI.ViewModels
{
    public class InventoryViewModel
    {
        public List<ProductViewModel> Products { 
            get {
                //return InventoryServiceProxy.Current.Products.ToList();
            } 
        }
    }
}

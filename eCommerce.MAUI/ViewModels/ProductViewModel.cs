using Amazon.Library.Models;
using Amazon.Library.Services;
using eCommerce.Library.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.MAUI.ViewModels
{
    public class ProductViewModel
    {
        public override string ToString()
        {
            if(Model == null)
            {
                return string.Empty;
            }
            return $"{Model.Id} - {Model.Name} - {Model.Price:C}";
        }
        public ProductDTO? Model { get; set; }

        public string DisplayPrice
        {
            get
            {
                if (Model == null) { return string.Empty; }
                return $"{Model.Price:C}";
            }
        }

        public string PriceAsString
        {
            set
            {
                if (Model == null)
                {
                    return;
                }
                if(decimal.TryParse(value, out var price)) {
                    Model.Price = price;
                }else
                {

                }
            }
        }

        public ProductViewModel(int productId = 0)
        {
            if(productId == 0)
            {
                Model = new ProductDTO();
            }
            else
            {
                Model = InventoryServiceProxy
                    .Current
                    .Products.FirstOrDefault(p => p.Id == productId)
                    ?? new ProductDTO();
            }
        }

        public ProductViewModel(ProductDTO? model)
        {
            if(model != null)
            {
                Model = model;
            }
            else
            {
                Model = new ProductDTO();
            }
        }

        public async void Add()
        {
            if (Model != null)
            {
                Model = await InventoryServiceProxy.Current.AddOrUpdate(Model);
            }
        }
    }
}

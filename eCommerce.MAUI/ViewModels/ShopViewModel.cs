using Amazon.Library.Models;
using Amazon.Library.Services;
using eCommerce.Library.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.MAUI.ViewModels
{
    public class ShopViewModel : INotifyPropertyChanged
    {
        public ShopViewModel() {
            InventoryQuery = string.Empty;
            SelectedCart = Carts.FirstOrDefault();
        }

        private string inventoryQuery;
        public string InventoryQuery {
            set
            {
                inventoryQuery = value;
                NotifyPropertyChanged();
            }
            get { return inventoryQuery; }
        }
        public List<ProductViewModel> Products
        {
            get
            {
                return InventoryServiceProxy.Current.Products
                    .Where(p => p != null)
                    .Where(p => p.Quantity > 0)
                    .Where(p => p?.Name?.ToUpper()?.Contains(InventoryQuery.ToUpper()) ?? false)
                    .Select(p => new ProductViewModel(p)).ToList()
                    ?? new List<ProductViewModel>();
            }
        }

        private ShoppingCart? selectedCart;

        public ShoppingCart? SelectedCart
        {
            get
            {
                return selectedCart;
            }

            set
            {
                selectedCart = value;
                NotifyPropertyChanged(nameof(ProductsInCart));
            }
        }

        public ObservableCollection<ShoppingCart> Carts
        {
            get
            {
                return new ObservableCollection<ShoppingCart>(ShoppingCartServiceProxy.Current.Carts);
            }
        }

        public List<ProductViewModel> ProductsInCart
        {
            get
            {
                return SelectedCart?.Contents?.Where(p => p != null)
                    .Where(p => p?.Name?.ToUpper()?.Contains(InventoryQuery.ToUpper()) ?? false)
                    .Select(p => new ProductViewModel(p)).ToList()
                    ?? new List<ProductViewModel>();
            }
        }

        //private Product productToBuy;
        private ProductViewModel? productToBuy;
        public ProductViewModel? ProductToBuy {
            get => productToBuy;

            set
            {
                productToBuy = value;
                
                if(productToBuy != null && productToBuy.Model == null)
                {
                    productToBuy.Model = new ProductDTO();
                } else if(productToBuy != null && productToBuy.Model != null) {
                    productToBuy.Model = new ProductDTO(productToBuy.Model);
                }

            }
        }



        public void Refresh()
        {
            InventoryQuery = string.Empty;
            NotifyPropertyChanged(nameof(Products));
            NotifyPropertyChanged(nameof(Carts));
        }

        public void Search()
        {
            NotifyPropertyChanged(nameof(Products));
        }

        public void PlaceInCart()
        {
            if(ProductToBuy?.Model == null)
            {
                return;
            }
            //ProductToBuy.Model = new Product(ProductToBuy.Model);
            ProductToBuy.Model.Quantity = 1;
            ShoppingCartServiceProxy.Current.AddToCart(ProductToBuy.Model, SelectedCart.Id);

            ProductToBuy = null;
            NotifyPropertyChanged(nameof(ProductsInCart));
            NotifyPropertyChanged(nameof(Products));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

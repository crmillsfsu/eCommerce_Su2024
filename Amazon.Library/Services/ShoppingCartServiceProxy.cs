using Amazon.Library.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Library.Services
{
    public class ShoppingCartServiceProxy
    {
        private static ShoppingCartServiceProxy? instance;
        private static object instanceLock = new object();

        private List<ShoppingCart> carts;
        public ReadOnlyCollection<ShoppingCart> Carts
        {
            get
            {
                return carts.AsReadOnly();
            }
        }

        public ShoppingCart Cart
        {
            get
            {
                if(!carts.Any())
                {
                    var newCart = new ShoppingCart();
                    carts.Add(newCart);
                    return newCart;
                }
                return carts?.FirstOrDefault() ?? new ShoppingCart();
            }
        }

        private ShoppingCartServiceProxy() { 
            carts = new List<ShoppingCart>();
        }

        public static ShoppingCartServiceProxy Current
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ShoppingCartServiceProxy();
                    }
                }
                return instance;
            }
        }

        //public ShoppingCart AddOrUpdate(ShoppingCart c)
        //{
        //    //TODO: Someone do this.
        //}

        public void AddToCart(Product newProduct)
        {
            if(Cart == null || Cart.Contents == null)
            {
                return;
            }

            var existingProduct = Cart?.Contents?
                .FirstOrDefault(existingProducts => existingProducts.Id == newProduct.Id);

            var inventoryProduct = InventoryServiceProxy.Current.Products.FirstOrDefault(invProd => invProd.Id == newProduct.Id);
            if(inventoryProduct == null)
            {
                return;
            }
            
            inventoryProduct.Quantity -= newProduct.Quantity;

            if(existingProduct != null)
            {
                // update
                existingProduct.Quantity += newProduct.Quantity;
            } else
            {
                //add
                Cart?.Contents?.Add(newProduct);
            }
        }

    }
}

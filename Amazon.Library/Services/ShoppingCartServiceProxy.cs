using Amazon.Library.Models;
using eCommerce.Library.DTO;
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
        public List<ShoppingCart> Carts
        {
            get
            {
                return carts;
            }
        }


        private ShoppingCartServiceProxy() { 
            carts = new List<ShoppingCart>() { new ShoppingCart { Id = 1, Name="My Cart" } };
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

        public void AddToCart(ProductDTO newProduct, int id)
        {
            var cartToUse = Carts.FirstOrDefault(c => c.Id == id);
            if(cartToUse == null || cartToUse.Contents == null)
            {
                return;
            }

            var existingProduct = cartToUse?.Contents?
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
                cartToUse?.Contents?.Add(newProduct);
            }
        }

    }
}

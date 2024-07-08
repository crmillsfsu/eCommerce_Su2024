using Amazon.Library.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Amazon.Library.Utilities;
using eCommerce.Library.DTO;

namespace Amazon.Library.Services
{
    public class InventoryServiceProxy
    {
        private static InventoryServiceProxy? instance;
        private static object instanceLock = new object();

        private List<ProductDTO> products;

        public ReadOnlyCollection<ProductDTO> Products
        {
            get
            {
                return products.AsReadOnly();
            }
        }

        

        public async Task<ProductDTO> AddOrUpdate(ProductDTO p)
        {

            JsonSerializerSettings settings = new JsonSerializerSettings { 
                TypeNameHandling = TypeNameHandling.All
            
            };
            var result = await new WebRequestHandler().Post("/Inventory",p);
            return JsonConvert.DeserializeObject<ProductDTO>(result, settings);
        }

        private InventoryServiceProxy()
        {
            //TODO: Make a web call
            var response = new WebRequestHandler().Get("/Inventory").Result;
            products = JsonConvert.DeserializeObject<List<ProductDTO>>(response);
        }

        public static InventoryServiceProxy Current
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new InventoryServiceProxy();
                    }
                }
                return instance;
            }
        }
    }
}

using Amazon.Library.Services;
using eCommerce.Library.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.MAUI.ViewModels
{
    public class ImportViewModel
    {
        public string FilePath { get; set; }

        public async void ImportFile(Stream? stream = null)
        {
            StreamReader? sr = null;
            try
            {
                if(stream == null) { sr = new StreamReader(FilePath); }
                else
                {
                    sr = new StreamReader(stream);
                }
                
            } catch(Exception ex)
            {

            }
            string line = string.Empty;
            var products = new List<ProductDTO>();
            while((line = sr.ReadLine()) != null) {
                var tokens = line.Split(['|']);

                products.Add(new ProductDTO
                {
                    Name = tokens[0],
                    Price = decimal.Parse(tokens[1]),
                    Quantity = int.Parse(tokens[2])
                });
            }

            foreach(var product in products)
            {
                await InventoryServiceProxy.Current.AddOrUpdate(product);
            }
        }
    }
}

using Amazon.Library.Models;
using eCommerce.API.Database;
using eCommerce.Library.DTO;
using System.Linq;

namespace eCommerce.API.EC
{
    public class InventoryEC
    {
        
        public InventoryEC() { 
        }

        public async Task<IEnumerable<ProductDTO>> Get()
        {
            return Filebase.Current.Products.Take(100).Select(p => new ProductDTO(p));
        }

        public async Task<IEnumerable<ProductDTO>> Search(string? query)
        {
            return Filebase.Current.Products.Where(p => 
            (p?.Name != null && p.Name.ToUpper().Contains(query?.ToUpper() ?? string.Empty) )
                || 
            (p?.Description != null && p.Description.ToUpper().Contains(query?.ToUpper() ?? string.Empty)))
                .Take(100).Select(p => new ProductDTO(p));
        }

        public async Task<ProductDTO?> Delete(int id)
        {
            return new ProductDTO(Filebase.Current.Delete(id));
                
                /*Products.FirstOrDefault(p => p.Id == id);
            if (itemToDelete == null)
            {
                return null;
            }

            FakeDatabase.Products.Remove(itemToDelete);
            return new ProductDTO(itemToDelete);*/
        }

        public async Task<ProductDTO> AddOrUpdate(ProductDTO p)
        {
            /*bool isAdd = false;
            if (p.Id == 0)
            {
                isAdd = true;
                p.Id = FakeDatabase.NextProductId;
            }

            if (isAdd)
            {
                FakeDatabase.Products.Add(new Product(p));
            } else
            {
                var prodToUpdate = FakeDatabase.Products.FirstOrDefault(a => a.Id == p.Id);
                if(prodToUpdate != null)
                {
                    var index = FakeDatabase.Products.IndexOf(prodToUpdate);
                    FakeDatabase.Products.RemoveAt(index);
                    prodToUpdate = new Product(p);
                    FakeDatabase.Products.Insert(index, prodToUpdate);
                }
            }*/

            //return new ProductDTO(Filebase.Current.AddOrUpdate(new Product(p)));

            return new ProductDTO(new MSSQLContext().AddProduct(new Product(p)));
            
        }
    }
}

﻿using Amazon.Library.Models;
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
            return FakeDatabase.Products.Take(100).Select(p => new ProductDTO(p));
        }

        public async Task<ProductDTO> AddOrUpdate(ProductDTO p)
        {
            bool isAdd = false;
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
            }

            return p;
            
        }
    }
}

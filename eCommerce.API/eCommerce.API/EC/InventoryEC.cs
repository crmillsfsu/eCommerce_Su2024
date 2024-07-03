using Amazon.Library.Models;
using eCommerce.API.Database;
using System.Linq;

namespace eCommerce.API.EC
{
    public class InventoryEC
    {
        
        public InventoryEC() { 
        }

        public async Task<IEnumerable<Product>> Get()
        {
            return FakeDatabase.Products.Take(100);
        }
    }
}

using eCommerce.Library.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Library.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public List<ProductDTO>? Contents { get; set; }
        public string Name { get; set; }

        public ShoppingCart() { 
            Contents = new List<ProductDTO>();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}

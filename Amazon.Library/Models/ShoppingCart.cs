using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Library.Models
{
    public class ShoppingCart
    {
        int Id { get; set; }
        public List<Product>? Contents { get; set; }
    }
}

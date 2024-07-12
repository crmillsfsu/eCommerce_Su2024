using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Library.DTO
{
    public class Query
    {
        public string? QueryString { get; set; }

        public Query(string queryString)
        {
            QueryString = queryString;
        }
    }
}

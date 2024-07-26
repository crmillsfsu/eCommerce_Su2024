using Amazon.Library.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace eCommerce.API.Database
{
    public class MSSQLContext
    {
        public Product AddProduct(Product p)
        {
            using(SqlConnection conn = new SqlConnection("Server=CMILLS;Database=Amazon;Trusted_Connection=yes;TrustServerCertificate=True") )
            {
                using(SqlCommand cmd = conn.CreateCommand())
                {
                    var sql = $"Product.InsertProduct";
                    cmd.CommandText = sql ;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Name", $"'{p.Name}'"));
                    cmd.Parameters.Add(new SqlParameter("@Description", ""));
                    cmd.Parameters.Add(new SqlParameter("@Quantity", p.Quantity));
                    cmd.Parameters.Add(new SqlParameter("@Price", p.Price));

                    var id = new SqlParameter("Id", p.Id);
                    id.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(id);
                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();

                        p.Id = (int)id.Value;
                    } catch(Exception ex)
                    {

                    }

                }
            }

            return p;
        }
    
        public List<Product> GetProducts()
        {
            var products = new List<Product>();
            using (SqlConnection conn = new SqlConnection("Server=CMILLS;Database=Amazon;Trusted_Connection=yes;TrustServerCertificate=True"))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    var sql = $"SELECT Id, REPLACE(name, '''','') as Name, Price, Quantity FROM PRODUCT";
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql ;

                    try
                    {
                        conn.Open();
                        var reader = cmd.ExecuteReader();

                        while(reader.Read())
                        {
                            products.Add(new Product
                            {
                                Id = (int)reader["Id"],
                                Name = reader["Name"].ToString(),
                                Price = (decimal)reader["Price"],
                                Quantity = (int)reader["Quantity"]
                            });
                            
                        }
                        conn.Close();
                    } catch(Exception ex) { 
                    }
                }
            }

            return products;
        }
    }
}

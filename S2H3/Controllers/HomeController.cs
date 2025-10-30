using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using WebGrease.Css.Ast.Selectors;
using S2H3.Models;
using System.ComponentModel.Design;

namespace S2H3.Controllers
{
    public class HomeController : Controller
    {
        // Connection string to the SQL Server database
        public static string connectionString = "Data Source=.;Initial Catalog=BikeStores;Integrated Security=True";

        public ActionResult Index()
        {

            var model = new Models.IndexViewModel();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string getStaffQuery = @"Select staff_id, first_name, last_name, email, phone, active from sales.staffs";

                using (SqlCommand command = new SqlCommand(getStaffQuery, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            model.StaffList.Add(new Models.Staff
                            {
                                StaffId = reader.GetInt32(reader.GetOrdinal("staff_id")),
                                FirstName = reader.GetString(reader.GetOrdinal("first_name")),
                                LastName = reader.GetString(reader.GetOrdinal("last_name")),
                                Email = reader.GetString(reader.GetOrdinal("email")),
                                Phone = reader.IsDBNull(reader.GetOrdinal("phone")) ? "" : reader.GetString(reader.GetOrdinal("phone")),
                                Active = reader.GetBoolean(reader.GetOrdinal("active"))
                            });
                        }
                    }
                }

                string getCustomerQuery = @"Select customer_id, first_name, last_name, email, phone, street, city, state, zip_code from sales.customers";

                using (SqlCommand cmd = new SqlCommand(getCustomerQuery, connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            model.CustomerList.Add(new Customer
                            {
                                CustomerId = reader.GetInt32(0),
                                FirstName = reader.GetString(1),
                                LastName = reader.GetString(2),
                                Email = reader.GetString(3),
                                Phone = reader.IsDBNull(4) ? "" : reader.GetString(4),
                                Street = reader.IsDBNull(5) ? "" : reader.GetString(5),
                                City = reader.IsDBNull(6) ? "" : reader.GetString(6),
                                State = reader.IsDBNull(7) ? "" : reader.GetString(7),
                                ZipCode = reader.IsDBNull(8) ? "" : reader.GetString(8)
                            });
                        }
                    }
                }

                string getProductQuery = @"Select p.product_id, p.product_name, b.brand_name, c.category_name, p.model_year, p.list_price
                                          from production.product p
                                          inner join production.brands b 
                                          on p.brand_id = b.brand_id
                                          inner join production_categories c
                                          on p.category_id = c.category_id";

                using (SqlCommand command = new SqlCommand(getProductQuery, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            model.ProductList.Add(new Models.Product
                            {
                                ProductId = reader.GetInt32(0),
                                ProductName = reader.GetString(1),
                                BrandName = reader.GetString(2),
                                CategoryName = reader.GetString(3),
                                ModelYear = reader.GetDecimal(4),
                                ListPrice = reader.GetDecimal(5)
                            });
                        }
                    }
                }

                string getBrandQuery = @"Select brand_id, brand_name from production.brands";

                using (SqlCommand cmd = new SqlCommand(getBrandQuery, connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            model.Brands.Add(new SelectListItem
                            {
                                Value = reader.GetInt32(0).ToString(),
                                Text = reader.GetString(1)
                            });
                        }
                    }
                }

                string getCategoriesQuery = "SELECT category_id, category_name FROM production.categories";
                using (SqlCommand cmd = new SqlCommand(getCategoriesQuery, connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            model.Categories.Add(new SelectListItem
                            {
                                Value = reader.GetInt32(0).ToString(),
                                Text = reader.GetString(1)
                            });
                        }
                    }
                }
            }

            return View(model);
        }
    }
}
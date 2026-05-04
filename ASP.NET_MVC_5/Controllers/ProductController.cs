using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;
using ASP.NET_MVC_5.Models;

namespace ASP.NET_MVC_5.Controllers
{
    public class ProductController : Controller
    {
        string cs = ConfigurationManager.ConnectionStrings["crud"].ConnectionString;
        // 🔹 READ
        public ActionResult Index()
        {
            List<Product> list = new List<Product>();

            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "SELECT * FROM Products";
                SqlCommand cmd = new SqlCommand(query, con);

                con.Open(); 
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    list.Add(new Product
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Name = dr["Name"].ToString(),
                        Price = Convert.ToDecimal(dr["Price"]),
                        Quantity = Convert.ToInt32(dr["Quantity"])
                    });
                }
            }

            return View(list);
        }

        // 🔹 CREATE GET
        public ActionResult Create()
        {
            return View();
        }

        // 🔹 CREATE POST
        [HttpPost]
        public ActionResult Create(Product p)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "INSERT INTO Products(Name,Price,Quantity) VALUES(@Name,@Price,@Quantity)";
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@Name", p.Name);
                cmd.Parameters.AddWithValue("@Price", p.Price);
                cmd.Parameters.AddWithValue("@Quantity", p.Quantity);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

        // 🔹 EDIT GET
        public ActionResult Edit(int id)
        {
            Product p = new Product();

            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "SELECT * FROM Products WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    p.Id = Convert.ToInt32(dr["Id"]);
                    p.Name = dr["Name"].ToString();
                    p.Price = Convert.ToDecimal(dr["Price"]);
                    p.Quantity = Convert.ToInt32(dr["Quantity"]);
                }
            }

            return View(p);
        }

        // 🔹 EDIT POST
        [HttpPost]
        public ActionResult Edit(Product p)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "UPDATE Products SET Name=@Name, Price=@Price, Quantity=@Quantity WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@Id", p.Id);
                cmd.Parameters.AddWithValue("@Name", p.Name);
                cmd.Parameters.AddWithValue("@Price", p.Price);
                cmd.Parameters.AddWithValue("@Quantity", p.Quantity);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

        // 🔹 DELETE
        public ActionResult Delete(int id)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "DELETE FROM Products WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }
    }
}
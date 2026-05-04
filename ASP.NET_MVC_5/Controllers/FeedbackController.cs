using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;
using ASP.NET_MVC_5.Models;

namespace ASP.NET_MVC_5.Controllers
{
    public class FeedbackController : Controller
    {
        string cs = ConfigurationManager.ConnectionStrings["crud"].ConnectionString;

        // GET
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Feedback f)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "INSERT INTO Feedback(Name,Email,Message) VALUES(@Name,@Email,@Message)";
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@Name", f.Name);
                cmd.Parameters.AddWithValue("@Email", f.Email);
                cmd.Parameters.AddWithValue("@Message", f.Message);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            ViewBag.Message = "✅ Feedback submitted successfully!";
            ModelState.Clear();   // 🔥 form reset

            return View();
        }
    }
}
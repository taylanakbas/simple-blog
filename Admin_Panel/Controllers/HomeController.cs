using Admin_Panel.Login;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace Admin_Panel.Controllers
{

    public class HomeController : Controller
    {
        // GET: Home
        [AdminLogin]
        //[OutputCache(Duration = 60)]
        public ActionResult Index()
        {
            string conString = "Data Source=VKT-TAYLANA\\SQLEXPRESS;Initial Catalog=BlogWebSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection connection = new SqlConnection(conString);

            if (connection.State == ConnectionState.Closed)
                connection.Open();

            string contactMessage = "Select * From Contact";
            SqlCommand contactCommand = new SqlCommand(contactMessage, connection);
            SqlDataReader reader = contactCommand.ExecuteReader();

            List<Models.ContactModel> contactModel = new List<Models.ContactModel>();
            Models.ContactModel item;
            while (reader.Read())
            {
                item = new Models.ContactModel();
                item.Id = Convert.ToInt32(reader["Id"]);
                item.ContactName = reader["Name"].ToString();
                item.ContactMail = reader["Email"].ToString();
                item.ContactMessage = reader["Message"].ToString();
                item.ContactDate = reader["Date"].ToString();
                contactModel.Add(item);
            }

            connection.Close();
            reader.Close();
            return View(contactModel);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            if (new LoginState().IsLoginSuccess(username, password) == true)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ErrorMessage = "Kullanıcı Adı yada Şifre Yanlış!";
                return View();
            }

        }

        [HttpGet]
        public ActionResult DeleteMessages(int Id)
        {

            string conString = "Data Source=VKT-TAYLANA\\SQLEXPRESS;Initial Catalog=BlogWebSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection connection = new SqlConnection(conString);

            if (connection.State == ConnectionState.Closed)
                connection.Open();
            string deleteMessageQuery = "DELETE FROM Contact WHERE Id = @Id";
            SqlCommand deleteContact = new SqlCommand(deleteMessageQuery, connection);
            deleteContact.Parameters.AddWithValue("@Id", Id);
            deleteContact.ExecuteNonQuery();
            connection.Close();
            return RedirectToAction("Index", "Home");

        }





    }

}
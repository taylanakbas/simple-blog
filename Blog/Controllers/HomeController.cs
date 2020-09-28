using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home

        [OutputCache(Duration = 60)]
        public ActionResult HomePage()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Contact(string name, string email, string message)
        {
            int rowCount;
            string resultMessage;
            string conString = "Data Source=VKT-TAYLANA\\SQLEXPRESS;Initial Catalog=BlogWebSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection connection = new SqlConnection(conString);

            if (connection.State == ConnectionState.Closed)
                connection.Open();

            string contactQuery = "INSERT INTO Contact(Name,Email,Message,Date) VALUES(@name,@email,@message,GETDATE())";
            SqlCommand contactCommand = new SqlCommand(contactQuery, connection);

            contactCommand.Parameters.AddWithValue("@Name", name);
            contactCommand.Parameters.AddWithValue("@Email", email);
            contactCommand.Parameters.AddWithValue("@Message", message);
            contactCommand.Parameters.AddWithValue("@Date", DateTime.Now.Date);

            rowCount = contactCommand.ExecuteNonQuery();

            resultMessage = (rowCount > 0) ? "Message is succesfully sended" : "Message is not sended!";
            connection.Close();

            return Json(resultMessage, JsonRequestBehavior.AllowGet);
        }

    }
}

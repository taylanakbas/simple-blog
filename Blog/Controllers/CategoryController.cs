using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        [OutputCache(Duration = 60)]
        public ActionResult Categories()
        {
            string conString = "Data Source=VKT-TAYLANA\\SQLEXPRESS;Initial Catalog=BlogWebSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";


            SqlConnection connection = new SqlConnection(conString);

            if (connection.State == ConnectionState.Closed)
                connection.Open();

            string categoryListQuery = "";


            categoryListQuery = "SELECT Category.CategoryId, Category.CategoryName, Category.CategoryDesc, Category.CategoryImage FROM Category";


            SqlCommand contentCommand = new SqlCommand(categoryListQuery, connection);
          
            SqlDataReader reader = contentCommand.ExecuteReader();


            List<Models.CategoryModel> categoryModel = new List<Models.CategoryModel>();

            Models.CategoryModel item;

            while (reader.Read())
            {
                item = new Models.CategoryModel();
                item.CategoryId = Convert.ToInt32(reader["CategoryId"]);
                item.CategoryName = reader["CategoryName"].ToString();
                item.CategoryDesc = reader["CategoryDesc"].ToString();
                item.CategoryImage = reader["CategoryImage"].ToString();

                categoryModel.Add(item);
            }

            connection.Close();
            reader.Close();

            return View(categoryModel);
        }
    }
}
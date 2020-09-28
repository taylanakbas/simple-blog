
using Admin_Panel.Login;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.Mvc;
using ImageResizer;
using System.Web;

namespace Admin_Panel.Controllers
{
    [AdminLogin]
    public class CategoriesController : Controller
    {
        [HttpGet]
        //[OutputCache(Duration = 60)]
        public ActionResult Categories()
        {
            string conString = "Data Source=VKT-TAYLANA\\SQLEXPRESS;Initial Catalog=BlogWebSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection connection = new SqlConnection(conString);

            if (connection.State == ConnectionState.Closed)
                connection.Open();

            string listQuery = "SELECT * FROM Category";
            SqlCommand list = new SqlCommand(listQuery, connection);
            SqlDataReader reader = list.ExecuteReader();

            List<Models.CategoryModel> Categorymodel = new List<Models.CategoryModel>();
            Models.CategoryModel item;
            while (reader.Read())
            {
                item = new Models.CategoryModel();

                item.CategoryId = Convert.ToInt32(reader["CategoryId"]);
                item.CategoryName = reader["CategoryName"].ToString();
                item.CategoryDesc = reader["CategoryDesc"].ToString();
                Categorymodel.Add(item);
            }

            connection.Close();
            reader.Close();
            return View(Categorymodel);

        }

        [HttpPost]
        public ActionResult Categories(string CategoryName,string CategoryDesc, HttpPostedFileBase CategoryImage)
        {
            string conString = "Data Source=VKT-TAYLANA\\SQLEXPRESS;Initial Catalog=BlogWebSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection connection = new SqlConnection(conString);

            if (connection.State == ConnectionState.Closed)
                connection.Open();

            var fileExtension = Path.GetExtension(CategoryImage.FileName);
            var fileName = Guid.NewGuid() + fileExtension;
            var folderPath = Server.MapPath("/Uploads" + @"\CategoryImage\");
            var filePath = String.Concat(folderPath, fileName);
            var resized = Server.MapPath("/Uploads" + @"\CategoryImageResized\");
            var resizedfilePath = String.Concat(resized, "re_" + fileName);

            CategoryImage.SaveAs(filePath);

            ResizeSettings resizeSetting = new ResizeSettings
            {
                Width = 150,
                Height = 100,
                Format = fileExtension
            };
            ImageBuilder.Current.Build(filePath, resizedfilePath, resizeSetting);

            string addCategoryQuery;
            addCategoryQuery = "INSERT INTO Category(CategoryName,CategoryDesc,CategoryImage) VALUES (@CategoryName,@CategoryDesc,@CategoryImage)";
            SqlCommand addCategory = new SqlCommand(addCategoryQuery, connection);

            addCategory.Parameters.AddWithValue("@CategoryName", CategoryName);
            addCategory.Parameters.AddWithValue("@CategoryDesc", CategoryDesc);
            addCategory.Parameters.AddWithValue("@CategoryImage", fileName);
           


            addCategory.ExecuteNonQuery();
            connection.Close();

            return RedirectToAction("Categories");
        }

        [HttpPost]
        public ActionResult UpdateCategory(int CategoryId, string CategoryName, string CategoryDesc, HttpPostedFileBase CategoryImage){

            string conString = "Data Source=VKT-TAYLANA\\SQLEXPRESS;Initial Catalog=BlogWebSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection connection = new SqlConnection(conString);

            if (connection.State == ConnectionState.Closed)
                connection.Open();

            var fileExtension = Path.GetExtension(CategoryImage.FileName);
            var fileName = Guid.NewGuid() + fileExtension;
            var folderPath = Server.MapPath("/Uploads" + @"\CategoryImage\");
            var filePath = String.Concat(folderPath, fileName);
            var resized = Server.MapPath("/Uploads" + @"\CategoryImageResized\");
            var resizedfilePath = String.Concat(resized, "re_" + fileName);

            CategoryImage.SaveAs(filePath);

            ResizeSettings resizeSetting = new ResizeSettings
            {
                Width = 150,
                Height = 100,
                Format = fileExtension
            };
            ImageBuilder.Current.Build(filePath, resizedfilePath, resizeSetting);

            string addCategoryQuery = "Update Category SET CategoryName=@CategoryName, CategoryDesc=@CategoryDesc, CategoryImage=@CategoryImage, Updated = GETDATE() where CategoryId=@CategoryId";
            SqlCommand addCategory = new SqlCommand(addCategoryQuery, connection);

            addCategory.Parameters.AddWithValue("@CategoryName", CategoryName);
            addCategory.Parameters.AddWithValue("@CategoryDesc", CategoryDesc);
            addCategory.Parameters.AddWithValue("@CategoryImage", fileName);
            addCategory.Parameters.AddWithValue("@Updated", DateTime.Now.Date);
            addCategory.Parameters.AddWithValue("@CategoryId", CategoryId);


            addCategory.ExecuteNonQuery();
            connection.Close();

            return RedirectToAction("Categories");
        }



        public ActionResult LogCategory()
        {
            string conString = "Data Source=VKT-TAYLANA\\SQLEXPRESS;Initial Catalog=BlogWebSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection connection = new SqlConnection(conString);

            if (connection.State == ConnectionState.Closed)
                connection.Open();

            string listQuery = "SELECT * FROM LogCategory";
            SqlCommand list = new SqlCommand(listQuery, connection);
            SqlDataReader reader = list.ExecuteReader();

            List<Models.CategoryModel> Categorymodel = new List<Models.CategoryModel>();
            Models.CategoryModel item;
            while (reader.Read())
            {
                item = new Models.CategoryModel();

                item.CategoryId = Convert.ToInt32(reader["CategoryId"]);
                item.LogId = Convert.ToInt32(reader["LogId"]);
                item.CategoryName = reader["CategoryName"].ToString();
                item.CategoryDesc = reader["CategoryDesc"].ToString();
                item.Updated = reader["Updated"].ToString();

                Categorymodel.Add(item);
            }

            connection.Close();
            reader.Close();
            return View(Categorymodel);

        }


    }
}
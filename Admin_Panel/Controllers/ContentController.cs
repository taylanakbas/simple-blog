
using Admin_Panel.Login;
using ImageResizer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace Admin_Panel.Controllers
{
    [AdminLogin]
    public class ContentController : Controller
    {
        [HttpGet]
        //[OutputCache(Duration = 60)]
        public ActionResult Contents()
        {
            string conString = "Data Source=VKT-TAYLANA\\SQLEXPRESS;Initial Catalog=BlogWebSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection connection = new SqlConnection(conString);

            if (connection.State == ConnectionState.Closed)
                connection.Open();

            string listQuery = "SELECT PostId, PostTitle, PostText, AuthorName, Published, CategoryName FROM Post INNER JOIN Category ON Post.CategoryId = Category.CategoryId INNER JOIN Author ON Post.AuthorId = Author.AuthorId";
            SqlCommand list = new SqlCommand(listQuery, connection);
            SqlDataReader reader = list.ExecuteReader();

            List<Models.ContentModel> contentModel = new List<Models.ContentModel>();
            Models.ContentModel item;
            while (reader.Read())
            {
                item = new Models.ContentModel();

                item.PostId = Convert.ToInt32(reader["PostId"]);
                item.PostTitle = reader["PostTitle"].ToString();
                item.PostText = reader["PostText"].ToString();
                item.AuthorName = reader["AuthorName"].ToString();
                item.Published = reader["Published"].ToString();
                item.CategoryName = reader["CategoryName"].ToString();
                contentModel.Add(item);
            }

            connection.Close();
            reader.Close();
            return View(contentModel);


        }

        [HttpGet]
        //[OutputCache(Duration = 60)]
        public ActionResult AddContent()
        {
            string conString = "Data Source=VKT-TAYLANA\\SQLEXPRESS;Initial Catalog=BlogWebSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection connection = new SqlConnection(conString);
            SqlDataReader dataRead = null;

            if (connection.State == ConnectionState.Closed)
                connection.Open();
            string listQuery = "SELECT * FROM Category";

            SqlCommand list = new SqlCommand(listQuery, connection);
            dataRead = list.ExecuteReader();
            List<Models.ContentModel> contentModel = new List<Models.ContentModel>();
            Models.ContentModel item;
            while (dataRead.Read())
            {
                item = new Models.ContentModel();
                item.CategoryName = dataRead["CategoryName"].ToString();
                item.CategoryId = Convert.ToInt32(dataRead["CategoryId"]);

                contentModel.Add(item);
            }

            connection.Close();
            dataRead.Close();
            ViewBag.Categories = contentModel;
            return View();

        }

        [HttpGet]
        public ActionResult DeleteContent(int PostId)
        {
            string conString = "Data Source=VKT-TAYLANA\\SQLEXPRESS;Initial Catalog=BlogWebSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection connection = new SqlConnection(conString);

            if (connection.State == ConnectionState.Closed)
                connection.Open();
            string deleteContentQuery = "DELETE FROM Post WHERE PostId = @PostId";
            SqlCommand deleteContent = new SqlCommand(deleteContentQuery, connection);
            deleteContent.Parameters.AddWithValue("@PostId", PostId);
            deleteContent.ExecuteNonQuery();
            connection.Close();
            return RedirectToAction("Contents");
        }

        [HttpGet]
        public ActionResult UpdateContent(int PostId)
        {
            string conString = "Data Source=VKT-TAYLANA\\SQLEXPRESS;Initial Catalog=BlogWebSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection connection = new SqlConnection(conString);
            SqlDataReader dataRead = null;

            if (connection.State == ConnectionState.Closed)
                connection.Open();
            string listQuery = "SELECT * FROM Category";

            SqlCommand list = new SqlCommand(listQuery, connection);
            dataRead = list.ExecuteReader();
            List<Models.ContentModel> contentModel = new List<Models.ContentModel>();
            Models.ContentModel item;
            while (dataRead.Read())
            {
                item = new Models.ContentModel();
                item.CategoryName = dataRead["CategoryName"].ToString();
                item.CategoryId = Convert.ToInt32(dataRead["CategoryId"]);

                contentModel.Add(item);
            }

            connection.Close();
            dataRead.Close();
            ViewBag.Categories = contentModel;
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddContent(string PostTitle, string PostText, string AuthorId, int CategoryId, HttpPostedFileBase PostImage)
        {

            string conString = "Data Source=VKT-TAYLANA\\SQLEXPRESS;Initial Catalog=BlogWebSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection connection = new SqlConnection(conString);

            if (connection.State == ConnectionState.Closed)
                connection.Open();


            var fileExtension = Path.GetExtension(PostImage.FileName);
            var fileName = Guid.NewGuid() + fileExtension;
            var folderPath = Server.MapPath("/Uploads" + @"\ContentImage\");
            var filePath = String.Concat(folderPath, fileName);
            var resized = Server.MapPath("/Uploads" + @"\ContentImageResized\");
            var resizedfilePath = String.Concat(resized, "re_"+fileName);

            PostImage.SaveAs(filePath);

            ResizeSettings resizeSetting = new ResizeSettings
            {
                Width = 150,
                Height = 100,
                Format = fileExtension
            };
            ImageBuilder.Current.Build(filePath, resizedfilePath, resizeSetting);


            string addContentQuery = "INSERT INTO Post(CategoryId,PostTitle,PostText,AuthorId,PostImage,Published) VALUES(@CategoryId,@PostTitle,@PostText,@AuthorId,@PostImage,GETDATE())";
            SqlCommand addContent = new SqlCommand(addContentQuery, connection);

            addContent.Parameters.AddWithValue("@CategoryId", CategoryId);
            addContent.Parameters.AddWithValue("@PostTitle", PostTitle);
            addContent.Parameters.AddWithValue("@PostText", PostText);
            addContent.Parameters.AddWithValue("@PostImage", fileName);
            addContent.Parameters.AddWithValue("@AuthorId", AuthorId);
            addContent.Parameters.AddWithValue("@Published", DateTime.Now.Date);
            addContent.ExecuteNonQuery();

            connection.Close(); 
            return RedirectToAction("Contents", "Content");

        }

        [HttpPost]
        public ActionResult UpdateContent(int AuthorId, int PostId, string PostTitle, string PostText, string AuthorName, int CategoryId, HttpPostedFileBase PostImage)
        {

            string conString = "Data Source=VKT-TAYLANA\\SQLEXPRESS;Initial Catalog=BlogWebSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection connection = new SqlConnection(conString);

            if (connection.State == ConnectionState.Closed)
                connection.Open();


            var fileExtension = Path.GetExtension(PostImage.FileName);
            var fileName = Guid.NewGuid() + fileExtension;
            var folderPath = Server.MapPath("/Uploads" + @"\ContentImage\");
            var filePath = String.Concat(folderPath, fileName);
            PostImage.SaveAs(filePath);


            string updateContentQuery = "UPDATE Post SET CategoryId = @CategoryId, PostTitle = @PostTitle, PostText = @PostText, PostImage = @PostImage, AuthorId = @AuthorId, Published = GETDATE() WHERE PostId = @PostId ";

            SqlCommand updateContent = new SqlCommand(updateContentQuery, connection);
            
            updateContent.Parameters.AddWithValue("@CategoryId", CategoryId);
            updateContent.Parameters.AddWithValue("@PostTitle", PostTitle);
            updateContent.Parameters.AddWithValue("@PostText", PostText);
            updateContent.Parameters.AddWithValue("@PostImage", fileName);
            updateContent.Parameters.AddWithValue("@AuthorId", AuthorId);
            updateContent.Parameters.AddWithValue("@Published", DateTime.Now.Date);
            updateContent.Parameters.AddWithValue("@PostId", PostId);

            updateContent.ExecuteNonQuery();

            connection.Close();
            return RedirectToAction("Contents","Content");

        }


        [HttpGet]
        public ActionResult LogContent()
        {
            string conString = "Data Source=VKT-TAYLANA\\SQLEXPRESS;Initial Catalog=BlogWebSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection connection = new SqlConnection(conString);

            if (connection.State == ConnectionState.Closed)
                connection.Open();

            string listQuery = "SELECT PostId, PostTitle, PostText, AuthorName, AuthorLname, Published, Deleted, CategoryName FROM LogPost INNER JOIN Category ON LogPost.CategoryId = Category.CategoryId INNER JOIN Author ON LogPost.AuthorId = Author.AuthorId";
            SqlCommand list = new SqlCommand(listQuery, connection);
            SqlDataReader reader = list.ExecuteReader();

            List<Models.LogModel> LogModel = new List<Models.LogModel>();
            Models.LogModel item;
            while (reader.Read())
            {
                item = new Models.LogModel();

                item.PostId = Convert.ToInt32(reader["PostId"]);
                item.PostTitle = reader["PostTitle"].ToString();
                item.AuthorName = reader["AuthorName"].ToString();
                item.AuthorLname = reader["AuthorLname"].ToString();
                item.Published = reader["Published"].ToString();
                item.Deleted = reader["Deleted"].ToString();
                item.CategoryName = reader["CategoryName"].ToString();
                LogModel.Add(item);
            }

            connection.Close();
            reader.Close();
            return View(LogModel);
        }


    }
}
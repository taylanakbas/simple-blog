using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using System.Linq;

namespace Blog.Controllers
{
    public class ContentController : Controller
    {
        [OutputCache(Duration = 60)]
        public ActionResult Content(int PostId)
        {
            string conString = "Data Source=VKT-TAYLANA\\SQLEXPRESS;Initial Catalog=BlogWebSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection connection = new SqlConnection(conString);

            if (connection.State == ConnectionState.Closed)
                connection.Open();

            string contentQuery = "SELECT  PostTitle, PostText, PostImage, Published, AuthorName, AuthorLname, AudienceName, AudienceMsg, CommentPublished FROM Post INNER JOIN Author ON Post.AuthorId = Author.AuthorId INNER JOIN Comment ON  Comment.PostId = @PostId  WHERE Post.PostId = @PostId";
            SqlCommand contentCommand = new SqlCommand(contentQuery, connection);
            contentCommand.Parameters.AddWithValue("@PostId", PostId);

            SqlDataReader reader = contentCommand.ExecuteReader();

            List<Models.ContentModel> contentModel = new List<Models.ContentModel>();
            Models.ContentModel item;

            

            while (reader.Read())
            {
                item = new Models.ContentModel();
                item.PostTitle = reader["PostTitle"].ToString();
                item.PostText = reader["PostText"].ToString();
                item.PostImage = reader["PostImage"].ToString();
                item.AuthorName = reader["AuthorName"].ToString();
                item.AuthorLname = reader["AuthorLname"].ToString();
                item.Published = reader["Published"].ToString();
                item.AudienceName = reader["AudienceName"].ToString();
                item.AudienceMessage = reader["AudienceMsg"].ToString();
                item.CommentPublished = reader["CommentPublished"].ToString();
                contentModel.Add(item);
            }

            connection.Close();
            reader.Close();

            return View(contentModel);
        }

        [HttpPost]
        public JsonResult Content(int postId,string name, string email, string message)
        {
            int rowCount;
            string resultComment;
            string conString = "Data Source=VKT-TAYLANA\\SQLEXPRESS;Initial Catalog=BlogWebSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection connection = new SqlConnection(conString);

            if (connection.State == ConnectionState.Closed)
                connection.Open();

            string contactQuery = "INSERT INTO Comment(PostId,AudienceName,AudienceMail,AudienceMsg,CommentPublished) VALUES(@postId, @AudienceName, @AudienceMail, @AudienceMsg, GETDATE())";
            SqlCommand contactCommand = new SqlCommand(contactQuery, connection);

            contactCommand.Parameters.AddWithValue("@PostId", postId);
            contactCommand.Parameters.AddWithValue("@AudienceName", name);
            contactCommand.Parameters.AddWithValue("@AudienceMail", email);
            contactCommand.Parameters.AddWithValue("@AudienceMsg", message);
            contactCommand.Parameters.AddWithValue("@CommentPublished", DateTime.Now.Date);

            rowCount = contactCommand.ExecuteNonQuery();

            resultComment = (rowCount > 0) ? "Message is succesfully sended" : "Message is not sended!";
            connection.Close();

            return Json(resultComment, JsonRequestBehavior.AllowGet);
        }

        [OutputCache(Duration = 60)]
        public ActionResult ContentList(int CategoryId, int pageNo = 1)
        {


            string conString = "Data Source=VKT-TAYLANA\\SQLEXPRESS;Initial Catalog=BlogWebSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection connection = new SqlConnection(conString);

            if (connection.State == ConnectionState.Closed)
                connection.Open();



            string contentListQuery = "SELECT PostId, PostTitle, PostText, PostImage, Published, AuthorName, AuthorLname,CategoryName FROM Post INNER JOIN Category ON Post.CategoryId = Category.CategoryId INNER JOIN Author ON Post.AuthorId = Author.AuthorId WHERE Post.CategoryId = @CategoryId order by Post.Published DESC";



            SqlCommand contentCommand = new SqlCommand(contentListQuery, connection);
            contentCommand.Parameters.AddWithValue("@CategoryId", CategoryId);

            SqlDataReader reader = contentCommand.ExecuteReader();


            List<Models.ContentModel> contentModel = new List<Models.ContentModel>();

            Models.ContentModel item;

            while (reader.Read())
            {
                item = new Models.ContentModel();
                item.PostId = Convert.ToInt32(reader["PostId"]);
                item.CategoryName = reader["CategoryName"].ToString();
                item.PostTitle = reader["PostTitle"].ToString();
                item.PostText = reader["PostText"].ToString();
                item.PostImage = reader["PostImage"].ToString();
                item.AuthorName = reader["AuthorName"].ToString();
                item.AuthorLname = reader["AuthorLname"].ToString();
                item.Published = reader["Published"].ToString();
                item.CategoryId = CategoryId;
                contentModel.Add(item);
            }

            connection.Close();
            reader.Close();



            ViewBag.PagedLists = contentModel.ToPagedList(pageNo, 5);
            contentModel = contentModel.Skip((pageNo - 1) * 5).Take(5).ToList();

            return View(contentModel);
        }
    }
}
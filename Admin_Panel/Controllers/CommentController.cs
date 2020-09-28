using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Admin_Panel.Controllers
{
    public class CommentController : Controller
    {
        [HttpGet]
        public ActionResult Comment()
        {
            string conString = "Data Source=VKT-TAYLANA\\SQLEXPRESS;Initial Catalog=BlogWebSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection connection = new SqlConnection(conString);

            if (connection.State == ConnectionState.Closed)
                connection.Open();

            string commentQuery = "Select CommentId,Comment.PostId,PostTitle,AudienceName,AudienceMail,AudienceMsg,CommentPublished From Comment INNER JOIN Post ON Comment.PostId = Post.PostId ORDER BY CommentPublished DESC";
            SqlCommand list = new SqlCommand(commentQuery, connection);
            SqlDataReader reader = list.ExecuteReader();

                List<Models.CommentModel> commentModel = new List<Models.CommentModel>();
            Models.CommentModel item;
            while (reader.Read())
            {
                item = new Models.CommentModel();

                item.CommentId = Convert.ToInt32(reader["CommentId"]);
                item.PostId = Convert.ToInt32(reader["PostId"]);
                item.PostTitle = reader["PostTitle"].ToString();
                item.AudienceName = reader["AudienceName"].ToString();
                item.AudienceMail = reader["AudienceMail"].ToString();
                item.AudienceMsg = reader["AudienceMsg"].ToString();
                item.CommentPublished = reader["CommentPublished"].ToString();
                commentModel.Add(item);
            }

            connection.Close();
            reader.Close();
            return View(commentModel);

        }

    }
}
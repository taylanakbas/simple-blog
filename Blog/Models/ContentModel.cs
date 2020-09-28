
using PagedList;

namespace Blog.Models
{
    public class ContentModel
    {
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }

        public int PostId { get; set; }
        public string PostTitle { get; set; }
        public string PostText { get; set; }
        public string PostImage { get; set; }
        public string Published { get; set; }

        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string AuthorLname { get; set; }

        public string AudienceName { get; set; }
        public string AudienceMessage { get; set; }
        public string CommentPublished { get; set; }


    }  
}
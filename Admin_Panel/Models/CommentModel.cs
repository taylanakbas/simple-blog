using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin_Panel.Models
{
    public class CommentModel
    {
        public int CommentId { get; set; }
        public int PostId { get; set; }
        public string PostTitle { get; set; }
        public string AudienceName { get; set; }
        public string AudienceMail { get; set; }
        public string  AudienceMsg { get; set; }
        public string CommentPublished { get; set; }
    }
}
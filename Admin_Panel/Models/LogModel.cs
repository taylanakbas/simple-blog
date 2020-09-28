using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin_Panel.Models
{
    public class LogModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public int PostId { get; set; }
        public string PostTitle { get; set; }
        public string Published { get; set; }
        public string Deleted { get; set; }

        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string AuthorLname { get; set; }

    }
}
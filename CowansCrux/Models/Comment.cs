using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CowansCrux.Models
{
    public class Comment
    {
        public int id { get; set; }
        public string comment { get; set; }

        public int commentUserId { get; set; }
        public int webcomicId { get; set; }

        public ApplicationUser commentUser { get; set; }
        public Webcomic webcomic { get; set; }
    }
}
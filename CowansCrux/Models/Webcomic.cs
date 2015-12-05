using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CowansCrux.Models
{
    public class Webcomic
    {
        public int id { get; set; }
        public string title { get; set; }
        public string url { get; set; }
        public string background { get; set; }
        public DateTimeOffset dateUploaded { get; set; }
        public DateTimeOffset? dateToDisplay { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
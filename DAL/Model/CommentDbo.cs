using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Model
{
    public class CommentDbo
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public DateTime PubDate { get; set; }

        public int? UserId { get; set; }
        public UserDbo User { get; set; }

        public int? ItemId { get; set; }
        public ItemDbo Item { get; set; }
    }
}

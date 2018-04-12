using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RssCrawleraApi.Models
{
    public class Comment
    {
        public string Text { get; set; }

        public int ItemId { get; set; }

        public int UserId { get; set; }
    }
}

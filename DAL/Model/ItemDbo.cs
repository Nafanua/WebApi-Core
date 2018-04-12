using System;
using System.Collections.Generic;

namespace DAL.Model
{
    public partial class ItemDbo
    {
        public int Id { get; set; }

        public byte[] ItemExternalId { get; set; }

        public string Title { get; set; }

        public string Link { get; set; }

        public string Description { get; set; }

        public string Fulltext { get; set; }

        public string Author { get; set; }

        public string Image { get; set; }

        public DateTime PubDate { get; set; }

        public string Guid { get; set; }

        public string CategoryUrl { get; set; }

        public string Comments { get; set; }

        public string SourceUrl { get; set; }

        public string Tags { get; set; }

        public string EnclosureUrl { get; set; }

        public virtual DatasourceDbo Datasource { get; set; }

        public virtual ICollection<CommentDbo> ItemComments { get; set; }

    }
}

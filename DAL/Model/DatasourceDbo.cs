using System.Collections.Generic;

namespace DAL.Model
{
    public class DatasourceDbo
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public int TypeOfData { get; set; }

        public virtual ICollection<ItemDbo> Items { get; set; }

        public DatasourceDbo()
        {
            Items = new List<ItemDbo>();
        }
    }
}

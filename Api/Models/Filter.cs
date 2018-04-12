namespace RssCrawleraApi.Models
{
    public class Filter
    {
        public int CurrentPage { get; set; }

        public int PageSize { get; set; }

        public string Tag { get; set; }

        public string Author { get; set; }

        public string ArticleName { get; set; }

        public string DateStart { get; set; }

        public string DateEnd { get; set; }
    }
}

using DAL.Model;
using System.Collections.Generic;

namespace RssCrawleraApi.ViewModels
{
    public class ViewModel
    {
        public List<ItemDbo> News { get; set; }

        public int TotalPages { get; set; }

        public int PageSize { get; set; }
    }
}

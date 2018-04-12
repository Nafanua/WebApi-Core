using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Model;
using DAL.Service;
using DAL.Services.ComentsService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RssCrawleraApi.Models;
using RssCrawleraApi.SignalR;
using RssCrawleraApi.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace RssCrawleraApi.Controllers
{
	[Route("api/[controller]")]
    //[Authorize]
	public class NewsController : Controller
	{
		private List<ItemDbo> _data;
        private readonly IComentsService _comentsService;
        private readonly IUserService _userService;
        private readonly IItemService _itemService;

        public NewsController(IComentsService commentService, IUserService userService, IItemService itemService)
		{
            _comentsService = commentService;
            _itemService = itemService;
            _userService = userService;
		}

		// GET api/values/5
		[HttpGet("Get/{id}")]
		public ItemDbo Get(int id)
		{
			var art = _itemService.GetAll()/*.Include(i => i.ItemComments)*/.FirstOrDefault(x => x.Id == id);

			return art;
		}

        [HttpPost("addComment")]
        public void AddComment([FromBody] Comment comment)
        {
            var commentDb = new CommentDbo();

            commentDb.Text = comment.Text;
            commentDb.PubDate = DateTime.UtcNow;
            commentDb.ItemId = comment.ItemId;
            commentDb.UserId = comment.UserId;

            _comentsService.AddComment(commentDb);
        }


		[HttpPost("SearchArt")]
		public ViewModel SearchArt([FromBody]Filter filter)
		{
			var news = _data.AsQueryable();

			if(filter.Author != null)
			{
				news = news.Where(x => x.Author == filter.Author);
			}
			else if (filter.ArticleName != null)
			{
				news = news.Where(x => x.Title == filter.ArticleName);
			}
			else if (filter.Tag != null)
			{
				news = news.Where(x => x.Tags == filter.Tag);
			}

			int pageSize = filter.PageSize;

			int totalPages = (int)Math.Ceiling((double)news.Count() / pageSize);


			totalPages = (news.Count() / pageSize) + (news.Count() % pageSize > 0 ? 1 : 0);

			var res = news
				.SkipLast((filter.CurrentPage - 1) * pageSize)
				.TakeLast(pageSize)
				.ToList();

			ViewModel viewModel = new ViewModel()
			{
				News = res,
				TotalPages = totalPages,
				PageSize = pageSize
			};

			return viewModel;
		}
	}
}

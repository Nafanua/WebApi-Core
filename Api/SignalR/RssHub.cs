using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reactive.Linq;
using Microsoft.AspNetCore.SignalR;
using DAL.Service;
using DAL.Model;
using RssCrawleraApi.ViewModels;

namespace RssCrawleraApi.SignalR
{
	public class RssHub : Hub
	{
		public const int PageSize = 4;
        public List<ItemDbo> listOfNews;

        public RssHub(IItemService itemService)
		{
            listOfNews = itemService.GetAll().ToList();
        }		
		
		public Task SendStreamInit()
		{
			return Clients.Client(Context.ConnectionId).InvokeAsync("streamStarted");
		}

		public IObservable<ViewModel> StartStreaming()
		{
            return Observable.Create(
				async (IObserver<ViewModel> observer) =>
				{
					while (Context.ConnectionId != null)
                    {                        
                        List<ItemDbo> news = listOfNews.Skip(Math.Max(0, listOfNews.Count() - PageSize)).ToList();

						int totalPages = (int)Math.Ceiling((double)listOfNews.Count / PageSize);
						ViewModel viewModel = new ViewModel() { News = news, TotalPages = totalPages, PageSize = PageSize };

						observer.OnNext(viewModel);
						await Task.Delay(30000);						
					}
				});
		}
	}
}

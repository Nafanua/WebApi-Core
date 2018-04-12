using DAL.Model;
using System;
using System.Linq;

namespace DAL.Service
{
    public interface IItemService : IDisposable
    {
        IQueryable<ItemDbo> GetAll();
        ItemDbo GetById(int id);
        void AddItem(ItemDbo item);
    }
}

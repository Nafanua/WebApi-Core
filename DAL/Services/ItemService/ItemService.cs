using DAL.Model;
using DAL.UnitWork;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Service
{
    public class ItemService : IItemService
    {
        private readonly IUnitOfWork unitOfWork;

        public ItemService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void AddItem(ItemDbo item)
        {
            unitOfWork.ItemRepository.Add(item);
            unitOfWork.Save();
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }

        public IQueryable<ItemDbo> GetAll()
        {
            return unitOfWork.ItemRepository.GetAll();
        }

        public ItemDbo GetById(int id)
        {
            return unitOfWork.ItemRepository.GetAll().FirstOrDefault(i => i.Id == id);
        }
    }
}
